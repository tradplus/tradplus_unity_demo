using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Android
{

    public class TradplusOfferwallAndroid
    {
        private static AndroidJavaClass TPOfferWallClass = new AndroidJavaClass("com.tradplus.unity.plugin.offerwall.TPOfferWallManager");
        private AndroidJavaObject TPOfferWall = TPOfferWallClass.CallStatic<AndroidJavaObject>("getInstance");

        private static TradplusOfferwallAndroid _instance;

        public static TradplusOfferwallAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusOfferwallAndroid();
            }
            return _instance;
        }

        public void LoadOfferwallAd(string adUnitId, TPOfferwallExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();
            info.Add("customMap", extra.customMap);
            info.Add("localParams", extra.localParams);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);


            TPOfferWall.Call("loadAd", adUnitId, Json.Serialize(info), loadListenerAdapter);
        }

      
        public void ShowOfferwallAd(string adUnitId, string sceneId)
        {
            TPOfferWall.Call("showAd", adUnitId, sceneId);

        }

        public bool OfferwallAdReady(string adUnitId)
        {
            return TPOfferWall.Call<bool>("isReady", adUnitId);
        }

        public void EntryOfferwallAdScenario(string adUnitId, string sceneId)
        {
            TPOfferWall.Call("entryAdScenario", adUnitId, sceneId);

        }

        public void SetUserId(string adUnitId, string userId)
        {
            TPOfferWall.Call("setUserId", adUnitId, userId);

        }

        public void GetCurrencyBalance(string adUnitId)
        {
            TPOfferWall.Call("getCurrencyBalance", adUnitId);

        }

        public void SpendBalance(string adUnitId, int count)
        {
            TPOfferWall.Call("spendCurrency", adUnitId,count);

        }

        public void AwardBalance(string adUnitId, int count)
        {
            TPOfferWall.Call("awardCurrency", adUnitId,count);

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPOfferWall.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.offerwall.TPOfferWallListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

          

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TradplusOfferwallAndroid.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallAllLoaded(unitId, b);

            }


            void onAdIsLoading(string unitId)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallIsLoading(unitId);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void currencyBalanceSuccess(String unitId, int amount, String msg)
            {
                TradplusOfferwallAndroid.Instance().OnCurrencyBalanceSuccess(unitId,amount,msg);
            }
            void currencyBalanceFailed(String unitId, String msg)
            {
                TradplusOfferwallAndroid.Instance().OnCurrencyBalanceFailed(unitId, msg);

            }
            void spendCurrencySuccess(String unitId, int amount, String msg)
            {
                TradplusOfferwallAndroid.Instance().OnSpendCurrencySuccess(unitId, amount, msg);

            }
            void spendCurrencyFailed(String unitId, String msg)
            {
                TradplusOfferwallAndroid.Instance().OnSpendCurrencyFailed(unitId, msg);

            }
            void awardCurrencySuccess(String unitId, int amount, String msg)
            {
                TradplusOfferwallAndroid.Instance().OnAwardCurrencySuccess(unitId, amount, msg);

            }
            void awardCurrencyFailed(String unitId, String msg)
            {
                TradplusOfferwallAndroid.Instance().OnAwardCurrencyFailed(unitId, msg);

            }
            void setUserIdSuccess(String unitId)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallSetUserIdFinish(unitId,true);

            }
            void setUserIdFailed(String unitId, String msg)
            {
                TradplusOfferwallAndroid.Instance().OnOfferwallSetUserIdFinish(unitId, false);

            }
        }


        public TradplusOfferwallAndroid()
        {
        }

        public event Action<string, Dictionary<string, object>> OnOfferwallLoaded;

        public event Action<string, Dictionary<string, object>> OnOfferwallLoadFailed;

        public event Action<string, Dictionary<string, object>> OnOfferwallImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnOfferwallShowFailed;

        public event Action<string, Dictionary<string, object>> OnOfferwallClicked;

        public event Action<string, Dictionary<string, object>> OnOfferwallClosed;

        public event Action<string, Dictionary<string, object>> OnOfferwallStartLoad;

        public event Action<string, Dictionary<string, object>> OnOfferwallOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnOfferwallOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnOfferwallOneLayerLoadFailed;

        public event Action<string, bool> OnOfferwallAllLoaded;

        public event Action<string> OnOfferwallIsLoading;

        public event Action<string, bool> OnOfferwallSetUserIdFinish;

        public event Action<string, int, string> OnCurrencyBalanceSuccess;

        public event Action<string, string> OnCurrencyBalanceFailed;

        public event Action<string, int, string> OnSpendCurrencySuccess;

        public event Action<string, string> OnSpendCurrencyFailed;

        public event Action<string, int, string> OnAwardCurrencySuccess;

        public event Action<string, string> OnAwardCurrencyFailed;
    }
}