//
//  TPUSDKManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/5.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPOnInitFinishCallback)(bool success);
typedef void (*TPOnDialogClosedCallback)(int level);
typedef void (*TPOnCurrentAreaSuccessCallback)(bool isEu, bool isCn, bool isCa);
typedef void (*TPOnCurrentAreaFailedCallback)(const char* msg);
typedef void (*TPOnAdImpressionCallback)(const char* msg);

@interface TPUSDKManager : NSObject

+ (TPUSDKManager *)sharedInstance;

- (void)checkCurrentArea;
- (void)initSDKWithAppID:(NSString *)appID;
- (void)setCustomMap:(NSDictionary *)customMap;
- (void)setSettingDataParam:(NSDictionary *)settingMap;
- (NSString *)getVersion;
- (BOOL)isEUTraffic;
- (BOOL)isCalifornia;
- (void)setGDPRDataCollection:(BOOL)canDataCollection;
- (int)getGDPRDataCollection;
- (void)setCCPADoNotSell:(BOOL)canDataCollection;
- (int)getCCPADoNotSell;
- (void)setCOPPAIsAgeRestrictedUser:(BOOL)isChild;
- (int)getCOPPAIsAgeRestrictedUser;
- (void)setLGPDConsent:(BOOL)consent;
- (int)getLGPDConsent;
- (void)showGDPRDialog;
- (void)setOpenPersonalizedAd:(BOOL)open;
- (BOOL)isOpenPersonalizedAd;
- (void)clearCache:(NSString *)adUnitId;
- (void)setAutoExpiration:(BOOL)autoCheck;
- (void)checkAutoExpiration;
- (void)setCnServer:(BOOL)onlyCn;

@property(nonatomic, assign) TPOnInitFinishCallback onInitFinishCallback;
@property(nonatomic, assign) TPOnDialogClosedCallback onDialogClosedCallback;
@property(nonatomic, assign) TPOnCurrentAreaSuccessCallback onCurrentAreaSuccessCallback;
@property(nonatomic, assign) TPOnCurrentAreaFailedCallback onCurrentAreaFailedCallback;
@property(nonatomic, assign) TPOnAdImpressionCallback onAdImpressionCallback;
@end

NS_ASSUME_NONNULL_END
