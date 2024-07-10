
#import "TPUInterstitialManager.h"
#import "TPURewardVideoManager.h"
#import "TPUOfferwallManager.h"
#import "TPUBannerManager.h"
#import "TPUNativeManager.h"
#import "TPUNativeBannerManager.h"
#import "TPUSDKManager.h"
#import "TPUSplashManager.h"

static NSString *stringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

static const char *cStringCopy(const char *string) {
  if (!string) {
    return NULL;
  }
  char *res = (char *)malloc(strlen(string) + 1);
  strcpy(res, string);
  return res;
}

#pragma mark - sdk

void TradplusCheckCurrentArea()
{
    [[TPUSDKManager sharedInstance] checkCurrentArea];
}

void TradplusInitSDK(const char* appId)
{
    NSString *appIdStr = stringFromUTF8String(appId);
    [[TPUSDKManager sharedInstance] initSDKWithAppID:appIdStr];
}

void TradplusSetCustomMap(const char* customMap)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    [[TPUSDKManager sharedInstance] setCustomMap:dic];
}

void TradplusSetSettingDataParam (const char* settingMap)
{
    NSString *jsonString = stringFromUTF8String(settingMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    [[TPUSDKManager sharedInstance] setSettingDataParam:dic];
}


const char* TradplusVersion()
{
    NSString *version = [[TPUSDKManager sharedInstance] getVersion];
    return cStringCopy(version.UTF8String);
}

bool TradplusIsEUTraffic()
{
    return [[TPUSDKManager sharedInstance] isEUTraffic];
}

bool TradplusIsCalifornia()
{
    return [[TPUSDKManager sharedInstance] isCalifornia];
}


void TradplusSetGDPRDataCollection(bool canDataCollection)
{
    [[TPUSDKManager sharedInstance] setGDPRDataCollection:canDataCollection];
}

int TradplusGetGDPRDataCollection()
{
    return [[TPUSDKManager sharedInstance] getGDPRDataCollection];
}

void TradplusSetCCPADoNotSell(bool canDataCollection)
{
    [[TPUSDKManager sharedInstance] setCCPADoNotSell:canDataCollection];
}

int TradplusGetCCPADoNotSell()
{
    return [[TPUSDKManager sharedInstance] getCCPADoNotSell];
}

void TradplusSetCOPPAIsAgeRestrictedUser(bool isChild)
{
    [[TPUSDKManager sharedInstance] setCOPPAIsAgeRestrictedUser:isChild];
}


int TradplusGetCOPPAIsAgeRestrictedUser()
{
    return [[TPUSDKManager sharedInstance] getCOPPAIsAgeRestrictedUser];
}

void TradplusShowGDPRDialog()
{
    [[TPUSDKManager sharedInstance] showGDPRDialog];
}

void TradplusSetOpenPersonalizedAd(bool open)
{
    [[TPUSDKManager sharedInstance] setOpenPersonalizedAd:open];
}

bool TradplusIsOpenPersonalizedAd()
{
    return [[TPUSDKManager sharedInstance] isOpenPersonalizedAd];
}

void TradplusClearCache(const char* adUnitId)
{
    NSString *adUnitIdStr = stringFromUTF8String(adUnitId);
    [[TPUSDKManager sharedInstance] clearCache:adUnitIdStr];
}

void TradplusSetAutoExpiration(bool autoCheck)
{
    [[TPUSDKManager sharedInstance] setAutoExpiration:autoCheck];
}

void TradplusCheckAutoExpiration()
{
    [[TPUSDKManager sharedInstance] checkAutoExpiration];
}

void TradplusSetCnServer(bool onlyCn)
{
    [[TPUSDKManager sharedInstance] setCnServer:onlyCn];
}

int TradplusGetLGPDConsent()
{
    return [[TPUSDKManager sharedInstance] getLGPDConsent];
}

void TradplusSetLGPDConsent(bool consent)
{
    [[TPUSDKManager sharedInstance] setLGPDConsent:consent];
}

void TradplusOpenTradPlusTool()
{
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wundeclared-selector"
    Class TPMediationHelper = NSClassFromString(@"TPMediationHelper");
    if(TPMediationHelper != nil)
    {
        if([TPMediationHelper respondsToSelector:@selector(open)])
        {
            [TPMediationHelper performSelector:@selector(open)];
        }
    }
    else
    {
        NSLog(@"***************");
        NSLog(@"no find TPMediationHelper SDK");
        NSLog(@"***************");
    }
#pragma clang diagnostic pop
}


void TradplusSDKSetCallbacks(TPOnInitFinishCallback onInitFinishCallback,
                             TPOnDialogClosedCallback onDialogClosedCallback,
                             TPOnCurrentAreaSuccessCallback onCurrentAreaSuccessCallback,
                             TPOnCurrentAreaFailedCallback onCurrentAreaFailedCallback)
{
    [TPUSDKManager sharedInstance].onInitFinishCallback = onInitFinishCallback;
    [TPUSDKManager sharedInstance].onDialogClosedCallback = onDialogClosedCallback;
    [TPUSDKManager sharedInstance].onCurrentAreaSuccessCallback = onCurrentAreaSuccessCallback;
    [TPUSDKManager sharedInstance].onCurrentAreaFailedCallback = onCurrentAreaFailedCallback;
}

void TradplusSDKSetAdImpressionCallback(TPOnAdImpressionCallback onAdImpressionCallback)
{
    [TPUSDKManager sharedInstance].onAdImpressionCallback = onAdImpressionCallback;
}

#pragma mark - Interstitial

void TradplusLoadInterstitialAd(const char* adUnitId,const char* customMap,const char* localParams,bool openAutoLoadCallback, float maxWaitTime)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *localParamsString = stringFromUTF8String(localParams);
    NSDictionary *localParamsDic = nil;
    if(localParamsString != nil)
    {
        NSData *data = [localParamsString dataUsingEncoding:NSUTF8StringEncoding];
        localParamsDic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUInterstitialManager sharedInstance] loadWithAdUnitID:adUnitIDStr customMap:dic localParams:localParamsDic openAutoLoadCallback:openAutoLoadCallback maxWaitTime:maxWaitTime];
}

