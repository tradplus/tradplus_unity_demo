//
//  TPUNativeBanner.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/7.
//

#import "TPUNativeBanner.h"
#import "TPUPluginUtil.h"
#import "TPUNativeBannerManager.h"

@interface TPUNativeBanner()<TradPlusADNativeBannerDelegate>

@property (nonatomic,strong)Class renderClass;
@property (nonatomic,copy)NSString *sceneId;
@property (nonatomic,assign)BOOL didShow;
@end

@implementation TPUNativeBanner

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.nativeBanner = [[TradPlusNativeBanner alloc] init];
        self.nativeBanner.delegate = self;
    }
    return self;
}

- (void)setClassName:(NSString *)className
{
    _className = className;
    if(className != nil)
    {
        self.renderClass = NSClassFromString(className);
        if(self.renderClass == nil)
        {
            MSLogInfo(@"NativeBanner renderClass is nil className:%@",className);
        }
        else
        {
            //传入自定义模版
            self.nativeBanner.autoShow = NO;
        }
    }
}

- (void)setCloseAutoShow:(BOOL)closeAutoShow
{
    _closeAutoShow = closeAutoShow;
    if(self.closeAutoShow && !self.didShow)
    {
        self.nativeBanner.hidden = YES;
    }
    self.nativeBanner.autoShow = !closeAutoShow;
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID
{
    MSLogTrace(@"%s adUnitID:%@", __PRETTY_FUNCTION__,adUnitID);
    [self.nativeBanner setAdUnitID:adUnitID];
}

- (void)loadAdWithSceneId:(nullable NSString *)sceneId
{
    self.sceneId = sceneId;
    [self.nativeBanner loadAdWithSceneId:sceneId];
}

- (void)showAdWithSceneId:(nullable NSString *)sceneId
{
    //关闭自动展示的并且未展示过的，在首次展示取消默认的隐藏状态
    if(self.closeAutoShow && !self.didShow)
    {
        self.didShow = YES;
        self.nativeBanner.hidden = NO;
    }
    if(self.renderClass == nil)
    {
        [self.nativeBanner showWithSceneId:sceneId];
    }
    else
    {
        [self.nativeBanner showWithRenderingViewClass:self.renderClass sceneId:sceneId];
    }
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.nativeBanner.segmentTag = segmentTag;
    }
    self.nativeBanner.dicCustomValue = dic;
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.nativeBanner entryAdScenario:sceneId];
}

- (void)setNativeBannerSize:(CGSize)size
{
    CGRect rect = CGRectZero;
    rect.size = size;
    self.nativeBanner.frame = rect;
}

- (void)setX:(float)x y:(float)y adPosition:(int)adPosition
{
    CGRect rect = self.nativeBanner.bounds;
    UIViewController *rootController = [TPUPluginUtil unityViewController];
    if(x > 0 || y > 0)
    {
        rect.origin.x = x;
        rect.origin.y = y;
    }
    else
    {
        rect = [TPUPluginUtil getRectWithSize:rect.size adPosition:adPosition];
    }
    self.nativeBanner.frame = rect;
    [rootController.view addSubview:self.nativeBanner];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.nativeBanner.isAdReady;
}

- (void)hide
{
    self.nativeBanner.hidden = YES;
}

- (void)display
{
    self.nativeBanner.hidden = NO;
}

- (void)destroy
{
    [self.nativeBanner removeFromSuperview];
}

#pragma mark - TradPlusADNativeBannerDelegate

- (nullable UIViewController *)viewControllerForPresentingModalView
{
    return [TPUPluginUtil unityViewController];
}

///AD加载完成 首个广告源加载成功时回调 一次加载流程只会回调一次
- (void)tpNativeBannerAdDidLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    //传入自定义模版 并且 使用自动展示
    if(self.renderClass != nil && !self.closeAutoShow)
    {
        [self showAdWithSceneId:self.sceneId];
    }
    
    if([TPUNativeBannerManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeBannerManager sharedInstance].loadedCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD加载失败
///tpNativeBannerAdOneLayerLoaded:didFailWithError：返回三方源的错误信息
- (void)tpNativeBannerAdLoadFailWithError:(NSError *)error
{
    MSLogInfo(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPUNativeBannerManager sharedInstance].loadFailedCallback)
    {
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeBannerManager sharedInstance].loadFailedCallback(self.nativeBanner.unitID.UTF8String,errorString.UTF8String);
    }
}

///AD展现
- (void)tpNativeBannerAdImpression:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeBannerManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeBannerManager sharedInstance].impressionCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpNativeBannerAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUNativeBannerManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeBannerManager sharedInstance].showFailedCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD被点击
- (void)tpNativeBannerAdClicked:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeBannerManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeBannerManager sharedInstance].clickedCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+新增 开始加载流程
- (void)tpNativeBannerAdStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeBannerManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeBannerManager sharedInstance].startLoadCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源开始加载时会都会回调一次。
///v7.6.0+新增。替代原回调接口：tpNativeBannerAdLoadStart:(NSDictionary *)adInfo;
- (void)tpNativeBannerAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeBannerManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeBannerManager sharedInstance].oneLayerStartLoadCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding开始
- (void)tpNativeBannerAdBidStart:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeBannerManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeBannerManager sharedInstance].biddingStartCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding结束 error = nil 表示成功
- (void)tpNativeBannerAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUNativeBannerManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeBannerManager sharedInstance].biddingEndCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///当每个广告源加载成功后会都会回调一次。
- (void)tpNativeBannerAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeBannerManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeBannerManager sharedInstance].oneLayerLoadedCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载失败后会都会回调一次，返回三方源的错误信息
- (void)tpNativeBannerAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUNativeBannerManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeBannerManager sharedInstance].oneLayerLoadFailedCallback(self.nativeBanner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///加载流程全部结束
- (void)tpNativeBannerAdAllLoaded:(BOOL)success
{
    MSLogInfo(@"%s success:%@", __PRETTY_FUNCTION__, @(success));
    if([TPUNativeBannerManager sharedInstance].allLoadedCallback)
    {
        [TPUNativeBannerManager sharedInstance].allLoadedCallback(self.nativeBanner.unitID.UTF8String,success);
    }
}
@end
