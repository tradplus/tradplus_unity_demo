#if UNITY_IOS

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.iOS
{
    public class TradplusInterstitialiOS
    {
        private static TradplusInterstitialiOS _instance;

        public static TradplusInterstitialiOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusInterstitialiOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusLoadInterstitialAd(string adUnitId,string customMap,string localParams, bool openAutoLoadCallback, float maxWaitTime);
        public void LoadInterstitialAd(string adUnitId, TPInterstitialExtra extra)
        {
            string customMapString = null;
            if(extra.customMap != null)
            {
                customMapString = Json.Serialize(extra.customMap);
            }
            string localParamsString = null;
            if (extra.localParams != null)
            {
                localParamsString = Json.Serialize(extra.localParams);
            }
            TradplusLoadInterstitialAd(adUnitId, customMapString, localParamsString,extra.openAutoLoadCallback,extra.maxWaitTime);
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowInterstitialAd(string adUnitId, string sceneId);
        public void ShowInterstitialAd(string adUnitId, string sceneId)
        {
            TradplusShowInterstitialAd(adUnitId, sceneId);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusInterstitialAdReady(string adUnitId);
        public bool InterstitialAdReady(string adUnitId)
        {
            return TradplusInterstitialAdReady(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusEntryInterstitialAdScenario(string adUnitId, string sceneId);
        public void EntryInterstitialAdScenario(string adUnitId, string sceneId)
        {
            TradplusEntryInterstitialAdScenario(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomAdInfoInterstitial(string adUnitId, string customAdInfo);
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            if (customAdInfo != null)
            {
                string customAdInfoString = Json.Serialize(customAdInfo);
                if (customAdInfoString != null)
                {
                    TradplusSetCustomAdInfoInterstitial(adUnitId, customAdInfoString);
                }
                else
                {
                    Debug.LogError("customAdInfo wrong format");
                }
            }
            else
            {
                Debug.LogError("customAdInfo is null");
            }
        }

        public void ClearCallback()
        {
            TradplusInterstitialSetCallbacks(
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
           );
        }

        //回调部分

        //注册回调
        [DllImport("__Internal")]
        private static extern void TradplusInterstitialSetCallbacks(
            TPInterstitialLoadedCallback adLoadedCallback,
            TPInterstitialLoadFailedCallback adLoadFailedCallback,
            TPInterstitialImpressionCallback adImpressionCallback,
            TPInterstitialShowFailedCallback adShowFailedCallback,
            TPInterstitialClickedCallback adClickedCallback,
            TPInterstitialClosedCallback adClosedCallback,
            TPInterstitialStartLoadCallback adStartLoadCallback,
            TPInterstitialBiddingStartCallback adBiddingStartCallback,
            TPInterstitialBiddingEndCallback adBiddingEndCallback,
            TPInterstitialOneLayerStartLoadCallback adOneLayerStartLoadCallback,
            TPInterstitialOneLayerLoadedCallback adOneLayerLoadedCallback,
            TPInterstitialOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
            TPInterstitialVideoPlayStartCallback adVideoPlayStartCallback,
            TPInterstitialVideoPlayEndCallback adVideoPlayEndCallback,
            TPInterstitialAllLoadedCallback adAllLoadedCallback,
            TPInterstitialAdIsLoadingCallback adIsLoadingCallback
        );
        public TradplusInterstitialiOS()
        {
            TradplusInterstitialSetCallbacks(
                InterstitialLoadedCallback,
                InterstitialLoadFailedCallback,
                InterstitialImpressionCallback, 
                InterstitialShowFailedCallback,
                InterstitialClickedCallback,
                InterstitialClosedCallback,
                InterstitialStartLoadCallback,
                InterstitialBiddingStartCallback,
                InterstitialBiddingEndCallback,
                InterstitialOneLayerStartLoadCallback,
                InterstitialOneLayerLoadedCallback,
                InterstitialOneLayerLoadFailedCallback,
                InterstitialVideoPlayStartCallback,
                InterstitialVideoPlayEndCallback,
                InterstitialAllLoadedCallback,
                InterstitialAdIsLoadingCallback
                );
        }

        // OnInterstitialLoaded
        public event Action<string, Dictionary<string, object>> OnInterstitialLoaded;

        internal delegate void TPInterstitialLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialLoadedCallback))]
        private static void InterstitialLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialLoaded(adUnitId, adInfo);
            }
        }

        //OnInterstitialLoadFailed
        public event Action<string, Dictionary<string, object>> OnInterstitialLoadFailed;

        internal delegate void TPInterstitialLoadFailedCallback(string adUnitId, string errorStr);
        [MonoPInvokeCallback(typeof(TPInterstitialLoadFailedCallback))]
        private static void InterstitialLoadFailedCallback(string adUnitId, string errorStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialLoadFailed != null)
            {
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialLoadFailed(adUnitId, error);
            }
        }

        //OnInterstitialImpression
        public event Action<string, Dictionary<string, object>> OnInterstitialImpression;

        internal delegate void TPInterstitialImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialImpressionCallback))]
        private static void InterstitialImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialImpression(adUnitId, adInfo);
            }
        }

        //OnInterstitialShowFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialShowFailed;

        internal delegate void TPInterstitialShowFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPInterstitialShowFailedCallback))]
        private static void InterstitialShowFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialShowFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialShowFailed(adUnitId, adInfo, error);
            }
        }

        //OnInterstitialClicked
        public event Action<string, Dictionary<string, object>> OnInterstitialClicked;

        internal delegate void TPInterstitialClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialClickedCallback))]
        private static void InterstitialClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialClicked(adUnitId, adInfo);
            }
        }

        //OnInterstitialClosed
        public event Action<string, Dictionary<string, object>> OnInterstitialClosed;

        internal delegate void TPInterstitialClosedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialClosedCallback))]
        private static void InterstitialClosedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialClosed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialClosed(adUnitId, adInfo);
            }
        }

        //OnInterstitialStartLoad
        public event Action<string, Dictionary<string, object>> OnInterstitialStartLoad;

        internal delegate void TPInterstitialStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialStartLoadCallback))]
        private static void InterstitialStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialStartLoad(adUnitId, adInfo);
            }
        }

        //OnInterstitialBiddingStart
        public event Action<string, Dictionary<string, object>> OnInterstitialBiddingStart;

        internal delegate void TPInterstitialBiddingStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialBiddingStartCallback))]
        private static void InterstitialBiddingStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialBiddingStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialBiddingStart(adUnitId, adInfo);
            }
        }

        //OnInterstitialBiddingEnd
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialBiddingEnd;

        internal delegate void TPInterstitialBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPInterstitialBiddingEndCallback))]
        private static void InterstitialBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialBiddingEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialBiddingEnd(adUnitId, adInfo, error);
            }
        }

        //OnInterstitialOneLayerStartLoad
        public event Action<string, Dictionary<string, object>> OnInterstitialOneLayerStartLoad;

        internal delegate void TPInterstitialOneLayerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialOneLayerStartLoadCallback))]
        private static void InterstitialOneLayerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialOneLayerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialOneLayerStartLoad(adUnitId, adInfo);
            }
        }

        //OnInterstitialOneLayerLoaded
        public event Action<string, Dictionary<string, object>> OnInterstitialOneLayerLoaded;

        internal delegate void TPInterstitialOneLayerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialOneLayerLoadedCallback))]
        private static void InterstitialOneLayerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialOneLayerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialOneLayerLoaded(adUnitId, adInfo);
            }
        }

        //OnInterstitialOneLayerLoadFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialOneLayerLoadFailed;

        internal delegate void TPInterstitialOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPInterstitialOneLayerLoadFailedCallback))]
        private static void InterstitialOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialOneLayerLoadFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialOneLayerLoadFailed(adUnitId, adInfo, error);
            }
        }

        //OnInterstitialVideoPlayStart
        public event Action<string, Dictionary<string, object>> OnInterstitialVideoPlayStart;

        internal delegate void TPInterstitialVideoPlayStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialVideoPlayStartCallback))]
        private static void InterstitialVideoPlayStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialVideoPlayStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialVideoPlayStart(adUnitId, adInfo);
            }
        }

        //OnInterstitialVideoPlayEnd
        public event Action<string, Dictionary<string, object>> OnInterstitialVideoPlayEnd;

        internal delegate void TPInterstitialVideoPlayEndCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPInterstitialVideoPlayEndCallback))]
        private static void InterstitialVideoPlayEndCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialVideoPlayEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusInterstitialiOS.Instance().OnInterstitialVideoPlayEnd(adUnitId, adInfo);
            }
        }

        //OnInterstitialAllLoaded
        public event Action<string, bool> OnInterstitialAllLoaded;

        internal delegate void TPInterstitialAllLoadedCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPInterstitialAllLoadedCallback))]
        private static void InterstitialAllLoadedCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialAllLoaded != null)
            {
                TradplusInterstitialiOS.Instance().OnInterstitialAllLoaded(adUnitId, isSuccess);
            }
        }

        //OnInterstitialIsLoading
        public event Action<string> OnInterstitialIsLoading;

        internal delegate void TPInterstitialAdIsLoadingCallback(string adUnitId);
        [MonoPInvokeCallback(typeof(TPInterstitialAdIsLoadingCallback))]
        private static void InterstitialAdIsLoadingCallback(string adUnitId)
        {
            if (TradplusInterstitialiOS.Instance().OnInterstitialIsLoading != null)
            {
                TradplusInterstitialiOS.Instance().OnInterstitialIsLoading(adUnitId);
            }
        }

    }
}

#endif