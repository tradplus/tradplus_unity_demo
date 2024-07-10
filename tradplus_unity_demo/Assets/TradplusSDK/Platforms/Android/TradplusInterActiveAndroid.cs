
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Android
{

    public class TradplusInterActiveAndroid
    {
        private static AndroidJavaClass TPInterActiveClass = new AndroidJavaClass("com.tradplus.unity.plugin.interactive.TPInterActiveManager");
        private AndroidJavaObject TPInterActive = TPInterActiveClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TradplusInterActiveAndroid _instance;

        public static TradplusInterActiveAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusInterActiveAndroid();
            }
            return _instance;
        }

        public void LoadInterActiveAd(string adUnitId, TPInterActiveExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();

            info.Add("width", extra.width);
            info.Add("height", extra.height);
            info.Add("x", extra.x);
            info.Add("y", extra.y);
            info.Add("customMap", extra.customMap);
            info.Add("localParams", extra.localParams);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);



            TPInterActive.Call("loadAd", adUnitId, Json.Serialize(info), loadListenerAdapter);

        }

        public void ShowInterActiveAd(string adUnitId, string sceneId)
        {
            TPInterActive.Call("showAd", adUnitId, sceneId);

        }

        public bool InterActiveAdReady(string adUnitId)
        {
            return TPInterActive.Call<bool>("isReady", adUnitId);
        }

        public void HideInterActive(string adUnitId)
        {
            TPInterActive.Call("hideInterActive", adUnitId);

        }

        public void DisplayInterActive(string adUnitId)
        {
            TPInterActive.Call("displayInterActive", adUnitId);

        }


        public void DestroyInterActive(string adUnitId)
        {
            TPInterActive.Call("destroyInterActive", adUnitId);

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPInterActive.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.interactive.TPInterActiveListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
              //  TradplusInterActiveAndroid.Instance().OnInterActiveVideoError(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TradplusInterActiveAndroid.Instance().OnInterActiveIsLoading(unitId);

            }

        }

        public TradplusInterActiveAndroid()
        {
        }

        public event Action<string, Dictionary<string, object>> OnInterActiveLoaded;

        public event Action<string, Dictionary<string, object>> OnInterActiveLoadFailed;

        public event Action<string, Dictionary<string, object>> OnInterActiveImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterActiveShowFailed;

        public event Action<string, Dictionary<string, object>> OnInterActiveClicked;

        public event Action<string, Dictionary<string, object>> OnInterActiveClosed;

        public event Action<string, Dictionary<string, object>> OnInterActiveStartLoad;

        public event Action<string, Dictionary<string, object>> OnInterActiveBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterActiveBiddingEnd;

        public event Action<string> OnInterActiveIsLoading;

        public event Action<string, Dictionary<string, object>> OnInterActiveOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnInterActiveOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterActiveOneLayerLoadFailed;

        public event Action<string, Dictionary<string, object>> OnInterActiveVideoPlayStart;

        public event Action<string, Dictionary<string, object>> OnInterActiveVideoPlayEnd;

        public event Action<string, bool> OnInterActiveAllLoaded;

    }

}