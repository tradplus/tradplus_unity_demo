
using System;
using System.Collections.Generic;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Android
{
    public class TradplusAdsAndroid
    {
        private static AndroidJavaClass TradPlusSdk = new AndroidJavaClass("com.tradplus.unity.plugin.TradPlusSdk");

        private static TradplusAdsAndroid _instance;

        public static TradplusAdsAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusAdsAndroid();
            }
            return _instance;
        }

        private class ListenerAdapter : AndroidJavaProxy
        {

            public ListenerAdapter() : base("com.tradplus.unity.plugin.TPInitListener")
            {
            }

            void onResult(string str)
            {
                Debug.Log("init success");

                TradplusAdsAndroid.Instance().OnInitFinish(true);
            }
        }


        private class CurrentAreaListenerAdapter : AndroidJavaProxy
        {

            public CurrentAreaListenerAdapter() : base("com.tradplus.unity.plugin.TPPrivacyRegionListener")
            {
            }

            void onSuccess(bool isEu, bool isCn, bool isCa)
            {
                Debug.Log("unity CurrentAreaListenerAdapter success");
                TradplusAdsAndroid.Instance().OnCurrentAreaSuccess(isEu,isCn,isCa);
            }



            void onFailed()
            {
                Debug.Log("unity CurrentAreaListenerAdapter failed");

                TradplusAdsAndroid.Instance().OnCurrentAreaFailed("unknown");
            }
        }


        private class ShowGDPRListenerAdapter : AndroidJavaProxy
        {

            public ShowGDPRListenerAdapter() : base("com.tradplus.unity.plugin.TPGDPRDialogListener")
            {
            }

            void onAuthResult(int level)
            {
                TradplusAdsAndroid.Instance().OnDialogClosed(level);
            }

        }

        private class GlobalImpressionListener : AndroidJavaProxy
        {

            public GlobalImpressionListener() : base("com.tradplus.unity.plugin.TPGlobalImpressionListener")
            {
            }

            void onImpressionSuccess(string msg)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(msg) as Dictionary<string, object>;
                TradplusAdsAndroid.Instance().OnGlobalAdImpression(adInfo);
            }
        }

        public void AddGlobalAdImpressionListener()
        {
            //设置回调
            GlobalImpressionListener listener = new GlobalImpressionListener();
            TradPlusSdk.CallStatic("setGlobalImpressionListener", listener);
            
        }

        public void CheckCurrentArea()
        {
            CurrentAreaListenerAdapter listener = new CurrentAreaListenerAdapter();
            TradPlusSdk.CallStatic("checkCurrentArea", listener);
        }

        public void InitSDK(string appId)
        {
            Debug.Log("unity init sdk");
            ListenerAdapter listener = new ListenerAdapter();
            TradPlusSdk.CallStatic("initSdk", appId, listener);

        }

        public void SetCustomMap(Dictionary<string, string> customMap)
        {
            TradPlusSdk.CallStatic("initCustomMap",Json.Serialize(customMap));
        }


        public void SetSettingDataParam(Dictionary<string, object> settingMap)
        {
            TradPlusSdk.CallStatic("setSettingDataParam", Json.Serialize(settingMap));
        }

        public string Version()
        {
            return TradPlusSdk.CallStatic<string>("getSdkVersion");
        }

        public bool IsEUTraffic()
        {
            return TradPlusSdk.CallStatic<bool>("isEUTraffic");
        }

        public bool IsCalifornia()
        {
            return TradPlusSdk.CallStatic<bool>("isCalifornia");
        }

        public void SetGDPRDataCollection(bool canDataCollection)
        {
            TradPlusSdk.CallStatic("setGDPRDataCollection", canDataCollection);

        }
        public int GetGDPRDataCollection()
        {
            return TradPlusSdk.CallStatic<int>("getGDPRDataCollection"); ;
        }

        public void SetLGPDConsent(bool consent)
        {
            TradPlusSdk.CallStatic("setLGPDConsent", consent);
        }

        public int GetLGPDConsent()
        {
            return TradPlusSdk.CallStatic<int>("getLGPDConsent"); ;
        }

        public void SetCCPADoNotSell(bool canDataCollection)
        {
            TradPlusSdk.CallStatic("setCCPADoNotSell", canDataCollection);

        }
        public int GetCCPADoNotSell()
        {
            return TradPlusSdk.CallStatic<int>("isCCPADoNotSell"); ;
        }

        public void SetCOPPAIsAgeRestrictedUser(bool isChild)
        {
            TradPlusSdk.CallStatic("setCOPPAIsAgeRestrictedUser", isChild);
        }

        public int GetCOPPAIsAgeRestrictedUser()
        {
            return TradPlusSdk.CallStatic<int>("isCOPPAAgeRestrictedUser"); ;
        }

        public void ShowGDPRDialog(string url)
        {
            ShowGDPRListenerAdapter listener = new ShowGDPRListenerAdapter();
            TradPlusSdk.CallStatic("showGDPRDialog", listener,url);
        }

        public void SetOpenPersonalizedAd(bool open)
        {
            TradPlusSdk.CallStatic("setOpenPersonalizedAd", open);
        }

        public bool IsOpenPersonalizedAd()
        {
            return TradPlusSdk.CallStatic<bool>("isOpenPersonalizedAd"); ;
        }

        public void ClearCache(string adUnitId)
        {
            TradPlusSdk.CallStatic("clearCache", adUnitId);

        }

        public bool IsPrivacyUserAgree()
        {
            return TradPlusSdk.CallStatic<bool>("isPrivacyUserAgree"); ;
        }

        public void SetPrivacyUserAgree(bool open)
        {
            TradPlusSdk.CallStatic("setPrivacyUserAgree", open);

        }
        public void SetAuthUID(bool needUid)
        {
            TradPlusSdk.CallStatic("setAuthUID", needUid);

        }

        public void SetMaxDatabaseSize(int size)
        {
            TradPlusSdk.CallStatic("setMaxDatabaseSize", size);
        }


        public void SetFirstShowGDPR(bool first)
        {
            TradPlusSdk.CallStatic("setFirstShowGDPR", first);
        }

        public bool IsFirstShowGDPR()
        {
            return TradPlusSdk.CallStatic<bool>("isFirstShowGDPR"); ;
        }

        public void SetAutoExpiration(bool autoCheck)
        {
            TradPlusSdk.CallStatic("setAutoExpiration", autoCheck);
        }

        public void CheckAutoExpiration()
        {
            TradPlusSdk.CallStatic("checkAutoExpiration");

        }

        public void SetCnServer(bool onlyCn)
        {
            TradPlusSdk.CallStatic("setCnServer", onlyCn);

        }

        public void SetOpenDelayLoadAds(bool isOpen)
        {
            TradPlusSdk.CallStatic("setOpenDelayLoadAds", isOpen);

        }

        public void OpenTradPlusTool(string appId)
        {
            TradPlusSdk.CallStatic("openTradPlusTool", appId);
        }

        public TradplusAdsAndroid()
        {
        }

        public event Action<bool> OnInitFinish;

        public event Action<int> OnDialogClosed;

        public event Action<bool, bool, bool> OnCurrentAreaSuccess;

        public event Action<string> OnCurrentAreaFailed;

        public event Action<string> OnGDPRSuccess;

        public event Action<string> OnGDPRFailed;

        public event Action<Dictionary<string, object>> OnGlobalAdImpression;

    }
}