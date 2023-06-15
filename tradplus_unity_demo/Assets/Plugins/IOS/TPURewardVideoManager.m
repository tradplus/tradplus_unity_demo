//
//  TPURewardVideoManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import "TPURewardVideoManager.h"
#import "TPURewardVideo.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPURewardVideoManager()

@property (nonatomic,strong)NSMutableDictionary <NSString *,TPURewardVideo *>*rewardVideoAds;
@end

@implementation TPURewardVideoManager

+ (TPURewardVideoManager *)sharedInstance
{
    static TPURewardVideoManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPURewardVideoManager alloc] init];
    });
    return manager;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.rewardVideoAds = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (TPURewardVideo *)getRewardVideoWithAdUnitID:(NSString *)adUnitId
{
    if([self.rewardVideoAds valueForKey:adUnitId])
    {
        return self.rewardVideoAds[adUnitId];
    }
    return nil;
}

- (void)loadWithAdUnitID:(NSString *)adUnitID userId:(NSString *)userId customData:(NSString *)customData customMap:(NSDictionary *)customMap localParams:(NSDictionary *)localParams
{
    if(adUnitID == nil)
    {
        MSLogInfo(@"adUnitId is null");
        return;
    }
    TPURewardVideo *rewardVideo = [self getRewardVideoWithAdUnitID:adUnitID];
    if(rewardVideo == nil)
    {
        rewardVideo = [[TPURewardVideo alloc] init];
        self.rewardVideoAds[adUnitID] = rewardVideo;
    }
    [rewardVideo setCustomMap:customMap];
    [rewardVideo setLocalParams:localParams];
    [rewardVideo setAdUnitID:adUnitID];
    if(userId != nil)
    {
        [rewardVideo setServerSideVerificationOptionsWithUserID:userId customData:customData];
    }
    [rewardVideo loadAd];
}

- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
{
    
    TPURewardVideo *rewardVideo = [self getRewardVideoWithAdUnitID:adUnitID];
    if(rewardVideo != nil)
    {
        [rewardVideo showAdWithSceneId:sceneId];
    }
    else
    {
        MSLogInfo(@"RewardVideo adUnitID:%@ not initialize",adUnitID);
    }
}

- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID
{
    TPURewardVideo *rewardVideo = [self getRewardVideoWithAdUnitID:adUnitID];
    if(rewardVideo != nil)
    {
        return rewardVideo.isAdReady;
    }
    else
    {
        MSLogInfo(@"RewardVideo adUnitID:%@ not initialize",adUnitID);
        return false;
    }
}

- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPURewardVideo *rewardVideo = [self getRewardVideoWithAdUnitID:adUnitID];
    if(rewardVideo != nil)
    {
        [rewardVideo entryAdScenario:sceneId];
    }
    else
    {
        MSLogInfo(@"RewardVideo adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID
{
    TPURewardVideo *rewardVideo = [self getRewardVideoWithAdUnitID:adUnitID];
    if(rewardVideo != nil)
    {
        [rewardVideo setCustomAdInfo:customAdInfo];
    }
    else
    {
        MSLogInfo(@"RewardVideo adUnitID:%@ not initialize",adUnitID);
    }
}
@end
