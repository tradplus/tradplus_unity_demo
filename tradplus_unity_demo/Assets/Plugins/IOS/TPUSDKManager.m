//
//  TPUSDKManager.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/5.
//

#import "TPUSDKManager.h"
#import <TradPlusAds/TradPlusAds.h>
#import "TPUPluginUtil.h"

@interface TPUSDKManager() <TradPlusAdImpressionDelegate>

@end

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

- (void)setSettingDataParam:(NSDictionary *)settingMap
{
    if(settingMap == nil || ![settingMap isKindOfClass:[NSDictionary class]])
    {
        return;
    }
    //交叉推广超时
    if([settingMap valueForKey:@"http_timeout_crosspromotion"])
    {
        NSInteger http_timeout_crosspromotion = [settingMap[@"http_timeout_crosspromotion"] integerValue];
        if(http_timeout_crosspromotion > 0)
        {
            http_timeout_crosspromotion = http_timeout_crosspromotion/1000;
            if(http_timeout_crosspromotion == 0)
                http_timeout_crosspromotion = 1;
            gTPHttpTimeoutCross = http_timeout_crosspromotion;
        }
    }
    //adx超时
    if([settingMap valueForKey:@"http_timeout_adx"])
    {
        NSInteger http_timeout_adx = [settingMap[@"http_timeout_adx"] integerValue];
        if(http_timeout_adx > 0)
        {
            http_timeout_adx = http_timeout_adx/1000;
            if(http_timeout_adx == 0)
                http_timeout_adx = 1;
            gTPHttpTimeoutAdx = http_timeout_adx;
        }
    }
    //配置超时
    if([settingMap valueForKey:@"http_timeout_conf"])
    {
        NSInteger http_timeout_conf = [settingMap[@"http_timeout_conf"] integerValue];
        if(http_timeout_conf > 0)
        {
            http_timeout_conf = http_timeout_conf/1000;
            if(http_timeout_conf == 0)
                http_timeout_conf = 1;
            gTPHttpTimeoutConf = http_timeout_conf;
        }
    }
    //其他网络超时
    if([settingMap valueForKey:@"http_timeout_event"])
    {
        NSInteger http_timeout_event = [settingMap[@"http_timeout_event"] integerValue];
        if(http_timeout_event > 0)
        {
            http_timeout_event = http_timeout_event/1000;
            if(http_timeout_event == 0)
                http_timeout_event = 1;
            gTPHttpTimeoutEvent = http_timeout_event;
        }
    }
    NSMutableDictionary *dic = [[NSMutableDictionary alloc] init];
    if([settingMap valueForKey:@"autoload_close"])
    {
        id autoload_close = settingMap[@"autoload_close"];
        if([autoload_close isKindOfClass:[NSArray class]])
        {
            dic[@"autoload_close"] = autoload_close;
        }
    }
    [[TradPlus sharedInstance] setSettingDataParam:dic];
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

- (void)setLGPDConsent:(BOOL)consent
{
    [TradPlus setLGPDIsConsentEnabled:consent];
}

- (int)getLGPDConsent
{
    int callbackState = 2;//未设置
    if([[NSUserDefaults standardUserDefaults] objectForKey:gTPLGPDStorageKey])
    {
        NSInteger ldpdState = [[NSUserDefaults standardUserDefaults] integerForKey:gTPLGPDStorageKey];
        if(ldpdState == 2)
        {
            callbackState = 0;//允许
        }
        else if(ldpdState == 1)
        {
            callbackState = 1;//不允许
        }
    }
    return callbackState;
}

- (void)setLGPDConsent:(BOOL)consent
{
    [TradPlus setLGPDIsConsentEnabled:consent];
}

- (int)getLGPDConsent
{
    int callbackState = 2;//未设置
    if([[NSUserDefaults standardUserDefaults] objectForKey:gTPLGPDStorageKey])
    {
        NSInteger ldpdState = [[NSUserDefaults standardUserDefaults] integerForKey:gTPLGPDStorageKey];
        if(ldpdState == 2)
        {
            callbackState = 0;//允许
        }
        else if(ldpdState == 1)
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


- (void)setOnAdImpressionCallback:(TPOnAdImpressionCallback)onAdImpressionCallback
{
    [TradPlus sharedInstance].impressionDelegate = self;
    _onAdImpressionCallback = onAdImpressionCallback;
}

#pragma mark - TradPlusAdImpressionDelegate
- (void)tradPlusAdImpression:(NSDictionary *)adInfo
{
    if([TPUSDKManager sharedInstance].onAdImpressionCallback != nil)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUSDKManager sharedInstance].onAdImpressionCallback(jsonString.UTF8String);
    }
}
@end
