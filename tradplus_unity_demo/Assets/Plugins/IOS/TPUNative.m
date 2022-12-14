//
//  TPUNative.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/13.
//

#import "TPUNative.h"
#import <TradPlusAds/TradPlusAds.h>
#import "TPUPluginUtil.h"
#import "TPUNativeManager.h"

@interface TPUNative()<TradPlusADNativeDelegate>

@property (nonatomic,strong)TradPlusAdNative *native;
@end

@implementation TPUNative

- (instancetype)init
{
    self = [super init];
    if (self) {
        [TradPlus setLogLevel:MSLogLevelOff];
        self.native = [[TradPlusAdNative alloc] init];
        self.native.delegate = self;
        self.adView = [[UIView alloc] init];
    }
    return self;
}

- (void)setTemplateRenderSize:(CGSize)size
{
    MSLogTrace(@"%s size:%@", __PRETTY_FUNCTION__,@(size));
    CGRect rect = CGRectZero;
    rect.size = size;
    self.adView.frame = rect;
    self.adView.backgroundColor = [UIColor clearColor];
    [self.native setTemplateRenderSize:size];
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID
{
    MSLogTrace(@"%s adUnitID:%@", __PRETTY_FUNCTION__,adUnitID);
    [self.native setAdUnitID:adUnitID];
}

- (void)loadAd
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    [self.native loadAd];
}

- (void)setX:(float)x y:(float)y adPosition:(int)adPosition
{
    CGRect rect = self.adView.bounds;
    if(x > 0 || y > 0)
    {
        rect.origin.x = x;
        rect.origin.y = y;
    }
    else
    {
        rect = [TPUPluginUtil getRectWithSize:rect.size adPosition:adPosition];
    }
    self.adView.frame = rect;
}

- (TradPlusAdNativeObject *)getReadyNativeObject
{
    return self.native.getReadyNativeObject;
}

- (void)showWithClassName:(Class)viewClass sceneId:(NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    UIViewController *rootController = [TPUPluginUtil unityViewController];
    [rootController.view addSubview:self.adView];
    [self.native showADWithRenderingViewClass:viewClass subview:self.adView sceneId:sceneId];
}

- (void)showWithRenderer:(TradPlusNativeRenderer *)renderer sceneId:(NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.native showADWithNativeRenderer:renderer subview:self.adView sceneId:sceneId];
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.native.segmentTag = segmentTag;
    }
    self.native.dicCustomValue = dic;
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.native entryAdScenario:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.native.isAdReady;
}

- (NSInteger)getLoadedCount
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.native.readyAdCount;
}

- (void)hide
{
    self.adView.hidden = YES;
}

- (void)display
{
    self.adView.hidden = NO;
}

- (void)destroy
{
    [self.adView removeFromSuperview];
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.native.customAdInfo = customAdInfo;
}

#pragma mark - TradPlusADNativeDelegate

- (nullable UIViewController *)viewControllerForPresentingModalView
{
    return [TPUPluginUtil unityViewController];
}

- (void)tpNativeAdIsLoading:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].adIsLoadingCallback)
    {
        [TPUNativeManager sharedInstance].adIsLoadingCallback(self.native.unitID.UTF8String);
    }
}

///AD???????????? ???????????????????????????????????? ????????????????????????????????????
- (void)tpNativeAdLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].loadedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD????????????
///tpNativeAdOneLayerLoad:didFailWithError?????????????????????????????????
- (void)tpNativeAdLoadFailWithError:(NSError *)error
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, error);
    if([TPUNativeManager sharedInstance].loadFailedCallback)
    {
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].loadFailedCallback(self.native.unitID.UTF8String,errorString.UTF8String);
    }
}

///AD??????
- (void)tpNativeAdImpression:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].impressionCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD????????????
- (void)tpNativeAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s %@ %@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUNativeManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].showFailedCallback(self.native.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD?????????
- (void)tpNativeAdClicked:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].clickedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+?????? ??????????????????
- (void)tpNativeAdStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].startLoadCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///?????????????????????????????????????????????????????????
///v7.6.0+?????????????????????????????????tpNativeAdLoadStart:(NSDictionary *)adInfo;
- (void)tpNativeAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].oneLayerStartLoadCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD?????????
- (void)tpNativeAdClose:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].closedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding??????
- (void)tpNativeAdBidStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].biddingStartCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding?????? error = nil ????????????
- (void)tpNativeAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogTrace(@"%s %@ %@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUNativeManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].biddingEndCallback(self.native.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///?????????????????????????????????????????????????????????
- (void)tpNativeAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].oneLayerLoadedCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///???????????????????????????????????????????????????????????????????????????????????????
- (void)tpNativeAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUNativeManager sharedInstance].oneLayerLoadFailedCallback(self.native.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///????????????????????????
- (void)tpNativeAdAllLoaded:(BOOL)success
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, @(success));
    if([TPUNativeManager sharedInstance].allLoadedCallback)
    {
        [TPUNativeManager sharedInstance].allLoadedCallback(self.native.unitID.UTF8String,success);
    }
}

///???????????? v7.8.0+
- (void)tpNativeAdVideoPlayStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].videoPlayStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].videoPlayStartCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///???????????? v7.8.0+
- (void)tpNativeAdVideoPlayEnd:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
    if([TPUNativeManager sharedInstance].videoPlayEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUNativeManager sharedInstance].videoPlayEndCallback(self.native.unitID.UTF8String,jsonString.UTF8String);
    }
}

///???????????????????????????????????? v6.8.0+
- (void)tpNativePasterDidPlayFinished:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s %@", __PRETTY_FUNCTION__, adInfo);
}
@end
