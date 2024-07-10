//
//  TPUNativeBannerManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import "TPUNativeBannerManager.h"
#import "TPUNativeBanner.h"
#import "TPUPluginUtil.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUNativeBannerManager()

@property (nonatomic,strong)NSMutableDictionary <NSString *,TPUNativeBanner *>*nativeBannerAds;
@end

@implementation TPUNativeBannerManager

+ (TPUNativeBannerManager *)sharedInstance
{
    static TPUNativeBannerManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPUNativeBannerManager alloc] init];
    });
    return manager;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.nativeBannerAds = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (TPUNativeBanner *)getNativeBannerWithAdUnitID:(NSString *)adUnitId
{
    if([self.nativeBannerAds valueForKey:adUnitId])
    {
        return self.nativeBannerAds[adUnitId];
    }
    return nil;
}

- (void)loadWithAdUnitID:(NSString *)adUnitID closeAutoShow:(BOOL)closeAutoShow x:(float)x y:(float)y width:(float)width height:(float)height adPosition:(int)adPosition sceneId:(NSString *)sceneId customMap:(NSDictionary *)customMap className:(NSString *)className localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime backgroundColor:(NSString *)backgroundColor
{
    if(adUnitID == nil)
    {
        MSLogInfo(@"adUnitId is null");
        return;
    }
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner == nil)
    {
        nativeBanner = [[TPUNativeBanner alloc] init];
        self.nativeBannerAds[adUnitID] = nativeBanner;
    }
    [nativeBanner setAdUnitID:adUnitID];
    if(openAutoLoadCallback)
    {
        [nativeBanner openAutoLoadCallback];
    }
    [nativeBanner setCustomMap:customMap];
    [nativeBanner setLocalParams:localParams];
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
    [nativeBanner setNativeBannerSize:size];
    [nativeBanner setX:x y:y adPosition:adPosition];
    nativeBanner.closeAutoShow = closeAutoShow;
    nativeBanner.className = className;
    [nativeBanner setBackgroundColor:backgroundColor];
    [nativeBanner loadAdWithSceneId:sceneId maxWaitTime:maxWaitTime];
}

- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
{
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner != nil)
    {
        [nativeBanner showAdWithSceneId:sceneId];
    }
    else
    {
        MSLogInfo(@"NativeBanner adUnitID:%@ not initialize",adUnitID);
    }
}

- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID
{
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner != nil)
    {
        return nativeBanner.isAdReady;
    }
    else
    {
        MSLogInfo(@"NativeBanner adUnitID:%@ not initialize",adUnitID);
        return false;
    }
}

- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner != nil)
    {
        [nativeBanner entryAdScenario:sceneId];
    }
    else
    {
        MSLogInfo(@"NativeBanner adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)hideWithAdUnitID:(NSString *)adUnitID
{
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner != nil)
    {
        [nativeBanner hide];
    }
    else
    {
        MSLogInfo(@"NativeBanner adUnitID:%@ not initialize",adUnitID);
    }
}
- (void)displayWithAdUnitID:(NSString *)adUnitID
{
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner != nil)
    {
        [nativeBanner display];
    }
    else
    {
        MSLogInfo(@"NativeBanner adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)destroyWithAdUnitID:(NSString *)adUnitID
{
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner != nil)
    {
        [nativeBanner destroy];
        [self.nativeBannerAds removeObjectForKey:adUnitID];
    }
    else
    {
        MSLogInfo(@"NativeBanner adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID
{
    TPUNativeBanner *nativeBanner = [self getNativeBannerWithAdUnitID:adUnitID];
    if(nativeBanner != nil)
    {
        [nativeBanner setCustomAdInfo:customAdInfo];
    }
    else
    {
        MSLogInfo(@"NativeBanner adUnitID:%@ not initialize",adUnitID);
    }
}
@end
