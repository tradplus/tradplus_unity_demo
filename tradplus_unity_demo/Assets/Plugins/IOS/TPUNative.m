//
//  TPUNative.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/13.
//

#import "TPUNative.h"
#import <TradPlusAds/TradPlusAds.h>
#import "TPUPluginUtil.h"
#import "TPUNativeManager.h"

@interface TPUNative()<TradPlusADNativeDelegate>

@property (nonatomic,strong)TradPlusAdNative *native;
@end

@implementation TPUNative

- (instancetype)init
{
    self = [super init];
    if (self) {
        [TradPlus setLogLevel:MSLogLevelOff];
        self.native = [[TradPlusAdNative alloc] init];
        self.native.delegate = self;
        self.adView = [[UIView alloc] init];
    }
    return self;
}

- (void)setTemplateRenderSize:(CGSize)size
{
    MSLogTrace(@"%s size:%@", __PRETTY_FUNCTION__,@(size));
    CGRect rect = CGRectZero;
    rect.size = size;
    self.adView.frame = rect;
    self.adView.backgroundColor = [UIColor clearColor];
    [self.native setTemplateRenderSize:size];
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID isAutoLoad:(BOOL)isAutoLoad;
{
    MSLogTrace(@"%s adUnitID:%@ isAutoLoad:%@", __PRETTY_FUNCTION__,adUnitID,@(isAutoLoad));
    [self.native setAdUnitID:adUnitID isAutoLoad:isAutoLoad];
}

- (void)loadAd
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    [self.native loadAd];
}

- (void)setX:(float)x y:(float)y adPosition:(int)adPosition
{
    CGRect rect = self.adView.bounds;
    if(x > 0 || y > 0)
    {
        rect.origin.x = x;
        rect.origin.y = y;
    }
    else
    {
        rect = [TPUPluginUtil getRectWithSize:rect.size adPosition:adPosition];
    }
    self.adView.frame = rect;
}

- (TradPlusAdNativeObject *)getReadyNativeObject
{
    return self.native.getReadyNativeObject;
}

- (void)showWithClassName:(Class)viewClass sceneId:(NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    UIViewController *rootController = [TPUPluginUtil unityViewController];
    [rootController.view addSubview:self.adView];
    [self.native showADWithRenderingViewClass:viewClass subview:self.adView sceneId:sceneId];
}

- (void)showWithRenderer:(TradPlusNativeRenderer *)renderer sceneId:(NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.native showADWithNativeRenderer:renderer subview:self.adView sceneId:sceneId];
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.native.segmentTag = segmentTag;
    }
    self.native.dicCustomValue = dic;
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.native entryAdScenario:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.native.isAdReady;
}

- (NSInteger)getLoadedCount
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.native.readyAdCount;
}

- (void)hide
{
    self.adView.hidden = YES;
}

- (void)display
{
    self.adView.hidden = NO;
}

- (void)destroy
{
    [self.adView removeFromSuperview];
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.native.customAdInfo = customAdInfo;
}

#pragma mark - TradPlusADNativeDelegate

- (nullable UIViewController *)viewControllerForPresentingModalView
{
    return [TPUPluginUtil unityViewController];
}

///AD加载完成 首个广告源加载成功时回调 一次加载流程只会回调一次
- (void)tpNativeAdLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].loadedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD加载失败
///tpNativeAdOneLayerLoad:didFailWithError：返回三方源的错误信息
- (void)tpNativeAdLoadFailWithError:(NSError *)error
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, error);
    if([TPUNativeManager sharedInstance].loadFailedCallback)
    {
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].loadFailedCallback(self.native.unitID.UTF8String,errorString.UTF8String);
    }
}

///AD展现
- (void)tpNativeAdImpression:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].impressionCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpNativeAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s %@ %@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUNativeManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].showFailedCallback(self.native.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD被点击
- (void)tpNativeAdClicked:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].clickedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+新增 开始加载流程
- (void)tpNativeAdStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].startLoadCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源开始加载时会都会回调一次。
///v7.6.0+新增。替代原回调接口：tpNativeAdLoadStart:(NSDictionary *)adInfo;
- (void)tpNativeAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].oneLayerStartLoadCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD被关闭
- (void)tpNativeAdClose:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].closedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding开始
- (void)tpNativeAdBidStart:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].biddingStartCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding结束 error = nil 表示成功
- (void)tpNativeAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogInfo(@"%s %@ %@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUNativeManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].biddingEndCallback(self.native.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///当每个广告源加载成功后会都会回调一次。
- (void)tpNativeAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].oneLayerLoadedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载失败后会都会回调一次，返回三方源的错误信息
- (void)tpNativeAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].oneLayerLoadFailedCallback(self.native.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///加载流程全部结束
- (void)tpNativeAdAllLoaded:(BOOL)success
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, @(success));
    if([TPUNativeManager sharedInstance].allLoadedCallback)
    {
        [TPUNativeManager sharedInstance].allLoadedCallback(self.native.unitID.UTF8String,success);
    }
}

///开始播放 v7.8.0+
- (void)tpNativeAdVideoPlayStart:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].videoPlayStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].videoPlayStartCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///播放结束 v7.8.0+
- (void)tpNativeAdVideoPlayEnd:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].videoPlayEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].videoPlayEndCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///视频贴片类型播放完成回调 v6.8.0+
- (void)tpNativePasterDidPlayFinished:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s %@", __PRETTY_FUNCTION__, adInfo);
}
@end
