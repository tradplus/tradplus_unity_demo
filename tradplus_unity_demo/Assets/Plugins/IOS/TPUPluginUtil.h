//
//  TPUPluginUtil.h
//  UnityFramework
//
//  Created by xuejun on 2022/9/5.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface TPUPluginUtil : NSObject

+ (UIViewController*)unityViewController;
+ (NSString *)getJsonStringWithDic:(NSDictionary *)dic;
+ (NSString *)getJsonStringWithError:(NSError *)error;
+ (CGRect)getRectWithSize:(CGSize)size adPosition:(int)adPosition;
@end

NS_ASSUME_NONNULL_END
