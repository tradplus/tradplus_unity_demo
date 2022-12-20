//
//  TPUInterstitial.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import "TPUInterstitial.h"
#import "TPUInterstitialManager.h"
#import "TPUPluginUtil.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUInterstitial()<TradPlusADInterstitialDelegate>

@property (nonatomic,strong)TradPlusAdInterstitial *interstitial;
@end

@implementation TPUInterstitial

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.interstitial = [[TradPlusAdInterstitial alloc] init];
        self.interstitial.delegate = self;
    }
    return self;
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID
{
    MSLogTrace(@"%s adUnitID:%@", __PRETTY_FUNCTION__,adUnitID);
    [self.interstitial setAdUnitID:adUnitID];
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.interstitial.segmentTag = segmentTag;
    }
    self.interstitial.dicCustomValue = dic;
}

- (void)loadAd
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    [self.interstitial loadAd];
}

- (void)showAdWithSceneId:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.interstitial showAdFromRootViewController:[TPUPluginUtil unityViewController] sceneId:sceneId];
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.interstitial entryAdScenario:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.interstitial.isAdReady;
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.interstitial.customAdInfo = customAdInfo;
}

#pragma mark - TradPlusADInterstitialDelegate

- (void)tpInterstitialAdIsLoading:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].adIsLoadingCallback)
    {
        [TPUInterstitialManager sharedInstance].adIsLoadingCallback(self.interstitial.unitID.UTF8String);
    }
}

///AD加载完成 首个广告源加载成功时回调 一次加载流程只会回调一次
- (void)tpInterstitialAdLoaded:(NSDictionary *)adInfo
{
    
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].loadedCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD加载失败
///tpInterstitialAdOneLayerLoaded:didFailWithError：返回三方源的错误信息
- (void)tpInterstitialAdLoadFailWithError:(NSError *)error
{
    MSLogTrace(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPUInterstitialManager sharedInstance].loadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUInterstitialManager sharedInstance].loadFailedCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现
- (void)tpInterstitialAdImpression:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].impressionCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpInterstitialAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUInterstitialManager sharedInstance].showFailedCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD被点击
- (void)tpInterstitialAdClicked:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].clickedCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD关闭
- (void)tpInterstitialAdDismissed:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].closedCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+ 开始加载流程
- (void)tpInterstitialAdStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].startLoadCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源开始加载时会都会回调一次。
///v7.6.0+新增。替代原回调接口：tpInterstitialAdLoadStart:(NSDictionary *)adInfo;
- (void)tpInterstitialAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].oneLayerStartLoadCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding开始
- (void)tpInterstitialAdBidStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].biddingStartCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding结束 error = nil 表示成功
- (void)tpInterstitialAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUInterstitialManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUInterstitialManager sharedInstance].biddingEndCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///当每个广告源加载成功后会都会回调一次。
- (void)tpInterstitialAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].oneLayerLoadedCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载失败后会都会回调一次，返回三方源的错误信息
- (void)tpInterstitialAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUInterstitialManager sharedInstance].oneLayerLoadFailedCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///加载流程全部结束
- (void)tpInterstitialAdAllLoaded:(BOOL)success
{
    MSLogTrace(@"%s success:%@", __PRETTY_FUNCTION__, @(success));
    if([TPUInterstitialManager sharedInstance].allLoadedCallback)
    {
        [TPUInterstitialManager sharedInstance].allLoadedCallback(self.interstitial.unitID.UTF8String,success);
    }
}

///开始播放
- (void)tpInterstitialAdPlayStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].videoPlayStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].videoPlayStartCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}

///播放结束
- (void)tpInterstitialAdPlayEnd:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUInterstitialManager sharedInstance].videoPlayEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUInterstitialManager sharedInstance].videoPlayEndCallback(self.interstitial.unitID.UTF8String,jsonString.UTF8String);
    }
}
@end
