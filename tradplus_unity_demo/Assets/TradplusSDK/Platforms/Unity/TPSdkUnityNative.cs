#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{

    public class TPSdkUnityNative
    {
        private static AndroidJavaClass TPNativeClass = new AndroidJavaClass("com.tradplus.unity.plugin.nativead.TPNativeManager");
        private AndroidJavaObject TPNative = TPNativeClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TPSdkUnityNative _instance;

        public static TPSdkUnityNative Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityNative();
            }
            return _instance;
        }

        public void LoadNativeAd(string adUnitId, TPNativeExtra extra)
        {


        }

        public void ShowNativeAd(string adUnitId, string sceneId, string className)
        {
           

        }

        public bool NativeAdReady(string adUnitId)
        {
            return true;
        }

        public void EntryNativeAdScenario(string adUnitId, string sceneId)
        {
            

        }

        public void HideNative(string adUnitId)
        {
            

        }

        public void DisplayNative(string adUnitId)
        {
           

        }


        public void DestroyNative(string adUnitId)
        {
            

        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.nativead.TPNativeListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdLoadFailed(string unitId, string error)
            {
                TPSdkUnityNative.Instance().OnNativeLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdVideoError(string unitId, string tpAdInfo, string error)
            {
                //TPSdkUnityNative.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId, bool b)
            {
                TPSdkUnityNative.Instance().OnNativeAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror), (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TPSdkUnityNative.Instance().OnNativeStartLoad(unitId, null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityNative.Instance().OnNativeBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TPSdkUnityNative.Instance().OnNativeBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TPSdkUnityNative.Instance().OnNativeIsLoading(unitId);

            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNative.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TPSdkUnityNative.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName, progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNative.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNative.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNative.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityNative.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TPSdkUnityNative()
        {
        }

        public event Action<string, Dictionary<string, object>> OnNativeLoaded;

        public event Action<string, Dictionary<string, object>> OnNativeLoadFailed;

        public event Action<string, Dictionary<string, object>> OnNativeImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeShowFailed;

        public event Action<string, Dictionary<string, object>> OnNativeClicked;

        public event Action<string, Dictionary<string, object>> OnNativeClosed;

        public event Action<string, Dictionary<string, object>> OnNativeStartLoad;

        public event Action<string, Dictionary<string, object>> OnNativeBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBiddingEnd;

        public event Action<string> OnNativeIsLoading;

        public event Action<string, Dictionary<string, object>> OnNativeOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnNativeOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeOneLayerLoadFailed;

        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayStart;

        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayEnd;

        public event Action<string, bool> OnNativeAllLoaded;

        //国内下载监听
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadStart;

        public event Action<string, Dictionary<string, object>, long, long, string, string, int> OnDownloadUpdate;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadPause;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFinish;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFailed;

        public event Action<string, Dictionary<string, object>, long, long, string, string> OnInstalled;
    }

}
#endif