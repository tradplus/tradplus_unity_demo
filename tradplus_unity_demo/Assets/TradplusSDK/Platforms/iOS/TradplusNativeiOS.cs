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

    public class TradplusNativeiOS
    {

        private static TradplusNativeiOS _instance;

        public static TradplusNativeiOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusNativeiOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusLoadNativeAd(string adUnitId, float x, float y, float width, float height, int adPosition, string customMap, string localParams, bool openAutoLoadCallback, float maxWaitTime);
        public void LoadNativeAd(string adUnitId, TPNativeExtra extra)
        {
            string customMapString = null;
            if (extra.customMap != null)
            {
                customMapString = Json.Serialize(extra.customMap);
            }
            string localParamsString = null;
            if (extra.localParams != null)
            {
                localParamsString = Json.Serialize(extra.localParams);
            }
            TradplusLoadNativeAd(adUnitId,extra.x,extra.y,extra.width,extra.height,(int)extra.adPosition, customMapString, localParamsString,extra.openAutoLoadCallback,extra.maxWaitTime);
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowNativeAd(string adUnitId, string sceneId,string className);
        public void ShowNativeAd(string adUnitId, string sceneId, string className)
        {
            TradplusShowNativeAd(adUnitId, sceneId, className);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusNativeAdReady(string adUnitId);
        public bool NativeAdReady(string adUnitId)
        {
            return TradplusNativeAdReady(adUnitId); ;
        }

        [DllImport("__Internal")]
        private static extern void TradplusEntryNativeAdScenario(string adUnitId, string sceneId);
        public void EntryNativeAdScenario(string adUnitId, string sceneId)
        {
            TradplusEntryNativeAdScenario(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusHideNative(string adUnitId);
        public void HideNative(string adUnitId)
        {
            TradplusHideNative(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusDisplayNative(string adUnitId);
        public void DisplayNative(string adUnitId)
        {
            TradplusDisplayNative(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusDestroyNative(string adUnitId);
        public void DestroyNative(string adUnitId)
        {
            TradplusDestroyNative(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomAdInfoNative(string adUnitId, string customAdInfo);
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            if (customAdInfo != null)
            {
                string customAdInfoString = Json.Serialize(customAdInfo);
                if (customAdInfoString != null)
                {
                    TradplusSetCustomAdInfoNative(adUnitId, customAdInfoString);
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
            TradplusNativeSetCallbacks(
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
        private static extern void TradplusNativeSetCallbacks(
            TPNativeLoadedCallback adLoadedCallback,
            TPNativeLoadFailedCallback adLoadFailedCallback,
            TPNativeImpressionCallback adImpressionCallback,
            TPNativeShowFailedCallback adShowFailedCallback,
            TPNativeClickedCallback adClickedCallback,
            TPNativeClosedCallback adClosedCallback,
            TPNativeStartLoadCallback adStartLoadCallback,
            TPNativeBiddingStartCallback adBiddingStartCallback,
            TPNativeBiddingEndCallback adBiddingEndCallback,
            TPNativeOneLayerStartLoadCallback adOneLayerStartLoadCallback,
            TPNativeOneLayerLoadedCallback adOneLayerLoadedCallback,
            TPNativeOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
            TPNativeVideoPlayStartCallback adVideoPlayStartCallback,
            TPNativeVideoPlayEndCallback adVideoPlayEndCallback,
            TPNativeAllLoadedCallback adAllLoadedCallback,
            TPNativeAdIsLoadingCallback adIsLoadingCallback
        );
        public TradplusNativeiOS()
        {
            TradplusNativeSetCallbacks(
                NativeLoadedCallback,
                NativeLoadFailedCallback,
                NativeImpressionCallback,
                NativeShowFailedCallback,
                NativeClickedCallback,
                NativeClosedCallback,
                NativeStartLoadCallback,
                NativeBiddingStartCallback,
                NativeBiddingEndCallback,
                NativeOneLayerStartLoadCallback,
                NativeOneLayerLoadedCallback,
                NativeOneLayerLoadFailedCallback,
                NativeVideoPlayStartCallback,
                NativeVideoPlayEndCallback,
                NativeAllLoadedCallback,
                NativeAdIsLoadingCallback
                );
        }

        // OnNativeLoaded
        public event Action<string, Dictionary<string, object>> OnNativeLoaded;

        internal delegate void TPNativeLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeLoadedCallback))]
        private static void NativeLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeLoaded(adUnitId, adInfo);
            }
        }

        //OnNativeLoadFailed
        public event Action<string, Dictionary<string, object>> OnNativeLoadFailed;

        internal delegate void TPNativeLoadFailedCallback(string adUnitId, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeLoadFailedCallback))]
        private static void NativeLoadFailedCallback(string adUnitId, string errorStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeLoadFailed != null)
            {
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeLoadFailed(adUnitId, error);
            }
        }

        //OnNativeImpression
        public event Action<string, Dictionary<string, object>> OnNativeImpression;

        internal delegate void TPNativeImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeImpressionCallback))]
        private static void NativeImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeImpression(adUnitId, adInfo);
            }
        }

        //OnNativeShowFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeShowFailed;

        internal delegate void TPNativeShowFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeShowFailedCallback))]
        private static void NativeShowFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeShowFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeShowFailed(adUnitId, adInfo, error);
            }
        }

        //OnNativeClicked
        public event Action<string, Dictionary<string, object>> OnNativeClicked;

        internal delegate void TPNativeClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeClickedCallback))]
        private static void NativeClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeClicked(adUnitId, adInfo);
            }
        }

        //OnNativeClosed
        public event Action<string, Dictionary<string, object>> OnNativeClosed;

        internal delegate void TPNativeClosedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeClosedCallback))]
        private static void NativeClosedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeClosed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeClosed(adUnitId, adInfo);
            }
        }

        //OnNativeStartLoad
        public event Action<string, Dictionary<string, object>> OnNativeStartLoad;

        internal delegate void TPNativeStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeStartLoadCallback))]
        private static void NativeStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeStartLoad(adUnitId, adInfo);
            }
        }

        //OnNativeBiddingStart
        public event Action<string, Dictionary<string, object>> OnNativeBiddingStart;

        internal delegate void TPNativeBiddingStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBiddingStartCallback))]
        private static void NativeBiddingStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeBiddingStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeBiddingStart(adUnitId, adInfo);
            }
        }

        //OnNativeBiddingEnd
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBiddingEnd;

        internal delegate void TPNativeBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeBiddingEndCallback))]
        private static void NativeBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeBiddingEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeBiddingEnd(adUnitId, adInfo, error);
            }
        }

        //OnNativeOneLayerStartLoad
        public event Action<string, Dictionary<string, object>> OnNativeOneLayerStartLoad;

        internal delegate void TPNativeOneLayerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeOneLayerStartLoadCallback))]
        private static void NativeOneLayerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeOneLayerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeOneLayerStartLoad(adUnitId, adInfo);
            }
        }

        //OnNativeOneLayerLoaded
        public event Action<string, Dictionary<string, object>> OnNativeOneLayerLoaded;

        internal delegate void TPNativeOneLayerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeOneLayerLoadedCallback))]
        private static void NativeOneLayerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeOneLayerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeOneLayerLoaded(adUnitId, adInfo);
            }
        }

        //OnNativeOneLayerLoadFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeOneLayerLoadFailed;

        internal delegate void TPNativeOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeOneLayerLoadFailedCallback))]
        private static void NativeOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeOneLayerLoadFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeOneLayerLoadFailed(adUnitId, adInfo, error);
            }
        }

        //OnNativeVideoPlayStart
        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayStart;

        internal delegate void TPNativeVideoPlayStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeVideoPlayStartCallback))]
        private static void NativeVideoPlayStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeVideoPlayStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeVideoPlayStart(adUnitId, adInfo);
            }
        }

        //OnNativeVideoPlayEnd
        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayEnd;

        internal delegate void TPNativeVideoPlayEndCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeVideoPlayEndCallback))]
        private static void NativeVideoPlayEndCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeiOS.Instance().OnNativeVideoPlayEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeiOS.Instance().OnNativeVideoPlayEnd(adUnitId, adInfo);
            }
        }

        //OnNativeAllLoaded
        public event Action<string, bool> OnNativeAllLoaded;

        internal delegate void TPNativeAllLoadedCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPNativeAllLoadedCallback))]
        private static void NativeAllLoadedCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusNativeiOS.Instance().OnNativeAllLoaded != null)
            {
                TradplusNativeiOS.Instance().OnNativeAllLoaded(adUnitId, isSuccess);
            }
        }

        //OnNativeIsLoading
        public event Action<string> OnNativeIsLoading;

        internal delegate void TPNativeAdIsLoadingCallback(string adUnitId);
        [MonoPInvokeCallback(typeof(TPNativeAdIsLoadingCallback))]
        private static void NativeAdIsLoadingCallback(string adUnitId)
        {
            if (TradplusNativeiOS.Instance().OnNativeIsLoading != null)
            {
                TradplusNativeiOS.Instance().OnNativeIsLoading(adUnitId);
            }
        }
    }

}

#endif