void TradplusShowInterstitialAd(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUInterstitialManager sharedInstance] showWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

bool TradplusInterstitialAdReady(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    return [[TPUInterstitialManager sharedInstance] adReadyWithAdUnitID:adUnitIDStr];
}

void TradplusEntryInterstitialAdScenario(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUInterstitialManager sharedInstance] entryAdScenarioWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

void TradplusSetCustomAdInfoInterstitial(const char* adUnitId,const char* customAdInfo)
{
    NSString *jsonString = stringFromUTF8String(customAdInfo);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUInterstitialManager sharedInstance] setCustomAdInfo:dic adUnitID:adUnitIDStr];
}

void TradplusInterstitialSetCallbacks(TPInterstitialLoadedCallback adLoadedCallback,
                                      TPInterstitialLoadFailedCallback adLoadFailedCallback,
                                      TPInterstitialImpressionCallback adImpressionCallback,
                                      TPInterstitialShowFailedCallback adShowFailedCallback,
                                      TPInterstitialClickedCallback adClickedCallback,
                                      TPInterstitialClosedCallback adClosedCallback,
                                      TPInterstitialStartLoadCallback adStartLoadCallback,
                                      TPInterstitialBiddingStartCallback adBiddingStartCallback,
                                      TPInterstitialBiddingEndCallback adBiddingEndCallback,
                                      TPInterstitialOneLayerStartLoadCallback adOneLayerStartLoadCallback,
                                      TPInterstitialOneLayerLoadedCallback adOneLayerLoadedCallback,
                                      TPInterstitialOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
                                      TPInterstitialVideoPlayStartCallback adVideoPlayStartCallback,
                                      TPInterstitialVideoPlayEndCallback adVideoPlayEndCallback,
                                      TPInterstitialAllLoadedCallback adAllLoadedCallback,
                                      TPInterstitialAdIsLoadingCallback adIsLoadingCallback)
{
    [TPUInterstitialManager sharedInstance].loadedCallback = adLoadedCallback;
    [TPUInterstitialManager sharedInstance].loadFailedCallback = adLoadFailedCallback;
    [TPUInterstitialManager sharedInstance].impressionCallback = adImpressionCallback;
    [TPUInterstitialManager sharedInstance].showFailedCallback = adShowFailedCallback;
    [TPUInterstitialManager sharedInstance].clickedCallback = adClickedCallback;
    [TPUInterstitialManager sharedInstance].closedCallback = adClosedCallback;
    [TPUInterstitialManager sharedInstance].startLoadCallback = adStartLoadCallback;
    [TPUInterstitialManager sharedInstance].biddingStartCallback = adBiddingStartCallback;
    [TPUInterstitialManager sharedInstance].biddingEndCallback = adBiddingEndCallback;
    [TPUInterstitialManager sharedInstance].oneLayerStartLoadCallback = adOneLayerStartLoadCallback;
    [TPUInterstitialManager sharedInstance].oneLayerLoadedCallback = adOneLayerLoadedCallback;
    [TPUInterstitialManager sharedInstance].oneLayerLoadFailedCallback = adOneLayerLoadFailedCallback;
    [TPUInterstitialManager sharedInstance].videoPlayStartCallback = adVideoPlayStartCallback;
    [TPUInterstitialManager sharedInstance].videoPlayEndCallback = adVideoPlayEndCallback;
    [TPUInterstitialManager sharedInstance].allLoadedCallback = adAllLoadedCallback;
    [TPUInterstitialManager sharedInstance].adIsLoadingCallback = adIsLoadingCallback;
}

#pragma mark - RewardVideo

