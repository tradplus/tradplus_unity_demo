using System.Collections;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    bool onlyCn;
    bool splashShow;
    string splashUnitId;
    // Start is called before the first frame update
    void Start()
    {
        int index = PlayerPrefs.GetInt("tp.demo.UseAdCustomMap", 1);
        Configure.Instance().UseAdCustomMap = (index == 1);

 #if UNITY_ANDROID
        index = PlayerPrefs.GetInt("tp.demo.SimplifyListener", 0);
        Configure.Instance().SimplifyListener = (index == 1);
 #endif
        index = PlayerPrefs.GetInt("tp.demo.onlyCn", 0);
        onlyCn = (index == 1);

        //初始化SDK
        string appId = Configure.Instance().AppId;
        Dictionary<string, string> customMap = Configure.Instance().MainCustomMap;
        TradplusAds.Instance().SetCustomMap(customMap);
        TradplusAds.Instance().SetCnServer(onlyCn);
        TradplusAds.Instance().InitSDK(appId);

        splashUnitId = Configure.Instance().SplashUnitId;

        //全局展示回调
        TradplusAds.Instance().AddGlobalAdImpression(OnGlobalAdImpression);

        LoadSplash();
    }

    void LoadSplash()
    {
        TPSplashExtra extra = new TPSplashExtra();
        TradplusSplash.Instance().LoadSplashAd(splashUnitId, extra);
        TradplusSplash.Instance().EntrySplashAdScenario(splashUnitId, "EntrySplashAdScenario");
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            splashUnitId = Configure.Instance().SplashUnitId;
            if (TradplusSplash.Instance().SplashAdReady(splashUnitId) && !splashShow)
            {
                splashShow = true;
                TradplusSplash.Instance().ShowSplashAd(splashUnitId, "SplashAdScenarioId");
            }
            else
            {
                LoadSplash();
            }
        }
    }

    void OnGlobalAdImpression(Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("OnGlobalAdImpression ------ adInfo:" + Json.Serialize(adInfo));
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
        if (GUILayout.Button("加载开屏"))
        {
            LoadSplash();
        }
        GUILayout.Space(20);
        if (GUILayout.Button("展示开屏"))
        {
            TradplusSplash.Instance().ShowSplashAd(splashUnitId, "SplashAdScenarioId");
        }
        GUILayout.Space(30);
        if (GUILayout.Button("插屏"))
        {
            SceneManager.LoadScene("Interstitial");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("激励视频"))
        {
            SceneManager.LoadScene("Rewarded");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("积分墙"))
        {
            SceneManager.LoadScene("Offerwall");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("原生"))
        {
            SceneManager.LoadScene("Native");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("横幅"))
        {
            SceneManager.LoadScene("Banner");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("隐私权限"))
        {
            SceneManager.LoadScene("Privacy");
        }
        //GUILayout.Space(20);
        //if (GUILayout.Button("原生横幅"))
        //{
        //    SceneManager.LoadScene("NativeBanner");
        //}
#if UNITY_ANDROID
        GUILayout.Space(20);
        if (GUILayout.Button("互动"))
        {
            SceneManager.LoadScene("InterActive");
        }
#endif

#if UNITY_ANDROID
        string text = "复杂回调关闭中，点击开启";
        if(!Configure.Instance().SimplifyListener)
        {
            text = "复杂回调开启中，点击关闭";
        }
         GUILayout.Space(40);
        if (GUILayout.Button(text))
        {
            Configure.Instance().SimplifyListener = !Configure.Instance().SimplifyListener;
        }
#endif
        GUILayout.EndArea();
    }

    //回调设置
    private void OnEnable()
    {
        TradplusAds.Instance().OnInitFinish += OnInitFinish;

        //常用
        TradplusSplash.Instance().OnSplashLoaded += OnlLoaded;
        TradplusSplash.Instance().OnSplashLoadFailed += OnLoadFailed;
        TradplusSplash.Instance().OnSplashImpression += OnImpression;
        TradplusSplash.Instance().OnSplashShowFailed += OnShowFailed;
        TradplusSplash.Instance().OnSplashClicked += OnClicked;
        TradplusSplash.Instance().OnSplashClosed += OnClosed;
        TradplusSplash.Instance().OnSplashOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusSplash.Instance().OnSplashStartLoad += OnStartLoad;
        TradplusSplash.Instance().OnSplashBiddingStart += OnBiddingStart;
        TradplusSplash.Instance().OnSplashBiddingEnd += OnBiddingEnd;
        TradplusSplash.Instance().OnSplashIsLoading += OnAdIsLoading;

        TradplusSplash.Instance().OnSplashOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusSplash.Instance().OnSplashOneLayerLoaded += OnOneLayerLoaded;
        TradplusSplash.Instance().OnSplashZoomOutStart += OnZoomOutStart;
        TradplusSplash.Instance().OnSplashZoomOutEnd += OnZoomOutEnd;
        TradplusSplash.Instance().OnSplashSkip += OnSkip;
        TradplusSplash.Instance().OnSplashAllLoaded += OnAllLoaded;


#if UNITY_ANDROID
        TradplusSplash.Instance().OnDownloadStart += OnDownloadStart;
        TradplusSplash.Instance().OnDownloadUpdate += OnDownloadUpdate;
        TradplusSplash.Instance().OnDownloadFinish += OnDownloadFinish;
        TradplusSplash.Instance().OnDownloadFailed += OnDownloadFailed;
        TradplusSplash.Instance().OnDownloadPause += OnDownloadPause;
        TradplusSplash.Instance().OnInstalled += OnInstallled;
#endif
    }

    private void OnDestroy()
    {
        TradplusAds.Instance().OnInitFinish -= OnInitFinish;

        //常用
        TradplusSplash.Instance().OnSplashLoaded -= OnlLoaded;
        TradplusSplash.Instance().OnSplashLoadFailed -= OnLoadFailed;
        TradplusSplash.Instance().OnSplashImpression -= OnImpression;
        TradplusSplash.Instance().OnSplashShowFailed -= OnShowFailed;
        TradplusSplash.Instance().OnSplashClicked -= OnClicked;
        TradplusSplash.Instance().OnSplashClosed -= OnClosed;
        TradplusSplash.Instance().OnSplashOneLayerLoadFailed -= OnOneLayerLoadFailed;
        //
        TradplusSplash.Instance().OnSplashStartLoad -= OnStartLoad;
        TradplusSplash.Instance().OnSplashBiddingStart -= OnBiddingStart;
        TradplusSplash.Instance().OnSplashBiddingEnd -= OnBiddingEnd;
        TradplusSplash.Instance().OnSplashIsLoading -= OnAdIsLoading;

        TradplusSplash.Instance().OnSplashOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusSplash.Instance().OnSplashOneLayerLoaded -= OnOneLayerLoaded;
        TradplusSplash.Instance().OnSplashZoomOutStart -= OnZoomOutStart;
        TradplusSplash.Instance().OnSplashZoomOutEnd -= OnZoomOutEnd;
        TradplusSplash.Instance().OnSplashSkip -= OnSkip;
        TradplusSplash.Instance().OnSplashAllLoaded -= OnAllLoaded;

#if UNITY_ANDROID
        TradplusSplash.Instance().OnDownloadStart -= OnDownloadStart;
        TradplusSplash.Instance().OnDownloadUpdate -= OnDownloadUpdate;
        TradplusSplash.Instance().OnDownloadFinish -= OnDownloadFinish;
        TradplusSplash.Instance().OnDownloadFailed -= OnDownloadFailed;
        TradplusSplash.Instance().OnDownloadPause -= OnDownloadPause;
        TradplusSplash.Instance().OnInstalled -= OnInstallled;
#endif
    }

    void OnInitFinish(bool success)
    {
        Configure.Instance().ShowLog("MaineUI OnInitFinish ------ success:" + success);
    }

    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("Splash OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("Splash OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
        splashShow = false;
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
        splashShow = false;
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnBiddingStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnBiddingEnd(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("Splash OnBiddingEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnAdIsLoading(string adunit)
    {
        Configure.Instance().ShowLog("Splash OnAdIsLoading ------ adunit:" + adunit);
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("Splash OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnZoomOutStart(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnZoomOutStart ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnZoomOutEnd(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnZoomOutEnd ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnSkip(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("Splash OnSkip ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("Splash OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnDownloadStart(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("Splash OnDownloadStart ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadUpdate(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName, int progress)
    {
        Configure.Instance().ShowLog("Splash OnDownloadUpdate ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "; progress:" + progress);
    }

    void OnDownloadPause(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("Splash OnDownloadPause ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFinish(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("Splash OnDownloadFinish ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnDownloadFailed(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("Splash OnDownloadFailed ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }


    void OnInstallled(string adunit, Dictionary<string, object> adInfo, long totalBytes, long currBytes, string fileName, string appName)
    {
        Configure.Instance().ShowLog("Splash OnInstallled ------ adunit:" + adunit + "; totalBytes: " + totalBytes + "; currBytes: " + currBytes + "" + "; fileName: " + fileName + "" + "; appName: " + appName + "");
    }
}
