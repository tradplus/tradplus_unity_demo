//
//  TPUPluginUtil.m
//  UnityFramework
//
//  Created by xuejun on 2022/9/5.
//

#import "TPUPluginUtil.h"

@implementation TPUPluginUtil

+ (UIViewController*)unityViewController
{
    return UnityGetGLViewController()?: UnityGetMainWindow().rootViewController?:[UIApplication sharedApplication].keyWindow.rootViewController;
}

+ (NSString *)getJsonStringWithDic:(NSDictionary *)dic
{
    if(dic == nil)
    {
        return @"{}";
    }
    NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
    NSString *msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
    if(msg == nil)
    {
        msg = @"{}";
    }
    return msg;
}

+ (NSString *)getJsonStringWithError:(NSError *)error
{
    NSMutableDictionary *dic = [[NSMutableDictionary alloc] init];
    NSString *msg = error.localizedDescription;
    if(msg == nil)
    {
        msg = @"";
    }
    dic[@"code"] = @(error.code);
    dic[@"message"] = msg;
    return [TPUPluginUtil getJsonStringWithDic:dic];
}

+ (CGRect)getRectWithSize:(CGSize)size adPosition:(int)adPosition
{
    CGRect rect = CGRectZero;
    rect.size = size;
    UIView *rootView = [TPUPluginUtil unityViewController].view;
    CGFloat top = 0;
    CGFloat bottom = 0;
    CGFloat right = 0;
    CGFloat left = 0;
    if (@available(iOS 11.0, *))
    {
        UIEdgeInsets safeAreaInsets = rootView.safeAreaInsets;
        top = safeAreaInsets.top;
        bottom = safeAreaInsets.bottom;
        right = safeAreaInsets.right;
        left = safeAreaInsets.left;
    }
    switch (adPosition)
    {
        case 0://TopLeft
        {
            rect.origin.y = top;
            rect.origin.x = left;
            break;
        }
        case 1://TopCenter
        {
            rect.origin.y = top;
            rect.origin.x = CGRectGetWidth(rootView.bounds)/2 - CGRectGetWidth(rect)/2;
            break;
        }
        case 2://TopRight
        {
            rect.origin.y = top;
            rect.origin.x = CGRectGetWidth(rootView.bounds) - CGRectGetWidth(rect) - right;
            break;
        }
        case 3://Centered
        {
            rect.origin.x =  CGRectGetWidth(rootView.bounds)/2 - CGRectGetWidth(rect)/2;
            rect.origin.y =  CGRectGetHeight(rootView.bounds)/2 - CGRectGetHeight(rect)/2;
            break;
        }
        case 4://BottomLeft
        {
            rect.origin.y = CGRectGetHeight(rootView.bounds) - CGRectGetHeight(rect)  - bottom;
            rect.origin.x = left;
            break;
        }
        case 5://BottomCenter
        {
            rect.origin.y = CGRectGetHeight(rootView.bounds) - CGRectGetHeight(rect)  - bottom;
            rect.origin.x = CGRectGetWidth(rootView.bounds)/2 - CGRectGetWidth(rect)/2;
            break;
        }
        case 6://BottomRight
        {
            rect.origin.y = CGRectGetHeight(rootView.bounds) - CGRectGetHeight(rect)  - bottom;
            rect.origin.x = CGRectGetWidth(rootView.bounds) - CGRectGetWidth(rect) - right;
            break;
        }
    }
    return rect;
}

+ (UIColor *)colorWithString:(NSString *)colorStr
{
    if([colorStr isEqualToString:@"clearColor"])
    {
        return [UIColor clearColor];
    }
    colorStr = [colorStr stringByReplacingOccurrencesOfString:@"#" withString:@""];
    if(colorStr.length <= 6)
    {
        return [self colorWithHexString:colorStr alpha:1];
    }
    else if(colorStr.length == 8)
    {
        NSRange range;
        range.location = 0;
        range.length = 2;
        //alpha
        NSString *aString = [colorStr substringToIndex:2];
        unsigned int a;
        [[NSScanner scannerWithString:aString] scanHexInt:&a];
        if(a == 0)
        {
            return [UIColor clearColor];
        }
        else
        {
            NSString *rgbString = [colorStr substringFromIndex:2];
            return [self colorWithHexString:rgbString alpha:((float) a / 255.0f)];
        }
    }
    return [UIColor clearColor];
}

+ (UIColor *)colorWithHexString:(NSString *)hexString alpha:(float)alpha
{
    NSRange range;
    range.location = 0;
    range.length = 2;
    if(hexString.length < 6)
    {
        hexString = @"000000";
    }
    //R、G、B
    NSString *rString = [hexString substringWithRange:range];
    
    range.location = 2;
    NSString *gString = [hexString substringWithRange:range];
    
    range.location = 4;
    NSString *bString = [hexString substringWithRange:range];
    
    // Scan values
    unsigned int r, g, b;
    [[NSScanner scannerWithString:rString] scanHexInt:&r];
    [[NSScanner scannerWithString:gString] scanHexInt:&g];
    [[NSScanner scannerWithString:bString] scanHexInt:&b];
    return [UIColor colorWithRed:((float) r / 255.0f) green:((float) g / 255.0f) blue:((float) b / 255.0f) alpha:alpha];
}

@end
