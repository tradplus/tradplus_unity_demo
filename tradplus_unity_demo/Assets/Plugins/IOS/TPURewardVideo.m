//
//  TPInterstitial.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import "TPURewardVideo.h"
#import "TPURewardVideoManager.h"
#import "TPUPluginUtil.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPURewardVideo()<TradPlusADRewardedDelegate,TradPlusADRewardedPlayAgainDelegate>

@property (nonatomic,strong)TradPlusAdRewarded *rewarded;
@end

@implementation TPURewardVideo

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.rewarded = [[TradPlusAdRewarded alloc] init];
        self.rewarded.delegate = self;
        self.rewarded.playAgainDelegate = self;
    }
    return self;
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID
{
    MSLogTrace(@"%s adUnitID:%@", __PRETTY_FUNCTION__,adUnitID);
    [self.rewarded setAdUnitID:adUnitID];
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.rewarded.segmentTag = segmentTag;
    }
    self.rewarded.dicCustomValue = dic;
}

- (void)setLocalParams:(NSDictionary *)dic
{
    self.rewarded.localParams = dic;
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__,dic);
}

- (void)setServerSideVerificationOptionsWithUserID:(nonnull NSString *)userID customData:(nullable NSString *)customData
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    [self.rewarded setServerSideVerificationOptionsWithUserID:userID customData:customData];
}

- (void)loadAdWithMaxWaitTime:(float)maxWaitTime
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    [self.rewarded loadAdWithMaxWaitTime:maxWaitTime];
}

- (void)openAutoLoadCallback
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    [self.rewarded openAutoLoadCallback];
}

- (void)showAdWithSceneId:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.rewarded showAdFromRootViewController:[TPUPluginUtil unityViewController] sceneId:sceneId];
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.rewarded entryAdScenario:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    return self.rewarded.isAdReady;
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.rewarded.customAdInfo = customAdInfo;
}

#pragma mark - TradPlusADRewardedDelegate

- (void)tpRewardedAdIsLoading:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].adIsLoadingCallback)
    {
        [TPURewardVideoManager sharedInstance].adIsLoadingCallback(self.rewarded.unitID.UTF8String);
    }
}

///AD加载完成 首个广告源加载成功时回调 一次加载流程只会回调一次
- (void)tpRewardedAdLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].loadedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD加载失败
///tpRewardedAdOneLayerLoad:didFailWithError：返回三方源的错误信息
- (void)tpRewardedAdLoadFailWithError:(NSError *)error
{
    MSLogTrace(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPURewardVideoManager sharedInstance].loadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithError:error];
        [TPURewardVideoManager sharedInstance].loadFailedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现
- (void)tpRewardedAdImpression:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].impressionCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpRewardedAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPURewardVideoManager sharedInstance].oneLayerLoadFailedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD被点击
- (void)tpRewardedAdClicked:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].clickedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD关闭
- (void)tpRewardedAdDismissed:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].closedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///完成奖励
- (void)tpRewardedAdReward:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].rewardCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].rewardCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+新增 开始加载流程
- (void)tpRewardedAdStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].startLoadCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源开始加载时会都会回调一次。
///v7.6.0+新增。替代原回调接口：tpRewardedAdLoadStart:(NSDictionary *)adInfo;
- (void)tpRewardedAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].oneLayerStartLoadCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding开始
- (void)tpRewardedAdBidStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].biddingStartCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding结束 error = nil 表示成功
- (void)tpRewardedAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPURewardVideoManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPURewardVideoManager sharedInstance].biddingEndCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///当每个广告源加载成功后会都会回调一次。
- (void)tpRewardedAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].oneLayerLoadedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载失败后会都会回调一次，返回三方源的错误信息
- (void)tpRewardedAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPURewardVideoManager sharedInstance].oneLayerLoadFailedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///加载流程全部结束
- (void)tpRewardedAdAllLoaded:(BOOL)success
{
    MSLogTrace(@"%s success:%@", __PRETTY_FUNCTION__, @(success));
    if([TPURewardVideoManager sharedInstance].allLoadedCallback)
    {
        [TPURewardVideoManager sharedInstance].allLoadedCallback(self.rewarded.unitID.UTF8String,success);
    }
}

///开始播放
- (void)tpRewardedAdPlayStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].videoPlayStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].videoPlayStartCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///播放结束
- (void)tpRewardedAdPlayEnd:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].videoPlayEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].videoPlayEndCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

#pragma mark - TradPlusADRewardedPlayAgainDelegate

///AD展现
- (void)tpRewardedAdPlayAgainImpression:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].playAgainImpressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].playAgainImpressionCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpRewardedAdPlayAgainShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
}

///AD被点击
- (void)tpRewardedAdPlayAgainClicked:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].playAgainClickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].playAgainClickedCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///完成奖励
- (void)tpRewardedAdPlayAgainReward:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].playAgainRewardCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].playAgainRewardCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///开始播放
- (void)tpRewardedAdPlayAgainPlayStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].playAgainVideoPlayStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].playAgainVideoPlayStartCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}

///播放结束
- (void)tpRewardedAdPlayAgainPlayEnd:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPURewardVideoManager sharedInstance].playAgainVideoPlayEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPURewardVideoManager sharedInstance].playAgainVideoPlayEndCallback(self.rewarded.unitID.UTF8String,jsonString.UTF8String);
    }
}
@end
