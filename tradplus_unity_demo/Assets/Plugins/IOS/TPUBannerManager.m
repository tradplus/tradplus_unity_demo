//
//  TPUBannerManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import "TPUBannerManager.h"
#import "TPUBanner.h"
#import <TradPlusAds/TradPlusAds.h>
#import "TPUPluginUtil.h"

@interface TPUBannerManager()

@property (nonatomic,strong)NSMutableDictionary <NSString *,TPUBanner *>*bannerAds;
@end

@implementation TPUBannerManager

+ (TPUBannerManager *)sharedInstance
{
    static TPUBannerManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPUBannerManager alloc] init];
    });
    return manager;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.bannerAds = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (TPUBanner *)getBannerWithAdUnitID:(NSString *)adUnitId
{
    if([self.bannerAds valueForKey:adUnitId])
    {
        return self.bannerAds[adUnitId];
    }
    return nil;
}

- (void)loadWithAdUnitID:(NSString *)adUnitID closeAutoShow:(BOOL)closeAutoShow x:(float)x y:(float)y width:(float)width height:(float)height adPosition:(int)adPosition contentMode:(int)contentMode sceneId:(NSString *)sceneId customMap:(NSDictionary *)customMap className:(NSString *)className localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime backgroundColor:(NSString *)backgroundColor
{
    if(adUnitID == nil)
    {
        MSLogInfo(@"adUnitId is null");
        return;
    }
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner == nil)
    {
        banner = [[TPUBanner alloc] init];
        self.bannerAds[adUnitID] = banner;
    }
    [banner setClassName:className];
    [banner setCustomMap:customMap];
    [banner setLocalParams:localParams];
    CGSize size = CGSizeZero;
    if(width == 0)
    {
        UIView *rootView = [TPUPluginUtil unityViewController].view;
        size.width = [UIScreen mainScreen].bounds.size.width;
        if (@available(iOS 11.0, *)) {
            size.width = size.width - rootView.safeAreaInsets.left - rootView.safeAreaInsets.right;
        }
    }
    else
    {
        size.width = width;
    }
    if(height == 0)
    {
        size.height = 50;
    }
    else
    {
        size.height = height;
    }
    [banner setBannerSize:size];
    [banner setBannerContentMode:contentMode];
    banner.closeAutoShow = closeAutoShow;
    [banner setX:x y:y adPosition:adPosition];
    [banner setAdUnitID:adUnitID];
    [banner setBackgroundColor:backgroundColor];
    if(openAutoLoadCallback)
    {
        [banner openAutoLoadCallback];
    }
    [banner loadAdWithSceneId:sceneId maxWaitTime:maxWaitTime];
}

- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
{
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner != nil)
    {
        [banner showAdWithSceneId:sceneId];
    }
    else
    {
        MSLogInfo(@"Banner adUnitID:%@ not initialize",adUnitID);
    }
}

- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID
{
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner != nil)
    {
        return banner.isAdReady;
    }
    else
    {
        MSLogInfo(@"Banner adUnitID:%@ not initialize",adUnitID);
        return false;
    }
}

- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner != nil)
    {
        [banner entryAdScenario:sceneId];
    }
    else
    {
        MSLogInfo(@"Banner adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)hideWithAdUnitID:(NSString *)adUnitID
{
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner != nil)
    {
        [banner hide];
    }
    else
    {
        MSLogInfo(@"Banner adUnitID:%@ not initialize",adUnitID);
    }
}
- (void)displayWithAdUnitID:(NSString *)adUnitID
{
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner != nil)
    {
        [banner display];
    }
    else
    {
        MSLogInfo(@"Banner adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)destroyWithAdUnitID:(NSString *)adUnitID
{
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner != nil)
    {
        [banner destroy];
        [self.bannerAds removeObjectForKey:adUnitID];
    }
    else
    {
        MSLogInfo(@"Banner adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID
{
    TPUBanner *banner = [self getBannerWithAdUnitID:adUnitID];
    if(banner != nil)
    {
        [banner setCustomAdInfo:customAdInfo];
    }
    else
    {
        MSLogInfo(@"Banner adUnitID:%@ not initialize",adUnitID);
    }
}
@end
