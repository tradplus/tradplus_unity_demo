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
    public class TradplusSplashiOS
    {
        private static TradplusSplashiOS _instance;

        public static TradplusSplashiOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusSplashiOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusLoadSplashAd(string adUnitId,string customMap,string localParams, bool openAutoLoadCallback, float maxWaitTime);
        public void LoadSplashAd(string adUnitId, TPSplashExtra extra)
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
            TradplusLoadSplashAd(adUnitId, customMapString, localParamsString, extra.openAutoLoadCallback, extra.maxWaitTime);
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowSplashAd(string adUnitId, string sceneId);
        public void ShowSplashAd(string adUnitId,string sceneId)
        {
            TradplusShowSplashAd(adUnitId, sceneId);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusSplashAdReady(string adUnitId);
        public bool SplashAdReady(string adUnitId)
        {
            return TradplusSplashAdReady(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusEntrySplashAdScenario(string adUnitId, string sceneId);
        public void EntrySplashAdScenario(string adUnitId, string sceneId)
        {
            TradplusEntrySplashAdScenario(adUnitId, sceneId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomAdInfoSplash(string adUnitId, string customAdInfo);
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            if (customAdInfo != null)
            {
                string customAdInfoString = Json.Serialize(customAdInfo);
                if (customAdInfoString != null)
                {
                    TradplusSetCustomAdInfoSplash(adUnitId, customAdInfoString);
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
            TradplusSplashSetCallbacks(
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
            null,
            null
           );
        }

        //回调部分

        //注册回调
        [DllImport("__Internal")]
        private static extern void TradplusSplashSetCallbacks(
            TPSplashLoadedCallback adLoadedCallback,
            TPSplashLoadFailedCallback adLoadFailedCallback,
            TPSplashImpressionCallback adImpressionCallback,
            TPSplashShowFailedCallback adShowFailedCallback,
            TPSplashClickedCallback adClickedCallback,
            TPSplashClosedCallback adClosedCallback,
            TPSplashStartLoadCallback adStartLoadCallback,
            TPSplashBiddingStartCallback adBiddingStartCallback,
            TPSplashBiddingEndCallback adBiddingEndCallback,
            TPSplashOneLayerStartLoadCallback adOneLayerStartLoadCallback,
            TPSplashOneLayerLoadedCallback adOneLayerLoadedCallback,
            TPSplashOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
            TPSplashAllLoadedCallback adAllLoadedCallback,
            TPSplashAdIsLoadingCallback adIsLoadingCallback,
            TPSplashZoomOutStartCallback adZoomOutStartCallback,
            TPSplashZoomOutEndCallback adZoomOutEndCallback,
            TPSplashSkipCallback adSkipCallback
        );
        public TradplusSplashiOS()
        {
            TradplusSplashSetCallbacks(
                SplashLoadedCallback,
                SplashLoadFailedCallback,
                SplashImpressionCallback, 
                SplashShowFailedCallback,
                SplashClickedCallback,
                SplashClosedCallback,
                SplashStartLoadCallback,
                SplashBiddingStartCallback,
                SplashBiddingEndCallback,
                SplashOneLayerStartLoadCallback,
                SplashOneLayerLoadedCallback,
                SplashOneLayerLoadFailedCallback,
                SplashAllLoadedCallback,
                SplashAdIsLoadingCallback,
                SplashZoomOutStartCallback,
                SplashZoomOutEndCallback,
                SplashSkipCallback
                );
        }

        // OnSplashLoaded
        public event Action<string, Dictionary<string, object>> OnSplashLoaded;

        internal delegate void TPSplashLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashLoadedCallback))]
        private static void SplashLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashLoaded(adUnitId, adInfo);
            }
        }

        //OnSplashLoadFailed
        public event Action<string, Dictionary<string, object>> OnSplashLoadFailed;

        internal delegate void TPSplashLoadFailedCallback(string adUnitId, string errorStr);
        [MonoPInvokeCallback(typeof(TPSplashLoadFailedCallback))]
        private static void SplashLoadFailedCallback(string adUnitId, string errorStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashLoadFailed != null)
            {
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashLoadFailed(adUnitId, error);
            }
        }

        //OnSplashImpression
        public event Action<string, Dictionary<string, object>> OnSplashImpression;

        internal delegate void TPSplashImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashImpressionCallback))]
        private static void SplashImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashImpression(adUnitId, adInfo);
            }
        }

        //OnSplashShowFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashShowFailed;

        internal delegate void TPSplashShowFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPSplashShowFailedCallback))]
        private static void SplashShowFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashShowFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashShowFailed(adUnitId, adInfo, error);
            }
        }

        //OnSplashClicked
        public event Action<string, Dictionary<string, object>> OnSplashClicked;

        internal delegate void TPSplashClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashClickedCallback))]
        private static void SplashClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashClicked(adUnitId, adInfo);
            }
        }

        //OnSplashClosed
        public event Action<string, Dictionary<string, object>> OnSplashClosed;

        internal delegate void TPSplashClosedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashClosedCallback))]
        private static void SplashClosedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashClosed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashClosed(adUnitId, adInfo);
            }
        }

        //OnSplashStartLoad
        public event Action<string, Dictionary<string, object>> OnSplashStartLoad;

        internal delegate void TPSplashStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashStartLoadCallback))]
        private static void SplashStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashStartLoad(adUnitId, adInfo);
            }
        }

        //OnSplashBiddingStart
        public event Action<string, Dictionary<string, object>> OnSplashBiddingStart;

        internal delegate void TPSplashBiddingStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashBiddingStartCallback))]
        private static void SplashBiddingStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashBiddingStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashBiddingStart(adUnitId, adInfo);
            }
        }

        //OnSplashBiddingEnd
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashBiddingEnd;

        internal delegate void TPSplashBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPSplashBiddingEndCallback))]
        private static void SplashBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashBiddingEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashBiddingEnd(adUnitId, adInfo, error);
            }
        }

        //OnSplashOneLayerStartLoad
        public event Action<string, Dictionary<string, object>> OnSplashOneLayerStartLoad;

        internal delegate void TPSplashOneLayerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashOneLayerStartLoadCallback))]
        private static void SplashOneLayerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashOneLayerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashOneLayerStartLoad(adUnitId, adInfo);
            }
        }

        //OnSplashOneLayerLoaded
        public event Action<string, Dictionary<string, object>> OnSplashOneLayerLoaded;

        internal delegate void TPSplashOneLayerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashOneLayerLoadedCallback))]
        private static void SplashOneLayerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashOneLayerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashOneLayerLoaded(adUnitId, adInfo);
            }
        }

        //OnSplashOneLayerLoadFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashOneLayerLoadFailed;

        internal delegate void TPSplashOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPSplashOneLayerLoadFailedCallback))]
        private static void SplashOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashOneLayerLoadFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashOneLayerLoadFailed(adUnitId, adInfo, error);
            }
        }

        //OnSplashZoomOutStart
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutStart;

        internal delegate void TPSplashZoomOutStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashZoomOutStartCallback))]
        private static void SplashZoomOutStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashZoomOutStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashZoomOutStart(adUnitId, adInfo);
            }
        }

        //OnSplashZoomOutEnd
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutEnd;

        internal delegate void TPSplashZoomOutEndCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashZoomOutEndCallback))]
        private static void SplashZoomOutEndCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashZoomOutEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashZoomOutEnd(adUnitId, adInfo);
            }
        }

        //OnSplashSkip
        public event Action<string, Dictionary<string, object>> OnSplashSkip;

        internal delegate void TPSplashSkipCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPSplashSkipCallback))]
        private static void SplashSkipCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusSplashiOS.Instance().OnSplashSkip != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusSplashiOS.Instance().OnSplashSkip(adUnitId, adInfo);
            }
        }

        //OnSplashAllLoaded
        public event Action<string, bool> OnSplashAllLoaded;

        internal delegate void TPSplashAllLoadedCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPSplashAllLoadedCallback))]
        private static void SplashAllLoadedCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusSplashiOS.Instance().OnSplashAllLoaded != null)
            {
                TradplusSplashiOS.Instance().OnSplashAllLoaded(adUnitId, isSuccess);
            }
        }

        //OnSplashIsLoading
        public event Action<string> OnSplashIsLoading;

        internal delegate void TPSplashAdIsLoadingCallback(string adUnitId);
        [MonoPInvokeCallback(typeof(TPSplashAdIsLoadingCallback))]
        private static void SplashAdIsLoadingCallback(string adUnitId)
        {
            if (TradplusSplashiOS.Instance().OnSplashIsLoading != null)
            {
                TradplusSplashiOS.Instance().OnSplashIsLoading(adUnitId);
            }
        }

    }
}

#endif