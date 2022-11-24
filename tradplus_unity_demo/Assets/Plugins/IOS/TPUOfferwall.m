//
//  TPUOfferwall.m
//  tradplus_sdk
//
//  Created by xuejun on 2022/7/19.
//

#import "TPUOfferwall.h"
#import "TPUPluginUtil.h"
#import "TPUOfferwallManager.h"
#import <TradPlusAds/TradPlusAds.h>

@interface TPUOfferwall()<TradPlusADOfferwallDelegate>

@property (nonatomic,strong)TradPlusAdOfferwall *offerwall;
@end

@implementation TPUOfferwall

- (instancetype)init
{
    self = [super init];
    if (self) {
        self.offerwall = [[TradPlusAdOfferwall alloc] init];
        self.offerwall.delegate = self;
    }
    return self;
}

- (void)setAdUnitID:(NSString * _Nonnull)adUnitID isAutoLoad:(BOOL)isAutoLoad
{
    MSLogTrace(@"%s adUnitID:%@ isAutoLoad:%@", __PRETTY_FUNCTION__,adUnitID,@(isAutoLoad));
    [self.offerwall setAdUnitID:adUnitID isAutoLoad:isAutoLoad];
}

- (void)setCustomMap:(NSDictionary *)dic
{
    MSLogTrace(@"%s dic:%@", __PRETTY_FUNCTION__,dic);
    id segmentTag = dic[@"segment_tag"];
    if([segmentTag isKindOfClass:[NSString class]])
    {
        self.offerwall.segmentTag = segmentTag;
    }
    self.offerwall.dicCustomValue = dic;
}

- (void)loadAd
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    [self.offerwall loadAd];
}

- (void)showAdWithSceneId:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.offerwall showAdFromRootViewController:[TPUPluginUtil unityViewController] sceneId:sceneId];
}

- (void)entryAdScenario:(nullable NSString *)sceneId
{
    MSLogTrace(@"%s sceneId:%@", __PRETTY_FUNCTION__,sceneId);
    [self.offerwall entryAdScenario:sceneId];
}

- (BOOL)isAdReady
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    return self.offerwall.isAdReady;
}

- (void)getCurrency
{
    MSLogTrace(@"%s ", __PRETTY_FUNCTION__);
    [self.offerwall getCurrencyBalance];
}

- (void)spendWithAmount:(int)amount
{
    MSLogTrace(@"%s amount:%@", __PRETTY_FUNCTION__,@(amount));
    [self.offerwall spendCurrency:amount];
}

- (void)awardWithAmount:(int)amount
{
    MSLogTrace(@"%s amount:%@", __PRETTY_FUNCTION__,@(amount));
    [self.offerwall awardCurrency:amount];
}

- (void)setUserId:(NSString *)userId
{
    MSLogTrace(@"%s userId:%@", __PRETTY_FUNCTION__,userId);
    [self.offerwall setUserId:userId];
}

- (void)setCustomAdInfo:(NSDictionary *)customAdInfo
{
    MSLogTrace(@"%s", __PRETTY_FUNCTION__);
    self.offerwall.customAdInfo = customAdInfo;
}

#pragma mark - TradPlusADOfferwallDelegate

///AD加载完成 首个广告源加载成功时回调 一次加载流程只会回调一次
- (void)tpOfferwallAdLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].loadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUOfferwallManager sharedInstance].loadedCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD加载失败
///tpOfferwallAdOneLayerLoaded:didFailWithError：返回三方源的错误信息
- (void)tpOfferwallAdLoadFailWithError:(NSError *)error
{
    MSLogInfo(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPUOfferwallManager sharedInstance].loadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUOfferwallManager sharedInstance].loadFailedCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现
- (void)tpOfferwallAdImpression:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].impressionCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUOfferwallManager sharedInstance].impressionCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD展现失败
- (void)tpOfferwallAdShow:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].showFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUOfferwallManager sharedInstance].showFailedCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///AD被点击
- (void)tpOfferwallAdClicked:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].clickedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUOfferwallManager sharedInstance].clickedCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///AD关闭
- (void)tpOfferwallAdDismissed:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].closedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUOfferwallManager sharedInstance].closedCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///开始加载流程
- (void)tpOfferwallAdStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].startLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUOfferwallManager sharedInstance].startLoadCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源开始加载时会都会回调一次。
- (void)tpOfferwallAdOneLayerStartLoad:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].oneLayerStartLoadCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUOfferwallManager sharedInstance].oneLayerStartLoadCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载成功后会都会回调一次。
- (void)tpOfferwallAdOneLayerLoaded:(NSDictionary *)adInfo
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].oneLayerLoadedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        [TPUOfferwallManager sharedInstance].oneLayerLoadedCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String);
    }
}