void TradplusLoadRewardVideoAd(const char* adUnitId,const char* userId,const char* customData,const char* customMap,const char* localParams,bool openAutoLoadCallback, float maxWaitTime)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *userIDStr = stringFromUTF8String(userId);
    NSString *customDataStr = stringFromUTF8String(customData);
    NSString *localParamsString = stringFromUTF8String(localParams);
    NSDictionary *localParamsDic = nil;
    if(localParamsString != nil)
    {
        NSData *data = [localParamsString dataUsingEncoding:NSUTF8StringEncoding];
        localParamsDic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    [[TPURewardVideoManager sharedInstance] loadWithAdUnitID:adUnitIDStr userId:userIDStr customData:customDataStr customMap:dic localParams:localParamsDic openAutoLoadCallback:openAutoLoadCallback maxWaitTime:maxWaitTime];
}

void TradplusShowRewardVideoAd(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPURewardVideoManager sharedInstance] showWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

bool TradplusRewardVideoAdReady(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    return [[TPURewardVideoManager sharedInstance] adReadyWithAdUnitID:adUnitIDStr];
}

void TradplusEntryRewardVideoAdScenario(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPURewardVideoManager sharedInstance] entryAdScenarioWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

void TradplusSetCustomAdInfoRewardVideo(const char* adUnitId,const char* customAdInfo)
{
    NSString *jsonString = stringFromUTF8String(customAdInfo);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPURewardVideoManager sharedInstance] setCustomAdInfo:dic adUnitID:adUnitIDStr];
}

void TradplusRewardVideoSetCallbacks(TPRewardVideoLoadedCallback adLoadedCallback,
                                     TPRewardVideoLoadFailedCallback adLoadFailedCallback,
                                     TPRewardVideoImpressionCallback adImpressionCallback,
                                     TPRewardVideoShowFailedCallback adShowFailedCallback,
                                     TPRewardVideoClickedCallback adClickedCallback,
                                     TPRewardVideoClosedCallback adClosedCallback,
                                     TPRewardVideoRewardCallback adRewardCallback,
                                     TPRewardVideoStartLoadCallback adStartLoadCallback,
                                     TPRewardVideoBiddingStartCallback adBiddingStartCallback,
                                     TPRewardVideoBiddingEndCallback adBiddingEndCallback,
                                     TPRewardVideoOneLayerStartLoadCallback adOneLayerStartLoadCallback,
                                     TPRewardVideoOneLayerLoadedCallback adOneLayerLoadedCallback,
                                     TPRewardVideoOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
                                     TPRewardVideoPlayStartCallback adVideoPlayStartCallback,
                                     TPRewardVideoPlayEndCallback adVideoPlayEndCallback,
                                     TPRewardVideoAllLoadedCallback adAllLoadedCallback,
                                     TPRewardVideoPlayAgainImpressionCallback adPlayAgainImpressionCallback,
                                     TPRewardVideoPlayAgainRewardCallback adPlayAgainRewardCallback,
                                     TPRewardVideoPlayAgainClickedCallback adPlayAgainClickedCallback,
                                     TPRewardVideoPlayAgainVideoPlayStartCallback adPlayAgainVideoPlayStartCallback,
                                     TPRewardVideoPlayAgainVideoPlayEndCallback adPlayAgainVideoPlayEndCallback,
                                     TPRewardVideoAdIsLoadingCallback adIsLoadingCallback
                                 )
{
    [TPURewardVideoManager sharedInstance].loadedCallback = adLoadedCallback;
    [TPURewardVideoManager sharedInstance].loadFailedCallback = adLoadFailedCallback;
    [TPURewardVideoManager sharedInstance].impressionCallback = adImpressionCallback;
    [TPURewardVideoManager sharedInstance].showFailedCallback = adShowFailedCallback;
    [TPURewardVideoManager sharedInstance].clickedCallback = adClickedCallback;
    [TPURewardVideoManager sharedInstance].rewardCallback = adRewardCallback;
    [TPURewardVideoManager sharedInstance].closedCallback = adClosedCallback;
    [TPURewardVideoManager sharedInstance].startLoadCallback = adStartLoadCallback;
    [TPURewardVideoManager sharedInstance].biddingStartCallback = adBiddingStartCallback;
    [TPURewardVideoManager sharedInstance].biddingEndCallback = adBiddingEndCallback;
    [TPURewardVideoManager sharedInstance].oneLayerStartLoadCallback = adOneLayerStartLoadCallback;
    [TPURewardVideoManager sharedInstance].oneLayerLoadedCallback = adOneLayerLoadedCallback;
    [TPURewardVideoManager sharedInstance].oneLayerLoadFailedCallback = adOneLayerLoadFailedCallback;
    [TPURewardVideoManager sharedInstance].videoPlayStartCallback = adVideoPlayStartCallback;
    [TPURewardVideoManager sharedInstance].videoPlayEndCallback = adVideoPlayEndCallback;
    [TPURewardVideoManager sharedInstance].allLoadedCallback = adAllLoadedCallback;
    
    [TPURewardVideoManager sharedInstance].playAgainImpressionCallback = adPlayAgainImpressionCallback;
    [TPURewardVideoManager sharedInstance].playAgainRewardCallback = adPlayAgainRewardCallback;
    [TPURewardVideoManager sharedInstance].playAgainClickedCallback = adPlayAgainClickedCallback;
    [TPURewardVideoManager sharedInstance].playAgainVideoPlayStartCallback = adPlayAgainVideoPlayStartCallback;
    [TPURewardVideoManager sharedInstance].playAgainVideoPlayEndCallback = adPlayAgainVideoPlayEndCallback;
    [TPURewardVideoManager sharedInstance].adIsLoadingCallback = adIsLoadingCallback;
}

