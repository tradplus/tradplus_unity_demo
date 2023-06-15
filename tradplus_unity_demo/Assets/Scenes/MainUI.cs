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

        //全局展示回调
        TradplusAds.Instance().AddGlobalAdImpression(OnGlobalAdImpression);
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
