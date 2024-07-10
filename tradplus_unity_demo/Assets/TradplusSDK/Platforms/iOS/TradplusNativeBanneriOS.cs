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

    public class TradplusNativeBanneriOS
    {

        private static TradplusNativeBanneriOS _instance;

        public static TradplusNativeBanneriOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusNativeBanneriOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusLoadNativeBannerAd(string adUnitId, bool closeAutoShow, float x, float y, float width, float height, int adPosition, string sceneId, string customMap, string className,string localParams, bool openAutoLoadCallback, float maxWaitTime,string backgroundColor);
        public void LoadNativeBannerAd(string adUnitId, string sceneId, TPNativeBannerExtra extra)
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
            TradplusLoadNativeBannerAd(adUnitId,extra.closeAutoShow,extra.x,extra.y,extra.width,extra.height,(int)extra.adPosition,sceneId, customMapString,extra.className,localParamsString,extra.openAutoLoadCallback,extra.maxWaitTime,extra.backgroundColor);
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowNativeBannerAd(string adUnitId, string sceneId);
        public void ShowNativeBannerAd(string adUnitId, string sceneId)
        {
            TradplusShowNativeBannerAd(adUnitId, sceneId);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusNativeBannerAdReady(string adUnitId);
        public bool NativeBannerAdReady(string adUnitId)
        {
            return TradplusNativeBannerAdReady(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusEntryNativeBannerAdScenario(string adUnitId, string sceneId);
        public void EntryNativeBannerAdScenario(string adUnitId, string sceneId)
        {
            TradplusEntryNativeBannerAdScenario(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusHideNativeBanner(string adUnitId);
        public void HideNativeBanner(string adUnitId)
        {
            TradplusHideNativeBanner(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusDisplayNativeBanner(string adUnitId);
        public void DisplayNativeBanner(string adUnitId)
        {
            TradplusDisplayNativeBanner(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusDestroyNativeBanner(string adUnitId);
        public void DestroyNativeBanner(string adUnitId)
        {
            TradplusDestroyNativeBanner(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomAdInfoNativeBanner(string adUnitId, string customAdInfo);
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            if (customAdInfo != null)
            {
                string customAdInfoString = Json.Serialize(customAdInfo);
                if (customAdInfoString != null)
                {
                    TradplusSetCustomAdInfoNativeBanner(adUnitId, customAdInfoString);
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
            TradplusNativeBannerSetCallbacks(
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
        private static extern void TradplusNativeBannerSetCallbacks(
            TPNativeBannerLoadedCallback adLoadedCallback,
            TPNativeBannerLoadFailedCallback adLoadFailedCallback,
            TPNativeBannerImpressionCallback adImpressionCallback,
            TPNativeBannerShowFailedCallback adShowFailedCallback,
            TPNativeBannerClickedCallback adClickedCallback,
            TPNativeBannerClosedCallback adClosedCallback,
            TPNativeBannerStartLoadCallback adStartLoadCallback,
            TPNativeBannerBiddingStartCallback adBiddingStartCallback,
            TPNativeBannerBiddingEndCallback adBiddingEndCallback,
            TPNativeBannerOneLayerStartLoadCallback adOneLayerStartLoadCallback,
            TPNativeBannerOneLayerLoadedCallback adOneLayerLoadedCallback,
            TPNativeBannerOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
            TPNativeBannerAllLoadedCallback adAllLoadedCallback,
            TPNativeBannerAdIsLoadingCallback adIsLoadingCallback
        );
        public TradplusNativeBanneriOS()
        {
            TradplusNativeBannerSetCallbacks(
                NativeBannerLoadedCallback,
                NativeBannerLoadFailedCallback,
                NativeBannerImpressionCallback,
                NativeBannerShowFailedCallback,
                NativeBannerClickedCallback,
                NativeBannerClosedCallback,
                NativeBannerStartLoadCallback,
                NativeBannerBiddingStartCallback,
                NativeBannerBiddingEndCallback,
                NativeBannerOneLayerStartLoadCallback,
                NativeBannerOneLayerLoadedCallback,
                NativeBannerOneLayerLoadFailedCallback,
                NativeBannerAllLoadedCallback,
                NativeBannerAdIsLoadingCallback
                );
        }

        // OnNativeBannerLoaded
        public event Action<string, Dictionary<string, object>> OnNativeBannerLoaded;

        internal delegate void TPNativeBannerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerLoadedCallback))]
        private static void NativeBannerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerLoaded(adUnitId, adInfo);
            }
        }

        //OnNativeBannerLoadFailed
        public event Action<string, Dictionary<string, object>> OnNativeBannerLoadFailed;

        internal delegate void TPNativeBannerLoadFailedCallback(string adUnitId, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerLoadFailedCallback))]
        private static void NativeBannerLoadFailedCallback(string adUnitId, string errorStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerLoadFailed != null)
            {
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerLoadFailed(adUnitId, error);
            }
        }

        //OnNativeBannerImpression
        public event Action<string, Dictionary<string, object>> OnNativeBannerImpression;

        internal delegate void TPNativeBannerImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerImpressionCallback))]
        private static void NativeBannerImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerImpression(adUnitId, adInfo);
            }
        }

        //OnNativeBannerShowFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerShowFailed;

        internal delegate void TPNativeBannerShowFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerShowFailedCallback))]
        private static void NativeBannerShowFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerShowFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerShowFailed(adUnitId, adInfo, error);
            }
        }

        //OnNativeBannerClicked
        public event Action<string, Dictionary<string, object>> OnNativeBannerClicked;

        internal delegate void TPNativeBannerClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerClickedCallback))]
        private static void NativeBannerClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerClicked(adUnitId, adInfo);
            }
        }

        //OnNativeBannerClosed
        public event Action<string, Dictionary<string, object>> OnNativeBannerClosed;

        internal delegate void TPNativeBannerClosedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerClosedCallback))]
        private static void NativeBannerClosedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerClosed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerClosed(adUnitId, adInfo);
            }
        }

        //OnNativeBannerStartLoad
        public event Action<string, Dictionary<string, object>> OnNativeBannerStartLoad;

        internal delegate void TPNativeBannerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerStartLoadCallback))]
        private static void NativeBannerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerStartLoad(adUnitId, adInfo);
            }
        }

        //OnNativeBannerBiddingStart
        public event Action<string, Dictionary<string, object>> OnNativeBannerBiddingStart;

        internal delegate void TPNativeBannerBiddingStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerBiddingStartCallback))]
        private static void NativeBannerBiddingStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerBiddingStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerBiddingStart(adUnitId, adInfo);
            }
        }

        //OnNativeBannerBiddingEnd
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerBiddingEnd;

        internal delegate void TPNativeBannerBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerBiddingEndCallback))]
        private static void NativeBannerBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerBiddingEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerBiddingEnd(adUnitId, adInfo, error);
            }
        }

        //OnNativeBannerOneLayerStartLoad
        public event Action<string, Dictionary<string, object>> OnNativeBannerOneLayerStartLoad;

        internal delegate void TPNativeBannerOneLayerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerOneLayerStartLoadCallback))]
        private static void NativeBannerOneLayerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerOneLayerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerOneLayerStartLoad(adUnitId, adInfo);
            }
        }

        //OnNativeBannerOneLayerLoaded
        public event Action<string, Dictionary<string, object>> OnNativeBannerOneLayerLoaded;

        internal delegate void TPNativeBannerOneLayerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerOneLayerLoadedCallback))]
        private static void NativeBannerOneLayerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerOneLayerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerOneLayerLoaded(adUnitId, adInfo);
            }
        }

        //OnNativeBannerOneLayerLoadFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerOneLayerLoadFailed;

        internal delegate void TPNativeBannerOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPNativeBannerOneLayerLoadFailedCallback))]
        private static void NativeBannerOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerOneLayerLoadFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusNativeBanneriOS.Instance().OnNativeBannerOneLayerLoadFailed(adUnitId, adInfo, error);
            }
        }

        //OnNativeBannerAllLoaded
        public event Action<string, bool> OnNativeBannerAllLoaded;

        internal delegate void TPNativeBannerAllLoadedCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPNativeBannerAllLoadedCallback))]
        private static void NativeBannerAllLoadedCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerAllLoaded != null)
            {
                TradplusNativeBanneriOS.Instance().OnNativeBannerAllLoaded(adUnitId, isSuccess);
            }
        }

        //OnNativeBannerIsLoading
        public event Action<string> OnNativeBannerIsLoading;

        internal delegate void TPNativeBannerAdIsLoadingCallback(string adUnitId);
        [MonoPInvokeCallback(typeof(TPNativeBannerAdIsLoadingCallback))]
        private static void NativeBannerAdIsLoadingCallback(string adUnitId)
        {
            if (TradplusNativeBanneriOS.Instance().OnNativeBannerIsLoading != null)
            {
                TradplusNativeBanneriOS.Instance().OnNativeBannerIsLoading(adUnitId);
            }
        }
    }
}

#endif