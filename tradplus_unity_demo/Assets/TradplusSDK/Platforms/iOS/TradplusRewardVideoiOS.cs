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

    public class TradplusRewardVideoiOS
    {
        private static TradplusRewardVideoiOS _instance;

        public static TradplusRewardVideoiOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusRewardVideoiOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusLoadRewardVideoAd(string adUnitId,string userId,string customData, string customMap, string localParams, bool openAutoLoadCallback, float maxWaitTime);
        public void LoadRewardVideoAd(string adUnitId, TPRewardVideoExtra extra)
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
            TradplusLoadRewardVideoAd(adUnitId, extra.userId,extra.customData, customMapString, localParamsString,extra.openAutoLoadCallback,extra.maxWaitTime);
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowRewardVideoAd(string adUnitId, string sceneId);
        public void ShowRewardVideoAd(string adUnitId, string sceneId)
        {
            TradplusShowRewardVideoAd(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusRewardVideoAdReady(string adUnitId);
        public bool RewardVideoAdReady(string adUnitId)
        {
            return TradplusRewardVideoAdReady(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusEntryRewardVideoAdScenario(string adUnitId, string sceneId);
        public void EntryRewardVideoAdScenario(string adUnitId, string sceneId)
        {
            TradplusEntryRewardVideoAdScenario(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomAdInfoRewardVideo(string adUnitId, string customAdInfo);
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            if (customAdInfo != null)
            {
                string customAdInfoString = Json.Serialize(customAdInfo);
                if (customAdInfoString != null)
                {
                    TradplusSetCustomAdInfoRewardVideo(adUnitId, customAdInfoString);
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
            TradplusRewardVideoSetCallbacks(
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
        private static extern void TradplusRewardVideoSetCallbacks(
            TPRewardVideoLoadedCallback adLoadedCallback,
            TPRewardVideoLoadFailedCallback adLoadFailedCallback,
            TPRewardVideoImpressionCallback adImpressionCallback,
            TPRewardVideoShowFailedCallback adShowFailedCallback,
            TPRewardVideoClickedCallback adClickedCallback,
            TPRewardVideoClosedCallback adClosedCallback,
            TPRewardVideoRewardCallback adRewardCallback,
            TPRewardVideoStartLoadCallback adStartLoadCallback,
            TPRewardVideoBiddingStartCallback adBiddingStartCallback,
            TPRewardVideoBiddingEndCallback adBiddingEndCallback,
            TPRewardVideoOneLayerStartLoadCallback adOneLayerStartLoadCallback,
            TPRewardVideoOneLayerLoadedCallback adOneLayerLoadedCallback,
            TPRewardVideoOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
            TPRewardVideoPlayStartCallback adVideoPlayStartCallback,
            TPRewardVideoPlayEndCallback adVideoPlayEndCallback,
            TPRewardVideoAllLoadedCallback adAllLoadedCallback,
            TPRewardVideoPlayAgainImpressionCallback adPlayAgainImpressionCallback,
            TPRewardVideoPlayAgainRewardCallback adPlayAgainRewardCallback,
            TPRewardVideoPlayAgainClickedCallback adPlayAgainClickedCallback,
            TPRewardVideoPlayAgainVideoPlayStartCallback adPlayAgainVideoPlayStartCallback,
            TPRewardVideoPlayAgainVideoPlayEndCallback adPlayAgainVideoPlayEndCallback,
            TPRewardVideoAdIsLoadingCallback adIsLoadingCallback
        );

        public TradplusRewardVideoiOS()
        {
            TradplusRewardVideoSetCallbacks(
            RewardVideoLoadedCallback,
            RewardVideoLoadFailedCallback,
            RewardVideoImpressionCallback,
            RewardVideoShowFailedCallback,
            RewardVideoClickedCallback,
            RewardVideoClosedCallback,
            RewardVideoRewardCallback,
            RewardVideoStartLoadCallback,
            RewardVideoBiddingStartCallback,
            RewardVideoBiddingEndCallback,
            RewardVideoOneLayerStartLoadCallback,
            RewardVideoOneLayerLoadedCallback,
            RewardVideoOneLayerLoadFailedCallback,
            RewardVideoPlayStartCallback,
            RewardVideoPlayEndCallback,
            RewardVideoAllLoadedCallback,
            RewardVideoPlayAgainImpressionCallback,
            RewardVideoPlayAgainRewardCallback,
            RewardVideoPlayAgainClickedCallback,
            RewardVideoPlayAgainVideoPlayStartCallback,
            RewardVideoPlayAgainVideoPlayEndCallback,
            RewardVideoAdIsLoadingCallback
           );
        }

        //OnRewardVideoLoaded
        public event Action<string, Dictionary<string, object>> OnRewardVideoLoaded;

        internal delegate void TPRewardVideoLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoLoadedCallback))]
        private static void RewardVideoLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoLoaded(adUnitId, adInfo);
            }
        }

        //OnRewardVideoLoadFailed
        public event Action<string, Dictionary<string, object>> OnRewardVideoLoadFailed;

        internal delegate void TPRewardVideoLoadFailedCallback(string adUnitId, string errorStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoLoadFailedCallback))]
        private static void RewardVideoLoadFailedCallback(string adUnitId, string errorStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoLoadFailed != null)
            {
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoLoadFailed(adUnitId, error);
            }
        }

        //OnRewardVideoImpression
        public event Action<string, Dictionary<string, object>> OnRewardVideoImpression;

        internal delegate void TPRewardVideoImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoImpressionCallback))]
        private static void RewardVideoImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoImpression(adUnitId, adInfo);
            }
        }

        //OnRewardVideoShowFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoShowFailed;

        internal delegate void TPRewardVideoShowFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoShowFailedCallback))]
        private static void RewardVideoShowFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoShowFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoShowFailed(adUnitId, adInfo, error);
            }
        }

        //OnRewardVideoClicked
        public event Action<string, Dictionary<string, object>> OnRewardVideoClicked;

        internal delegate void TPRewardVideoClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoClickedCallback))]
        private static void RewardVideoClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoClicked(adUnitId, adInfo);
            }
        }

        //OnRewardVideoClosed
        public event Action<string, Dictionary<string, object>> OnRewardVideoClosed;

        internal delegate void TPRewardVideoClosedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoClosedCallback))]
        private static void RewardVideoClosedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoClosed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoClosed(adUnitId, adInfo);
            }
        }

        //OnRewardVideoReward
        public event Action<string, Dictionary<string, object>> OnRewardVideoReward;

        internal delegate void TPRewardVideoRewardCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoRewardCallback))]
        private static void RewardVideoRewardCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoReward != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoReward(adUnitId, adInfo);
            }
        }

        //OnRewardVideoStartLoad
        public event Action<string, Dictionary<string, object>> OnRewardVideoStartLoad;

        internal delegate void TPRewardVideoStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoStartLoadCallback))]
        private static void RewardVideoStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoStartLoad(adUnitId, adInfo);
            }
        }

        //OnRewardVideoBiddingStart
        public event Action<string, Dictionary<string, object>> OnRewardVideoBiddingStart;

        internal delegate void TPRewardVideoBiddingStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoBiddingStartCallback))]
        private static void RewardVideoBiddingStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoBiddingStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoBiddingStart(adUnitId, adInfo);
            }
        }

        //OnRewardVideoBiddingEnd
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoBiddingEnd;

        internal delegate void TPRewardVideoBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoBiddingEndCallback))]
        private static void RewardVideoBiddingEndCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoBiddingEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoBiddingEnd(adUnitId, adInfo, error);
            }
        }


        //OnRewardVideoOneLayerStartLoad
        public event Action<string, Dictionary<string, object>> OnRewardVideoOneLayerStartLoad;

        internal delegate void TPRewardVideoOneLayerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoOneLayerStartLoadCallback))]
        private static void RewardVideoOneLayerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoOneLayerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoOneLayerStartLoad(adUnitId, adInfo);
            }
        }

        //OnRewardVideoOneLayerLoaded
        public event Action<string, Dictionary<string, object>> OnRewardVideoOneLayerLoaded;

        internal delegate void TPRewardVideoOneLayerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoOneLayerLoadedCallback))]
        private static void RewardVideoOneLayerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoOneLayerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoOneLayerLoaded(adUnitId, adInfo);
            }
        }

        //OnRewardVideoOneLayerLoadFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoOneLayerLoadFailed;

        internal delegate void TPRewardVideoOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoOneLayerLoadFailedCallback))]
        private static void RewardVideoOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoOneLayerLoadFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoOneLayerLoadFailed(adUnitId, adInfo, error);
            }
        }

        //OnRewardVideoPlayStart
        public event Action<string, Dictionary<string, object>> OnRewardVideoPlayStart;

        internal delegate void TPRewardVideoPlayStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoPlayStartCallback))]
        private static void RewardVideoPlayStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoPlayStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoPlayStart(adUnitId, adInfo);
            }
        }

        //OnRewardVideoPlayEnd
        public event Action<string, Dictionary<string, object>> OnRewardVideoPlayEnd;

        internal delegate void TPRewardVideoPlayEndCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoPlayEndCallback))]
        private static void RewardVideoPlayEndCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoPlayEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnRewardVideoPlayEnd(adUnitId, adInfo);
            }
        }

        //OnRewardVideoAllLoaded
        public event Action<string, bool> OnRewardVideoAllLoaded;

        internal delegate void TPRewardVideoAllLoadedCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPRewardVideoAllLoadedCallback))]
        private static void RewardVideoAllLoadedCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoAllLoaded != null)
            {
                TradplusRewardVideoiOS.Instance().OnRewardVideoAllLoaded(adUnitId, isSuccess);
            }
        }

        //OnInterstitialIsLoading
        public event Action<string> OnRewardVideoIsLoading;

        internal delegate void TPRewardVideoAdIsLoadingCallback(string adUnitId);
        [MonoPInvokeCallback(typeof(TPRewardVideoAdIsLoadingCallback))]
        private static void RewardVideoAdIsLoadingCallback(string adUnitId)
        {
            if (TradplusRewardVideoiOS.Instance().OnRewardVideoIsLoading != null)
            {
                TradplusRewardVideoiOS.Instance().OnRewardVideoIsLoading(adUnitId);
            }
        }

        // 国内再看一个相关回调

        //OnPlayAgainImpression
        public event Action<string, Dictionary<string, object>> OnPlayAgainImpression;

        internal delegate void TPRewardVideoPlayAgainImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoPlayAgainImpressionCallback))]
        private static void RewardVideoPlayAgainImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnPlayAgainImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnPlayAgainImpression(adUnitId, adInfo);
            }
        }

        //OnPlayAgainReward
        public event Action<string, Dictionary<string, object>> OnPlayAgainReward;

        internal delegate void TPRewardVideoPlayAgainRewardCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoPlayAgainRewardCallback))]
        private static void RewardVideoPlayAgainRewardCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnPlayAgainReward != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnPlayAgainReward(adUnitId, adInfo);
            }
        }

        //OnPlayAgainClicked
        public event Action<string, Dictionary<string, object>> OnPlayAgainClicked;

        internal delegate void TPRewardVideoPlayAgainClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoPlayAgainClickedCallback))]
        private static void RewardVideoPlayAgainClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnPlayAgainClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnPlayAgainClicked(adUnitId, adInfo);
            }
        }

        //OnPlayAgainVideoPlayStart
        public event Action<string, Dictionary<string, object>> OnPlayAgainVideoPlayStart;

        internal delegate void TPRewardVideoPlayAgainVideoPlayStartCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoPlayAgainVideoPlayStartCallback))]
        private static void RewardVideoPlayAgainVideoPlayStartCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnPlayAgainVideoPlayStart != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnPlayAgainVideoPlayStart(adUnitId, adInfo);
            }
        }

        //OnPlayAgainVideoPlayEnd
        public event Action<string, Dictionary<string, object>> OnPlayAgainVideoPlayEnd;


        internal delegate void TPRewardVideoPlayAgainVideoPlayEndCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPRewardVideoPlayAgainVideoPlayEndCallback))]
        private static void RewardVideoPlayAgainVideoPlayEndCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusRewardVideoiOS.Instance().OnPlayAgainVideoPlayEnd != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusRewardVideoiOS.Instance().OnPlayAgainVideoPlayEnd(adUnitId, adInfo);
            }
        }

    }
}

#endif