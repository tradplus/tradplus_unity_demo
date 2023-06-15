//
//  TPUNativeBanner.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/7.
//

#import <Foundation/Foundation.h>

#import <TradPlusAds/TradPlusAds.h>

NS_ASSUME_NONNULL_BEGIN

@interface TPUNativeBanner : NSObject


@property (nonatomic,readonly)BOOL isAdReady;

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID;
- (void)loadAdWithSceneId:(nullable NSString *)sceneId;
- (void)showAdWithSceneId:(nullable NSString *)sceneId;
- (void)setCustomMap:(NSDictionary *)dic;
- (void)setLocalParams:(NSDictionary *)dic;
- (void)entryAdScenario:(nullable NSString *)sceneId;
- (void)setNativeBannerSize:(CGSize)size;
- (void)setX:(float)x y:(float)y adPosition:(int)adPosition;
- (void)hide;
- (void)display;
- (void)destroy;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo;


@property (nonatomic,assign)BOOL closeAutoShow;
@property (nonatomic,copy)NSString *className;
@property (nonatomic,strong)TradPlusNativeBanner *nativeBanner;
@end

NS_ASSUME_NONNULL_END
