
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Android
{

    public class TradplusNativeBannerAndroid
    {
        private static AndroidJavaClass TPNativeBannerClass = new AndroidJavaClass("com.tradplus.unity.plugin.nativebanner.TPNativeBannerManager");
        private AndroidJavaObject TPNativeBanner = TPNativeBannerClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TradplusNativeBannerAndroid _instance;

        public static TradplusNativeBannerAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusNativeBannerAndroid();
            }
            return _instance;
        }


        public void LoadNativeBannerAd(string adUnitId, string sceneId, TPNativeBannerExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();

            info.Add("width", extra.width);
            info.Add("height", extra.height);
            info.Add("className", extra.className);
            info.Add("customMap", extra.customMap);
            info.Add("adPosition", (int)extra.adPosition);
            info.Add("closeAutoShow", extra.closeAutoShow);
            info.Add("closeAutoDestroy", extra.closeAutoDestroy);
            info.Add("x", extra.x);
            info.Add("y", extra.y);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);


            info.Add("localParams", extra.localParams);


            TPNativeBanner.Call("loadAd", adUnitId, sceneId, Json.Serialize(info), loadListenerAdapter);

        }

        public void ShowNativeBannerAd(string adUnitId, string sceneId)
        {
            TPNativeBanner.Call("showAd", adUnitId, sceneId);

        }

        public bool NativeBannerAdReady(string adUnitId)
        {
            return TPNativeBanner.Call<bool>("isReady", adUnitId);
        }

        public void EntryNativeBannerAdScenario(string adUnitId, string sceneId)
        {
            TPNativeBanner.Call("entryAdScenario", adUnitId, sceneId);

        }

        public void HideNativeBanner(string adUnitId)
        {
            TPNativeBanner.Call("hideBanner", adUnitId);

        }

        public void DisplayNativeBanner(string adUnitId)
        {
            TPNativeBanner.Call("displayBanner", adUnitId);

        }

        public void DestroyNativeBanner(string adUnitId)
        {
            TPNativeBanner.Call("destroyBanner", adUnitId);

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPNativeBanner.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.nativebanner.TPNativeBannerListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TradplusNativeBannerAndroid.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TradplusNativeBannerAndroid.Instance().OnNativeBannerIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusNativeBannerAndroid.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TradplusNativeBannerAndroid.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName, progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusNativeBannerAndroid.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusNativeBannerAndroid.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusNativeBannerAndroid.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusNativeBannerAndroid.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TradplusNativeBannerAndroid()
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