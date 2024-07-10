#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{

    public class TPSdkUnityNativeBanner
    {
        private static AndroidJavaClass TPNativeBannerClass = new AndroidJavaClass("com.tradplus.unity.plugin.nativebanner.TPNativeBannerManager");
        private AndroidJavaObject TPNativeBanner = TPNativeBannerClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TPSdkUnityNativeBanner _instance;

        public static TPSdkUnityNativeBanner Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityNativeBanner();
            }
            return _instance;
        }


        public void LoadNativeBannerAd(string adUnitId, string sceneId, TPNativeBannerExtra extra)
        {

        }

        public void ShowNativeBannerAd(string adUnitId, string sceneId)
        {
            

        }

        public bool NativeBannerAdReady(string adUnitId)
        {
            return true;
        }

        public void EntryNativeBannerAdScenario(string adUnitId, string sceneId)
        {
            
        }

        public void HideNativeBanner(string adUnitId)
        {
            

        }

        public void DisplayNativeBanner(string adUnitId)
        {
            

        }

        public void DestroyNativeBanner(string adUnitId)
        {
            

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
           

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.nativebanner.TPNativeBannerListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TPSdkUnityNativeBanner.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TPSdkUnityNativeBanner.Instance().OnNativeBannerIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNativeBanner.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TPSdkUnityNativeBanner.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName, progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNativeBanner.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNativeBanner.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNativeBanner.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNativeBanner.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TPSdkUnityNativeBanner()
        {
        }

        public event Action<string, Dictionary<string, object>> OnNativeBannerLoaded;

        public event Action<string, Dictionary<string, object>> OnNativeBannerLoadFailed;

        public event Action<string, Dictionary<string, object>> OnNativeBannerImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerShowFailed;

        public event Action<string, Dictionary<string, object>> OnNativeBannerClicked;

        public event Action<string, Dictionary<string, object>> OnNativeBannerClosed;

        public event Action<string, Dictionary<string, object>> OnNativeBannerStartLoad;

        public event Action<string, Dictionary<string, object>> OnNativeBannerBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerBiddingEnd;

        public event Action<string> OnNativeBannerIsLoading;


        public event Action<string, Dictionary<string, object>> OnNativeBannerOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnNativeBannerOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerOneLayerLoadFailed;

        public event Action<string, bool> OnNativeBannerAllLoaded;

        //国内下载监听
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadStart;

        public event Action<string, Dictionary<string, object>, long, long, string, string, int> OnDownloadUpdate;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadPause;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFinish;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFailed;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnInstalled;

    }
}
#endif