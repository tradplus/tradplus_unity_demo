//
//  TPFBanner.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import "TPUBanner.h"
#import "TPUBannerManager.h"
#import "TPUPluginUtil.h"

@interface TPUBanner()<TradPlusADBannerDelegate>

@property (nonatomic,assign)BOOL didShow;
@end

@implementation TPUBanner

- (instancetype)init
{
    self = [super init];
    if (self)
    {
        self.banner = [[TradPlusAdBanner alloc] init];
        self.banner.delegate = self;
    }
    return self;
}

- (void)setX:(float)x y:(float)y adPosition:(int)adPosition
{
    CGRect rect = self.banner.bounds;
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
    self.banner.frame = rect;
    [rootController.view addSubview:self.banner];
}

- (void)setCloseAutoShow:(BOOL)closeAutoShow
{
    _closeAutoShow = closeAutoShow;
    if(self.closeAutoShow && !self.didShow)
    {
        self.banner.hidden = YES;
    }
    self.banner.autoShow = !closeAutoShow;
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID
{
    MSLogTrace(@"%s adUnitID:%@", __PRETTY_FUNCTION__,adUnitID);
    [self.banner setAdUnitID:adUnitID];
}

- (void)setBannerSize:(CGSize)size
{
    CGRect rect = CGRectZero;
    rect.size = size;
    self.banner.frame = rect;
    [self.banner setBannerSize:size];
}

- (void)setBannerContentMode:(NSInteger)mode
{
    self.banner.bannerContentMode = mode;
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.banner.segmentTag = segmentTag;
    }
    self.banner.dicCustomValue = dic;
}

- (void)loadAdWithSceneId:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.banner loadAdWithSceneId:sceneId];
}

- (void)showAdWithSceneId:(nullable NSString *)sceneId
{
    //关闭自动展示的并且未展示过的，在首次展示取消默认的隐藏状态
    if(self.closeAutoShow && !self.didShow)
    {
        self.didShow = YES;
        self.banner.hidden = NO;
    }
    [self.banner showWithSceneId:sceneId];
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s entryAdScenario:%@", __PRETTY_FUNCTION__,sceneId);
    [self.banner entryAdScenario:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.banner.isAdReady;
}

- (void)hide
{
    self.banner.hidden = YES;
}

- (void)display
{
    self.banner.hidden = NO;
}

- (void)destroy
{
    [self.banner removeFromSuperview];
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.banner.customAdInfo = customAdInfo;
}

#pragma mark - TradPlusADBannerDelegate

- (nullable UIViewController *)viewControllerForPresentingModalView
{
    return [TPUPluginUtil unityViewController];
}

///AD加载完成 首个广告源加载成功时回调 一次加载流程只会回调一次
- (void)tpBannerAdLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].loadedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD加载失败
///tpBannerAdOneLayerLoad:didFailWithError：返回三方源的错误信息
- (void)tpBannerAdLoadFailWithError:(NSError *)error
{
    MSLogInfo(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPUBannerManager sharedInstance].loadFailedCallback)
    {
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].loadFailedCallback(self.banner.unitID.UTF8String,errorString.UTF8String);
    }
}

///AD展现
- (void)tpBannerAdImpression:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].impressionCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpBannerAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].showFailedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD被点击
- (void)tpBannerAdClicked:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].clickedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+新增 开始加载流程
- (void)tpBannerAdStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].startLoadCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源开始加载时会都会回调一次。
///v7.6.0+新增。替代原回调接口：tpBannerAdLoadStart:(NSDictionary *)adInfo;
- (void)tpBannerAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].oneLayerStartLoadCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding开始
- (void)tpBannerAdBidStart:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].biddingStartCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding结束 error = nil 表示成功
- (void)tpBannerAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUBannerManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].biddingEndCallback(self.banner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///当每个广告源加载成功后会都会回调一次。
- (void)tpBannerAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].oneLayerLoadedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载失败后会都会回调一次，返回三方源的错误信息
- (void)tpBannerAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].oneLayerLoadFailedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///加载流程全部结束
- (void)tpBannerAdAllLoaded:(BOOL)success
{
    MSLogInfo(@"%s success:%@", __PRETTY_FUNCTION__, @(success));
    if([TPUBannerManager sharedInstance].allLoadedCallback)
    {
        [TPUBannerManager sharedInstance].allLoadedCallback(self.banner.unitID.UTF8String,success);
    }
}

///三方关闭按钮触发时的回调
- (void)tpBannerAdClose:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].closedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

@end
