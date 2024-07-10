
using System;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;

namespace TradplusSDK.Android
{

    public class TradplusRewardVideoAndroid
    {
        private static AndroidJavaClass TPRewardClass = new AndroidJavaClass("com.tradplus.unity.plugin.reward.TPRewardManager");
        private AndroidJavaObject TPReward = TPRewardClass.CallStatic<AndroidJavaObject>("getInstance");


        private static TradplusRewardVideoAndroid _instance;

        public static TradplusRewardVideoAndroid Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusRewardVideoAndroid();
            }
            return _instance;
        }

        public void LoadRewardVideoAd(string adUnitId, TPRewardVideoExtra extra)
        {
            AdLoadListenerAdapter loadListenerAdapter = new AdLoadListenerAdapter();
            Dictionary<string, object> info = new Dictionary<string, object>();
            info.Add("customMap",extra.customMap);
            info.Add("localParams", extra.localParams);
            info.Add("userId", extra.userId);
            info.Add("customData", extra.customData);
            info.Add("isSimpleListener", extra.isSimpleListener);
            info.Add("maxWaitTime", extra.maxWaitTime);
            info.Add("openAutoLoadCallback", extra.openAutoLoadCallback);



            TPReward.Call("loadAd",adUnitId,Json.Serialize(info),loadListenerAdapter);
        }

        public void ShowRewardVideoAd(string adUnitId, string sceneId)
        {
            TPReward.Call("showAd", adUnitId, sceneId);

        }

        public bool RewardVideoAdReady(string adUnitId)
        {
            return TPReward.Call<bool>("isReady",adUnitId);
        }

        public void EntryRewardVideoAdScenario(string adUnitId, string sceneId)
        {
            TPReward.Call("entryAdScenario", adUnitId, sceneId);
        }

        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPReward.Call("setCustomShowData", adUnitId, Json.Serialize(customAdInfo));

        }

        private class AdLoadListenerAdapter : AndroidJavaProxy
        {

            public AdLoadListenerAdapter() : base("com.tradplus.unity.plugin.reward.TPRewardListener")
            {
            }

            void onAdLoaded(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdClicked(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdImpression(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }// 展示 1300

            void onAdFailed(string unitId, string error) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(error));
            }

            void onAdClosed(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoClosed(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 关闭 1400

            void onAdReward(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoReward(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdVideoStart(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoPlayStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 播放开始，仅回调给客户 8.1

            void onAdVideoEnd(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoPlayEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            } // 播放结束，仅回调给客户 8.1

            void onAdVideoError(string unitId, string tpAdInfo, string error) {
                //TradplusRewardVideoAndroid.Instance().videoerror(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAllLoaded(string unitId,bool b)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoAllLoaded(unitId, b);

            }

            void oneLayerLoadFailed(string unitId, string tperror, string tpAdInfo)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoOneLayerLoadFailed(unitId, (Dictionary<string, object>)Json.Deserialize(tperror),(Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void oneLayerLoaded(string unitId, string tpAdInfo)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoOneLayerLoaded(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onAdStartLoad(string unitId)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoStartLoad(unitId,null);

            }

            void oneLayerLoadStart(string unitId, string tpAdInfo)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoOneLayerStartLoad(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingStart(string unitId, string tpAdInfo)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoBiddingStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));

            }

            void onBiddingEnd(string unitId, string tpAdInfo, string tpAdError)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoBiddingEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), (Dictionary<string, object>)Json.Deserialize(tpAdError));

            }

            void onAdIsLoading(string unitId)
            {
                TradplusRewardVideoAndroid.Instance().OnRewardVideoIsLoading(unitId);

            }

            void onAdAgainImpression(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnPlayAgainImpression(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAgainVideoStart(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnPlayAgainVideoPlayStart(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAgainVideoEnd(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnPlayAgainVideoPlayEnd(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdAgainVideoClicked(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnPlayAgainClicked(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onAdPlayAgainReward(string unitId, string tpAdInfo) {
                TradplusRewardVideoAndroid.Instance().OnPlayAgainReward(unitId, (Dictionary<string, object>)Json.Deserialize(tpAdInfo));
            }

            void onDownloadStart(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusRewardVideoAndroid.Instance().OnDownloadStart(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes,currBytes,fileName,appName);
            }

            void onDownloadUpdate(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
            {
                TradplusRewardVideoAndroid.Instance().OnDownloadUpdate(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName,progress);
            }

            void onDownloadPause(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusRewardVideoAndroid.Instance().OnDownloadPause(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFinish(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusRewardVideoAndroid.Instance().OnDownloadFinish(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onDownloadFail(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusRewardVideoAndroid.Instance().OnDownloadFailed(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }


            void onInstallled(string adunit, string tpAdInfo, long totalBytes, long currBytes, string fileName, string appName)
            {
                TradplusRewardVideoAndroid.Instance().OnInstalled(adunit, (Dictionary<string, object>)Json.Deserialize(tpAdInfo), totalBytes, currBytes, fileName, appName);
            }
        }

        public TradplusRewardVideoAndroid()
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