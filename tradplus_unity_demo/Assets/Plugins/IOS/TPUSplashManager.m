//
//  TPUSplashManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import "TPUSplashManager.h"
#import "TPUSplash.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUSplashManager()

@property (nonatomic,strong)NSMutableDictionary <NSString *,TPUSplash *>*splashAds;
@end

@implementation TPUSplashManager

+ (TPUSplashManager *)sharedInstance
{
    static TPUSplashManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPUSplashManager alloc] init];
    });
    return manager;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.splashAds = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (TPUSplash *)getsplashWithAdUnitID:(NSString *)adUnitId
{
    if([self.splashAds valueForKey:adUnitId])
    {
        return self.splashAds[adUnitId];
    }
    return nil;
}

- (void)loadWithAdUnitID:(NSString *)adUnitID customMap:(NSDictionary *)customMap localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime;
{
    if(adUnitID == nil)
    {
        MSLogInfo(@"adUnitId is null");
        return;
    }
    TPUSplash *splash = [self getsplashWithAdUnitID:adUnitID];
    if(splash == nil)
    {
        splash = [[TPUSplash alloc] init];
        self.splashAds[adUnitID] = splash;
    }
    [splash setLocalParams:localParams];
    [splash setCustomMap:customMap];
    if(openAutoLoadCallback)
    {
        [splash openAutoLoadCallback];
    }
    [splash setAdUnitID:adUnitID];
    [splash loadAdWithMaxWaitTime:maxWaitTime];
}

- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPUSplash *splash = [self getsplashWithAdUnitID:adUnitID];
    if(splash != nil)
    {
        [splash showAdWithSceneId:sceneId];
    }
    else
    {
        MSLogInfo(@"splash adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPUSplash *splash = [self getsplashWithAdUnitID:adUnitID];
    if(splash != nil)
    {
        [splash entryAdScenario:sceneId];
    }
    else
    {
        MSLogInfo(@"splash adUnitID:%@ not initialize",adUnitID);
    }
}

- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID
{
    TPUSplash *splash = [self getsplashWithAdUnitID:adUnitID];
    if(splash != nil)
    {
        return splash.isAdReady;
    }
    else
    {
        MSLogInfo(@"splash adUnitID:%@ not initialize",adUnitID);
        return false;
    }
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID
{
    TPUSplash *splash = [self getsplashWithAdUnitID:adUnitID];
    if(splash != nil)
    {
        [splash setCustomAdInfo:customAdInfo];
    }
    else
    {
        MSLogInfo(@"splash adUnitID:%@ not initialize",adUnitID);
    }
}
@end