#pragma mark - Offerwall

void TradplusLoadOfferwallAd(const char* adUnitId,const char* customMap,const char* localParams,bool openAutoLoadCallback, float maxWaitTime)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *localParamsString = stringFromUTF8String(localParams);
    NSDictionary *localParamsDic = nil;
    if(localParamsString != nil)
    {
        NSData *data = [localParamsString dataUsingEncoding:NSUTF8StringEncoding];
        localParamsDic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUOfferwallManager sharedInstance] loadWithAdUnitID:adUnitIDStr customMap:dic localParams:localParamsDic openAutoLoadCallback:openAutoLoadCallback maxWaitTime:maxWaitTime];
}

void TradplusShowOfferwallAd(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUOfferwallManager sharedInstance] showWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

bool TradplusOfferwallAdReady(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    return [[TPUOfferwallManager sharedInstance] adReadyWithAdUnitID:adUnitIDStr];
}

void TradplusEntryOfferwallAdScenario(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUOfferwallManager sharedInstance] entryAdScenarioWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

void TradplusSetUserId(const char* adUnitId,const char* userId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *userIdStr = stringFromUTF8String(userId);
    [[TPUOfferwallManager sharedInstance] setUserIdWithAdUnitID:adUnitIDStr userId:userIdStr];
}

void TradplusGetCurrencyBalance(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUOfferwallManager sharedInstance] getCurrencyBalanceWithAdUnitID:adUnitIDStr];
}

void TradplusSpendBalance(const char* adUnitId, int count)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUOfferwallManager sharedInstance] spendBalanceWithAdUnitID:adUnitIDStr count:count];
}

void TradplusAwardBalance(const char* adUnitId, int count)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUOfferwallManager sharedInstance] awardBalanceWithAdUnitID:adUnitIDStr count:count];
}

void TradplusSetCustomAdInfoOfferwall(const char* adUnitId,const char* customAdInfo)
{
    NSString *jsonString = stringFromUTF8String(customAdInfo);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUOfferwallManager sharedInstance] setCustomAdInfo:dic adUnitID:adUnitIDStr];
}

void TradplusOfferwallSetCallbacks(TPOfferwallLoadedCallback adLoadedCallback,
                                   TPOfferwallLoadFailedCallback adLoadFailedCallback,
                                   TPOfferwallImpressionCallback adImpressionCallback,
                                   TPOfferwallShowFailedCallback adShowFailedCallback,
                                   TPOfferwallClickedCallback adClickedCallback,
                                   TPOfferwallClosedCallback adClosedCallback,
                                   TPOfferwallStartLoadCallback adStartLoadCallback,
                                   TPOfferwallOneLayerStartLoadCallback adOneLayerStartLoadCallback,
                                   TPOfferwallOneLayerLoadedCallback adOneLayerLoadedCallback,
                                   TPOfferwallOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
                                   TPOfferwallAllLoadedCallback adAllLoadedCallback,
                                   TPOfferwallSetUserIdFinishCallback adSetUserIdFinishCallback,
                                   TPOfferwallCurrencyBalanceSuccessCallback adCurrencyBalanceSuccessCallback,
                                   TPOfferwallCurrencyBalanceFailedCallback adCurrencyBalanceFailedCallback,
                                   TPOfferwallSpendCurrencySuccessCallback adSpendCurrencySuccessCallback,
                                   TPOfferwallSpendCurrencyFailedCallback adSpendCurrencyFailedCallback,
                                   TPOfferwallAwardCurrencySuccesCallback adAwardCurrencySuccesCallback,
                                   TPOfferwallAwardCurrencyFailedCallback adAwardCurrencyFailedCallback,
                                   TPOfferwallAdIsLoadingCallback adIsLoadingCallback)
{
    [TPUOfferwallManager sharedInstance].loadedCallback = adLoadedCallback;
    [TPUOfferwallManager sharedInstance].loadFailedCallback = adLoadFailedCallback;
    [TPUOfferwallManager sharedInstance].impressionCallback = adImpressionCallback;
    [TPUOfferwallManager sharedInstance].showFailedCallback = adShowFailedCallback;
    [TPUOfferwallManager sharedInstance].clickedCallback = adClickedCallback;
    [TPUOfferwallManager sharedInstance].closedCallback = adClosedCallback;
    [TPUOfferwallManager sharedInstance].startLoadCallback = adStartLoadCallback;
    [TPUOfferwallManager sharedInstance].oneLayerStartLoadCallback = adOneLayerStartLoadCallback;
    [TPUOfferwallManager sharedInstance].oneLayerLoadedCallback = adOneLayerLoadedCallback;
    [TPUOfferwallManager sharedInstance].oneLayerLoadFailedCallback = adOneLayerLoadFailedCallback;
    [TPUOfferwallManager sharedInstance].allLoadedCallback = adAllLoadedCallback;
    [TPUOfferwallManager sharedInstance].setUserIdFinishCallback = adSetUserIdFinishCallback;
    [TPUOfferwallManager sharedInstance].currencyBalanceSuccessCallback = adCurrencyBalanceSuccessCallback;
    [TPUOfferwallManager sharedInstance].currencyBalanceFailedCallback = adCurrencyBalanceFailedCallback;
    [TPUOfferwallManager sharedInstance].spendCurrencySuccessCallback = adSpendCurrencySuccessCallback;
    [TPUOfferwallManager sharedInstance].spendCurrencyFailedCallback = adSpendCurrencyFailedCallback;
    [TPUOfferwallManager sharedInstance].awardCurrencySuccesCallback = adAwardCurrencySuccesCallback;
    [TPUOfferwallManager sharedInstance].awardCurrencyFailedCallback = adAwardCurrencyFailedCallback;
    [TPUOfferwallManager sharedInstance].adIsLoadingCallback = adIsLoadingCallback;
}

