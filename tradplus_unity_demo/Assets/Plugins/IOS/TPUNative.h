//
//  TPUNative.h
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/13.
//

#import <Foundation/Foundation.h>
#import <TradPlusAds/TradPlusAds.h>

NS_ASSUME_NONNULL_BEGIN

@interface TPUNative : NSObject

@property (nonatomic,copy)NSString *adUnitID;
@property (nonatomic,readonly)BOOL isAdReady;

- (void)loadAd;
- (void)setX:(float)x y:(float)y adPosition:(int)adPosition;
- (void)setTemplateRenderSize:(CGSize)size;
- (void)setAdUnitID:(NSString * _Nonnull)adUnitID isAutoLoad:(BOOL)isAutoLoad;
- (void)showWithClassName:(Class)viewClass sceneId:(NSString *)sceneId;
- (void)showWithRenderer:(TradPlusNativeRenderer *)renderer sceneId:(NSString *)sceneId;
- (TradPlusAdNativeObject *)getReadyNativeObject;
- (void)entryAdScenario:(nullable NSString *)sceneId;
- (void)setCustomMap:(NSDictionary *)dic;
- (NSInteger)getLoadedCount;
- (void)hide;
- (void)display;
- (void)destroy;
- (void)setCustomAdInfo:(NSDictionary *)customAdInfo;

@property (nonatomic,strong)UIView *adView;
@end

NS_ASSUME_NONNULL_END
