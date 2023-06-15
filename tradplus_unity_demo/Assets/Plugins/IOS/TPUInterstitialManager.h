//
//  TPUInterstitialManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPInterstitialLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialLoadFailedCallback)(const char* adUnitId,const char* error);
typedef void (*TPInterstitialImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialShowFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPInterstitialClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialClosedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialBiddingStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialBiddingEndCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPInterstitialOneLayerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialOneLayerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialOneLayerLoadFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPInterstitialVideoPlayStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialVideoPlayEndCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPInterstitialAllLoadedCallback)(const char* adUnitId,bool isSuccess);
typedef void (*TPInterstitialAdIsLoadingCallback)(const char* adUnitId);

@interface TPUInterstitialManager : NSObject

+ (TPUInterstitialManager *)sharedInstance;

- (void)loadWithAdUnitID:(NSString *)adUnitID customMap:(NSDictionary *)customMap localParams:(NSDictionary *)localParams;
- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID;
- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID;

@property(nonatomic, assign) TPInterstitialLoadedCallback loadedCallback;
@property(nonatomic, assign) TPInterstitialLoadFailedCallback loadFailedCallback;
@property(nonatomic, assign) TPInterstitialImpressionCallback impressionCallback;
@property(nonatomic, assign) TPInterstitialShowFailedCallback showFailedCallback;
@property(nonatomic, assign) TPInterstitialClickedCallback clickedCallback;
@property(nonatomic, assign) TPInterstitialClosedCallback closedCallback;
@property(nonatomic, assign) TPInterstitialStartLoadCallback startLoadCallback;
@property(nonatomic, assign) TPInterstitialBiddingStartCallback biddingStartCallback;
@property(nonatomic, assign) TPInterstitialBiddingEndCallback biddingEndCallback;
@property(nonatomic, assign) TPInterstitialOneLayerStartLoadCallback oneLayerStartLoadCallback;
@property(nonatomic, assign) TPInterstitialOneLayerLoadedCallback oneLayerLoadedCallback;
@property(nonatomic, assign) TPInterstitialOneLayerLoadFailedCallback oneLayerLoadFailedCallback;
@property(nonatomic, assign) TPInterstitialVideoPlayStartCallback videoPlayStartCallback;
@property(nonatomic, assign) TPInterstitialVideoPlayEndCallback videoPlayEndCallback;
@property(nonatomic, assign) TPInterstitialAllLoadedCallback allLoadedCallback;
@property (nonatomic,assign) TPInterstitialAdIsLoadingCallback adIsLoadingCallback;
@end

NS_ASSUME_NONNULL_END
