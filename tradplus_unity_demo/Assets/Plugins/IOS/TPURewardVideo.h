//
//  TPURewardVideo.h
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface TPURewardVideo : NSObject

@property (nonatomic,readonly)BOOL isAdReady;

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID;
- (void)loadAdWithMaxWaitTime:(float)maxWaitTime;
- (void)openAutoLoadCallback;
- (void)setCustomMap:(NSDictionary *)dic;
- (void)setServerSideVerificationOptionsWithUserID:(nonnull NSString *)userID customData:(nullable NSString *)customData;
- (void)showAdWithSceneId:(nullable NSString *)sceneId;
- (void)entryAdScenario:(nullable NSString *)sceneId;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo;
- (void)setLocalParams:(NSDictionary *)dic;
@end

NS_ASSUME_NONNULL_END
