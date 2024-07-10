//
//  TPUBanner.h
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import <Foundation/Foundation.h>
#import <TradPlusAds/TradPlusAds.h>

NS_ASSUME_NONNULL_BEGIN

@interface TPUBanner : NSObject

@property (nonatomic,readonly)BOOL isAdReady;

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID;
- (void)loadAdWithSceneId:(nullable NSString *)sceneId maxWaitTime:(float)maxWaitTime;
- (void)openAutoLoadCallback;
- (void)showAdWithSceneId:(nullable NSString *)sceneId;
- (void)setCustomMap:(NSDictionary *)dic;
- (void)entryAdScenario:(nullable NSString *)sceneId;
- (void)setBannerSize:(CGSize)size;
- (void)setBannerContentMode:(NSInteger)mode;
- (void)setLocalParams:(NSDictionary *)dic;
- (void)setBackgroundColor:(NSString *)backgroundColor;
- (void)setX:(float)x y:(float)y adPosition:(int)adPosition;
- (void)hide;
- (void)display;
- (void)destroy;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo;
- (void)setClassName:(NSString *)className;

@property (nonatomic,assign)BOOL closeAutoShow;

@property (nonatomic,strong)TradPlusAdBanner *banner;
@end

NS_ASSUME_NONNULL_END
