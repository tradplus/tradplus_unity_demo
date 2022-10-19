using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using System;

public class InterstitialUI : MonoBehaviour
{
    string adUnitId = Configure.Instance().InterstitialUnitId;
    string sceneId = Configure.Instance().InterstitialSceneId;
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
        float height = (Screen.height - 140) / 9 - 20;
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
            TPInterstitialExtra extra = new TPInterstitialExtra();
            extra.isAutoLoad = Configure.Instance().AutoLoad;
            if (Configure.Instance().UseAdCustomMap)
            {
                //流量分组相关
                Dictionary<string, string> customMap = new Dictionary<string, string>();
                customMap.Add("user_id", "test_interstitial_userid");
                customMap.Add("custom_data", "interstitial_TestIMP");
                customMap.Add("segment_tag", "interstitial_segment_tag");
                extra.customMap = customMap;
                //Android设置特殊参数
                Dictionary<string, string> localParams = new Dictionary<string, string>();
                localParams.Add("user_id", "interstitial_userId");
                localParams.Add("custom_data", "interstitial_customData");
                extra.localParams = localParams;
            }
            //加载插屏
            TradplusInterstitial.Instance().LoadInterstitialAd(adUnitId, extra);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("isReady"))
        {
            bool isReady = TradplusInterstitial.Instance().InterstitialAdReady(adUnitId);
            infoStr = "isReady: "+ isReady;
        }
        GUILayout.Space(20);
        if (GUILayout.Button("展示"))
        {
            infoStr = "";
            bool isReady = TradplusInterstitial.Instance().InterstitialAdReady(adUnitId);
            //判断是否有广告
            if (isReady)
            {
                //调用展示前设置自定义信息
                Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
                customAdInfo.Add("act", "Show");
                customAdInfo.Add("time", "" + DateTimeOffset.Now);
                TradplusInterstitial.Instance().SetCustomAdInfo(adUnitId, customAdInfo);

                //展示广告
                TradplusInterstitial.Instance().ShowInterstitialAd(adUnitId, sceneId);
            }
        }
        GUILayout.Space(20);
        GUILayout.Label(infoStr);
        GUILayout.Space(20);
        if (GUILayout.Button("进入广告场景"))
        {
            //进入广告场景
            TradplusInterstitial.Instance().EntryInterstitialAdScenario(adUnitId, sceneId);
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
        TradplusInterstitial.Instance().OnInterstitialLoaded += OnlLoaded;
        TradplusInterstitial.Instance().OnInterstitialLoadFailed += OnLoadFailed;
        TradplusInterstitial.Instance().OnInterstitialImpression += OnImpression;
        TradplusInterstitial.Instance().OnInterstitialShowFailed += OnShowFailed;
        TradplusInterstitial.Instance().OnInterstitialClicked += OnClicked;
        TradplusInterstitial.Instance().OnInterstitialClosed += OnClosed;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusInterstitial.Instance().OnInterstitialStartLoad += OnStartLoad;
        TradplusInterstitial.Instance().OnInterstitialBiddingStart += OnBiddingStart;
        TradplusInterstitial.Instance().OnInterstitialBiddingEnd += OnBiddingEnd;
        TradplusInterstitial.Instance().OnInterstitialOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoaded += OnOneLayerLoaded;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayStart += OnVideoPlayStart;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayEnd += OnVideoPlayEnd;
        TradplusInterstitial.Instance().OnInterstitialAllLoaded += OnAllLoaded;

#if UNITY_ANDROID

        TradplusInterstitial.Instance().OnDownloadStart += OnDownloadStart;
        TradplusInterstitial.Instance().OnDownloadUpdate += OnDownloadUpdate;
        TradplusInterstitial.Instance().OnDownloadFinish += OnDownloadFinish;
        TradplusInterstitial.Instance().OnDownloadFailed += OnDownloadFailed;
        TradplusInterstitial.Instance().OnDownloadPause += OnDownloadPause;
        TradplusInterstitial.Instance().OnInstalled += OnInstallled;
#endif
    }

    private void OnDestroy()
    {
        TradplusInterstitial.Instance().OnInterstitialLoaded -= OnlLoaded;
        TradplusInterstitial.Instance().OnInterstitialLoadFailed -= OnLoadFailed;
        TradplusInterstitial.Instance().OnInterstitialImpression -= OnImpression;
        TradplusInterstitial.Instance().OnInterstitialShowFailed -= OnShowFailed;
        TradplusInterstitial.Instance().OnInterstitialClicked -= OnClicked;
        TradplusInterstitial.Instance().OnInterstitialClosed -= OnClosed;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoadFailed -= OnOneLayerLoadFailed;

        TradplusInterstitial.Instance().OnInterstitialStartLoad -= OnStartLoad;
        TradplusInterstitial.Instance().OnInterstitialBiddingStart -= OnBiddingStart;
        TradplusInterstitial.Instance().OnInterstitialBiddingEnd -= OnBiddingEnd;
        TradplusInterstitial.Instance().OnInterstitialOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusInterstitial.Instance().OnInterstitialOneLayerLoaded -= OnOneLayerLoaded;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayStart -= OnVideoPlayStart;
        TradplusInterstitial.Instance().OnInterstitialVideoPlayEnd -= OnVideoPlayEnd;
        TradplusInterstitial.Instance().OnInterstitialAllLoaded -= OnAllLoaded;

#if UNITY_ANDROID

        TradplusInterstitial.Instance().OnDownloadStart -= OnDownloadStart;
        TradplusInterstitial.Instance().OnDownloadUpdate -= OnDownloadUpdate;
        TradplusInterstitial.Instance().OnDownloadFinish -= OnDownloadFinish;
        TradplusInterstitial.Instance().OnDownloadFailed -= OnDownloadFailed;
        TradplusInterstitial.Instance().OnDownloadPause -= OnDownloadPause;
        TradplusInterstitial.Instance().OnInstalled -= OnInstallled;
#endif

    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        Configure.Instance().ShowLog("InterstitialUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        Configure.Instance().ShowLog("InterstitialUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterstitialUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterstitialUI OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterstitialUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnVideoPlayStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterstitialUI OnVideoPlayEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("InterstitialUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnDownloadStart(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadStart ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadUpdate(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadUpdate ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "; progress:" + progress);
    }

    void OnDownloadPause(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadPause ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFinish(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadFinish ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFailed(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnDownloadFailed ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnInstallled(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("InterstitialUI OnInstallled ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }
}