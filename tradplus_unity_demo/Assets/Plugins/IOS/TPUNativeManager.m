//
//  TPUNativeManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import "TPUNativeManager.h"
#import "TPUNative.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUNativeManager()

@property (nonatomic,strong)NSMutableDictionary <NSString *,TPUNative *>*nativeAds;
@end

@implementation TPUNativeManager

+ (TPUNativeManager *)sharedInstance
{
    static TPUNativeManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPUNativeManager alloc] init];
    });
    return manager;
}

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.nativeAds = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (TPUNative *)getNativeWithAdUnitID:(NSString *)adUnitId
{
    if([self.nativeAds valueForKey:adUnitId])
    {
        return self.nativeAds[adUnitId];
    }
    return nil;
}

- (void)loadWithAdUnitID:(NSString *)adUnitID isAutoLoad:(BOOL)isAutoLoad x:(float)x y:(float)y width:(float)width height:(float)height adPosition:(int)adPosition customMap:(NSDictionary *)customMap
{
    if(adUnitID == nil)
    {
        MSLogInfo(@"adUnitId is null");
        return;
    }
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native == nil)
    {
        native = [[TPUNative alloc] init];
        self.nativeAds[adUnitID] = native;
    }
    [native setCustomMap:customMap];
    CGSize size = CGSizeZero;
    size.width = width;
    size.height = height;
    [native setTemplateRenderSize:size];
    [native setX:x y:y adPosition:adPosition];
    [native setAdUnitID:adUnitID isAutoLoad:isAutoLoad];
    if(!isAutoLoad)
    {
        [native loadAd];
    }
}

- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId className:(NSString *)className
{
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native != nil)
    {
        if(className == nil)
        {
            className = @"TPNativeTemplate";
        }
        Class renderClass = NSClassFromString(className);
        if(renderClass != nil)
        {
            [native showWithClassName:renderClass sceneId:sceneId];
        }
        else
        {
            MSLogInfo(@"Native renderClass is nil className:%@ adUnitID:%@",className,adUnitID);
        }
    }
    else
    {
        MSLogInfo(@"Native adUnitID:%@ not initialize",adUnitID);
    }
}

- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID
{
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native != nil)
    {
        return native.isAdReady;
    }
    else
    {
        MSLogInfo(@"Native adUnitID:%@ not initialize",adUnitID);
        return false;
    }
}

- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId
{
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native != nil)
    {
        [native entryAdScenario:sceneId];
    }
    else
    {
        MSLogInfo(@"Native adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)hideWithAdUnitID:(NSString *)adUnitID
{
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native != nil)
    {
        [native hide];
    }
    else
    {
        MSLogInfo(@"Native adUnitID:%@ not initialize",adUnitID);
    }
}
- (void)displayWithAdUnitID:(NSString *)adUnitID
{
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native != nil)
    {
        [native display];
    }
    else
    {
        MSLogInfo(@"Native adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)destroyWithAdUnitID:(NSString *)adUnitID
{
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native != nil)
    {
        [native destroy];
        [self.nativeAds removeObjectForKey:adUnitID];
    }
    else
    {
        MSLogInfo(@"Native adUnitID:%@ not initialize",adUnitID);
    }
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID
{
    TPUNative *native = [self getNativeWithAdUnitID:adUnitID];
    if(native != nil)
    {
        [native setCustomAdInfo:customAdInfo];
    }
    else
    {
        MSLogInfo(@"Native adUnitID:%@ not initialize",adUnitID);
    }
}
@end
