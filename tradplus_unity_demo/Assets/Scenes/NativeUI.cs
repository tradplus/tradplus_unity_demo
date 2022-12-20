using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NativeUI : MonoBehaviour
{
    string adUnitId = Configure.Instance().NativeUnitId;
    string sceneId = Configure.Instance().NativeSceneId;
    string infoStr = "";
    bool editMode = false;
    string editInfo = "";
    TradplusBase.AdPosition adPostion = TradplusBase.AdPosition.TopLeft;
    string xStr = "0";
    string yStr = "0";
    string widthStr = "320";
    string heightStr = "200";

    private void checkEditInfo()
    {
        editInfo = xStr + "," + yStr + "," + widthStr + "," + heightStr + ",";
        if (adPostion == TradplusBase.AdPosition.TopLeft)
        {
            editInfo += "TopLeft";
        }
        else if (adPostion == TradplusBase.AdPosition.TopCenter)
        {
            editInfo += "TopCenter";
        }
        else if (adPostion == TradplusBase.AdPosition.TopRight)
        {
            editInfo += "TopRight";
        }
        else if (adPostion == TradplusBase.AdPosition.Centered)
        {
            editInfo += "Centered";
        }
        else if (adPostion == TradplusBase.AdPosition.BottomLeft)
        {
            editInfo += "BottomLeft";
        }
        else if (adPostion == TradplusBase.AdPosition.BottomCenter)
        {
            editInfo += "BottomCenter";
        }
        else if (adPostion == TradplusBase.AdPosition.BottomRight)
        {
            editInfo += "BottomRight";
        }
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
        GUILayout.Space(20);

        float height = (Screen.height - 180) / 9 - 20;
        GUI.skin.button.fixedHeight = height;
        GUI.skin.button.fontSize = (int)(height / 3);
        GUI.skin.label.fontSize = (int)(height / 3);
        GUI.skin.label.fixedHeight = height;
        GUI.skin.textField.fontSize = (int)(height / 3);
        GUI.skin.textField.fixedHeight = height / 2;

        if (!editMode)
        {

            if (GUILayout.Button("编辑模式"))
            {
                editMode = true;
            }
            GUILayout.Space(20);
            GUILayout.Label(editInfo);
            if (GUILayout.Button("加载"))
            {
                infoStr = "开始加载";
                TPNativeExtra extra = new TPNativeExtra();
                extra.x = int.Parse(xStr);
                extra.y = int.Parse(yStr);
                extra.width = int.Parse(widthStr);
                extra.height = int.Parse(heightStr);
                extra.adPosition = adPostion;

                #if UNITY_ANDROID
                extra.isSimpleListener = Configure.Instance().SimplifyListener;
                #endif

                if (Configure.Instance().UseAdCustomMap)
                {
                    //流量分组相关
                    Dictionary<string, string> customMap = new Dictionary<string, string>();
                    customMap.Add("user_id", "test_native_userid");
                    customMap.Add("custom_data", "native_TestIMP");
                    customMap.Add("segment_tag", "native_segment_tag");
                    extra.customMap = customMap;

                    Dictionary<string, string> localParams = new Dictionary<string, string>();
                    localParams.Add("user_id", "native_userId");
                    localParams.Add("custom_data", "native_customData");
                    extra.localParams = localParams;
                }

                TradplusNative.Instance().LoadNativeAd(adUnitId, extra);

                Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
                customAdInfo.Add("act", "Load");
                customAdInfo.Add("time", "" + DateTimeOffset.Now);
                TradplusNative.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("isReady"))
            {
                bool isReady = TradplusNative.Instance().NativeAdReady(adUnitId);
                infoStr = "isReady: " + isReady;
            }
            GUILayout.Space(20);
            if (GUILayout.Button("展示"))
            {
                Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
                customAdInfo.Add("act", "Show");
                customAdInfo.Add("time", "" + DateTimeOffset.Now);
                TradplusNative.Instance().SetCustomAdInfo(adUnitId, customAdInfo);

                infoStr = "";
                TradplusNative.Instance().ShowNativeAd(adUnitId, sceneId);
            }
            GUILayout.Space(20);
            GUILayout.Label(infoStr);

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("隐藏"))
            {
                infoStr = "已隐藏";
                TradplusNative.Instance().HideNative(adUnitId);
            }

            if (GUILayout.Button("显示"))
            {
                infoStr = "取消隐藏";
                TradplusNative.Instance().DisplayNative(adUnitId);
            }

            if (GUILayout.Button("销毁"))
            {
                infoStr = "已销毁";
                TradplusNative.Instance().DestroyNative(adUnitId);
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(20);
            if (GUILayout.Button("进入广告场景"))
            {
                TradplusNative.Instance().EntryNativeAdScenario(adUnitId, sceneId);
            }
            GUILayout.Space(20);
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
            GUILayout.Space(20);

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

            GUILayout.Space(20);
            string text = "";
            if (adPostion == TradplusBase.AdPosition.TopLeft)
            {
                text = "定位模式：TopLeft";
            }
            else if (adPostion == TradplusBase.AdPosition.TopCenter)
            {
                text = "定位模式：TopCenter";
            }
            else if (adPostion == TradplusBase.AdPosition.TopRight)
            {
                text = "定位模式：TopRight";
            }
            else if (adPostion == TradplusBase.AdPosition.Centered)
            {
                text = "定位模式：Centered";
            }
            else if (adPostion == TradplusBase.AdPosition.BottomLeft)
            {
                text = "定位模式：BottomLeft";
            }
            else if (adPostion == TradplusBase.AdPosition.BottomCenter)
            {
                text = "定位模式：BottomCenter";
            }
            else if (adPostion == TradplusBase.AdPosition.BottomRight)
            {
                text = "定位模式：BottomRight";
            }
            GUILayout.Label(text);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("TopLeft"))
            {
                adPostion = TradplusBase.AdPosition.TopLeft;
            }
            if (GUILayout.Button("TopCenter"))
            {
                adPostion = TradplusBase.AdPosition.TopCenter;
            }
            if (GUILayout.Button("TopRight"))
            {
                adPostion = TradplusBase.AdPosition.TopRight;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("BottomLeft"))
            {
                adPostion = TradplusBase.AdPosition.BottomLeft;
            }
            if (GUILayout.Button("BottomCenter"))
            {
                adPostion = TradplusBase.AdPosition.BottomCenter;
            }
            if (GUILayout.Button("BottomRight"))
            {
                adPostion = TradplusBase.AdPosition.BottomRight;
            }
            GUILayout.EndHorizontal();

        }
        GUILayout.EndArea();

    }

    //回调设置
    private void OnEnable()
    {
        //常用
        TradplusNative.Instance().OnNativeLoaded += OnlLoaded;
        TradplusNative.Instance().OnNativeLoadFailed += OnLoadFailed;
        TradplusNative.Instance().OnNativeImpression += OnImpression;
        TradplusNative.Instance().OnNativeShowFailed += OnShowFailed;
        TradplusNative.Instance().OnNativeClicked += OnClicked;
        TradplusNative.Instance().OnNativeClosed += OnClosed;
        TradplusNative.Instance().OnNativeOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusNative.Instance().OnNativeStartLoad += OnStartLoad;
        TradplusNative.Instance().OnNativeBiddingStart += OnBiddingStart;
        TradplusNative.Instance().OnNativeBiddingEnd += OnBiddingEnd;
        TradplusNative.Instance().OnNativeOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusNative.Instance().OnNativeOneLayerLoaded += OnOneLayerLoaded;
        TradplusNative.Instance().OnNativeVideoPlayStart += OnVideoPlayStart;
        TradplusNative.Instance().OnNativeVideoPlayEnd += OnVideoPlayEnd;
        TradplusNative.Instance().OnNativeAllLoaded += OnAllLoaded;

#if UNITY_ANDROID

        TradplusNative.Instance().OnDownloadStart += OnDownloadStart;
        TradplusNative.Instance().OnDownloadUpdate += OnDownloadUpdate;
        TradplusNative.Instance().OnDownloadFinish += OnDownloadFinish;
        TradplusNative.Instance().OnDownloadFailed += OnDownloadFailed;
        TradplusNative.Instance().OnDownloadPause += OnDownloadPause;
        TradplusNative.Instance().OnInstalled += OnInstallled;
#endif

    }

    private void OnDestroy()
    {
        TradplusNative.Instance().OnNativeLoaded -= OnlLoaded;
        TradplusNative.Instance().OnNativeLoadFailed -= OnLoadFailed;
        TradplusNative.Instance().OnNativeImpression -= OnImpression;
        TradplusNative.Instance().OnNativeShowFailed -= OnShowFailed;
        TradplusNative.Instance().OnNativeClicked -= OnClicked;
        TradplusNative.Instance().OnNativeClosed -= OnClosed;
        TradplusNative.Instance().OnNativeOneLayerLoadFailed -= OnOneLayerLoadFailed;

        TradplusNative.Instance().OnNativeStartLoad -= OnStartLoad;
        TradplusNative.Instance().OnNativeBiddingStart -= OnBiddingStart;
        TradplusNative.Instance().OnNativeBiddingEnd -= OnBiddingEnd;
        TradplusNative.Instance().OnNativeOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusNative.Instance().OnNativeOneLayerLoaded -= OnOneLayerLoaded;
        TradplusNative.Instance().OnNativeVideoPlayStart -= OnVideoPlayStart;
        TradplusNative.Instance().OnNativeVideoPlayEnd -= OnVideoPlayEnd;
        TradplusNative.Instance().OnNativeAllLoaded -= OnAllLoaded;

#if UNITY_ANDROID

        TradplusNative.Instance().OnDownloadStart -= OnDownloadStart;
        TradplusNative.Instance().OnDownloadUpdate -= OnDownloadUpdate;
        TradplusNative.Instance().OnDownloadFinish -= OnDownloadFinish;
        TradplusNative.Instance().OnDownloadFailed -= OnDownloadFailed;
        TradplusNative.Instance().OnDownloadPause -= OnDownloadPause;
        TradplusNative.Instance().OnInstalled -= OnInstallled;
#endif



        TradplusNative.Instance().DestroyNative(adUnitId);
    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        Configure.Instance().ShowLog("NativeUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        Configure.Instance().ShowLog("NativeUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "";
        Configure.Instance().ShowLog("NativeUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("NativeUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("NativeUI OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("NativeUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnVideoPlayStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnVideoPlayStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnVideoPlayEnd(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeUI OnVideoPlayEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("NativeUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnDownloadStart(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeUI OnDownloadStart ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadUpdate(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
    {
        Configure.Instance().ShowLog("NativeUI OnDownloadUpdate ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "; progress:" + progress);
    }

    void OnDownloadPause(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeUI OnDownloadPause ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFinish(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeUI OnDownloadFinish ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFailed(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeUI OnDownloadFailed ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnInstallled(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeUI OnInstallled ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }
}
