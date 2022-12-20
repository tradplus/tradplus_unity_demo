//
//  TPUOfferwallManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import "TPUOfferwallManager.h"
#import "TPUOfferwall.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUOfferwallManager()

@property (nonatomic,strong)NSMutableDictionary <NSString *,TPUOfferwall *>*offerwallAds;
@end

@implementation TPUOfferwallManager

+ (TPUOfferwallManager *)sharedInstance
{
    static TPUOfferwallManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPUOfferwallManager alloc] init];
    });
    return manager;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.offerwallAds = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (TPUOfferwall *)getOfferwallWithAdUnitID:(NSString *)adUnitId
{
    if([self.offerwallAds valueForKey:adUnitId])
    {
        return self.offerwallAds[adUnitId];
    }
    return nil;
}

- (void)loadWithAdUnitID:(NSString *)adUnitID customMap:(NSDictionary *)customMap
{
    if(adUnitID == nil)
    {
        MSLogInfo(@"adUnitId is null");
        return;
    }
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall == nil)
    {
        offerwall = [[TPUOfferwall alloc] init];
        self.offerwallAds[adUnitID] = offerwall;
    }
    [offerwall setAdUnitID:adUnitID];
    [offerwall setCustomMap:customMap];
    [offerwall loadAd];
}

- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
{
    
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        [offerwall showAdWithSceneId:sceneId];
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
    }
}

- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID
{
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        return offerwall.isAdReady;
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
        return false;
    }
}

- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        [offerwall entryAdScenario:sceneId];
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)setUserIdWithAdUnitID:(NSString *)adUnitID userId:(NSString *)userId
{
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        [offerwall setUserId:userId];
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)getCurrencyBalanceWithAdUnitID:(NSString *)adUnitID
{
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        [offerwall getCurrency];
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)spendBalanceWithAdUnitID:(NSString *)adUnitID count:(int)count
{
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        [offerwall spendWithAmount:count];
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)awardBalanceWithAdUnitID:(NSString *)adUnitID count:(int)count
{
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        [offerwall awardWithAmount:count];
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID
{
    TPUOfferwall *offerwall = [self getOfferwallWithAdUnitID:adUnitID];
    if(offerwall != nil)
    {
        [offerwall setCustomAdInfo:customAdInfo];
    }
    else
    {
        MSLogInfo(@"Offerwall adUnitID:%@ not initialize",adUnitID);
    }
}
@end
