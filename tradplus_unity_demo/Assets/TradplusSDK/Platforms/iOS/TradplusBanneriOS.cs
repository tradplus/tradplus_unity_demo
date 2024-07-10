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
    public class TradplusBanneriOS
    {
        private static TradplusBanneriOS _instance;

        public static TradplusBanneriOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusBanneriOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusLoadBannerAd(string adUnitId, bool closeAutoShow, float x, float y, float width, float height,int adPosition, int contentMode, string sceneId, string customMap,string className, string localParams, bool openAutoLoadCallback, float maxWaitTime,string backgroundColor);
        public void LoadBannerAd(string adUnitId, string sceneId , TPBannerExtra extra)
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
            TradplusLoadBannerAd(adUnitId, extra.closeAutoShow,extra.x,extra.y,extra.width,extra.height,(int)extra.adPosition, (int)extra.contentMode , sceneId,customMapString, extra.className, localParamsString, extra.openAutoLoadCallback, extra.maxWaitTime,extra.backgroundColor);
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowBannerAd(string adUnitId,string sceneId);
        public void ShowBannerAd(string adUnitId, string sceneId)
        {
            TradplusShowBannerAd(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusBannerAdReady(string adUnitId);
        public bool BannerAdReady(string adUnitId)
        {
            return TradplusBannerAdReady(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusEntryBannerAdScenario(string adUnitId, string sceneId);
        public void EntryBannerAdScenario(string adUnitId, string sceneId)
        {
            TradplusEntryBannerAdScenario(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusHideBanner(string adUnitId);
        public void HideBanner(string adUnitId)
        {
            TradplusHideBanner(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusDisplayBanner(string adUnitId);
        public void DisplayBanner(string adUnitId)
        {
            TradplusDisplayBanner(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusDestroyBanner(string adUnitId);
        public void DestroyBanner(string adUnitId)
        {
            TradplusDestroyBanner(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomAdInfoBanner(string adUnitId, string customAdInfo);
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            if(customAdInfo != null)
            {
                string customAdInfoString = Json.Serialize(customAdInfo);
                if(customAdInfoString != null)
                {
                    TradplusSetCustomAdInfoBanner(adUnitId, customAdInfoString);
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
            TradplusBannerSetCallbacks(
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
        private static extern void TradplusBannerSetCallbacks(
            TPBannerLoadedCallback adLoadedCallback,
            TPBannerLoadFailedCallback adLoadFailedCallback,
            TPBannerImpressionCallback adImpressionCallback,
            TPBannerShowFailedCallback adShowFailedCallback,
            TPBannerClickedCallback adClickedCallback,
            TPBannerClosedCallback adClosedCallback,
            TPBannerStartLoadCallback adStartLoadCallback,
            TPBannerBiddingStartCallback adBiddingStartCallback,
            TPBannerBiddingEndCallback adBiddingEndCallback,
            TPBannerOneLayerStartLoadCallback adOneLayerStartLoadCallback,
            TPBannerOneLayerLoadedCallback adOneLayerLoadedCallback,
            TPBannerOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
            TPBannerAllLoadedCallback adAllLoadedCallback,
            TPBannerAdIsLoadingCallback adIsLoadingCallback
        );
        public TradplusBanneriOS()
        {
            TradplusBannerSetCallbacks(
                BannerLoadedCallback,
                BannerLoadFailedCallback,
                BannerImpressionCallback,
                BannerShowFailedCallback,
                BannerClickedCallback,
                BannerClosedCallback,
                BannerStartLoadCallback,
                BannerBiddingStartCallback,
                BannerBiddingEndCallback,
                BannerOneLayerStartLoadCallback,
                BannerOneLayerLoadedCallback,
                BannerOneLayerLoadFailedCallback,
                BannerAllLoadedCallback,
                BannerAdIsLoadingCallback
                );
        }

        // OnBannerLoaded
        public event Action<string, Dictionary<string, object>> OnBannerLoaded;

        internal delegate void TPBannerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerLoadedCallback))]
        private static void BannerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerLoaded(adUnitId, adInfo);
            }
        }

        //OnBannerLoadFailed
        public event Action<string, Dictionary<string, object>> OnBannerLoadFailed;

        internal delegate void TPBannerLoadFailedCallback(string adUnitId, string errorStr);
        [MonoPInvokeCallback(typeof(TPBannerLoadFailedCallback))]
        private static void BannerLoadFailedCallback(string adUnitId, string errorStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerLoadFailed != null)
            {
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerLoadFailed(adUnitId, error);
            }
        }

        //OnBannerImpression
        public event Action<string, Dictionary<string, object>> OnBannerImpression;

        internal delegate void TPBannerImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerImpressionCallback))]
        private static void BannerImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerImpression(adUnitId, adInfo);
            }
        }

        //OnBannerShowFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerShowFailed;

        internal delegate void TPBannerShowFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPBannerShowFailedCallback))]
        private static void BannerShowFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerShowFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerShowFailed(adUnitId, adInfo, error);
            }
        }

        //OnBannerClicked
        public event Action<string, Dictionary<string, object>> OnBannerClicked;

        internal delegate void TPBannerClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerClickedCallback))]
        private static void BannerClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerClicked(adUnitId, adInfo);
            }
        }

        //OnBannerClosed
        public event Action<string, Dictionary<string, object>> OnBannerClosed;

        internal delegate void TPBannerClosedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerClosedCallback))]
        private static void BannerClosedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerClosed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerClosed(adUnitId, adInfo);
            }
        }

        //OnBannerStartLoad
        public event Action<string, Dictionary<string, object>> OnBannerStartLoad;

        internal delegate void TPBannerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerStartLoadCallback))]
        private static void BannerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerStartLoad(adUnitId, adInfo);
            }
        }

        //OnBannerBiddingStart
        public event Action<string, Dictionary<string, object>> OnBannerBiddingStart;

        internal delegate void TPBannerBiddingStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerBiddingStartCallback))]
        private static void BannerBiddingStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerBiddingStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerBiddingStart(adUnitId, adInfo);
            }
        }

        //OnBannerBiddingEnd
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerBiddingEnd;

        internal delegate void TPBannerBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPBannerBiddingEndCallback))]
        private static void BannerBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerBiddingEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerBiddingEnd(adUnitId, adInfo, error);
            }
        }

        //OnBannerOneLayerStartLoad
        public event Action<string, Dictionary<string, object>> OnBannerOneLayerStartLoad;

        internal delegate void TPBannerOneLayerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerOneLayerStartLoadCallback))]
        private static void BannerOneLayerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerOneLayerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerOneLayerStartLoad(adUnitId, adInfo);
            }
        }

        //OnBannerOneLayerLoaded
        public event Action<string, Dictionary<string, object>> OnBannerOneLayerLoaded;

        internal delegate void TPBannerOneLayerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPBannerOneLayerLoadedCallback))]
        private static void BannerOneLayerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerOneLayerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerOneLayerLoaded(adUnitId, adInfo);
            }
        }

        //OnBannerOneLayerLoadFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerOneLayerLoadFailed;

        internal delegate void TPBannerOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPBannerOneLayerLoadFailedCallback))]
        private static void BannerOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusBanneriOS.Instance().OnBannerOneLayerLoadFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusBanneriOS.Instance().OnBannerOneLayerLoadFailed(adUnitId, adInfo, error);
            }
        }

        //OnBannerAllLoaded
        public event Action<string, bool> OnBannerAllLoaded;

        internal delegate void TPBannerAllLoadedCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPBannerAllLoadedCallback))]
        private static void BannerAllLoadedCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusBanneriOS.Instance().OnBannerAllLoaded != null)
            {
                TradplusBanneriOS.Instance().OnBannerAllLoaded(adUnitId, isSuccess);
            }
        }

        //OnBannerIsLoading
        public event Action<string> OnBannerIsLoading;

        internal delegate void TPBannerAdIsLoadingCallback(string adUnitId);
        [MonoPInvokeCallback(typeof(TPBannerAdIsLoadingCallback))]
        private static void BannerAdIsLoadingCallback(string adUnitId)
        {
            if (TradplusBanneriOS.Instance().OnBannerIsLoading != null)
            {
                TradplusBanneriOS.Instance().OnBannerIsLoading(adUnitId);
            }
        }
    }
}

#endif