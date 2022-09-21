//
//  TPUSDKManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/5.
//

#import "TPUSDKManager.h"
#import <TradPlusAds/TradPlusAds.h>

@implementation TPUSDKManager

+(TPUSDKManager *)sharedInstance
{
    static TPUSDKManager *manager = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        manager = [[TPUSDKManager alloc] init];
    });
    return manager;
}

- (void)checkCurrentArea
{
    [TradPlus checkCurrentArea:^(BOOL isUnknown, BOOL isCN, BOOL isCA, BOOL isEU) {
        if(isUnknown)
        {
            if([TPUSDKManager sharedInstance].onCurrentAreaFailedCallback != nil)
            {
                [TPUSDKManager sharedInstance].onCurrentAreaFailedCallback(@"Unknown".UTF8String);
            }
        }
        else
        {
            if([TPUSDKManager sharedInstance].onCurrentAreaSuccessCallback != nil)
            {
                [TPUSDKManager sharedInstance].onCurrentAreaSuccessCallback(isEU, isCN, isCA);
            }
        }
    }];
}

- (void)initSDKWithAppID:(NSString *)appID
{
    [TradPlus initSDK:appID completionBlock:^(NSError * _Nonnull error) {
        if([TPUSDKManager sharedInstance].onInitFinishCallback != nil)
        {
            [TPUSDKManager sharedInstance].onInitFinishCallback((error == nil));
        }
    }];
}

- (void)setCustomMap:(NSDictionary *)customMap
{
    [TradPlus sharedInstance].dicCustomValue = customMap;
}

- (NSString *)getVersion
{
    return [TradPlus getVersion];
}

- (BOOL)isEUTraffic
{
    BOOL isEU = ([MSConsentManager sharedManager].isGDPRApplicable == MSBoolYes);
    return isEU;
}

- (BOOL)isCalifornia
{
    return gMsSDKIsCA;
}

- (void)setGDPRDataCollection:(BOOL)canDataCollection
{
    [TradPlus setGDPRDataCollection:canDataCollection];
}

- (int)getGDPRDataCollection
{
    int callbackState = 2;//未设置
    MSConsentStatus state =  [TradPlus getGDPRDataCollection];
    if(state == MSConsentStatusDenied)
    {
        callbackState = 1;//不允许
    }
    else if(state == MSConsentStatusConsented)
    {
        callbackState = 0;//允许
    }
    return callbackState;
}

- (void)setCCPADoNotSell:(BOOL)canDataCollection
{
    [TradPlus setCCPADoNotSell:canDataCollection];
}

- (int)getCCPADoNotSell
{
    int callbackState = 2;//未设置
    if([[NSUserDefaults standardUserDefaults] objectForKey:gTPCCPAStorageKey])
    {
        NSInteger ccpaStatus = [[NSUserDefaults standardUserDefaults] integerForKey:gTPCCPAStorageKey];
        if(ccpaStatus == 2)
        {
            callbackState = 0;//允许
        }
        else if(ccpaStatus == 1)
        {
            callbackState = 1;//不允许
        }
    }
    return callbackState;
}

- (void)setCOPPAIsAgeRestrictedUser:(BOOL)isChild
{
    [TradPlus setCOPPAIsAgeRestrictedUser:isChild];
}

- (int)getCOPPAIsAgeRestrictedUser
{
    int callbackState = 2;//未设置
    if([[NSUserDefaults standardUserDefaults] objectForKey:gTPCOPPAStorageKey])
    {
        NSInteger ccpaStatus = [[NSUserDefaults standardUserDefaults] integerForKey:gTPCOPPAStorageKey];
        if(ccpaStatus == 2)
        {
            callbackState = 0;//允许
        }
        else if(ccpaStatus == 1)
        {
            callbackState = 1;//不允许
        }
    }
    return callbackState;
}

- (void)setOpenPersonalizedAd:(BOOL)open
{
    [TradPlus setOpenPersonalizedAd:open];
}

- (BOOL)isOpenPersonalizedAd
{
    return [[TradPlus sharedInstance] isOpenPersonalizedAd];
}

- (void)showGDPRDialog
{
    [[MSConsentManager sharedManager] showConsentDialogFromViewController:[MsCommon getTopRootViewController] didShow:nil didDismiss:^{
        if([TPUSDKManager sharedInstance].onDialogClosedCallback != nil)
        {
            [TPUSDKManager sharedInstance].onDialogClosedCallback(0);
        }
    }];
}

- (void)clearCache:(NSString *)adUnitId
{
    [TradPlus clearCacheWithPlacementId:adUnitId];
}

- (void)setAutoExpiration:(BOOL)autoCheck
{
    [TradPlus sharedInstance].isExpiredAdChecking = autoCheck;
}

- (void)checkAutoExpiration
{
    [TradPlus expiredAdCheck];
}

- (void)setCnServer:(BOOL)onlyCn
{
    [TradPlus setCnServer:onlyCn];
}
@end