#pragma mark - Banner

void TradplusLoadBannerAd(const char* adUnitId, bool closeAutoShow, float x, float y, float width, float height,int adPosition,int contentMode, const char* sceneId, const char* customMap,const char* className,const char* localParams,bool openAutoLoadCallback, float maxWaitTime,const char* backgroundColor)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *localParamsString = stringFromUTF8String(localParams);
    NSDictionary *localParamsDic = nil;
    if(localParamsString != nil)
    {
        NSData *data = [localParamsString dataUsingEncoding:NSUTF8StringEncoding];
        localParamsDic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIdStr = stringFromUTF8String(sceneId);
    NSString *classNameStr = stringFromUTF8String(className);
    NSString *backgroundColorStr = stringFromUTF8String(backgroundColor);
    [[TPUBannerManager sharedInstance] loadWithAdUnitID:adUnitIDStr closeAutoShow:closeAutoShow x:x y:y width:width height:height adPosition:adPosition contentMode:contentMode sceneId:sceneIdStr customMap:dic className:classNameStr localParams:localParamsDic openAutoLoadCallback:openAutoLoadCallback maxWaitTime:maxWaitTime backgroundColor:backgroundColorStr];
}

void TradplusShowBannerAd(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUBannerManager sharedInstance] showWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

bool TradplusBannerAdReady(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    return [[TPUBannerManager sharedInstance] adReadyWithAdUnitID:adUnitIDStr];
}

void TradplusEntryBannerAdScenario(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUBannerManager sharedInstance] entryAdScenarioWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

void TradplusHideBanner(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUBannerManager sharedInstance] hideWithAdUnitID:adUnitIDStr];
}

void TradplusDisplayBanner(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUBannerManager sharedInstance] displayWithAdUnitID:adUnitIDStr];
}

void TradplusDestroyBanner(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUBannerManager sharedInstance] destroyWithAdUnitID:adUnitIDStr];
}

void TradplusSetCustomAdInfoBanner(const char* adUnitId,const char* customAdInfo)
{
    NSString *jsonString = stringFromUTF8String(customAdInfo);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUBannerManager sharedInstance] setCustomAdInfo:dic adUnitID:adUnitIDStr];
}

void TradplusBannerSetCallbacks(TPBannerLoadedCallback adLoadedCallback,
                              TPBannerLoadFailedCallback adLoadFailedCallback,
                              TPBannerImpressionCallback adImpressionCallback,
                              TPBannerShowFailedCallback adShowFailedCallback,
                              TPBannerClickedCallback adClickedCallback,
                              TPBannerClosedCallback adClosedCallback,
                              TPBannerStartLoadCallback adStartLoadCallback,
                              TPBannerBiddingStartCallback adBiddingStartCallback,
                              TPBannerBiddingEndCallback adBiddingEndCallback,
                              TPBannerOneLayerStartLoadCallback adOneLayerStartLoadCallback,
                              TPBannerOneLayerLoadedCallback adOneLayerLoadedCallback,
                              TPBannerOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
                              TPBannerAllLoadedCallback adAllLoadedCallback,
                                TPBannerAdIsLoadingCallback adIsLoadingCallback)
{
    [TPUBannerManager sharedInstance].loadedCallback = adLoadedCallback;
    [TPUBannerManager sharedInstance].loadFailedCallback = adLoadFailedCallback;
    [TPUBannerManager sharedInstance].impressionCallback = adImpressionCallback;
    [TPUBannerManager sharedInstance].showFailedCallback = adShowFailedCallback;
    [TPUBannerManager sharedInstance].clickedCallback = adClickedCallback;
    [TPUBannerManager sharedInstance].closedCallback = adClosedCallback;
    [TPUBannerManager sharedInstance].startLoadCallback = adStartLoadCallback;
    [TPUBannerManager sharedInstance].biddingStartCallback = adBiddingStartCallback;
    [TPUBannerManager sharedInstance].biddingEndCallback = adBiddingEndCallback;
    [TPUBannerManager sharedInstance].oneLayerStartLoadCallback = adOneLayerStartLoadCallback;
    [TPUBannerManager sharedInstance].oneLayerLoadedCallback = adOneLayerLoadedCallback;
    [TPUBannerManager sharedInstance].oneLayerLoadFailedCallback = adOneLayerLoadFailedCallback;
    [TPUBannerManager sharedInstance].allLoadedCallback = adAllLoadedCallback;
    [TPUBannerManager sharedInstance].adIsLoadingCallback = adIsLoadingCallback;
}

