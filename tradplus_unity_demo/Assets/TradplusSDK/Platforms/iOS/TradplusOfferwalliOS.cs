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

    public class TradplusOfferwalliOS
    {
        private static TradplusOfferwalliOS _instance;

        public static TradplusOfferwalliOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusOfferwalliOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusLoadOfferwallAd(string adUnitId, string customMap, string localParams, bool openAutoLoadCallback, float maxWaitTime);
        public void LoadOfferwallAd(string adUnitId, TPOfferwallExtra extra = null)
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
            TradplusLoadOfferwallAd(adUnitId, customMapString, localParamsString,extra.openAutoLoadCallback,extra.maxWaitTime);
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowOfferwallAd(string adUnitId, string sceneId);
        public void ShowOfferwallAd(string adUnitId, string sceneId)
        {
            TradplusShowOfferwallAd(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusOfferwallAdReady(string adUnitId);
        public bool OfferwallAdReady(string adUnitId)
        {
            return TradplusOfferwallAdReady(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusEntryOfferwallAdScenario(string adUnitId, string sceneId);
        public void EntryOfferwallAdScenario(string adUnitId, string sceneId)
        {
            TradplusEntryOfferwallAdScenario(adUnitId,sceneId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetUserId(string adUnitId, string userId);
        public void SetUserId(string adUnitId, string userId)
        {
            TradplusSetUserId(adUnitId,userId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusGetCurrencyBalance(string adUnitId);
        public void GetCurrencyBalance(string adUnitId)
        {
            TradplusGetCurrencyBalance(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSpendBalance(string adUnitId,int count);
        public void SpendBalance(string adUnitId, int count)
        {
            TradplusSpendBalance(adUnitId,count);
        }

        [DllImport("__Internal")]
        private static extern void TradplusAwardBalance(string adUnitId, int count);
        public void AwardBalance(string adUnitId, int count)
        {
            TradplusAwardBalance(adUnitId,count);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomAdInfoOfferwall(string adUnitId, string customAdInfo);
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            if (customAdInfo != null)
            {
                string customAdInfoString = Json.Serialize(customAdInfo);
                if (customAdInfoString != null)
                {
                    TradplusSetCustomAdInfoOfferwall(adUnitId, customAdInfoString);
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
            TradplusOfferwallSetCallbacks(
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
        private static extern void TradplusOfferwallSetCallbacks(
            TPOfferwallLoadedCallback adLoadedCallback,
            TPOfferwallLoadFailedCallback adLoadFailedCallback,
            TPOfferwallImpressionCallback adImpressionCallback,
            TPOfferwallShowFailedCallback adShowFailedCallback,
            TPOfferwallClickedCallback adClickedCallback,
            TPOfferwallClosedCallback adClosedCallback,
            TPOfferwallStartLoadCallback adStartLoadCallback,
            TPOfferwallOneLayerStartLoadCallback adOneLayerStartLoadCallback,
            TPOfferwallOneLayerLoadedCallback adOneLayerLoadedCallback,
            TPOfferwallOneLayerLoadFailedCallback adOneLayerLoadFailedCallback,
            TPOfferwallAllLoadedCallback adAllLoadedCallback,
            TPOfferwallSetUserIdFinishCallback adSetUserIdFinishCallback,
            TPOfferwallCurrencyBalanceSuccessCallback adCurrencyBalanceSuccessCallback,
            TPOfferwallCurrencyBalanceFailedCallback adCurrencyBalanceFailedCallback,
            TPOfferwallSpendCurrencySuccessCallback adSpendCurrencySuccessCallback,
            TPOfferwallSpendCurrencyFailedCallback adSpendCurrencyFailedCallback,
            TPOfferwallAwardCurrencySuccesCallback adAwardCurrencySuccesCallback,
            TPOfferwallAwardCurrencyFailedCallback adAwardCurrencyFailedCallback,
            TPOfferwallAdIsLoadingCallback adIsLoadingCallback
        );

        public TradplusOfferwalliOS()
        {
            TradplusOfferwallSetCallbacks(
                OfferwallLoadedCallback,
                OfferwallLoadFailedCallback,
                OfferwallImpressionCallback,
                OfferwallShowFailedCallback,
                OfferwallClickedCallback,
                OfferwallClosedCallback,
                OfferwallStartLoadCallback,
                OfferwallOneLayerStartLoadCallback,
                OfferwallOneLayerLoadedCallback,
                OfferwallOneLayerLoadFailedCallback,
                OfferwallAllLoadedCallback,
                OfferwallSetUserIdFinishCallback,
                OfferwallCurrencyBalanceSuccessCallback,
                OfferwallCurrencyBalanceFailedCallback,
                OfferwallSpendCurrencySuccessCallback,
                OfferwallSpendCurrencyFailedCallback,
                OfferwallAwardCurrencySuccesCallback,
                OfferwallAwardCurrencyFailedCallback,
                OfferwallAdIsLoadingCallback
                );
        }

        //OnOfferwallLoaded
        public event Action<string, Dictionary<string, object>> OnOfferwallLoaded;

        internal delegate void TPOfferwallLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPOfferwallLoadedCallback))]
        private static void OfferwallLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallLoaded(adUnitId, adInfo);
            }
        }

        //OnOfferwallLoadFailed
        public event Action<string, Dictionary<string, object>> OnOfferwallLoadFailed;
        internal delegate void TPOfferwallLoadFailedCallback(string adUnitId, string errorStr);
        [MonoPInvokeCallback(typeof(TPOfferwallLoadFailedCallback))]
        private static void OfferwallLoadFailedCallback(string adUnitId, string errorStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallLoadFailed != null)
            {
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallLoadFailed(adUnitId, error);
            }
        }

        //OnOfferwallImpression
        public event Action<string, Dictionary<string, object>> OnOfferwallImpression;

        internal delegate void TPOfferwallImpressionCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPOfferwallImpressionCallback))]
        private static void OfferwallImpressionCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallImpression != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallImpression(adUnitId, adInfo);
            }
        }

        //OnOfferwallShowFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnOfferwallShowFailed;

        internal delegate void TPOfferwallShowFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPOfferwallShowFailedCallback))]
        private static void OfferwallShowFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallShowFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallShowFailed(adUnitId, adInfo, error);
            }
        }

        //OnOfferwallClicked
        public event Action<string, Dictionary<string, object>> OnOfferwallClicked;

        internal delegate void TPOfferwallClickedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPOfferwallClickedCallback))]
        private static void OfferwallClickedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallClicked != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallClicked(adUnitId, adInfo);
            }
        }

        //OnOfferwallClosed
        public event Action<string, Dictionary<string, object>> OnOfferwallClosed;

        internal delegate void TPOfferwallClosedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPOfferwallClosedCallback))]
        private static void OfferwallClosedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallClosed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallClosed(adUnitId, adInfo);
            }
        }

        //OnOfferwallStartLoad
        public event Action<string, Dictionary<string, object>> OnOfferwallStartLoad;

        internal delegate void TPOfferwallStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPOfferwallStartLoadCallback))]
        private static void OfferwallStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallStartLoad(adUnitId, adInfo);
            }
        }

        //OnOfferwallOneLayerStartLoad
        public event Action<string, Dictionary<string, object>> OnOfferwallOneLayerStartLoad;

        internal delegate void TPOfferwallOneLayerStartLoadCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPOfferwallOneLayerStartLoadCallback))]
        private static void OfferwallOneLayerStartLoadCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallOneLayerStartLoad != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallOneLayerStartLoad(adUnitId, adInfo);
            }
        }

        //OnOfferwallOneLayerLoaded
        public event Action<string, Dictionary<string, object>> OnOfferwallOneLayerLoaded;

        internal delegate void TPOfferwallOneLayerLoadedCallback(string adUnitId, string adInfoStr);
        [MonoPInvokeCallback(typeof(TPOfferwallOneLayerLoadedCallback))]
        private static void OfferwallOneLayerLoadedCallback(string adUnitId, string adInfoStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallOneLayerLoaded != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallOneLayerLoaded(adUnitId, adInfo);
            }
        }

        //OnOfferwallOneLayerLoadFailed
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnOfferwallOneLayerLoadFailed;

        internal delegate void TPOfferwallOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr);
        [MonoPInvokeCallback(typeof(TPOfferwallOneLayerLoadFailedCallback))]
        private static void OfferwallOneLayerLoadFailedCallback(string adUnitId, string adInfoStr, string errorStr)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallOneLayerLoadFailed != null)
            {
                Dictionary<string, object> adInfo = Json.Deserialize(adInfoStr) as Dictionary<string, object>;
                Dictionary<string, object> error = Json.Deserialize(errorStr) as Dictionary<string, object>;
                TradplusOfferwalliOS.Instance().OnOfferwallOneLayerLoadFailed(adUnitId, adInfo, error);
            }
        }

        //OnOfferwallAllLoaded
        public event Action<string, bool> OnOfferwallAllLoaded;

        internal delegate void TPOfferwallAllLoadedCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPOfferwallAllLoadedCallback))]
        private static void OfferwallAllLoadedCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallAllLoaded != null)
            {
                TradplusOfferwalliOS.Instance().OnOfferwallAllLoaded(adUnitId, isSuccess);
            }
        }

        //OnOfferwallSetUserIdFinish
        public event Action<string, bool> OnOfferwallSetUserIdFinish;

        internal delegate void TPOfferwallSetUserIdFinishCallback(string adUnitId, bool isSuccess);
        [MonoPInvokeCallback(typeof(TPOfferwallSetUserIdFinishCallback))]
        private static void OfferwallSetUserIdFinishCallback(string adUnitId, bool isSuccess)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallSetUserIdFinish != null)
            {
                TradplusOfferwalliOS.Instance().OnOfferwallSetUserIdFinish(adUnitId, isSuccess);
            }
        }

        //OnCurrencyBalanceSuccess
        public event Action<string, int, string> OnCurrencyBalanceSuccess;

        internal delegate void TPOfferwallCurrencyBalanceSuccessCallback(string adUnitId, int amount, string msg);
        [MonoPInvokeCallback(typeof(TPOfferwallCurrencyBalanceSuccessCallback))]
        private static void OfferwallCurrencyBalanceSuccessCallback(string adUnitId, int amount, string msg)
        {
            if (TradplusOfferwalliOS.Instance().OnCurrencyBalanceSuccess != null)
            {
                TradplusOfferwalliOS.Instance().OnCurrencyBalanceSuccess(adUnitId, amount, msg);
            }
        }

        //OnCurrencyBalanceFailed
        public event Action<string, string> OnCurrencyBalanceFailed;

        internal delegate void TPOfferwallCurrencyBalanceFailedCallback(string adUnitId, string msg);
        [MonoPInvokeCallback(typeof(TPOfferwallCurrencyBalanceFailedCallback))]
        private static void OfferwallCurrencyBalanceFailedCallback(string adUnitId, string msg)
        {
            if (TradplusOfferwalliOS.Instance().OnCurrencyBalanceFailed != null)
            {
                TradplusOfferwalliOS.Instance().OnCurrencyBalanceFailed(adUnitId, msg);
            }
        }

        //OnSpendCurrencySuccess
        public event Action<string, int, string> OnSpendCurrencySuccess;

        internal delegate void TPOfferwallSpendCurrencySuccessCallback(string adUnitId, int amount, string msg);
        [MonoPInvokeCallback(typeof(TPOfferwallSpendCurrencySuccessCallback))]
        private static void OfferwallSpendCurrencySuccessCallback(string adUnitId, int amount, string msg)
        {
            if (TradplusOfferwalliOS.Instance().OnSpendCurrencySuccess != null)
            {
                TradplusOfferwalliOS.Instance().OnSpendCurrencySuccess(adUnitId, amount, msg);
            }
        }

        //OnSpendCurrencyFailed
        public event Action<string, string> OnSpendCurrencyFailed;

        internal delegate void TPOfferwallSpendCurrencyFailedCallback(string adUnitId, string msg);
        [MonoPInvokeCallback(typeof(TPOfferwallSpendCurrencyFailedCallback))]
        private static void OfferwallSpendCurrencyFailedCallback(string adUnitId, string msg)
        {
            if (TradplusOfferwalliOS.Instance().OnSpendCurrencyFailed != null)
            {
                TradplusOfferwalliOS.Instance().OnSpendCurrencyFailed(adUnitId, msg);
            }
        }

        public event Action<string, int, string> OnAwardCurrencySuccess;

        internal delegate void TPOfferwallAwardCurrencySuccesCallback(string adUnitId, int amount, string msg);
        [MonoPInvokeCallback(typeof(TPOfferwallAwardCurrencySuccesCallback))]
        private static void OfferwallAwardCurrencySuccesCallback(string adUnitId, int amount, string msg)
        {
            if (TradplusOfferwalliOS.Instance().OnAwardCurrencySuccess != null)
            {
                TradplusOfferwalliOS.Instance().OnAwardCurrencySuccess(adUnitId, amount, msg);
            }
        }

        public event Action<string, string> OnAwardCurrencyFailed;

        internal delegate void TPOfferwallAwardCurrencyFailedCallback(string adUnitId, string msg);
        [MonoPInvokeCallback(typeof(TPOfferwallAwardCurrencyFailedCallback))]
        private static void OfferwallAwardCurrencyFailedCallback(string adUnitId, string msg)
        {
            if (TradplusOfferwalliOS.Instance().OnAwardCurrencyFailed != null)
            {
                TradplusOfferwalliOS.Instance().OnAwardCurrencyFailed(adUnitId, msg);
            }
        }

        //OnOfferwallIsLoading
        public event Action<string> OnOfferwallIsLoading;

        internal delegate void TPOfferwallAdIsLoadingCallback(string adUnitId);
        [MonoPInvokeCallback(typeof(TPOfferwallAdIsLoadingCallback))]
        private static void OfferwallAdIsLoadingCallback(string adUnitId)
        {
            if (TradplusOfferwalliOS.Instance().OnOfferwallIsLoading != null)
            {
                TradplusOfferwalliOS.Instance().OnOfferwallIsLoading(adUnitId);
            }
        }
    }
}


#endif