///当每个广告源加载失败后会都会回调一次，返回三方源的错误信息
- (void)tpOfferwallAdOneLayerLoad:(NSDictionary *)adInfo didFailWithError:(NSError *)error
{
    MSLogInfo(@"%s adInfo:%@", __PRETTY_FUNCTION__, adInfo);
    if([TPUOfferwallManager sharedInstance].oneLayerLoadFailedCallback)
    {
        NSString *jsonString = [TPUPluginUtil getJsonStringWithDic:adInfo];
        NSString *errorString = [TPUPluginUtil getJsonStringWithError:error];
        [TPUOfferwallManager sharedInstance].oneLayerLoadFailedCallback(self.offerwall.unitID.UTF8String,jsonString.UTF8String,errorString.UTF8String);
    }
}

///加载流程全部结束
- (void)tpOfferwallAdAllLoaded:(BOOL)success
{
    MSLogInfo(@"%s success:%@", __PRETTY_FUNCTION__, @(success));
    if([TPUOfferwallManager sharedInstance].allLoadedCallback)
    {
        [TPUOfferwallManager sharedInstance].allLoadedCallback(self.offerwall.unitID.UTF8String,success);
    }
}

///userID 设置完成 error = nil 成功
- (void)tpOfferwallSetUserIdFinish:(NSError *)error
{
    MSLogInfo(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if([TPUOfferwallManager sharedInstance].setUserIdFinishCallback)
    {
        bool success = (error == nil);
        [TPUOfferwallManager sharedInstance].setUserIdFinishCallback(self.offerwall.unitID.UTF8String,success);
    }
}

///用户当前积分墙积分数量
- (void)tpOfferwallGetCurrencyBalance:(NSDictionary *)response error:(NSError *)error
{
    MSLogInfo(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if(error == nil)
    {
        if([TPUOfferwallManager sharedInstance].currencyBalanceSuccessCallback)
        {
            int amount = 0;
            if(response[@"amount"])
            {
                amount = [response[@"amount"] intValue];
            }
            NSMutableDictionary *dic = [[NSMutableDictionary alloc] initWithDictionary:response];
            [dic removeObjectForKey:@"amount"];
            NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
            NSString *msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            if(msg == nil)
            {
                msg = @"";
            }
            [TPUOfferwallManager sharedInstance].currencyBalanceSuccessCallback(self.offerwall.unitID.UTF8String,amount,msg.UTF8String);
        }
    }
    else
    {
        if([TPUOfferwallManager sharedInstance].currencyBalanceFailedCallback)
        {
            NSData *data = [NSJSONSerialization dataWithJSONObject:response options:0 error:nil];
            NSString *msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            if(msg == nil)
            {
                msg = @"";
            }
            [TPUOfferwallManager sharedInstance].currencyBalanceFailedCallback(self.offerwall.unitID.UTF8String,msg.UTF8String);
        }
    }
}

//扣除用户积分墙积分回调
- (void)tpOfferwallSpendCurrency:(NSDictionary *)response error:(NSError *)error
{
    MSLogInfo(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if(error == nil)
    {
        if([TPUOfferwallManager sharedInstance].spendCurrencySuccessCallback)
        {
            int amount = 0;
            if(response[@"amount"])
            {
                amount = [response[@"amount"] intValue];
            }
            NSMutableDictionary *dic = [[NSMutableDictionary alloc] initWithDictionary:response];
            [dic removeObjectForKey:@"amount"];
            NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
            NSString *msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            if(msg == nil)
            {
                msg = @"";
            }
            [TPUOfferwallManager sharedInstance].spendCurrencySuccessCallback(self.offerwall.unitID.UTF8String,amount,msg.UTF8String);
        }
    }
    else
    {
        if([TPUOfferwallManager sharedInstance].spendCurrencyFailedCallback)
        {
            NSData *data = [NSJSONSerialization dataWithJSONObject:response options:0 error:nil];
            NSString *msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            if(msg == nil)
            {
                msg = @"";
            }
            [TPUOfferwallManager sharedInstance].spendCurrencyFailedCallback(self.offerwall.unitID.UTF8String,msg.UTF8String);
        }
    }
}

//添加用户积分墙积分回调
- (void)tpOfferwallAwardCurrency:(NSDictionary *)response error:(NSError *)error
{
    MSLogInfo(@"%s error:%@", __PRETTY_FUNCTION__, error);
    if(error == nil)
    {
        if([TPUOfferwallManager sharedInstance].awardCurrencySuccesCallback)
        {
            int amount = 0;
            if(response[@"amount"])
            {
                amount = [response[@"amount"] intValue];
            }
            NSMutableDictionary *dic = [[NSMutableDictionary alloc] initWithDictionary:response];
            [dic removeObjectForKey:@"amount"];
            NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
            NSString *msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            if(msg == nil)
            {
                msg = @"";
            }
            [TPUOfferwallManager sharedInstance].awardCurrencySuccesCallback(self.offerwall.unitID.UTF8String,amount,msg.UTF8String);
        }
    }
    else
    {
        if([TPUOfferwallManager sharedInstance].awardCurrencyFailedCallback)
        {
            NSData *data = [NSJSONSerialization dataWithJSONObject:response options:0 error:nil];
            NSString *msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            if(msg == nil)
            {
                msg = @"";
            }
            [TPUOfferwallManager sharedInstance].awardCurrencyFailedCallback(self.offerwall.unitID.UTF8String,msg.UTF8String);
        }
    }
}

@end
