using System;
using System.Collections;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardedUI : MonoBehaviour
{
    string adUnitId = Configure.Instance().RewardedUnitId;
    string sceneId = Configure.Instance().RewardVideoSceneId;
    string infoStr = "";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        float height = (Screen.height - 140) / 8 - 20;
        GUI.skin.button.fixedHeight = height;
        GUI.skin.button.fontSize = (int)(height / 3);
        GUI.skin.label.fontSize = (int)(height / 3);
        GUI.skin.label.fixedHeight = height;

        var rect = new Rect(20, 20, Screen.width - 40, Screen.height);
        rect.y += Screen.safeArea.y;
        GUILayout.BeginArea(rect);
        GUILayout.Space(20);
        if (GUILayout.Button("加载"))
        {
            infoStr = "开始加载";

            TPRewardVideoExtra extra = new TPRewardVideoExtra();
            extra.isAutoLoad = Configure.Instance().AutoLoad;
              #if UNITY_ANDROID

                                  extra.isSimpleListener = Configure.Instance().SimplifyListener;
                           #endif
            if (Configure.Instance().UseAdCustomMap)
            {
                //流量分组相关
                Dictionary<string, string> customMap = new Dictionary<string, string>();
                customMap.Add("user_id", "test_rewardVideo_userid");
                customMap.Add("custom_data", "rewardVideo_TestIMP");
                customMap.Add("segment_tag", "rewardVideo_segment_tag");
                extra.customMap = customMap;

                Dictionary<string, string> localParams = new Dictionary<string, string>();
                localParams.Add("user_id", "rewardVideo_userId");
                localParams.Add("custom_data", "rewardVideo_customData");
                extra.localParams = localParams;
            }
            extra.userId = "rewardVideo_userId";
            extra.customData = "rewardVideo_customData";

            TradplusRewardVideo.Instance().LoadRewardVideoAd(adUnitId,extra);


            Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
            customAdInfo.Add("act", "Load");
            customAdInfo.Add("time", "" + DateTimeOffset.Now);
            TradplusRewardVideo.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("isReady"))
        {
            bool isReady = TradplusRewardVideo.Instance().RewardVideoAdReady(adUnitId);
            infoStr = "isReady: " + isReady;
        }
        GUILayout.Space(20);
        if (GUILayout.Button("展示"))
        {
            Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
            customAdInfo.Add("act", "Show");
            customAdInfo.Add("time", "" + DateTimeOffset.Now);
            TradplusRewardVideo.Instance().SetCustomAdInfo(adUnitId, customAdInfo);

            infoStr = "";

            TradplusRewardVideo.Instance().ShowRewardVideoAd(adUnitId, sceneId);
        }
        GUILayout.Space(20);
        GUILayout.Label(infoStr);
        GUILayout.Space(20);
        if (GUILayout.Button("进入广告场景"))
        {
            TradplusRewardVideo.Instance().EntryRewardVideoAdScenario(adUnitId, sceneId);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("日志"))
        {
            SceneManager.LoadScene("Log");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("返回首页"))
        {
            SceneManager.LoadScene("Main");
        }

        GUILayout.EndArea();
    }

    //回调设置
    private void OnEnable()
    {
        //常用
        TradplusRewardVideo.Instance().OnRewardVideoLoaded += OnlLoaded;
        TradplusRewardVideo.Instance().OnRewardVideoLoadFailed += OnLoadFailed;
        TradplusRewardVideo.Instance().OnRewardVideoImpression += OnImpression;
        TradplusRewardVideo.Instance().OnRewardVideoShowFailed += OnShowFailed;
        TradplusRewardVideo.Instance().OnRewardVideoClicked += OnClicked;
        TradplusRewardVideo.Instance().OnRewardVideoClosed += OnClosed;
        TradplusRewardVideo.Instance().OnRewardVideoReward += OnReward;
        TradplusRewardVideo.Instance().OnRewardVideoOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusRewardVideo.Instance().OnRewardVideoStartLoad += OnStartLoad;
        TradplusRewardVideo.Instance().OnRewardVideoBiddingStart += OnBiddingStart;
        TradplusRewardVideo.Instance().OnRewardVideoBiddingEnd += OnBiddingEnd;
        TradplusRewardVideo.Instance().OnRewardVideoOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusRewardVideo.Instance().OnRewardVideoOneLayerLoaded += OnOneLayerLoaded;
        TradplusRewardVideo.Instance().OnRewardVideoPlayStart += OnVideoPlayStart;
        TradplusRewardVideo.Instance().OnRewardVideoPlayEnd += OnVideoPlayEnd;
        TradplusRewardVideo.Instance().OnRewardVideoAllLoaded += OnAllLoaded;
        //再看一个
        TradplusRewardVideo.Instance().OnPlayAgainImpression += OnPlayAgainImpression;
        TradplusRewardVideo.Instance().OnPlayAgainReward += OnPlayAgainReward;
        TradplusRewardVideo.Instance().OnPlayAgainClicked += OnPlayAgainClicked;
        TradplusRewardVideo.Instance().OnPlayAgainVideoPlayStart += OnPlayAgainVideoPlayStart;
        TradplusRewardVideo.Instance().OnPlayAgainVideoPlayEnd += OnPlayAgainVideoPlayEnd;


#if UNITY_ANDROID

        TradplusRewardVideo.Instance().OnDownloadStart += OnDownloadStart;
        TradplusRewardVideo.Instance().OnDownloadUpdate += OnDownloadUpdate;
        TradplusRewardVideo.Instance().OnDownloadFinish += OnDownloadFinish;
        TradplusRewardVideo.Instance().OnDownloadFailed += OnDownloadFailed;
        TradplusRewardVideo.Instance().OnDownloadPause += OnDownloadPause;
        TradplusRewardVideo.Instance().OnInstalled += OnInstallled;
#endif
    }

    private void OnDestroy()
    {
        TradplusRewardVideo.Instance().OnRewardVideoLoaded -= OnlLoaded;
        TradplusRewardVideo.Instance().OnRewardVideoLoadFailed -= OnLoadFailed;
        TradplusRewardVideo.Instance().OnRewardVideoImpression -= OnImpression;
        TradplusRewardVideo.Instance().OnRewardVideoShowFailed -= OnShowFailed;
        TradplusRewardVideo.Instance().OnRewardVideoClicked -= OnClicked;
        TradplusRewardVideo.Instance().OnRewardVideoClosed -= OnClosed;
        TradplusRewardVideo.Instance().OnRewardVideoReward -= OnReward;
        TradplusRewardVideo.Instance().OnRewardVideoOneLayerLoadFailed -= OnOneLayerLoadFailed;

        TradplusRewardVideo.Instance().OnRewardVideoStartLoad -= OnStartLoad;
        TradplusRewardVideo.Instance().OnRewardVideoBiddingStart -= OnBiddingStart;
        TradplusRewardVideo.Instance().OnRewardVideoBiddingEnd -= OnBiddingEnd;
        TradplusRewardVideo.Instance().OnRewardVideoOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusRewardVideo.Instance().OnRewardVideoOneLayerLoaded -= OnOneLayerLoaded;
        TradplusRewardVideo.Instance().OnRewardVideoPlayStart -= OnVideoPlayStart;
        TradplusRewardVideo.Instance().OnRewardVideoPlayEnd -= OnVideoPlayEnd;
        TradplusRewardVideo.Instance().OnRewardVideoAllLoaded -= OnAllLoaded;

        TradplusRewardVideo.Instance().OnPlayAgainImpression -= OnPlayAgainImpression;
        TradplusRewardVideo.Instance().OnPlayAgainReward -= OnPlayAgainReward;
        TradplusRewardVideo.Instance().OnPlayAgainClicked -= OnPlayAgainClicked;
        TradplusRewardVideo.Instance().OnPlayAgainVideoPlayStart -= OnPlayAgainVideoPlayStart;
        TradplusRewardVideo.Instance().OnPlayAgainVideoPlayEnd -= OnPlayAgainVideoPlayEnd;

#if UNITY_ANDROID

        TradplusRewardVideo.Instance().OnDownloadStart -= OnDownloadStart;
        TradplusRewardVideo.Instance().OnDownloadUpdate -= OnDownloadUpdate;
        TradplusRewardVideo.Instance().OnDownloadFinish -= OnDownloadFinish;
        TradplusRewardVideo.Instance().OnDownloadFailed -= OnDownloadFailed;
        TradplusRewardVideo.Instance().OnDownloadPause -= OnDownloadPause;
        TradplusRewardVideo.Instance().OnInstalled -= OnInstallled;
#endif

    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        Configure.Instance().ShowLog("RewardVideoUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        Configure.Instance().ShowLog("RewardVideoUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnReward(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnReward ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnVideoPlayStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnVideoPlayEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnPlayAgainImpression(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnPlayAgainImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnPlayAgainReward(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnPlayAgainReward ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnPlayAgainClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnPlayAgainClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnPlayAgainVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnPlayAgainVideoPlayStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnPlayAgainVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnPlayAgainVideoPlayEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }


    void OnDownloadStart(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnDownloadStart ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadUpdate(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnDownloadUpdate ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "; progress:" + progress);
    }

    void OnDownloadPause(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnDownloadPause ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFinish(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnDownloadFinish ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFailed(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnDownloadFailed ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnInstallled(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("RewardVideoUI OnInstallled ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }
}