#pragma mark - Native

void TradplusLoadNativeAd(const char* adUnitId, float x, float y, float width, float height,int adPosition, const char* customMap,const char* localParams,bool openAutoLoadCallback, float maxWaitTime)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *localParamsString = stringFromUTF8String(localParams);
    NSDictionary *localParamsDic = nil;
    if(localParamsString != nil)
    {
        NSData *data = [localParamsString dataUsingEncoding:NSUTF8StringEncoding];
        localParamsDic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeManager sharedInstance] loadWithAdUnitID:adUnitIDStr x:x y:y width:width height:height adPosition:adPosition customMap:dic localParams:localParamsDic openAutoLoadCallback:openAutoLoadCallback maxWaitTime:maxWaitTime];
}

void TradplusShowNativeAd(const char* adUnitId,const char* sceneId,const char* className)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    NSString *classNameStr = stringFromUTF8String(className);
    [[TPUNativeManager sharedInstance] showWithAdUnitID:adUnitIDStr sceneId:sceneIDStr className:classNameStr];
}

bool TradplusNativeAdReady(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    return [[TPUNativeManager sharedInstance] adReadyWithAdUnitID:adUnitIDStr];
}

void TradplusEntryNativeAdScenario(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUNativeManager sharedInstance] entryAdScenarioWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

void TradplusHideNative(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeManager sharedInstance] hideWithAdUnitID:adUnitIDStr];
}

void TradplusDisplayNative(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeManager sharedInstance] displayWithAdUnitID:adUnitIDStr];
}

void TradplusDestroyNative(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeManager sharedInstance] destroyWithAdUnitID:adUnitIDStr];
}

void TradplusSetCustomAdInfoNative(const char* adUnitId,const char* customAdInfo)
{
    NSString *jsonString = stringFromUTF8String(customAdInfo);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeManager sharedInstance] setCustomAdInfo:dic adUnitID:adUnitIDStr];
}

void TradplusNativeSetCallbacks(TPNativeLoadedCallback adLoadedCallback,
                              TPNativeLoadFailedCallback adLoadFailedCallback,
                              TPNativeImpressionCallback adImpressionCallback,
                              TPNativeShowFailedCallback adShowFailedCallback,
                              TPNativeClickedCallback adClickedCallback,
                              TPNativeClosedCallback adClosedCallback,
                              TPNativeStartLoadCallback adStartLoadCallback,
                              TPNativeBiddingStartCallback adBiddingStartCallback,
                              TPNativeBiddingEndCallback adBiddingEndCallback,
                              TPNativeOneLayerStartLoadCallback adOneLayerStartLoadCallback,
                              TPNativeOneLayerLoadedCallback adOneLayerLoadedCallback,
                              TPNativeOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
                              TPNativeVideoPlayStartCallback adVideoPlayStartCallback,
                              TPNativeVideoPlayEndCallback adVideoPlayEndCallback,
                              TPNativeAllLoadedCallback adAllLoadedCallback,
                              TPNativeAdIsLoadingCallback adIsLoadingCallback)
{
    [TPUNativeManager sharedInstance].loadedCallback = adLoadedCallback;
    [TPUNativeManager sharedInstance].loadFailedCallback = adLoadFailedCallback;
    [TPUNativeManager sharedInstance].impressionCallback = adImpressionCallback;
    [TPUNativeManager sharedInstance].showFailedCallback = adShowFailedCallback;
    [TPUNativeManager sharedInstance].clickedCallback = adClickedCallback;
    [TPUNativeManager sharedInstance].closedCallback = adClosedCallback;
    [TPUNativeManager sharedInstance].startLoadCallback = adStartLoadCallback;
    [TPUNativeManager sharedInstance].biddingStartCallback = adBiddingStartCallback;
    [TPUNativeManager sharedInstance].biddingEndCallback = adBiddingEndCallback;
    [TPUNativeManager sharedInstance].oneLayerStartLoadCallback = adOneLayerStartLoadCallback;
    [TPUNativeManager sharedInstance].oneLayerLoadedCallback = adOneLayerLoadedCallback;
    [TPUNativeManager sharedInstance].oneLayerLoadFailedCallback = adOneLayerLoadFailedCallback;
    [TPUNativeManager sharedInstance].videoPlayStartCallback = adVideoPlayStartCallback;
    [TPUNativeManager sharedInstance].videoPlayEndCallback = adVideoPlayEndCallback;
    [TPUNativeManager sharedInstance].allLoadedCallback = adAllLoadedCallback;
    [TPUNativeManager sharedInstance].adIsLoadingCallback = adIsLoadingCallback;
}

