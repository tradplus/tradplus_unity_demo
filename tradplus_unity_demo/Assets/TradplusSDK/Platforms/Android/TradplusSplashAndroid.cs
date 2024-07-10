
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Android
{

    public class TradplusSplashAndroid
    {
        private static AndroidJavaClass TPSplashClass = new AndroidJavaClass("com.tradplus.unity.plugin.splash.TPSplashManager");
        private AndroidJavaObject TPSplash = TPSplashClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TradplusSplashAndroid _instance;

        public static TradplusSplashAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusSplashAndroid();
            }
            return _instance;
        }

        public void LoadSplashAd(string adUnitId, TPSplashExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();
            info.Add("customMap",extra.customMap);
            info.Add("localParams", extra.localParams);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);



            TPSplash.Call("loadAd",adUnitId,Json.Serialize(info),loadListenerAdapter);
        }

        public void ShowSplashAd(string adUnitId, string sceneId)
        {
            TPSplash.Call("showAd", adUnitId, sceneId);

        }

        public bool SplashAdReady(string adUnitId)
        {
            return TPSplash.Call<bool>("isReady",adUnitId);
        }

        public void EntrySplashAdScenario(string adUnitId, string sceneId)
        {
            TPSplash.Call("entryAdScenario", adUnitId, sceneId);
        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPSplash.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.splash.TPSplashListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo) {
            if(TradplusSplashAndroid.Instance().OnSplashLoaded != null)
                TradplusSplashAndroid.Instance().OnSplashLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo) {
                        if(TradplusSplashAndroid.Instance().OnSplashClicked != null)

                TradplusSplashAndroid.Instance().OnSplashClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo) {
                        if(TradplusSplashAndroid.Instance().OnSplashImpression != null)

                TradplusSplashAndroid.Instance().OnSplashImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error) {
                        if(TradplusSplashAndroid.Instance().OnSplashLoadFailed != null)

                TradplusSplashAndroid.Instance().OnSplashLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo) {
                        if(TradplusSplashAndroid.Instance().OnSplashClosed != null)

                TradplusSplashAndroid.Instance().OnSplashClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdAllLoaded(string unitId,bool b)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashAllLoaded != null)

                TradplusSplashAndroid.Instance().OnSplashAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashOneLayerLoadFailed != null)

                TradplusSplashAndroid.Instance().OnSplashOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror),(Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onZoomOutStart(string unitId, string tpAdInfo)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashZoomOutStart != null)

                TradplusSplashAndroid.Instance().OnSplashZoomOutStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onZoomOutEnd(string unitId, string tpAdInfo)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashZoomOutEnd != null)

                TradplusSplashAndroid.Instance().OnSplashZoomOutEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onSplashSkip(string unitId, string tpAdInfo)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashSkip != null)

                TradplusSplashAndroid.Instance().OnSplashSkip(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashOneLayerLoaded != null)

                TradplusSplashAndroid.Instance().OnSplashOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
            if(TradplusSplashAndroid.Instance().OnSplashStartLoad != null)

                TradplusSplashAndroid.Instance().OnSplashStartLoad(unitId,null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashOneLayerStartLoad != null)

                TradplusSplashAndroid.Instance().OnSplashOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashBiddingStart != null)

                TradplusSplashAndroid.Instance().OnSplashBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashBiddingEnd != null)

                TradplusSplashAndroid.Instance().OnSplashBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                        if(TradplusSplashAndroid.Instance().OnSplashIsLoading != null)

                TradplusSplashAndroid.Instance().OnSplashIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TradplusSplashAndroid.Instance().OnDownloadStart != null)

                TradplusSplashAndroid.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes,currBytes,fileName,appName);
            }

            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                        if(TradplusSplashAndroid.Instance().OnDownloadUpdate != null)

                TradplusSplashAndroid.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName,progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TradplusSplashAndroid.Instance().OnDownloadPause != null)

                TradplusSplashAndroid.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TradplusSplashAndroid.Instance().OnDownloadFinish != null)

                TradplusSplashAndroid.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TradplusSplashAndroid.Instance().OnDownloadFailed != null)

                TradplusSplashAndroid.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                        if(TradplusSplashAndroid.Instance().OnInstalled != null)

                TradplusSplashAndroid.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TradplusSplashAndroid()
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