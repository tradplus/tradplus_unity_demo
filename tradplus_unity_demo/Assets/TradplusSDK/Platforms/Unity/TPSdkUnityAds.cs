#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{
    public class TPSdkUnityAds
    {
        private static AndroidJavaClass TradPlusSdk = new AndroidJavaClass("com.tradplus.unity.plugin.TradPlusSdk");

        private static TPSdkUnityAds _instance;

        public static TPSdkUnityAds Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityAds();
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
              
            }
        }


        private class CurrentAreaListenerAdapter : AndroidJavaProxy
        {

            public CurrentAreaListenerAdapter() : base("com.tradplus.unity.plugin.TPPrivacyRegionListener")
            {
            }

            void onSuccess(bool isEu, bool isCn, bool isCa)
            {

            }



            void onFailed()
            {

            }
        }


        private class ShowGDPRListenerAdapter : AndroidJavaProxy
        {

            public ShowGDPRListenerAdapter() : base("com.tradplus.unity.plugin.TPGDPRDialogListener")
            {
            }

            void onAuthResult(int level)
            {
               
            }

        }

        private class GlobalImpressionListener : AndroidJavaProxy
        {

            public GlobalImpressionListener() : base("com.tradplus.unity.plugin.TPGlobalImpressionListener")
            {
            }

            void onImpressionSuccess(string msg)
            {

            }
        }

        public void AddGlobalAdImpressionListener()
        {
          
            
        }

        public void CheckCurrentArea()
        {
           
        }

        public void InitSDK(string appId)
        {
            Debug.Log("unity init sdk");

        }

        public void SetCustomMap(Dictionary<string, string> customMap)
        {
            
        }


        public void SetSettingDataParam(Dictionary<string, object> settingMap)
        {
           
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
           
        }

        public int GetLGPDConsent()
        {
            return TradPlusSdk.CallStatic<int>("getLGPDConsent"); ;
        }

        public void SetCCPADoNotSell(bool canDataCollection)
        {
            

        }
        public int GetCCPADoNotSell()
        {
            return TradPlusSdk.CallStatic<int>("isCCPADoNotSell"); ;
        }

        public void SetCOPPAIsAgeRestrictedUser(bool isChild)
        {
           
        }

        public int GetCOPPAIsAgeRestrictedUser()
        {
            return TradPlusSdk.CallStatic<int>("isCOPPAAgeRestrictedUser"); ;
        }

        public void ShowGDPRDialog(string url)
        {
            
           
        }

        public void SetOpenPersonalizedAd(bool open)
        {
           
        }

        public bool IsOpenPersonalizedAd()
        {
            return true ;
        }

        public void ClearCache(string adUnitId)
        {
           

        }

        public bool IsPrivacyUserAgree()
        {
            return true;
        }

        public void SetPrivacyUserAgree(bool open)
        {
            

        }
        public void SetAuthUID(bool needUid)
        {
           

        }

        public void SetMaxDatabaseSize(int size)
        {
           

        }


        public void SetFirstShowGDPR(bool first)
        {
           

        }

        public bool IsFirstShowGDPR()
        {
            return TradPlusSdk.CallStatic<bool>("isFirstShowGDPR"); ;
        }

        public void SetAutoExpiration(bool autoCheck)
        {
            
        }

        public void CheckAutoExpiration()
        {
           

        }

        public void SetCnServer(bool onlyCn)
        {
           

        }

        public void SetOpenDelayLoadAds(bool isOpen)
        {
            
        }

        public void OpenTradPlusTool(string appId)
        {
           
        }

        public TPSdkUnityAds()
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
#endif