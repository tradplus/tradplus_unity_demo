//
//  TPUOfferwall.h
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface TPUOfferwall : NSObject

@property (nonatomic,readonly)BOOL isAdReady;

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID;
- (void)loadAdWithMaxWaitTime:(float)maxWaitTime;
- (void)openAutoLoadCallback;
- (void)setCustomMap:(NSDictionary *)dic;
- (void)setLocalParams:(NSDictionary *)dic;
- (void)showAdWithSceneId:(nullable NSString *)sceneId;
- (void)entryAdScenario:(nullable NSString *)sceneId;
- (void)getCurrency;
- (void)spendWithAmount:(int)amount;
- (void)awardWithAmount:(int)amount;
- (void)setUserId:(NSString *)userId;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo;
@end

NS_ASSUME_NONNULL_END
