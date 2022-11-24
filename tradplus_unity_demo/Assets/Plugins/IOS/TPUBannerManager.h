//
//  TPUBannerManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPBannerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerLoadFailedCallback)(const char* adUnitId,const char* error);
typedef void (*TPBannerImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerShowFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPBannerClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerClosedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerBiddingStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerBiddingEndCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPBannerOneLayerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerOneLayerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPBannerOneLayerLoadFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPBannerAllLoadedCallback)(const char* adUnitId,bool isSuccess);

@interface TPUBannerManager : NSObject

+ (TPUBannerManager *)sharedInstance;

- (void)loadWithAdUnitID:(NSString *)adUnitID closeAutoShow:(BOOL)closeAutoShow x:(float)x y:(float)y width:(float)width height:(float)height adPosition:(int)adPosition contentMode:(int)contentMode sceneId:(NSString *)sceneId customMap:(NSDictionary *)customMap className:(NSString *)className;
- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID;
- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (void)hideWithAdUnitID:(NSString *)adUnitID;
- (void)displayWithAdUnitID:(NSString *)adUnitID;
- (void)destroyWithAdUnitID:(NSString *)adUnitID;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID;

@property(nonatomic, assign) TPBannerLoadedCallback loadedCallback;
@property(nonatomic, assign) TPBannerLoadFailedCallback loadFailedCallback;
@property(nonatomic, assign) TPBannerImpressionCallback impressionCallback;
@property(nonatomic, assign) TPBannerShowFailedCallback showFailedCallback;
@property(nonatomic, assign) TPBannerClickedCallback clickedCallback;
@property(nonatomic, assign) TPBannerClosedCallback closedCallback;
@property(nonatomic, assign) TPBannerStartLoadCallback startLoadCallback;
@property(nonatomic, assign) TPBannerBiddingStartCallback biddingStartCallback;
@property(nonatomic, assign) TPBannerBiddingEndCallback biddingEndCallback;
@property(nonatomic, assign) TPBannerOneLayerStartLoadCallback oneLayerStartLoadCallback;
@property(nonatomic, assign) TPBannerOneLayerLoadedCallback oneLayerLoadedCallback;
@property(nonatomic, assign) TPBannerOneLayerLoadFailedCallback oneLayerLoadFailedCallback;
@property(nonatomic, assign) TPBannerAllLoadedCallback allLoadedCallback;

@end

NS_ASSUME_NONNULL_END