#pragma mark - NativeBanner

void TradplusLoadNativeBannerAd(const char* adUnitId, bool closeAutoShow, float x, float y, float width, float height,int adPosition, const char* sceneId, const char* customMap,const char* className,const char* localParams,bool openAutoLoadCallback, float maxWaitTime,const char* backgroundColor)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *localParamsString = stringFromUTF8String(localParams);
    NSDictionary *localParamsDic = nil;
    if(localParamsString != nil)
    {
        NSData *data = [localParamsString dataUsingEncoding:NSUTF8StringEncoding];
        localParamsDic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIdStr = stringFromUTF8String(sceneId);
    NSString *classNameStr = stringFromUTF8String(className);
    NSString *backgroundColorStr = stringFromUTF8String(backgroundColor);
    [[TPUNativeBannerManager sharedInstance] loadWithAdUnitID:adUnitIDStr closeAutoShow:closeAutoShow x:x y:y width:width height:height adPosition:adPosition sceneId:sceneIdStr customMap:dic className:classNameStr localParams:localParamsDic openAutoLoadCallback:openAutoLoadCallback maxWaitTime:maxWaitTime backgroundColor:backgroundColorStr];
}

void TradplusShowNativeBannerAd(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUNativeBannerManager sharedInstance] showWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

bool TradplusNativeBannerAdReady(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    return [[TPUNativeBannerManager sharedInstance] adReadyWithAdUnitID:adUnitIDStr];
}

void TradplusEntryNativeBannerAdScenario(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUNativeBannerManager sharedInstance] entryAdScenarioWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

void TradplusHideNativeBanner(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeBannerManager sharedInstance] hideWithAdUnitID:adUnitIDStr];
}

void TradplusDisplayNativeBanner(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeBannerManager sharedInstance] displayWithAdUnitID:adUnitIDStr];
}

void TradplusDestroyNativeBanner(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeBannerManager sharedInstance] destroyWithAdUnitID:adUnitIDStr];
}

void TradplusSetCustomAdInfoNativeBanner(const char* adUnitId,const char* customAdInfo)
{
    NSString *jsonString = stringFromUTF8String(customAdInfo);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUNativeBannerManager sharedInstance] setCustomAdInfo:dic adUnitID:adUnitIDStr];
}

void TradplusNativeBannerSetCallbacks(TPNativeBannerLoadedCallback adLoadedCallback,
                              TPNativeBannerLoadFailedCallback adLoadFailedCallback,
                              TPNativeBannerImpressionCallback adImpressionCallback,
                              TPNativeBannerShowFailedCallback adShowFailedCallback,
                              TPNativeBannerClickedCallback adClickedCallback,
                              TPNativeBannerClosedCallback adClosedCallback,
                              TPNativeBannerStartLoadCallback adStartLoadCallback,
                              TPNativeBannerBiddingStartCallback adBiddingStartCallback,
                              TPNativeBannerBiddingEndCallback adBiddingEndCallback,
                              TPNativeBannerOneLayerStartLoadCallback adOneLayerStartLoadCallback,
                              TPNativeBannerOneLayerLoadedCallback adOneLayerLoadedCallback,
                              TPNativeBannerOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
                              TPNativeBannerAllLoadedCallback adAllLoadedCallback,
                              TPNativeBannerAdIsLoadingCallback adIsLoadingCallback)
{
    [TPUNativeBannerManager sharedInstance].loadedCallback = adLoadedCallback;
    [TPUNativeBannerManager sharedInstance].loadFailedCallback = adLoadFailedCallback;
    [TPUNativeBannerManager sharedInstance].impressionCallback = adImpressionCallback;
    [TPUNativeBannerManager sharedInstance].showFailedCallback = adShowFailedCallback;
    [TPUNativeBannerManager sharedInstance].clickedCallback = adClickedCallback;
    [TPUNativeBannerManager sharedInstance].closedCallback = adClosedCallback;
    [TPUNativeBannerManager sharedInstance].startLoadCallback = adStartLoadCallback;
    [TPUNativeBannerManager sharedInstance].biddingStartCallback = adBiddingStartCallback;
    [TPUNativeBannerManager sharedInstance].biddingEndCallback = adBiddingEndCallback;
    [TPUNativeBannerManager sharedInstance].oneLayerStartLoadCallback = adOneLayerStartLoadCallback;
    [TPUNativeBannerManager sharedInstance].oneLayerLoadedCallback = adOneLayerLoadedCallback;
    [TPUNativeBannerManager sharedInstance].oneLayerLoadFailedCallback = adOneLayerLoadFailedCallback;
    [TPUNativeBannerManager sharedInstance].allLoadedCallback = adAllLoadedCallback;
    [TPUNativeBannerManager sharedInstance].adIsLoadingCallback = adIsLoadingCallback;
}

