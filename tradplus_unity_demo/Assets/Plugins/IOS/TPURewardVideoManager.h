//
//  TPURewardVideoManager.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/1.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (*TPRewardVideoLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoLoadFailedCallback)(const char* adUnitId,const char* error);
typedef void (*TPRewardVideoImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoShowFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPRewardVideoClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoClosedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoRewardCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoBiddingStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoBiddingEndCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPRewardVideoOneLayerStartLoadCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoOneLayerLoadedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoOneLayerLoadFailedCallback)(const char* adUnitId,const char* adInfo,const char* error);
typedef void (*TPRewardVideoPlayStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoPlayEndCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoAllLoadedCallback)(const char* adUnitId,bool isSuccess);
typedef void (*TPRewardVideoPlayAgainImpressionCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoPlayAgainRewardCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoPlayAgainClickedCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoPlayAgainVideoPlayStartCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoPlayAgainVideoPlayEndCallback)(const char* adUnitId,const char* adInfo);
typedef void (*TPRewardVideoAdIsLoadingCallback)(const char* adUnitId);

@interface TPURewardVideoManager : NSObject

+ (TPURewardVideoManager *)sharedInstance;

- (void)loadWithAdUnitID:(NSString *)adUnitID userId:(NSString *)userId customData:(NSString *)customData customMap:(NSDictionary *)customMap localParams:(NSDictionary *)localParams openAutoLoadCallback:(BOOL)openAutoLoadCallback maxWaitTime:(float)maxWaitTime;
- (void)showWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (BOOL)adReadyWithAdUnitID:(NSString *)adUnitID;
- (void)entryAdScenarioWithAdUnitID:(NSString *)adUnitID sceneId:(NSString *)sceneId;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo adUnitID:(NSString *)adUnitID;

@property(nonatomic, assign) TPRewardVideoLoadedCallback loadedCallback;
@property(nonatomic, assign) TPRewardVideoLoadFailedCallback loadFailedCallback;
@property(nonatomic, assign) TPRewardVideoImpressionCallback impressionCallback;
@property(nonatomic, assign) TPRewardVideoShowFailedCallback showFailedCallback;
@property(nonatomic, assign) TPRewardVideoClickedCallback clickedCallback;
@property(nonatomic, assign) TPRewardVideoClosedCallback closedCallback;
@property(nonatomic, assign) TPRewardVideoRewardCallback rewardCallback;
@property(nonatomic, assign) TPRewardVideoStartLoadCallback startLoadCallback;
@property(nonatomic, assign) TPRewardVideoBiddingStartCallback biddingStartCallback;
@property(nonatomic, assign) TPRewardVideoBiddingEndCallback biddingEndCallback;
@property(nonatomic, assign) TPRewardVideoOneLayerStartLoadCallback oneLayerStartLoadCallback;
@property(nonatomic, assign) TPRewardVideoOneLayerLoadedCallback oneLayerLoadedCallback;
@property(nonatomic, assign) TPRewardVideoOneLayerLoadFailedCallback oneLayerLoadFailedCallback;
@property(nonatomic, assign) TPRewardVideoPlayStartCallback videoPlayStartCallback;
@property(nonatomic, assign) TPRewardVideoPlayEndCallback videoPlayEndCallback;
@property(nonatomic, assign) TPRewardVideoAllLoadedCallback allLoadedCallback;
@property(nonatomic, assign) TPRewardVideoPlayAgainImpressionCallback playAgainImpressionCallback;
@property(nonatomic, assign) TPRewardVideoPlayAgainRewardCallback playAgainRewardCallback;
@property(nonatomic, assign) TPRewardVideoPlayAgainClickedCallback playAgainClickedCallback;
@property(nonatomic, assign) TPRewardVideoPlayAgainVideoPlayStartCallback playAgainVideoPlayStartCallback;
@property(nonatomic, assign) TPRewardVideoPlayAgainVideoPlayEndCallback playAgainVideoPlayEndCallback;
@property (nonatomic,assign) TPRewardVideoAdIsLoadingCallback adIsLoadingCallback;
@end

NS_ASSUME_NONNULL_END
