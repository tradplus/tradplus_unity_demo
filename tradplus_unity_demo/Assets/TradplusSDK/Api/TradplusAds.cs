using System;
#if UNITY_IOS
using TradplusSDK.iOS;
#endif
using TradplusSDK.Android;
using System.Collections.Generic;

namespace TradplusSDK.Api
{
    public class TPAds :
#if UNITY_IOS
        TradplusAdsiOS
#else
        TradplusAdsAndroid
#endif
    { }

    public class TradplusAds
    {
        public static string PluginVersion = "1.0.2";

        private static TradplusAds _instance;

        public static TradplusAds Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusAds();
            }
            return _instance;
        }

        ///<summary>
        ///查询当前地区，此接口一般在初始化前调用来获取当前设备的地区状态。开发者可根据返回数据针对地区情况来设置各隐私权限。
        ///使用时需要设置回调 OnCurrentAreaSuccess & OnCurrentAreaFailed 来获取查询状态。
        ///OnCurrentAreaSuccess 返回的地区数据包括： bool isEu 是否欧洲, bool isCn 是否中国, bool isCa 是否加州
        ///OnCurrentAreaFailed 时开发者需要自行查询或处理，设置各隐私权限。
        ///</summary>
        public void CheckCurrentArea()
        {
            TPAds.Instance().CheckCurrentArea();
        }

        ///<summary>
        ///初始化SDK
        ///</summary>
        public void InitSDK(string appId)
        {
            TPAds.Instance().InitSDK(appId);
        }

        ///<summary>
        ///设置流量分组等自定数据，需要在初始化前设置
        ///</summary>
        public void SetCustomMap(Dictionary<string,string> customMap)
        {
            TPAds.Instance().SetCustomMap(customMap);
        }

        ///<summary>
        ///获取 TradplusSDK 版本号
        ///</summary>
        public string Version()
        {
            return TPAds.Instance().Version();
        }

        ///<summary>
        ///是否在欧盟地区 此接口需要在初始化成功后调用
        ///</summary>
        public bool IsEUTraffic()
        {
            return TPAds.Instance().IsEUTraffic();
        }

        ///<summary>
        ///是否在加州地区 此接口需要在初始化成功后调用
        ///</summary>
        public bool IsCalifornia()
        {
            return TPAds.Instance().IsCalifornia();
        }

        ///<summary>
        ///设置 GDPR等级 是否允许数据上报: ture 设备数据允许上报, false 设备数据不允许上报
        ///</summary>
        public void SetGDPRDataCollection(bool canDataCollection)
        {
            TPAds.Instance().SetGDPRDataCollection(canDataCollection);
        }

        ///<summary>
        ///获取当前 GDPR等级：  0 允许上报 , 1 不允许上报, 2 未设置
        ///</summary>
        public int GetGDPRDataCollection()
        {
            return TPAds.Instance().GetGDPRDataCollection();
        }

        ///<summary>
        ///设置 CCPA等级 是否允许数据上报: ture 加州用户接受上报数据, false 加州用户均不上报数据
        ///</summary>
        public void SetCCPADoNotSell(bool canDataCollection)
        {
            TPAds.Instance().SetCCPADoNotSell(canDataCollection);
        }

        ///<summary>
        ///获取当前 CCPA等级： 0 允许上报 , 1 不允许上报, 2 未设置
        ///</summary>
        public int GetCCPADoNotSell()
        {
            return TPAds.Instance().GetCCPADoNotSell();
        }

        ///<summary>
        ///设置 COPPA等级 是否允许数据上报: ture 表明儿童, false 表明不是儿童
        ///</summary>
        public void SetCOPPAIsAgeRestrictedUser(bool isChild)
        {
            TPAds.Instance().SetCOPPAIsAgeRestrictedUser(isChild);
        }

        ///<summary>
        ///获取当前 COPPA等级： 0 表明儿童 , 1 表明不是儿童, 2 未设置
        ///</summary>
        public int GetCOPPAIsAgeRestrictedUser()
        {
            return TPAds.Instance().GetCOPPAIsAgeRestrictedUser();
        }

        ///<summary>
        ///TradplusSDK GDPR隐私授权页面
        ///需要监听回调 OnDialogClosed 获取页面关闭及状态
        ///</summary>
        public void ShowGDPRDialog()
        {
            TPAds.Instance().ShowGDPRDialog();
        }

        ///<summary>
        ///设置是否开启个性化推荐广告。 false 关闭 ，true 开启。SDK默认 true 开启
        ///</summary>
        public void SetOpenPersonalizedAd(bool open)
        {
            TPAds.Instance().SetOpenPersonalizedAd(open);
        }

        ///<summary>
        ///当前的个性化状态  false 关闭 ，true 开启
        ///</summary>
        public bool IsOpenPersonalizedAd()
        {
            return TPAds.Instance().IsOpenPersonalizedAd();
        }

        ///<summary>
        ///清理指定广告位下的广告缓存，一般使用场景：用于切换用户后清除激励视频的缓存广告
        ///</summary>
        public void ClearCache(string adUnitId)
        {
            TPAds.Instance().ClearCache(adUnitId);
        }

        ///<summary>
        ///设置是否关闭广告过期检测，bool autoCheck,默认为true 开启检测
        ///</summary>
        public void SetAutoExpiration(bool autoCheck)
        {
            TPAds.Instance().SetAutoExpiration(autoCheck);
        }

        ///<summary>
        ///主动触发广告检测
        ///</summary>
        public void CheckAutoExpiration()
        {
            TPAds.Instance().CheckAutoExpiration();
        }

        ///<summary>
        ///选择是否只使用TradPlus国内服务器还是全球服务器，bool onlyCn, 默认false 使用全球服务器
        ///</summary>
        public void SetCnServer(bool onlyCn)
        {
            TPAds.Instance().SetCnServer(onlyCn);
        }


