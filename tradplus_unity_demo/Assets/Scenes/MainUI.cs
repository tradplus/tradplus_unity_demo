﻿using System.Collections;
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