#pragma mark - Splash


void TradplusLoadSplashAd(const char* adUnitId,const char* customMap,const char* localParams,bool openAutoLoadCallback, float maxWaitTime)
{
    NSString *jsonString = stringFromUTF8String(customMap);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *localParamsString = stringFromUTF8String(localParams);
    NSDictionary *localParamsDic = nil;
    if(localParamsString != nil)
    {
        NSData *data = [localParamsString dataUsingEncoding:NSUTF8StringEncoding];
        localParamsDic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUSplashManager sharedInstance] loadWithAdUnitID:adUnitIDStr customMap:dic localParams:localParamsDic openAutoLoadCallback:openAutoLoadCallback maxWaitTime:maxWaitTime];
}

void TradplusShowSplashAd(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUSplashManager sharedInstance] showWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

bool TradplusSplashAdReady(const char* adUnitId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    return [[TPUSplashManager sharedInstance] adReadyWithAdUnitID:adUnitIDStr];
}

void TradplusEntrySplashAdScenario(const char* adUnitId,const char* sceneId)
{
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    NSString *sceneIDStr = stringFromUTF8String(sceneId);
    [[TPUSplashManager sharedInstance] entryAdScenarioWithAdUnitID:adUnitIDStr sceneId:sceneIDStr];
}

void TradplusSetCustomAdInfoSplash(const char* adUnitId,const char* customAdInfo)
{
    NSString *jsonString = stringFromUTF8String(customAdInfo);
    NSDictionary *dic = nil;
    if(jsonString != nil)
    {
        NSData *data = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        dic = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    }
    NSString *adUnitIDStr = stringFromUTF8String(adUnitId);
    [[TPUSplashManager sharedInstance] setCustomAdInfo:dic adUnitID:adUnitIDStr];
}

void TradplusSplashSetCallbacks(TPSplashLoadedCallback adLoadedCallback,
                                TPSplashLoadFailedCallback adLoadFailedCallback,
                                TPSplashImpressionCallback adImpressionCallback,
                                TPSplashShowFailedCallback adShowFailedCallback,
                                TPSplashClickedCallback adClickedCallback,
                                TPSplashClosedCallback adClosedCallback,
                                TPSplashStartLoadCallback adStartLoadCallback,
                                TPSplashBiddingStartCallback adBiddingStartCallback,
                                TPSplashBiddingEndCallback adBiddingEndCallback,
                                TPSplashOneLayerStartLoadCallback adOneLayerStartLoadCallback,
                                TPSplashOneLayerLoadedCallback adOneLayerLoadedCallback,
                                TPSplashOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
                                TPSplashAllLoadedCallback adAllLoadedCallback,
                                TPSplashAdIsLoadingCallback adIsLoadingCallback,
                                TPSplashZoomOutStartCallback adZoomOutStartCallback,
                                TPSplashZoomOutEndCallback adZoomOutEndCallback,
                                TPSplashSkipCallback adSkipCallback)
{
    [TPUSplashManager sharedInstance].loadedCallback = adLoadedCallback;
    [TPUSplashManager sharedInstance].loadFailedCallback = adLoadFailedCallback;
    [TPUSplashManager sharedInstance].impressionCallback = adImpressionCallback;
    [TPUSplashManager sharedInstance].showFailedCallback = adShowFailedCallback;
    [TPUSplashManager sharedInstance].clickedCallback = adClickedCallback;
    [TPUSplashManager sharedInstance].closedCallback = adClosedCallback;
    [TPUSplashManager sharedInstance].startLoadCallback = adStartLoadCallback;
    [TPUSplashManager sharedInstance].biddingStartCallback = adBiddingStartCallback;
    [TPUSplashManager sharedInstance].biddingEndCallback = adBiddingEndCallback;
    [TPUSplashManager sharedInstance].oneLayerStartLoadCallback = adOneLayerStartLoadCallback;
    [TPUSplashManager sharedInstance].oneLayerLoadedCallback = adOneLayerLoadedCallback;
    [TPUSplashManager sharedInstance].oneLayerLoadFailedCallback = adOneLayerLoadFailedCallback;
    [TPUSplashManager sharedInstance].allLoadedCallback = adAllLoadedCallback;
    [TPUSplashManager sharedInstance].adIsLoadingCallback = adIsLoadingCallback;
    [TPUSplashManager sharedInstance].zoomOutStartCallback = adZoomOutStartCallback;
    [TPUSplashManager sharedInstance].zoomOutEndCallback = adZoomOutEndCallback;
    [TPUSplashManager sharedInstance].skipCallback = adSkipCallback;
}
