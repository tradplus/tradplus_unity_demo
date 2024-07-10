//
//  TPUNativeBannerManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPNativeBannerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerLoadFailedCallback)(const char* adUnitId,const char* error);
typedef void (*TPNativeBannerImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerShowFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPNativeBannerClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerClosedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerBiddingStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerBiddingEndCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPNativeBannerOneLayerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerOneLayerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBannerOneLayerLoadFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPNativeBannerAllLoadedCallback)(const char* adUnitId,bool isSuccess);
typedef void (*TPNativeBannerAdIsLoadingCallback)(const char* adUnitId);

@interface TPUNativeBannerManager : NSObject

+ (TPUNativeBannerManager *)sharedInstance;

- (void)loadWithAdUnitID:(NSString *)adUnitID closeAutoShow:(BOOL)closeAutoShow x:(float)x y:(float)y width:(float)width height:(float)height adPosition:(int)adPosition sceneId:(NSString *)sceneId customMap:(NSDictionary *)customMap className:(NSString *)className localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime backgroundColor:(NSString *)backgroundColor;
- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID;
- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (void)hideWithAdUnitID:(NSString *)adUnitID;
- (void)displayWithAdUnitID:(NSString *)adUnitID;
- (void)destroyWithAdUnitID:(NSString *)adUnitID;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID;

@property(nonatomic, assign) TPNativeBannerLoadedCallback loadedCallback;
@property(nonatomic, assign) TPNativeBannerLoadFailedCallback loadFailedCallback;
@property(nonatomic, assign) TPNativeBannerImpressionCallback impressionCallback;
@property(nonatomic, assign) TPNativeBannerShowFailedCallback showFailedCallback;
@property(nonatomic, assign) TPNativeBannerClickedCallback clickedCallback;
@property(nonatomic, assign) TPNativeBannerClosedCallback closedCallback;
@property(nonatomic, assign) TPNativeBannerStartLoadCallback startLoadCallback;
@property(nonatomic, assign) TPNativeBannerBiddingStartCallback biddingStartCallback;
@property(nonatomic, assign) TPNativeBannerBiddingEndCallback biddingEndCallback;
@property(nonatomic, assign) TPNativeBannerOneLayerStartLoadCallback oneLayerStartLoadCallback;
@property(nonatomic, assign) TPNativeBannerOneLayerLoadedCallback oneLayerLoadedCallback;
@property(nonatomic, assign) TPNativeBannerOneLayerLoadFailedCallback oneLayerLoadFailedCallback;
@property(nonatomic, assign) TPNativeBannerAllLoadedCallback allLoadedCallback;
@property(nonatomic, assign) TPNativeBannerAdIsLoadingCallback adIsLoadingCallback;
@end

NS_ASSUME_NONNULL_END
