
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Android
{
    public class TradplusBannerAndroid
    {
        private static AndroidJavaClass TPBannerClass = new AndroidJavaClass("com.tradplus.unity.plugin.banner.TPBannerManager");
        private AndroidJavaObject TPBanner = TPBannerClass.CallStatic<AndroidJavaObject>("getInstance");

        private static TradplusBannerAndroid _instance;

        public static TradplusBannerAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusBannerAndroid();
            }
            return _instance;
        }

        public void LoadBannerAd(string adUnitId, string sceneId , TPBannerExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();

            info.Add("width", extra.width);
            info.Add("height", extra.height);
            info.Add("x", extra.x);
            info.Add("y", extra.y);
            info.Add("customMap", extra.customMap);
            info.Add("adPosition",(int)extra.adPosition);
            info.Add("closeAutoShow", extra.closeAutoShow);
            info.Add("closeAutoDestroy", extra.closeAutoDestroy);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("className", extra.className);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);
            info.Add("backgroundColor", extra.backgroundColor);


            info.Add("localParams", extra.localParams);


            TPBanner.Call("loadAd", adUnitId, sceneId,Json.Serialize(info), loadListenerAdapter);
        }

        public void ShowBannerAd(string adUnitId, string sceneId)
        {
            TPBanner.Call("showAd", adUnitId, sceneId);

        }


        public bool BannerAdReady(string adUnitId)
        {
            return TPBanner.Call<bool>("isReady", adUnitId);
        }

        public void EntryBannerAdScenario(string adUnitId, string sceneId)
        {
            TPBanner.Call("entryAdScenario", adUnitId,sceneId);

        }

        public void HideBanner(string adUnitId)
        {
            TPBanner.Call("hideBanner", adUnitId);

        }

        public void DisplayBanner(string adUnitId)
        {
            TPBanner.Call("displayBanner", adUnitId);

        }


        public void DestroyBanner(string adUnitId)
        {
            TPBanner.Call("destroyBanner", adUnitId);

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPBanner.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.banner.TPBannerListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                TradplusBannerAndroid.Instance().OnBannerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TradplusBannerAndroid.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TradplusBannerAndroid.Instance().OnBannerAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TradplusBannerAndroid.Instance().OnBannerStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TradplusBannerAndroid.Instance().OnBannerBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TradplusBannerAndroid.Instance().OnBannerBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TradplusBannerAndroid.Instance().OnBannerIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusBannerAndroid.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TradplusBannerAndroid.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName, progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusBannerAndroid.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusBannerAndroid.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusBannerAndroid.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusBannerAndroid.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }

        }

        public TradplusBannerAndroid()
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