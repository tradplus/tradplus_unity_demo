//
//  TPUSplash.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import "TPUSplash.h"
#import "TPUSplashManager.h"
#import "TPUPluginUtil.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUSplash()<TradPlusADSplashDelegate>

@property (nonatomic,strong)TradPlusAdSplash *splash;
@end

@implementation TPUSplash

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.splash = [[TradPlusAdSplash alloc] init];
        self.splash.delegate = self;
    }
    return self;
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID
{
    MSLogTrace(@"%s adUnitID:%@", __PRETTY_FUNCTION__,adUnitID);
    [self.splash setAdUnitID:adUnitID];
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.splash.segmentTag = segmentTag;
    }
    self.splash.dicCustomValue = dic;
}

- (void)setLocalParams:(NSDictionary *)dic
{
    self.splash.localParams = dic;
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__,dic);
}

- (void)loadAdWithMaxWaitTime:(float)maxWaitTime
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    UIWindow *window = UnityGetMainWindow();
    [self.splash loadAdWithWindow:window bottomView:nil maxWaitTime:maxWaitTime];
}

- (void)openAutoLoadCallback
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    [self.splash openAutoLoadCallback];
}

- (void)showAdWithSceneId:(NSString *)sceneId
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    [self.splash showWithSceneId:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.splash.isAdReady;
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.splash.customAdInfo = customAdInfo;
}

- (void)entryAdScenario:(NSString *)sceneId
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    [self.splash entryAdScenario:sceneId];
}

#pragma mark - TradPlusADSplashDelegate

- (void)tpSplashAdIsLoading:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].adIsLoadingCallback)
    {
        [TPUSplashManager sharedInstance].adIsLoadingCallback(self.splash.unitID.UTF8String);
    }
}

///AD加载完成 首个广告源加载成功时回调 一次加载流程只会回调一次
- (void)tpSplashAdLoaded:(NSDictionary *)adInfo
{
    
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].loadedCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD加载失败
///tpsplashAdOneLayerLoaded:didFailWithError：返回三方源的错误信息
- (void)tpSplashAdLoadFailWithError:(NSError *)error
{
    MSLogTrace(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPUSplashManager sharedInstance].loadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUSplashManager sharedInstance].loadFailedCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现
- (void)tpSplashAdImpression:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].impressionCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpSplashAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUSplashManager sharedInstance].showFailedCallback(self.splash.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD被点击
- (void)tpSplashAdClicked:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].clickedCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD关闭
- (void)tpSplashAdDismissed:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].closedCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+ 开始加载流程
- (void)tpSplashAdStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].startLoadCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源开始加载时会都会回调一次。
///v7.6.0+新增。替代原回调接口：tpsplashAdLoadStart:(NSDictionary *)adInfo;
- (void)tpSplashAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].oneLayerStartLoadCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding开始
- (void)tpSplashAdBidStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].biddingStartCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding结束 error = nil 表示成功
- (void)tpSplashAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUSplashManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUSplashManager sharedInstance].biddingEndCallback(self.splash.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///当每个广告源加载成功后会都会回调一次。
- (void)tpSplashAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].oneLayerLoadedCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载失败后会都会回调一次，返回三方源的错误信息
- (void)tpSplashAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUSplashManager sharedInstance].oneLayerLoadFailedCallback(self.splash.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///加载流程全部结束
- (void)tpSplashAdAllLoaded:(BOOL)success
{
    MSLogTrace(@"%s success:%@", __PRETTY_FUNCTION__, @(success));
    if([TPUSplashManager sharedInstance].allLoadedCallback)
    {
        [TPUSplashManager sharedInstance].allLoadedCallback(self.splash.unitID.UTF8String,success);
    }
}

///开始播放
- (void)tpSplashAdZoomOutViewShow:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].zoomOutStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].zoomOutStartCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///播放结束
- (void)tpSplashAdZoomOutViewClose:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].zoomOutEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].zoomOutEndCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}

///跳过
- (void)tpSplashAdSkip:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUSplashManager sharedInstance].skipCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSplashManager sharedInstance].skipCallback(self.splash.unitID.UTF8String,jsonString.UTF8String);
    }
}
@end
