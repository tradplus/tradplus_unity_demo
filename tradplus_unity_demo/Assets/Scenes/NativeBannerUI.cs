using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NativeBannerUI : MonoBehaviour
{
    string adUnitId = Configure.Instance().NativeBannerUnitId;
    string sceneId = Configure.Instance().NativeBannerSceneId;
    string infoStr = "";
    bool editMode = false;
    bool closeAutoShow = false;
    string editInfo = "";
    TradplusBase.AdPosition adPostion = TradplusBase.AdPosition.TopLeft;
    string xStr = "0";
    string yStr = "0";
    string widthStr = "320";
    string heightStr = "50";

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
        if (closeAutoShow)
        {
            editInfo += ", closeAutoShow";
        }
        else
        {
            editInfo += ", AutoShow";
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
                TPNativeBannerExtra extra = new TPNativeBannerExtra();
                extra.x = int.Parse(xStr);
                extra.y = int.Parse(yStr);
                extra.width = int.Parse(widthStr);
                extra.height = int.Parse(heightStr);
                extra.closeAutoShow = closeAutoShow;
                extra.adPosition = adPostion;
                if (Configure.Instance().UseAdCustomMap)
                {
                    //流量分组相关
                    Dictionary<string, string> customMap = new Dictionary<string, string>();
                    customMap.Add("user_id", "test_nativeBanner_userid");
                    customMap.Add("custom_data", "nativeBanner_TestIMP");
                    customMap.Add("segment_tag", "nativeBanner_segment_tag");
                    extra.customMap = customMap;
                    //Android设置特殊参数
                    Dictionary<string, string> localParams = new Dictionary<string, string>();
                    localParams.Add("user_id", "nativeBanner_userId");
                    localParams.Add("custom_data", "nativeBanner_customData");
                    extra.localParams = localParams;
                }
                //加载原生横幅
                TradplusNativeBanner.Instance().LoadNativeBannerAd(adUnitId, sceneId,extra);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("isReady"))
            {
                bool isReady = TradplusNativeBanner.Instance().NativeBannerAdReady(adUnitId);
                infoStr = "isReady: " + isReady;
            }
            GUILayout.Space(20);
            if (GUILayout.Button("展示"))
            {
                infoStr = "";
                TradplusNativeBanner.Instance().ShowNativeBannerAd(adUnitId, sceneId);
            }
            GUILayout.Space(20);
            GUILayout.Label(infoStr);

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("隐藏"))
            {
                infoStr = "已隐藏";
                TradplusNativeBanner.Instance().HideNativeBanner(adUnitId);
            }

            if (GUILayout.Button("显示"))
            {
                infoStr = "取消隐藏";
                TradplusNativeBanner.Instance().DisplayNativeBanner(adUnitId);
            }

            if (GUILayout.Button("销毁"))
            {
                infoStr = "已销毁";
                TradplusNativeBanner.Instance().DestroyNativeBanner(adUnitId);
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(20);
            if (GUILayout.Button("进入广告场景"))
            {
                TradplusNativeBanner.Instance().EntryNativeBannerAdScenario(adUnitId, sceneId);
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

            string title = "展示状态：AutoShow";
            if (closeAutoShow)
            {
                title = "展示状态：CloseAutoShow";
            }
            if (GUILayout.Button(title))
            {
                closeAutoShow = !closeAutoShow;
            }
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
        TradplusNativeBanner.Instance().OnNativeBannerLoaded += OnlLoaded;
        TradplusNativeBanner.Instance().OnNativeBannerLoadFailed += OnLoadFailed;
        TradplusNativeBanner.Instance().OnNativeBannerImpression += OnImpression;
        TradplusNativeBanner.Instance().OnNativeBannerShowFailed += OnShowFailed;
        TradplusNativeBanner.Instance().OnNativeBannerClicked += OnClicked;
        TradplusNativeBanner.Instance().OnNativeBannerOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusNativeBanner.Instance().OnNativeBannerStartLoad += OnStartLoad;
        TradplusNativeBanner.Instance().OnNativeBannerBiddingStart += OnBiddingStart;
        TradplusNativeBanner.Instance().OnNativeBannerBiddingEnd += OnBiddingEnd;
        TradplusNativeBanner.Instance().OnNativeBannerOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusNativeBanner.Instance().OnNativeBannerOneLayerLoaded += OnOneLayerLoaded;
        TradplusNativeBanner.Instance().OnNativeBannerAllLoaded += OnAllLoaded;


#if UNITY_ANDROID

        TradplusNativeBanner.Instance().OnDownloadStart += OnDownloadStart;
        TradplusNativeBanner.Instance().OnDownloadUpdate += OnDownloadUpdate;
        TradplusNativeBanner.Instance().OnDownloadFinish += OnDownloadFinish;
        TradplusNativeBanner.Instance().OnDownloadFailed += OnDownloadFailed;
        TradplusNativeBanner.Instance().OnDownloadPause += OnDownloadPause;
        TradplusNativeBanner.Instance().OnInstalled += OnInstallled;
#endif
    }

    private void OnDestroy()
    {
        TradplusNativeBanner.Instance().OnNativeBannerLoaded -= OnlLoaded;
        TradplusNativeBanner.Instance().OnNativeBannerLoadFailed -= OnLoadFailed;
        TradplusNativeBanner.Instance().OnNativeBannerImpression -= OnImpression;
        TradplusNativeBanner.Instance().OnNativeBannerShowFailed -= OnShowFailed;
        TradplusNativeBanner.Instance().OnNativeBannerClicked -= OnClicked;
        TradplusNativeBanner.Instance().OnNativeBannerOneLayerLoadFailed -= OnOneLayerLoadFailed;

        TradplusNativeBanner.Instance().OnNativeBannerStartLoad -= OnStartLoad;
        TradplusNativeBanner.Instance().OnNativeBannerBiddingStart -= OnBiddingStart;
        TradplusNativeBanner.Instance().OnNativeBannerBiddingEnd -= OnBiddingEnd;
        TradplusNativeBanner.Instance().OnNativeBannerOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusNativeBanner.Instance().OnNativeBannerOneLayerLoaded -= OnOneLayerLoaded;
        TradplusNativeBanner.Instance().OnNativeBannerAllLoaded -= OnAllLoaded;

#if UNITY_ANDROID

        TradplusNativeBanner.Instance().OnDownloadStart -= OnDownloadStart;
        TradplusNativeBanner.Instance().OnDownloadUpdate -= OnDownloadUpdate;
        TradplusNativeBanner.Instance().OnDownloadFinish -= OnDownloadFinish;
        TradplusNativeBanner.Instance().OnDownloadFailed -= OnDownloadFailed;
        TradplusNativeBanner.Instance().OnDownloadPause -= OnDownloadPause;
        TradplusNativeBanner.Instance().OnInstalled -= OnInstallled;
#endif

        TradplusNativeBanner.Instance().DestroyNativeBanner(adUnitId);
    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        Configure.Instance().ShowLog("NativeBannerUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        Configure.Instance().ShowLog("NativeBannerUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "";
        Configure.Instance().ShowLog("NativeBannerUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnDownloadStart(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnDownloadStart ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadUpdate(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnDownloadUpdate ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "; progress:" + progress);
    }

    void OnDownloadPause(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnDownloadPause ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFinish(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnDownloadFinish ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFailed(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnDownloadFailed ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnInstallled(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("NativeBannerUI OnInstallled ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }
}