/// 仅android支持的API

        ///<summary>
        ///选择是否开启自动加载重新load广告时，是否延迟2秒，bool isopen, 默认false 不使用延迟2s
        ///仅android支持
        ///</summary>
        public void SetOpenDelayLoadAds(bool isOpen)
        {
#if UNITY_ANDROID
            TPAds.Instance().SetOpenDelayLoadAds(isOpen);
#endif
        }

        ///<summary>
        ///android 国内隐私权限 是否打开，仅android支持（iOS时返回false）
        ///</summary>
        public bool IsPrivacyUserAgree()
        {
#if UNITY_ANDROID
        return TPAds.Instance().IsPrivacyUserAgree();

#else
        return false;
#endif
        }

        ///<summary>
        ///android 设置国内隐私权限，仅android支持
        ///</summary>
        public void SetPrivacyUserAgree(bool open)
        {
#if UNITY_ANDROID
            TPAds.Instance().SetPrivacyUserAgree(open);
#endif
        }

        ///<summary>
        ///android 设置可使用数据库容量大小，到达数值后自动清空数据库，默认20MB，仅android支持
        ///</summary>
        public void SetMaxDatabaseSize(int size)
        {
#if UNITY_ANDROID
            TPAds.Instance().SetMaxDatabaseSize(size);
#endif
        }

        ///<summary>
        ///android 设置测试设备，仅android支持
        ///</summary>
        public void SetTestDevice(bool testDevice, string testModeId = null)
        {
#if UNITY_ANDROID
            TPAds.Instance().SetTestDevice(testDevice, testModeId);
#endif
        }

        ///<summary>
        ///设置是否第一次show GDPR弹框, 仅支持 android
        ///</summary>
        public void SetFirstShowGDPR(bool first)
        {
#if UNITY_ANDROID
            TPAds.Instance().SetFirstShowGDPR(first);
#endif
        }

        ///<summary>
        ///获取是否是第一次show GDPR弹框 true 是，false 否,仅支持 android （iOS时返回false）
        ///</summary>
        public bool IsFirstShowGDPR()
        {
#if UNITY_ANDROID
            return TPAds.Instance().IsFirstShowGDPR();
#else
            return false;
#endif
        }

        ///<summary>
        ///初始化完成 bool success
        ///</summary>
        public event Action<bool> OnInitFinish;

        ///<summary>
        ///GDPR授权页面关闭 int level（iOS固定返回0）
        ///</summary>
        public event Action<int> OnDialogClosed;

        ///<summary>
        ///查询地区成功 bool isEu 是否欧洲, bool isCn 是否中国, bool isCa 是否加州
        ///</summary>
        public event Action<bool,bool,bool> OnCurrentAreaSuccess;

        ///<summary>
        ///查询地区失败，开发者需要自行查询或处理，设置各隐私权限。
        ///</summary>
        public event Action<string> OnCurrentAreaFailed;

        ///<summary>
        ///仅android 支持
        ///</summary>
        public event Action<string> OnGDPRSuccess;

        ///<summary>
        ///仅android 支持
        ///</summary>
        public event Action<string> OnGDPRFailed;


        public TradplusAds()
        {
            TPAds.Instance().OnInitFinish += (success) =>
            {
                if (this.OnInitFinish != null)
                {
                    this.OnInitFinish(success);
                }
            };

            TPAds.Instance().OnDialogClosed += (level) =>
            {
                if (this.OnDialogClosed != null)
                {
                    this.OnDialogClosed(level);
                }
            };

            TPAds.Instance().OnCurrentAreaSuccess += (isEu,isCn,isCa) =>
            {
                if (this.OnCurrentAreaSuccess != null)
                {
                    this.OnCurrentAreaSuccess(isEu, isCn, isCa);
                }
            };

            TPAds.Instance().OnCurrentAreaFailed += (msg) =>
            {
                if (this.OnCurrentAreaFailed != null)
                {
                    this.OnCurrentAreaFailed(msg);
                }
            };

#if UNITY_ANDROID
            TPAds.Instance().OnGDPRSuccess += (msg) =>
            {
                if (this.OnGDPRSuccess != null)
                {
                    this.OnGDPRSuccess(msg);
                }
            };

            TPAds.Instance().OnGDPRFailed += (msg) =>
            {
                if (this.OnGDPRFailed != null)
                {
                    this.OnGDPRFailed(msg);
                }
            };
#endif

        }
    }
}