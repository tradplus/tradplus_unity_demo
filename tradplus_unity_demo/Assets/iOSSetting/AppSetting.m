//
//  AppSetting.m
//  UnityFramework
//
//  Created by xuejun on 2022/8/18.
//

#import "AppSetting.h"
#import <AppTrackingTransparency/AppTrackingTransparency.h>

@implementation AppSetting

+ (void)load
{
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(setting) name:UIApplicationDidBecomeActiveNotification object:nil];
}

+ (void)setting
{
    if (@available(iOS 14.5, *))
    {
        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
        }];
    }
}

@end