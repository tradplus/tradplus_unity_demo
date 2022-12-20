using System;
using System.Collections;
using System.Collections.Generic;
using TradplusSDK.Api;
using TradplusSDK.ThirdParty.MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfferwallUI : MonoBehaviour
{
    string adUnitId = Configure.Instance().OfferwallUnitId;
    string sceneId = "323232";
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
        float height = (Screen.height - 160) / 9 - 20;
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

            TPOfferwallExtra extra = new TPOfferwallExtra();
              #if UNITY_ANDROID

                                  extra.isSimpleListener = Configure.Instance().SimplifyListener;
                           #endif

            if (Configure.Instance().UseAdCustomMap)
            {
                //流量分组相关
                Dictionary<string, string> customMap = new Dictionary<string, string>();
                customMap.Add("user_id", "test_offerWall_userid");
                customMap.Add("custom_data", "offerWall_TestIMP");
                customMap.Add("segment_tag", "offerWall_segment_tag");
                extra.customMap = customMap;

                Dictionary<string, string> localParams = new Dictionary<string, string>();
                localParams.Add("user_id", "offerwall_userId");
                localParams.Add("custom_data", "offerwall_customData");
                extra.localParams = localParams;
            }

            TradplusOfferwall.Instance().LoadOfferwallAd(adUnitId, extra);

            Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
            customAdInfo.Add("act", "Load");
            customAdInfo.Add("time", "" + DateTimeOffset.Now);
            TradplusOfferwall.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }
        GUILayout.Space(20);
        if (GUILayout.Button("isReady"))
        {
            bool isReady = TradplusOfferwall.Instance().OfferwallAdReady(adUnitId);
            infoStr = "isReady: " + isReady;
        }
        GUILayout.Space(20);
        if (GUILayout.Button("展示"))
        {

            Dictionary<string, string> customAdInfo = new Dictionary<string, string>();
            customAdInfo.Add("act", "Show");
            customAdInfo.Add("time", "" + DateTimeOffset.Now);
            TradplusOfferwall.Instance().SetCustomAdInfo(adUnitId, customAdInfo);

            infoStr = "";

            TradplusOfferwall.Instance().ShowOfferwallAd(adUnitId, sceneId);
        }
        GUILayout.Space(20);
        GUILayout.Label(infoStr);

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("查询积分"))
        {
            TradplusOfferwall.Instance().GetCurrencyBalance(adUnitId);
        }

        if (GUILayout.Button("设置用户名"))
        {
            TradplusOfferwall.Instance().SetUserId(adUnitId, "test_offerwall_userid");
        }
        GUILayout.EndHorizontal();

        
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("增加积分 20"))
        {
            TradplusOfferwall.Instance().AwardBalance(adUnitId,20);
        }

        if (GUILayout.Button("扣除积分 10"))
        {
            TradplusOfferwall.Instance().SpendBalance(adUnitId, 10);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        if (GUILayout.Button("进入广告场景"))
        {
            TradplusOfferwall.Instance().EntryOfferwallAdScenario(adUnitId, sceneId);
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
        TradplusOfferwall.Instance().OnOfferwallLoaded += OnlLoaded;
        TradplusOfferwall.Instance().OnOfferwallLoadFailed += OnLoadFailed;
        TradplusOfferwall.Instance().OnOfferwallImpression += OnImpression;
        TradplusOfferwall.Instance().OnOfferwallShowFailed += OnShowFailed;
        TradplusOfferwall.Instance().OnOfferwallClicked += OnClicked;
        TradplusOfferwall.Instance().OnOfferwallClosed += OnClosed;
        TradplusOfferwall.Instance().OnOfferwallOneLayerLoadFailed += OnOneLayerLoadFailed;
        //
        TradplusOfferwall.Instance().OnOfferwallStartLoad += OnStartLoad;
        TradplusOfferwall.Instance().OnOfferwallOneLayerStartLoad += OnOneLayerStartLoad;
        TradplusOfferwall.Instance().OnOfferwallOneLayerLoaded += OnOneLayerLoaded;
        TradplusOfferwall.Instance().OnOfferwallAllLoaded += OnAllLoaded;

        TradplusOfferwall.Instance().OnOfferwallSetUserIdFinish += OnSetUserIdFinish;
        TradplusOfferwall.Instance().OnCurrencyBalanceSuccess += OnCurrencyBalanceSuccess;
        TradplusOfferwall.Instance().OnCurrencyBalanceFailed += OnCurrencyBalanceFailed;
        TradplusOfferwall.Instance().OnSpendCurrencySuccess += OnSpendCurrencySuccess;
        TradplusOfferwall.Instance().OnSpendCurrencyFailed += OnSpendCurrencyFailed;
        TradplusOfferwall.Instance().OnAwardCurrencySuccess += OnAwardCurrencySuccess;
        TradplusOfferwall.Instance().OnAwardCurrencyFailed += OnAwardCurrencyFailed;

    }

    private void OnDestroy()
    {
        TradplusOfferwall.Instance().OnOfferwallLoaded -= OnlLoaded;
        TradplusOfferwall.Instance().OnOfferwallLoadFailed -= OnLoadFailed;
        TradplusOfferwall.Instance().OnOfferwallImpression -= OnImpression;
        TradplusOfferwall.Instance().OnOfferwallShowFailed -= OnShowFailed;
        TradplusOfferwall.Instance().OnOfferwallClicked -= OnClicked;
        TradplusOfferwall.Instance().OnOfferwallClosed -= OnClosed;
        TradplusOfferwall.Instance().OnOfferwallOneLayerLoadFailed -= OnOneLayerLoadFailed;

        TradplusOfferwall.Instance().OnOfferwallStartLoad -= OnStartLoad;
        TradplusOfferwall.Instance().OnOfferwallOneLayerStartLoad -= OnOneLayerStartLoad;
        TradplusOfferwall.Instance().OnOfferwallOneLayerLoaded -= OnOneLayerLoaded;
        TradplusOfferwall.Instance().OnOfferwallAllLoaded -= OnAllLoaded;

        TradplusOfferwall.Instance().OnOfferwallSetUserIdFinish -= OnSetUserIdFinish;
        TradplusOfferwall.Instance().OnCurrencyBalanceSuccess -= OnCurrencyBalanceSuccess;
        TradplusOfferwall.Instance().OnCurrencyBalanceFailed -= OnCurrencyBalanceFailed;
        TradplusOfferwall.Instance().OnSpendCurrencySuccess -= OnSpendCurrencySuccess;
        TradplusOfferwall.Instance().OnSpendCurrencyFailed -= OnSpendCurrencyFailed;
        TradplusOfferwall.Instance().OnAwardCurrencySuccess -= OnAwardCurrencySuccess;
        TradplusOfferwall.Instance().OnAwardCurrencyFailed -= OnAwardCurrencyFailed;

    }


    void OnlLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        infoStr = "加载成功";
        Configure.Instance().ShowLog("OfferwallUI OnlLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnLoadFailed(string adunit, Dictionary<string, object> error)
    {
        infoStr = "加载失败";
        Configure.Instance().ShowLog("OfferwallUI OnLoadFailed ------ adunit:" + adunit + "; error: " + Json.Serialize(error));
    }

    void OnImpression(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("OfferwallUI OnImpression ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnShowFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("OfferwallUI OnShowFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnClicked(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("OfferwallUI OnClicked ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnClosed(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("OfferwallUI OnClosed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("OfferwallUI OnStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerStartLoad(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("OfferwallUI OnOneLayerStartLoad ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoaded(string adunit, Dictionary<string, object> adInfo)
    {
        Configure.Instance().ShowLog("OfferwallUI OnOneLayerLoaded ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo));
    }

    void OnOneLayerLoadFailed(string adunit, Dictionary<string, object> adInfo, Dictionary<string, object> error)
    {
        Configure.Instance().ShowLog("OfferwallUI OnOneLayerLoadFailed ------ adunit:" + adunit + "; adInfo: " + Json.Serialize(adInfo) + "; error: " + Json.Serialize(error));
    }

    void OnAllLoaded(string adunit, bool isSuccess)
    {
        Configure.Instance().ShowLog("OfferwallUI OnAllLoaded ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }

    void OnSetUserIdFinish(string adunit, bool isSuccess)
    {
        if(isSuccess)
        {
            infoStr = "用户名设置成功";
        }
        else
        {
            infoStr = "用户名设置失败";
        }
        Configure.Instance().ShowLog("OfferwallUI OnSetUserIdFinish ------ adunit:" + adunit + "; isSuccess: " + isSuccess);
    }


    void OnCurrencyBalanceSuccess(string adunit, int amount, string msg)
    {
        infoStr = "查询成功，用户积分："+ amount;
        Configure.Instance().ShowLog("OfferwallUI OnCurrencyBalanceSuccess ------ adunit:" + adunit + "; amount: " + amount + "; msg: " + msg);
    }

    void OnCurrencyBalanceFailed(string adunit, string msg)
    {
        infoStr = "查询失败";
        Configure.Instance().ShowLog("OfferwallUI OnCurrencyBalanceSuccess ------ adunit:" + adunit + "; msg: " + msg);
    }


    void OnSpendCurrencySuccess(string adunit, int amount, string msg)
    {
        infoStr = "扣除成功，用户积分：" + amount;
        Configure.Instance().ShowLog("OfferwallUI OnSpendCurrencySuccess ------ adunit:" + adunit + "; amount: " + amount + "; msg: " + msg);
    }

    void OnSpendCurrencyFailed(string adunit, string msg)
    {
        infoStr = "扣除失败";
        Configure.Instance().ShowLog("OfferwallUI OnCurrencyBalanceSuccess ------ adunit:" + adunit + "; msg: " + msg);
    }

    void OnAwardCurrencySuccess(string adunit, int amount, string msg)
    {
        infoStr = "增加成功，用户积分：" + amount;
        Configure.Instance().ShowLog("OfferwallUI OnAwardCurrencySuccess ------ adunit:" + adunit + "; amount: " + amount + "; msg: " + msg);
    }

    void OnAwardCurrencyFailed(string adunit, string msg)
    {
        infoStr = "增加失败";
        Configure.Instance().ShowLog("OfferwallUI OnCurrencyBalanceSuccess ------ adunit:" + adunit + "; msg: " + msg);
    }
}