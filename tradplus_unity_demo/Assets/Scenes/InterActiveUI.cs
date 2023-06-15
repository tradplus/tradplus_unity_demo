using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterActiveUI : MonoBehaviour
{
    string adUnitId = Configure.Instance().InterActiveUnitId;
    string sceneId = Configure.Instance().InterActiveSceneId;
    string infoStr = "";
    bool editMode = false;
    string editInfo = "";
    string xStr = "0";
    string yStr = "0";
    string widthStr = "50";
    string heightStr = "50";

    private void checkEditInfo()
    {
        editInfo = xStr + "," + yStr + "," + widthStr + "," + heightStr;
    }

    // Start is called before the first frame update
    void Start()
    {
       checkEditInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        var rect = new Rect(20, 20, Screen.width - 40, Screen.height);
        rect.y += Screen.safeArea.y;
        GUILayout.BeginArea(rect);
        GUILayout.Space(10);

        float height = (Screen.height - 180) / 15 - 20;
        GUI.skin.button.fixedHeight = height;
        GUI.skin.button.fontSize = (int)(height / 3);
        GUI.skin.label.fontSize = (int)(height / 3);
        GUI.skin.label.fixedHeight = height;
        GUI.skin.textField.fontSize = (int)(height / 3);
        GUI.skin.textField.fixedHeight = height / 3;

        if (!editMode)
        {

            if (GUILayout.Button("编辑模式"))
            {
                editMode = true;
            }
            GUILayout.Space(10);
            GUILayout.Label(editInfo);
            if (GUILayout.Button("加载"))
            {
                infoStr = "开始加载";
                TPInterActiveExtra extra = new TPInterActiveExtra();
                extra.x = int.Parse(xStr);
                extra.y = int.Parse(yStr);
                extra.width = int.Parse(widthStr);
                extra.height = int.Parse(heightStr);
#if UNITY_ANDROID

                        extra.isSimpleListener = Configure.Instance().SimplifyListener;
#endif
                Dictionary<string, object> localParams = new Dictionary<string, object>();
                if (Configure.Instance().UseAdCustomMap)
                {
                    //流量分组相关
                    Dictionary<string, string> customMap = new Dictionary<string, string>();
                    customMap.Add("user_id", "test_InterActive_userid");
                    customMap.Add("custom_data", "InterActive_TestIMP");
                    customMap.Add("segment_tag", "InterActive_segment_tag");
                    extra.customMap = customMap;

                    localParams.Add("user_id", "InterActive_userId");
                    localParams.Add("custom_data", "InterActive_customData");
                }
                extra.localParams = localParams;

                TradplusInterActive.Instance().LoadInterActiveAd(adUnitId, extra);

                Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
                customAdInfo.Add("act", "Load");
                customAdInfo.Add("time", "" + DateTimeOffset.Now);
                TradplusInterActive.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
            }
            GUILayout.Space(10);
            if (GUILayout.Button("isReady"))
            {
                bool isReady = TradplusInterActive.Instance().InterActiveAdReady(adUnitId);
                infoStr = "isReady: " + isReady;
            }
            GUILayout.Space(10);
            if (GUILayout.Button("展示"))
            {
                Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
                customAdInfo.Add("act", "Show");
                customAdInfo.Add("time", "" + DateTimeOffset.Now);
                TradplusInterActive.Instance().SetCustomAdInfo(adUnitId, customAdInfo);

                infoStr = "";
                TradplusInterActive.Instance().ShowInterActiveAd(adUnitId, sceneId);
            }
            GUILayout.Space(10);
            GUILayout.Label(infoStr);

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("隐藏"))
            {
                infoStr = "已隐藏";
                TradplusInterActive.Instance().HideInterActive(adUnitId);
            }

            if (GUILayout.Button("显示"))
            {
                infoStr = "取消隐藏";
                TradplusInterActive.Instance().DisplayInterActive(adUnitId);
            }

            if (GUILayout.Button("销毁"))
            {
                infoStr = "已销毁";
                TradplusInterActive.Instance().DestroyInterActive(adUnitId);
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            if (GUILayout.Button("日志"))
            {
                SceneManager.LoadScene("Log");
            }
            GUILayout.Space(10);
            if (GUILayout.Button("返回首页"))
            {
                SceneManager.LoadScene("Main");
            }
        }
        else
        {
            //设置参数
            if (GUILayout.Button("退出编辑"))
            {
                checkEditInfo();
                editMode = false;
            }
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("x", GUILayout.Width(200));
            xStr = GUILayout.TextField(xStr, GUILayout.MaxWidth(Screen.width - 250));
            xStr = Regex.Replace(xStr, @"[^0-9]", "");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("y", GUILayout.Width(200));
            yStr = GUILayout.TextField(yStr, GUILayout.MaxWidth(Screen.width - 250));
            yStr = Regex.Replace(yStr, @"[^0-9]", "");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("width", GUILayout.Width(200));
            widthStr = GUILayout.TextField(widthStr, GUILayout.MaxWidth(Screen.width - 250));
            widthStr = Regex.Replace(widthStr, @"[^0-9]", "");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("height", GUILayout.Width(200));
            heightStr = GUILayout.TextField(heightStr, GUILayout.MaxWidth(Screen.width - 250));
            heightStr = Regex.Replace(heightStr, @"[^0-9]", "");
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();
    }

    //回调设置
    private void OnEnable()
    {
        //常用
        TradplusInterActive.Instance().OnInterActiveLoaded += OnlLoaded;
        TradplusInterActive.Instance().OnInterActiveLoadFailed += OnLoadFailed;
        TradplusInterActive.Instance().OnInterActiveImpression += OnImpression;
        TradplusInterActive.Instance().OnInterActiveShowFailed += OnShowFailed;
        TradplusInterActive.Instance().OnInterActiveClicked += OnClicked;
        TradplusInterActive.Instance().OnInterActiveClosed += OnClosed;
        TradplusInterActive.Instance().OnInterActiveOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusInterActive.Instance().OnInterActiveStartLoad += OnStartLoad;
        TradplusInterActive.Instance().OnInterActiveBiddingStart += OnBiddingStart;
        TradplusInterActive.Instance().OnInterActiveBiddingEnd += OnBiddingEnd;
        TradplusInterActive.Instance().OnInterActiveIsLoading += OnAdIsLoading;

        TradplusInterActive.Instance().OnInterActiveOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusInterActive.Instance().OnInterActiveOneLayerLoaded += OnOneLayerLoaded;
        TradplusInterActive.Instance().OnInterActiveVideoPlayStart += OnVideoPlayStart;
        TradplusInterActive.Instance().OnInterActiveVideoPlayEnd += OnVideoPlayEnd;
        TradplusInterActive.Instance().OnInterActiveAllLoaded += OnAllLoaded;

    }

    private void OnDestroy()
    {
        TradplusInterActive.Instance().OnInterActiveLoaded -= OnlLoaded;
        TradplusInterActive.Instance().OnInterActiveLoadFailed -= OnLoadFailed;
        TradplusInterActive.Instance().OnInterActiveImpression -= OnImpression;
        TradplusInterActive.Instance().OnInterActiveShowFailed -= OnShowFailed;
        TradplusInterActive.Instance().OnInterActiveClicked -= OnClicked;
        TradplusInterActive.Instance().OnInterActiveClosed -= OnClosed;
        TradplusInterActive.Instance().OnInterActiveOneLayerLoadFailed -= OnOneLayerLoadFailed;

        TradplusInterActive.Instance().OnInterActiveStartLoad -= OnStartLoad;
        TradplusInterActive.Instance().OnInterActiveBiddingStart -= OnBiddingStart;
        TradplusInterActive.Instance().OnInterActiveBiddingEnd -= OnBiddingEnd;
        TradplusInterActive.Instance().OnInterActiveIsLoading -= OnAdIsLoading;

        TradplusInterActive.Instance().OnInterActiveOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusInterActive.Instance().OnInterActiveOneLayerLoaded -= OnOneLayerLoaded;
        TradplusInterActive.Instance().OnInterActiveVideoPlayStart -= OnVideoPlayStart;
        TradplusInterActive.Instance().OnInterActiveVideoPlayEnd -= OnVideoPlayEnd;
        TradplusInterActive.Instance().OnInterActiveAllLoaded -= OnAllLoaded;
    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        Configure.Instance().ShowLog("InterActiveUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        Configure.Instance().ShowLog("InterActiveUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "";
        Configure.Instance().ShowLog("InterActiveUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterActiveUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterActiveUI OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnAdIsLoading(string adunit)
    {
        Configure.Instance().ShowLog("InterActiveUI OnAdIsLoading ------ adunit:" + adunit );
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("InterActiveUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnVideoPlayStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("InterActiveUI OnVideoPlayEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("InterActiveUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }
}
