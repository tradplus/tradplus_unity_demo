//
//  TPUInterstitialManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import "TPUInterstitialManager.h"
#import "TPUInterstitial.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUInterstitialManager()

@property (nonatomic,strong)NSMutableDictionary <NSString *,TPUInterstitial *>*interstitialAds;
@end

@implementation TPUInterstitialManager

+ (TPUInterstitialManager *)sharedInstance
{
    static TPUInterstitialManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPUInterstitialManager alloc] init];
    });
    return manager;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.interstitialAds = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (TPUInterstitial *)getInterstitialWithAdUnitID:(NSString *)adUnitId
{
    if([self.interstitialAds valueForKey:adUnitId])
    {
        return self.interstitialAds[adUnitId];
    }
    return nil;
}

- (void)loadWithAdUnitID:(NSString *)adUnitID customMap:(NSDictionary *)customMap localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime
{
    if(adUnitID == nil)
    {
        MSLogInfo(@"adUnitId is null");
        return;
    }
    TPUInterstitial *interstitial = [self getInterstitialWithAdUnitID:adUnitID];
    if(interstitial == nil)
    {
        interstitial = [[TPUInterstitial alloc] init];
        self.interstitialAds[adUnitID] = interstitial;
    }
    [interstitial setLocalParams:localParams];
    [interstitial setCustomMap:customMap];
    if(openAutoLoadCallback)
    {
        [interstitial openAutoLoadCallback];
    }
    [interstitial setAdUnitID:adUnitID];
    [interstitial loadAdWithMaxWaitTime:maxWaitTime];
}

- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
{
    
    TPUInterstitial *interstitial = [self getInterstitialWithAdUnitID:adUnitID];
    if(interstitial != nil)
    {
        [interstitial showAdWithSceneId:sceneId];
    }
    else
    {
        MSLogInfo(@"interstitial adUnitID:%@ not initialize",adUnitID);
    }
}

- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID
{
    TPUInterstitial *interstitial = [self getInterstitialWithAdUnitID:adUnitID];
    if(interstitial != nil)
    {
        return interstitial.isAdReady;
    }
    else
    {
        MSLogInfo(@"interstitial adUnitID:%@ not initialize",adUnitID);
        return false;
    }
}

- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPUInterstitial *interstitial = [self getInterstitialWithAdUnitID:adUnitID];
    if(interstitial != nil)
    {
        [interstitial entryAdScenario:sceneId];
    }
    else
    {
        MSLogInfo(@"interstitial adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID
{
    TPUInterstitial *interstitial = [self getInterstitialWithAdUnitID:adUnitID];
    if(interstitial != nil)
    {
        [interstitial setCustomAdInfo:customAdInfo];
    }
    else
    {
        MSLogInfo(@"interstitial adUnitID:%@ not initialize",adUnitID);
    }
}
@end
