#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{

    public class TPSdkUnityInterstitial
    {
        private static AndroidJavaClass TPInterstitialClass = new AndroidJavaClass("com.tradplus.unity.plugin.interstitial.TPInterstitialManager");
        private AndroidJavaObject TPInterstitial = TPInterstitialClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TPSdkUnityInterstitial _instance;

        public static TPSdkUnityInterstitial Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityInterstitial();
            }
            return _instance;
        }

        public void LoadInterstitialAd(string adUnitId, TPInterstitialExtra extra)
        {

        }

        public void ShowInterstitialAd(string adUnitId, string sceneId)
        {
            

        }

        public bool InterstitialAdReady(string adUnitId)
        {
            return true;
        }

        public void EntryInterstitialAdScenario(string adUnitId, string sceneId)
        {
            
        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
           

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.interstitial.TPInterstitialListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdReward(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialReward(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdVideoStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialVideoPlayStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 播放开始，仅回调给客户 8.1

            void onAdVideoEnd(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialVideoPlayEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 播放结束，仅回调给客户 8.1

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TPSdkUnityInterstitial.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TPSdkUnityInterstitial.Instance().OnInterstitialIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityInterstitial.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TPSdkUnityInterstitial.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName, progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityInterstitial.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityInterstitial.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityInterstitial.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityInterstitial.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TPSdkUnityInterstitial()
        {

        }

        public event Action<string, Dictionary<string, object>> OnInterstitialLoaded;

        public event Action<string, Dictionary<string, object>> OnInterstitialLoadFailed;

        public event Action<string, Dictionary<string, object>> OnInterstitialImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialShowFailed;

        public event Action<string, Dictionary<string, object>> OnInterstitialClicked;

        public event Action<string, Dictionary<string, object>> OnInterstitialClosed;

        public event Action<string, Dictionary<string, object>> OnInterstitialReward;

        public event Action<string, Dictionary<string, object>> OnInterstitialStartLoad;

        public event Action<string, Dictionary<string, object>> OnInterstitialBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialBiddingEnd;

        public event Action<string> OnInterstitialIsLoading;

        public event Action<string, Dictionary<string, object>> OnInterstitialOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnInterstitialOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialOneLayerLoadFailed;

        public event Action<string, Dictionary<string, object>> OnInterstitialVideoPlayStart;

        public event Action<string, Dictionary<string, object>> OnInterstitialVideoPlayEnd;

        public event Action<string, bool> OnInterstitialAllLoaded;

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