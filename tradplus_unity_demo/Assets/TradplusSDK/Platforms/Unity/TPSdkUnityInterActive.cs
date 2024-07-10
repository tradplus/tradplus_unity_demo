#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{

    public class TPSdkUnityInterActive
    {
        private static AndroidJavaClass TPInterActiveClass = new AndroidJavaClass("com.tradplus.unity.plugin.interactive.TPInterActiveManager");
        private AndroidJavaObject TPInterActive = TPInterActiveClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TPSdkUnityInterActive _instance;

        public static TPSdkUnityInterActive Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityInterActive();
            }
            return _instance;
        }

        public void LoadInterActiveAd(string adUnitId, TPInterActiveExtra extra)
        {
           

        }

        public void ShowInterActiveAd(string adUnitId, string sceneId)
        {
            

        }

        public bool InterActiveAdReady(string adUnitId)
        {
            return true;
        }

        public void HideInterActive(string adUnitId)
        {
            

        }

        public void DisplayInterActive(string adUnitId)
        {
            

        }


        public void DestroyInterActive(string adUnitId)
        {
           

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
           

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.interactive.TPInterActiveListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
              //  TPSdkUnityInterActive.Instance().OnInterActiveVideoError(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TPSdkUnityInterActive.Instance().OnInterActiveIsLoading(unitId);

            }

        }

        public TPSdkUnityInterActive()
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
#endif