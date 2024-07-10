#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{

    public class TPSdkUnityOfferwall
    {
        private static AndroidJavaClass TPOfferWallClass = new AndroidJavaClass("com.tradplus.unity.plugin.offerwall.TPOfferWallManager");
        private AndroidJavaObject TPOfferWall = TPOfferWallClass.CallStatic<AndroidJavaObject>("getInstance");

        private static TPSdkUnityOfferwall _instance;

        public static TPSdkUnityOfferwall Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityOfferwall();
            }
            return _instance;
        }

        public void LoadOfferwallAd(string adUnitId, TPOfferwallExtra extra)
        {
           
        }

      
        public void ShowOfferwallAd(string adUnitId, string sceneId)
        {
           

        }

        public bool OfferwallAdReady(string adUnitId)
        {
            return true;
        }

        public void EntryOfferwallAdScenario(string adUnitId, string sceneId)
        {
          

        }

        public void SetUserId(string adUnitId, string userId)
        {
           
        }

        public void GetCurrencyBalance(string adUnitId)
        {
           

        }

        public void SpendBalance(string adUnitId, int count)
        {
           
        }

        public void AwardBalance(string adUnitId, int count)
        {
            

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
         
        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.offerwall.TPOfferWallListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

          

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TPSdkUnityOfferwall.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallAllLoaded(unitId, b);

            }


            void onAdIsLoading(string unitId)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallIsLoading(unitId);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void currencyBalanceSuccess(String unitId, int amount, String msg)
            {
                TPSdkUnityOfferwall.Instance().OnCurrencyBalanceSuccess(unitId,amount,msg);
            }
            void currencyBalanceFailed(String unitId, String msg)
            {
                TPSdkUnityOfferwall.Instance().OnCurrencyBalanceFailed(unitId, msg);

            }
            void spendCurrencySuccess(String unitId, int amount, String msg)
            {
                TPSdkUnityOfferwall.Instance().OnSpendCurrencySuccess(unitId, amount, msg);

            }
            void spendCurrencyFailed(String unitId, String msg)
            {
                TPSdkUnityOfferwall.Instance().OnSpendCurrencyFailed(unitId, msg);

            }
            void awardCurrencySuccess(String unitId, int amount, String msg)
            {
                TPSdkUnityOfferwall.Instance().OnAwardCurrencySuccess(unitId, amount, msg);

            }
            void awardCurrencyFailed(String unitId, String msg)
            {
                TPSdkUnityOfferwall.Instance().OnAwardCurrencyFailed(unitId, msg);

            }
            void setUserIdSuccess(String unitId)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallSetUserIdFinish(unitId,true);

            }
            void setUserIdFailed(String unitId, String msg)
            {
                TPSdkUnityOfferwall.Instance().OnOfferwallSetUserIdFinish(unitId, false);

            }
        }


        public TPSdkUnityOfferwall()
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
#endif