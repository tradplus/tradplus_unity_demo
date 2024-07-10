//
//  TPUOfferwallManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPOfferwallLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPOfferwallLoadFailedCallback)(const char* adUnitId,const char* error);
typedef void (*TPOfferwallImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPOfferwallShowFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPOfferwallClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPOfferwallClosedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPOfferwallStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPOfferwallOneLayerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPOfferwallOneLayerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPOfferwallOneLayerLoadFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPOfferwallAllLoadedCallback)(const char* adUnitId,bool isSuccess);
typedef void (*TPOfferwallSetUserIdFinishCallback)(const char* adUnitId,bool isSuccess);
typedef void (*TPOfferwallCurrencyBalanceSuccessCallback)(const char* adUnitId,int amount,const char* msg);
typedef void (*TPOfferwallCurrencyBalanceFailedCallback)(const char* adUnitId,const char* msg);
typedef void (*TPOfferwallSpendCurrencySuccessCallback)(const char* adUnitId,int amount,const char* msg);
typedef void (*TPOfferwallSpendCurrencyFailedCallback)(const char* adUnitId,const char* msg);
typedef void (*TPOfferwallAwardCurrencySuccesCallback)(const char* adUnitId,int amount,const char* msg);
typedef void (*TPOfferwallAwardCurrencyFailedCallback)(const char* adUnitId,const char* msg);
typedef void (*TPOfferwallAdIsLoadingCallback)(const char* adUnitId);

@interface TPUOfferwallManager : NSObject

+ (TPUOfferwallManager *)sharedInstance;

- (void)loadWithAdUnitID:(NSString *)adUnitID customMap:(NSDictionary *)customMap localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime;
- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID;
- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (void)setUserIdWithAdUnitID:(NSString *)adUnitID userId:(NSString *)userId;
- (void)getCurrencyBalanceWithAdUnitID:(NSString *)adUnitID;
- (void)spendBalanceWithAdUnitID:(NSString *)adUnitID count:(int)count;
- (void)awardBalanceWithAdUnitID:(NSString *)adUnitID count:(int)count;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID;

@property(nonatomic, assign) TPOfferwallLoadedCallback loadedCallback;
@property(nonatomic, assign) TPOfferwallLoadFailedCallback loadFailedCallback;
@property(nonatomic, assign) TPOfferwallImpressionCallback impressionCallback;
@property(nonatomic, assign) TPOfferwallShowFailedCallback showFailedCallback;
@property(nonatomic, assign) TPOfferwallClickedCallback clickedCallback;
@property(nonatomic, assign) TPOfferwallClosedCallback closedCallback;
@property(nonatomic, assign) TPOfferwallStartLoadCallback startLoadCallback;
@property(nonatomic, assign) TPOfferwallOneLayerStartLoadCallback oneLayerStartLoadCallback;
@property(nonatomic, assign) TPOfferwallOneLayerLoadedCallback oneLayerLoadedCallback;
@property(nonatomic, assign) TPOfferwallOneLayerLoadFailedCallback oneLayerLoadFailedCallback;
@property(nonatomic, assign) TPOfferwallAllLoadedCallback allLoadedCallback;
@property(nonatomic, assign) TPOfferwallSetUserIdFinishCallback setUserIdFinishCallback;
@property(nonatomic, assign) TPOfferwallCurrencyBalanceSuccessCallback currencyBalanceSuccessCallback;
@property(nonatomic, assign) TPOfferwallCurrencyBalanceFailedCallback currencyBalanceFailedCallback;
@property(nonatomic, assign) TPOfferwallSpendCurrencySuccessCallback spendCurrencySuccessCallback;
@property(nonatomic, assign) TPOfferwallSpendCurrencyFailedCallback spendCurrencyFailedCallback;
@property(nonatomic, assign) TPOfferwallAwardCurrencySuccesCallback awardCurrencySuccesCallback;
@property(nonatomic, assign) TPOfferwallAwardCurrencyFailedCallback awardCurrencyFailedCallback;
@property(nonatomic, assign) TPOfferwallAdIsLoadingCallback adIsLoadingCallback;
@end

NS_ASSUME_NONNULL_END


