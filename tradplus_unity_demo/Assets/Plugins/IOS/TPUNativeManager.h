//
//  TPUNativeManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPNativeLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeLoadFailedCallback)(const char* adUnitId,const char* error);
typedef void (*TPNativeImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeShowFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPNativeClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeClosedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBiddingStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeBiddingEndCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPNativeOneLayerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeOneLayerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeOneLayerLoadFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPNativeVideoPlayStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeVideoPlayEndCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPNativeAllLoadedCallback)(const char* adUnitId,bool isSuccess);
typedef void (*TPNativeAdIsLoadingCallback)(const char* adUnitId);

@interface TPUNativeManager : NSObject

+ (TPUNativeManager *)sharedInstance;

- (void)loadWithAdUnitID:(NSString *)adUnitID x:(float)x y:(float)y width:(float)width height:(float)height adPosition:(int)adPosition customMap:(NSDictionary *)customMap;
- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId className:(NSString *)className;
- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID;
- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (void)hideWithAdUnitID:(NSString *)adUnitID;
- (void)displayWithAdUnitID:(NSString *)adUnitID;
- (void)destroyWithAdUnitID:(NSString *)adUnitID;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID;

@property(nonatomic, assign) TPNativeLoadedCallback loadedCallback;
@property(nonatomic, assign) TPNativeLoadFailedCallback loadFailedCallback;
@property(nonatomic, assign) TPNativeImpressionCallback impressionCallback;
@property(nonatomic, assign) TPNativeShowFailedCallback showFailedCallback;
@property(nonatomic, assign) TPNativeClickedCallback clickedCallback;
@property(nonatomic, assign) TPNativeClosedCallback closedCallback;
@property(nonatomic, assign) TPNativeStartLoadCallback startLoadCallback;
@property(nonatomic, assign) TPNativeBiddingStartCallback biddingStartCallback;
@property(nonatomic, assign) TPNativeBiddingEndCallback biddingEndCallback;
@property(nonatomic, assign) TPNativeOneLayerStartLoadCallback oneLayerStartLoadCallback;
@property(nonatomic, assign) TPNativeOneLayerLoadedCallback oneLayerLoadedCallback;
@property(nonatomic, assign) TPNativeOneLayerLoadFailedCallback oneLayerLoadFailedCallback;
@property(nonatomic, assign) TPNativeVideoPlayStartCallback videoPlayStartCallback;
@property(nonatomic, assign) TPNativeVideoPlayEndCallback videoPlayEndCallback;
@property(nonatomic, assign) TPNativeAllLoadedCallback allLoadedCallback;
@property(nonatomic,assign) TPNativeAdIsLoadingCallback adIsLoadingCallback;

@end

NS_ASSUME_NONNULL_END
