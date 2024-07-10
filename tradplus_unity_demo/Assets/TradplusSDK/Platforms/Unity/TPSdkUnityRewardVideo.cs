#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Unity
{

    public class TPSdkUnityRewardVideo
    {
        private static AndroidJavaClass TPRewardClass = new AndroidJavaClass("com.tradplus.unity.plugin.reward.TPRewardManager");
        private AndroidJavaObject TPReward = TPRewardClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TPSdkUnityRewardVideo _instance;

        public static TPSdkUnityRewardVideo Instance()
        {
            if (_instance == null)
            {
                _instance = new TPSdkUnityRewardVideo();
            }
            return _instance;
        }

        public void LoadRewardVideoAd(string adUnitId, TPRewardVideoExtra extra)
        {
            
        }

        public void ShowRewardVideoAd(string adUnitId, string sceneId)
        {
          

        }

        public bool RewardVideoAdReady(string adUnitId)
        {
            return true;
        }

        public void EntryRewardVideoAdScenario(string adUnitId, string sceneId)
        {
            
        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.reward.TPRewardListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdReward(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoReward(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdVideoStart(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoPlayStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 播放开始，仅回调给客户 8.1

            void onAdVideoEnd(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoPlayEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 播放结束，仅回调给客户 8.1

            void onAdVideoError(string unitId, string tpAdInfo, string error) {
                //TPSdkUnityRewardVideo.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId,bool b)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror),(Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoStartLoad(unitId,null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TPSdkUnityRewardVideo.Instance().OnRewardVideoIsLoading(unitId);

            }

            void onAdAgainImpression(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnPlayAgainImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAgainVideoStart(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnPlayAgainVideoPlayStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAgainVideoEnd(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnPlayAgainVideoPlayEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAgainVideoClicked(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnPlayAgainClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdPlayAgainReward(string unitId, string tpAdInfo) {
                TPSdkUnityRewardVideo.Instance().OnPlayAgainReward(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityRewardVideo.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes,currBytes,fileName,appName);
            }

            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TPSdkUnityRewardVideo.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName,progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityRewardVideo.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityRewardVideo.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityRewardVideo.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TPSdkUnityRewardVideo.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TPSdkUnityRewardVideo()
        {

        }

        public event Action<string, Dictionary<string, object>> OnRewardVideoLoaded;

        public event Action<string, Dictionary<string, object>> OnRewardVideoLoadFailed;

        public event Action<string, Dictionary<string, object>> OnRewardVideoImpression;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoShowFailed;

        public event Action<string, Dictionary<string, object>> OnRewardVideoClicked;

        public event Action<string, Dictionary<string, object>> OnRewardVideoClosed;

        public event Action<string, Dictionary<string, object>> OnRewardVideoReward;

        public event Action<string, Dictionary<string, object>> OnRewardVideoStartLoad;

        public event Action<string, Dictionary<string, object>> OnRewardVideoBiddingStart;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoBiddingEnd;

        public event Action<string> OnRewardVideoIsLoading;

        public event Action<string, Dictionary<string, object>> OnRewardVideoOneLayerStartLoad;

        public event Action<string, Dictionary<string, object>> OnRewardVideoOneLayerLoaded;

        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoOneLayerLoadFailed;

        public event Action<string, Dictionary<string, object>> OnRewardVideoPlayStart;

        public event Action<string, Dictionary<string, object>> OnRewardVideoPlayEnd;

        public event Action<string, bool> OnRewardVideoAllLoaded;

        // 国内再看一个相关回调

        public event Action<string, Dictionary<string, object>> OnPlayAgainImpression;

        public event Action<string, Dictionary<string, object>> OnPlayAgainReward;

        public event Action<string, Dictionary<string, object>> OnPlayAgainClicked;

        public event Action<string, Dictionary<string, object>> OnPlayAgainVideoPlayStart;

        public event Action<string, Dictionary<string, object>> OnPlayAgainVideoPlayEnd;

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