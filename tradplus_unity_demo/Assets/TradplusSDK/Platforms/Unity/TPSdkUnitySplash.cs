#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{

    public class TPSdkUnitySplash
    {
        private static AndroidJavaClass TPSplashClass = new AndroidJavaClass("com.tradplus.unity.plugin.splash.TPSplashManager");
        private AndroidJavaObject TPSplash = TPSplashClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TPSdkUnitySplash _instance;

        public static TPSdkUnitySplash Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnitySplash();
            }
            return _instance;
        }

        public void LoadSplashAd(string adUnitId, TPSplashExtra extra)
        {
           
        }

        public void ShowSplashAd(string adUnitId, string sceneId)
        {
           

        }

        public bool SplashAdReady(string adUnitId)
        {
            return false;
        }

        public void EntrySplashAdScenario(string adUnitId, string sceneId)
        {
            
        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.splash.TPSplashListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo) {
            if(TPSdkUnitySplash.Instance().OnSplashLoaded != null)
                TPSdkUnitySplash.Instance().OnSplashLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo) {
                        if(TPSdkUnitySplash.Instance().OnSplashClicked != null)

                TPSdkUnitySplash.Instance().OnSplashClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo) {
                        if(TPSdkUnitySplash.Instance().OnSplashImpression != null)

                TPSdkUnitySplash.Instance().OnSplashImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error) {
                        if(TPSdkUnitySplash.Instance().OnSplashLoadFailed != null)

                TPSdkUnitySplash.Instance().OnSplashLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo) {
                        if(TPSdkUnitySplash.Instance().OnSplashClosed != null)

                TPSdkUnitySplash.Instance().OnSplashClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdAllLoaded(string unitId,bool b)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashAllLoaded != null)

                TPSdkUnitySplash.Instance().OnSplashAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashOneLayerLoadFailed != null)

                TPSdkUnitySplash.Instance().OnSplashOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror),(Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onZoomOutStart(string unitId, string tpAdInfo)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashZoomOutStart != null)

                TPSdkUnitySplash.Instance().OnSplashZoomOutStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onZoomOutEnd(string unitId, string tpAdInfo)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashZoomOutEnd != null)

                TPSdkUnitySplash.Instance().OnSplashZoomOutEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onSplashSkip(string unitId, string tpAdInfo)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashSkip != null)

                TPSdkUnitySplash.Instance().OnSplashSkip(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashOneLayerLoaded != null)

                TPSdkUnitySplash.Instance().OnSplashOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
            if(TPSdkUnitySplash.Instance().OnSplashStartLoad != null)

                TPSdkUnitySplash.Instance().OnSplashStartLoad(unitId,null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashOneLayerStartLoad != null)

                TPSdkUnitySplash.Instance().OnSplashOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashBiddingStart != null)

                TPSdkUnitySplash.Instance().OnSplashBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashBiddingEnd != null)

                TPSdkUnitySplash.Instance().OnSplashBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                        if(TPSdkUnitySplash.Instance().OnSplashIsLoading != null)

                TPSdkUnitySplash.Instance().OnSplashIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TPSdkUnitySplash.Instance().OnDownloadStart != null)

                TPSdkUnitySplash.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes,currBytes,fileName,appName);
            }

            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                        if(TPSdkUnitySplash.Instance().OnDownloadUpdate != null)

                TPSdkUnitySplash.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName,progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TPSdkUnitySplash.Instance().OnDownloadPause != null)

                TPSdkUnitySplash.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TPSdkUnitySplash.Instance().OnDownloadFinish != null)

                TPSdkUnitySplash.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TPSdkUnitySplash.Instance().OnDownloadFailed != null)

                TPSdkUnitySplash.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TPSdkUnitySplash.Instance().OnInstalled != null)

                TPSdkUnitySplash.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TPSdkUnitySplash()
        {

        }

        public event Action<string, Dictionary<string, object>> OnSplashLoaded;

        public event Action<string, Dictionary<string, object>> OnSplashLoadFailed;

        public event Action<string, Dictionary<string, object>> OnSplashImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashShowFailed;

        public event Action<string, Dictionary<string, object>> OnSplashClicked;

        public event Action<string, Dictionary<string, object>> OnSplashClosed;

        public event Action<string, Dictionary<string, object>> OnSplashSplash;

        public event Action<string, Dictionary<string, object>> OnSplashStartLoad;

        public event Action<string, Dictionary<string, object>> OnSplashBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashBiddingEnd;

        public event Action<string> OnSplashIsLoading;

        public event Action<string, Dictionary<string, object>> OnSplashOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnSplashOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashOneLayerLoadFailed;


        public event Action<string, Dictionary<string, object>> OnSplashSkip;
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutEnd;
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutStart;

        public event Action<string, bool> OnSplashAllLoaded;


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