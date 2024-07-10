//
//  TPUSplashManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPSplashLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashLoadFailedCallback)(const char* adUnitId,const char* error);
typedef void (*TPSplashImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashShowFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPSplashClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashClosedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashBiddingStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashBiddingEndCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPSplashOneLayerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashOneLayerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashOneLayerLoadFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPSplashZoomOutStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashZoomOutEndCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashSkipCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPSplashAllLoadedCallback)(const char* adUnitId,bool isSuccess);
typedef void (*TPSplashAdIsLoadingCallback)(const char* adUnitId);

@interface TPUSplashManager : NSObject

+ (TPUSplashManager *)sharedInstance;

- (void)loadWithAdUnitID:(NSString *)adUnitID customMap:(NSDictionary *)customMap localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime;
- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID;
- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;

@property(nonatomic, assign) TPSplashLoadedCallback loadedCallback;
@property(nonatomic, assign) TPSplashLoadFailedCallback loadFailedCallback;
@property(nonatomic, assign) TPSplashImpressionCallback impressionCallback;
@property(nonatomic, assign) TPSplashShowFailedCallback showFailedCallback;
@property(nonatomic, assign) TPSplashClickedCallback clickedCallback;
@property(nonatomic, assign) TPSplashClosedCallback closedCallback;
@property(nonatomic, assign) TPSplashStartLoadCallback startLoadCallback;
@property(nonatomic, assign) TPSplashBiddingStartCallback biddingStartCallback;
@property(nonatomic, assign) TPSplashBiddingEndCallback biddingEndCallback;
@property(nonatomic, assign) TPSplashOneLayerStartLoadCallback oneLayerStartLoadCallback;
@property(nonatomic, assign) TPSplashOneLayerLoadedCallback oneLayerLoadedCallback;
@property(nonatomic, assign) TPSplashOneLayerLoadFailedCallback oneLayerLoadFailedCallback;
@property(nonatomic, assign) TPSplashZoomOutStartCallback zoomOutStartCallback;
@property(nonatomic, assign) TPSplashZoomOutEndCallback zoomOutEndCallback;
@property(nonatomic, assign) TPSplashSkipCallback skipCallback;
@property(nonatomic, assign) TPSplashAllLoadedCallback allLoadedCallback;
@property (nonatomic,assign) TPSplashAdIsLoadingCallback adIsLoadingCallback;
@end

NS_ASSUME_NONNULL_END
