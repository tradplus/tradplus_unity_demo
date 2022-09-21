using System.Collections;
using System.Collections.Generic;
using TradplusSDK.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrivacyUI : MonoBehaviour
{
    string gdprStateStr;
    int gdprState;
    string ccpaStateStr;
    int ccpaState;
    string coppaStateStr;
    int coppaState;
    string openPersonalizedStr;
    bool openPersonalized;
    string areaInfo = "";

    void checkInfo()
    {
        gdprState =  TradplusAds.Instance().GetGDPRDataCollection();
        gdprStateStr = "GDPR 未设置";
        if (gdprState == 0)
        {
            gdprStateStr = "GDPR 允许上报";
        }
        else if (gdprState == 1)
        {
            gdprStateStr = "GDPR 不允许上报";
        }

        ccpaState = TradplusAds.Instance().GetCCPADoNotSell();
        ccpaStateStr = "CCPA 未设置";
        if (ccpaState == 0)
        {
            ccpaStateStr = "CCPA 允许上报";
        }
        else if (ccpaState == 1)
        {
            ccpaStateStr = "CCPA 不允许上报";
        }


        coppaState = TradplusAds.Instance().GetCOPPAIsAgeRestrictedUser();
        coppaStateStr = "COPPA 未设置";
        if (coppaState == 0)
        {
            coppaStateStr = "COPPA 儿童";
        }
        else if (coppaState == 1)
        {
            coppaStateStr = "COPPA 不是儿童";
        }

        openPersonalized = TradplusAds.Instance().IsOpenPersonalizedAd();
        if(openPersonalized == true)
        {
            openPersonalizedStr = "个性化推荐已开启";
        }
        else
        {
            openPersonalizedStr = "个性化推荐已关闭";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        checkInfo();
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
        GUILayout.Label("PluginVersion :"+ TradplusAds.Instance().PluginVersion);
        GUILayout.Label("SDKVersion :" + TradplusAds.Instance().Version());
        GUILayout.Label("是否在欧洲 :" + TradplusAds.Instance().IsEUTraffic());
        GUILayout.Label("是否在加州 :" + TradplusAds.Instance().IsCalifornia());

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label(gdprStateStr, GUILayout.Width(Screen.width/2 - 50));
        if(gdprState != 0)
        {
            if (GUILayout.Button("设置为 允许上报"))
            {
                //设置 GDPR
                TradplusAds.Instance().SetGDPRDataCollection(true);
                checkInfo();
            }
        }
        else
        {
            if (GUILayout.Button("设置为 不允许上报"))
            {
                //设置 GDPR
                TradplusAds.Instance().SetGDPRDataCollection(false);
                checkInfo();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label(ccpaStateStr, GUILayout.Width(Screen.width / 2 - 50));
        if (ccpaState != 0)
        {
            if (GUILayout.Button("设置为 允许上报"))
            {
                //设置 CCPA
                TradplusAds.Instance().SetCCPADoNotSell(true);
                checkInfo();
            }
        }
        else
        {
            if (GUILayout.Button("设置为 不允许上报"))
            {
                //设置 CCPA
                TradplusAds.Instance().SetCCPADoNotSell(false);
                checkInfo();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label(coppaStateStr, GUILayout.Width(Screen.width / 2 - 50));
        if (coppaState != 0)
        {
            if (GUILayout.Button("设置为 儿童"))
            {
                //设置 COPPA
                TradplusAds.Instance().SetCOPPAIsAgeRestrictedUser(true);
                checkInfo();
            }
        }
        else
        {
            if (GUILayout.Button("设置为 不是儿童"))
            {
                //设置 COPPA
                TradplusAds.Instance().SetCOPPAIsAgeRestrictedUser(false);
                checkInfo();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label(openPersonalizedStr, GUILayout.Width(Screen.width / 2 - 50));
        if (openPersonalized == false)
        {
            if (GUILayout.Button("设置为 开启"))
            {
                //设置 国内个性化推荐
                TradplusAds.Instance().SetOpenPersonalizedAd(true);
                checkInfo();
            }
        }
        else
        {
            if (GUILayout.Button("设置为 关闭"))
            {
                //设置 国内个性化推荐
                TradplusAds.Instance().SetOpenPersonalizedAd(false);
                checkInfo();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        if (GUILayout.Button("GDPR 授权页面"))
        {
            //调用 GDRP授权页面
            TradplusAds.Instance().ShowGDPRDialog();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("查询地区"))
        {
            //查询地区信息
            TradplusAds.Instance().CheckCurrentArea();
        }
        GUILayout.Label(areaInfo);

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
        TradplusAds.Instance().OnDialogClosed += OnDialogClosed;
        TradplusAds.Instance().OnCurrentAreaSuccess += OnCurrentAreaSuccess;
        TradplusAds.Instance().OnCurrentAreaFailed += OnCurrentAreaFailed;
    }

    private void OnDestroy()
    {
        TradplusAds.Instance().OnDialogClosed -= OnDialogClosed;
        TradplusAds.Instance().OnCurrentAreaSuccess -= OnCurrentAreaSuccess;
        TradplusAds.Instance().OnCurrentAreaFailed -= OnCurrentAreaFailed;
    }

    void OnDialogClosed(int level)
    {
        Configure.Instance().ShowLog("PrivacyUI OnDialogClosed ------ level:" + level);
        checkInfo();
    }

    void OnCurrentAreaSuccess(bool isEu, bool isCn, bool isCa)
    {
        areaInfo = "isEu: "+ isEu + "; isCn: "+ isCn + "; isCa: " + isCa;
    }

    void OnCurrentAreaFailed(string msg)
    {
        areaInfo = "地区获取失败或未知地区";
    }

}
