//
//  TPFBanner.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import "TPUBanner.h"
#import "TPUBannerManager.h"
#import "TPUPluginUtil.h"

@interface TPUBanner()<TradPlusADBannerDelegate>

@property (nonatomic,assign)BOOL didShow;
@end

@implementation TPUBanner

- (instancetype)init
{
    self = [super init];
    if (self)
    {
        self.banner = [[TradPlusAdBanner alloc] init];
        self.banner.delegate = self;
    }
    return self;
}

- (void)setClassName:(NSString *)className
{
    if(className != nil && className.length > 0)
    {
        Class class = NSClassFromString(className);
        if(class != nil)
        {
            self.banner.customRenderingViewClass = class;
        }
        else
        {
            MSLogTrace(@"no finid className %@",className);
        }
    }
    else
    {
        self.banner.customRenderingViewClass = nil;
    }
}

- (void)setX:(float)x y:(float)y adPosition:(int)adPosition
{
    CGRect rect = self.banner.bounds;
    UIViewController *rootController = [TPUPluginUtil unityViewController];
    if(x > 0 || y > 0)
    {
        rect.origin.x = x;
        rect.origin.y = y;
    }
    else
    {
        rect = [TPUPluginUtil getRectWithSize:rect.size adPosition:adPosition];
    }
    self.banner.frame = rect;
    [rootController.view addSubview:self.banner];
}

- (void)setCloseAutoShow:(BOOL)closeAutoShow
{
    _closeAutoShow = closeAutoShow;
    if(self.closeAutoShow && !self.didShow)
    {
        self.banner.hidden = YES;
    }
    self.banner.autoShow = !closeAutoShow;
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID
{
    MSLogTrace(@"%s adUnitID:%@", __PRETTY_FUNCTION__,adUnitID);
    [self.banner setAdUnitID:adUnitID];
}

- (void)setBannerSize:(CGSize)size
{
    CGRect rect = CGRectZero;
    rect.size = size;
    self.banner.frame = rect;
    [self.banner setBannerSize:size];
}

- (void)setBannerContentMode:(NSInteger)mode
{
    self.banner.bannerContentMode = mode;
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.banner.segmentTag = segmentTag;
    }
    self.banner.dicCustomValue = dic;
}

- (void)loadAdWithSceneId:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.banner loadAdWithSceneId:sceneId];
}

- (void)showAdWithSceneId:(nullable NSString *)sceneId
{
    //???????????????????????????????????????????????????????????????????????????????????????
    if(self.closeAutoShow && !self.didShow)
    {
        self.didShow = YES;
        self.banner.hidden = NO;
    }
    [self.banner showWithSceneId:sceneId];
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s entryAdScenario:%@", __PRETTY_FUNCTION__,sceneId);
    [self.banner entryAdScenario:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    return self.banner.isAdReady;
}

- (void)hide
{
    self.banner.hidden = YES;
}

- (void)display
{
    self.banner.hidden = NO;
}

- (void)destroy
{
    [self.banner removeFromSuperview];
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.banner.customAdInfo = customAdInfo;
}

#pragma mark - TradPlusADBannerDelegate

- (nullable UIViewController *)viewControllerForPresentingModalView
{
    return [TPUPluginUtil unityViewController];
}

- (void)tpBannerAdIsLoading:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].adIsLoadingCallback)
    {
        [TPUBannerManager sharedInstance].adIsLoadingCallback(self.banner.unitID.UTF8String);
    }
}

///AD???????????? ???????????????????????????????????? ????????????????????????????????????
- (void)tpBannerAdLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].loadedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD????????????
///tpBannerAdOneLayerLoad:didFailWithError?????????????????????????????????
- (void)tpBannerAdLoadFailWithError:(NSError *)error
{
    MSLogTrace(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPUBannerManager sharedInstance].loadFailedCallback)
    {
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].loadFailedCallback(self.banner.unitID.UTF8String,errorString.UTF8String);
    }
}

///AD??????
- (void)tpBannerAdImpression:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].impressionCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD????????????
- (void)tpBannerAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].showFailedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD?????????
- (void)tpBannerAdClicked:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].clickedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///v7.6.0+?????? ??????????????????
- (void)tpBannerAdStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].startLoadCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///?????????????????????????????????????????????????????????
///v7.6.0+?????????????????????????????????tpBannerAdLoadStart:(NSDictionary *)adInfo;
- (void)tpBannerAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].oneLayerStartLoadCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding??????
- (void)tpBannerAdBidStart:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].biddingStartCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].biddingStartCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///bidding?????? error = nil ????????????
- (void)tpBannerAdBidEnd:(NSDictionary *)adInfo error:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@ error:%@", __PRETTY_FUNCTION__, adInfo,error);
    if([TPUBannerManager sharedInstance].biddingEndCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].biddingEndCallback(self.banner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///?????????????????????????????????????????????????????????
- (void)tpBannerAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].oneLayerLoadedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

///???????????????????????????????????????????????????????????????????????????????????????
- (void)tpBannerAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUBannerManager sharedInstance].oneLayerLoadFailedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///????????????????????????
- (void)tpBannerAdAllLoaded:(BOOL)success
{
    MSLogTrace(@"%s success:%@", __PRETTY_FUNCTION__, @(success));
    if([TPUBannerManager sharedInstance].allLoadedCallback)
    {
        [TPUBannerManager sharedInstance].allLoadedCallback(self.banner.unitID.UTF8String,success);
    }
}

///????????????????????????????????????
- (void)tpBannerAdClose:(NSDictionary *)adInfo
{
    MSLogTrace(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUBannerManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUBannerManager sharedInstance].closedCallback(self.banner.unitID.UTF8String,jsonString.UTF8String);
    }
}

@end
