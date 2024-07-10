#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{
    public class TPSdkUnityBanner
    {
        private static AndroidJavaClass TPBannerClass = new AndroidJavaClass("com.tradplus.unity.plugin.banner.TPBannerManager");
        private AndroidJavaObject TPBanner = TPBannerClass.CallStatic<AndroidJavaObject>("getInstance");

        private static TPSdkUnityBanner _instance;

        public static TPSdkUnityBanner Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityBanner();
            }
            return _instance;
        }

        public void LoadBannerAd(string adUnitId, string sceneId , TPBannerExtra extra)
        {
  
        }

        public void ShowBannerAd(string adUnitId, string sceneId)
        {
            

        }


        public bool BannerAdReady(string adUnitId)
        {
            return true;
        }

        public void EntryBannerAdScenario(string adUnitId, string sceneId)
        {
            

        }

        public void HideBanner(string adUnitId)
        {
            

        }

        public void DisplayBanner(string adUnitId)
        {
           

        }


        public void DestroyBanner(string adUnitId)
        {
            

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
           

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.banner.TPBannerListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
              
                TPSdkUnityBanner.Instance().OnBannerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(""));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TPSdkUnityBanner.Instance().OnBannerClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TPSdkUnityBanner.Instance().OnBannerImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                TPSdkUnityBanner.Instance().OnBannerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TPSdkUnityBanner.Instance().OnBannerClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TPSdkUnityBanner.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TPSdkUnityBanner.Instance().OnBannerAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TPSdkUnityBanner.Instance().OnBannerOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityBanner.Instance().OnBannerOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TPSdkUnityBanner.Instance().OnBannerStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityBanner.Instance().OnBannerOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityBanner.Instance().OnBannerBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TPSdkUnityBanner.Instance().OnBannerBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TPSdkUnityBanner.Instance().OnBannerIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityBanner.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TPSdkUnityBanner.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName, progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityBanner.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityBanner.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityBanner.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityBanner.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }

        }

        public TPSdkUnityBanner()
        {
        }

        public event Action<string, Dictionary<string, object>> OnBannerLoaded;

        public event Action<string, Dictionary<string, object>> OnBannerLoadFailed;

        public event Action<string, Dictionary<string, object>> OnBannerImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerShowFailed;

        public event Action<string, Dictionary<string, object>> OnBannerClicked;

        public event Action<string, Dictionary<string, object>> OnBannerClosed;

        public event Action<string, Dictionary<string, object>> OnBannerStartLoad;

        public event Action<string, Dictionary<string, object>> OnBannerBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerBiddingEnd;

        public event Action<string> OnBannerIsLoading;

        public event Action<string, Dictionary<string, object>> OnBannerOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnBannerOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerOneLayerLoadFailed;

        public event Action<string, bool> OnBannerAllLoaded;

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