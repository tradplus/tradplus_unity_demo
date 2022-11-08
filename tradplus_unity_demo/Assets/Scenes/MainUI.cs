using System.Collections;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    bool onlyCn;
    // Start is called before the first frame update
    void Start()
    {
        int index = PlayerPrefs.GetInt("tp.demo.UseAdCustomMap", 1);
        Configure.Instance().UseAdCustomMap = (index == 1);

        index = PlayerPrefs.GetInt("tp.demo.AutoLoad", 1);
        Configure.Instance().AutoLoad = (index == 1);

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
        TradplusAds.Instance().SetTestDevice(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        float height = (Screen.height - 140) / 12 - 20;
        GUI.skin.button.fixedHeight = height;
        GUI.skin.button.fontSize = (int)(height / 3);
        GUI.skin.label.fontSize = (int)(height / 3);
        GUI.skin.label.fixedHeight = height;

        var rect = new Rect(20, 20, Screen.width - 40, Screen.height);
        rect.y += Screen.safeArea.y;
        GUILayout.BeginArea(rect);
        GUILayout.Space(20);
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
        if (GUILayout.Button("原生横幅"))
        {
            SceneManager.LoadScene("NativeBanner");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("隐私权限"))
        {
            SceneManager.LoadScene("Privacy");
        }
        GUILayout.Space(20);
        if (GUILayout.Button("日志"))
        {
            SceneManager.LoadScene("Log");
        }

        GUILayout.Space(40);
        string text = "自动加载开启中，点击关闭";
        if(!Configure.Instance().AutoLoad)
        {
            text = "自动加载关闭中，点击开启";
        }
        if (GUILayout.Button(text))
        {
            Configure.Instance().AutoLoad = !Configure.Instance().AutoLoad;
        }

   #if UNITY_ANDROID
        text = "复杂回调关闭中，点击开启";
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
  #if UNITY_ANDROID
        text = "点击开启自动加载2S延迟";

         GUILayout.Space(40);
        if (GUILayout.Button(text))
        {
           TradplusAds.Instance().SetOpenDelayLoadAds(true);
        }
 #endif
        text = "当前：使用广告位流量分组";
        if (!Configure.Instance().UseAdCustomMap)
        {
            text = "当前：使用全局流量分组";
        }
        GUILayout.Space(10);
        if (GUILayout.Button(text))
        {
            Configure.Instance().UseAdCustomMap = !Configure.Instance().UseAdCustomMap;
        }

        text = "当前全球服务器，点击切换为国内服务器。需重启";
        if (onlyCn)
        {
            text = "当前国内服务器：点击切换为全球服务器。需重启";
        }
        GUILayout.Space(10);
        if (GUILayout.Button(text))
        {
            onlyCn = !onlyCn;
            if (onlyCn)
            {
                PlayerPrefs.SetInt("tp.demo.onlyCn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("tp.demo.onlyCn", 0);
            }
        }

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("关闭缓存检测"))
        {
            TradplusAds.Instance().SetAutoExpiration(false);
        }

        if (GUILayout.Button("开启缓存检测"))
        {
            TradplusAds.Instance().SetAutoExpiration(true);
        }

        if (GUILayout.Button("手动检测缓存"))
        {
            TradplusAds.Instance().CheckAutoExpiration();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    //回调设置
    private void OnEnable()
    {
        TradplusAds.Instance().OnInitFinish += OnInitFinish;
    }

    private void OnDestroy()
    {
        TradplusAds.Instance().OnInitFinish -= OnInitFinish;
    }

    void OnInitFinish(bool success)
    {
        Configure.Instance().ShowLog("MaineUI OnInitFinish ------ success:" + success);
    }
}
