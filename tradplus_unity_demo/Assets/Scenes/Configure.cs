using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configure
{
    private static Configure _instance;

    public static Configure Instance()
    {
        if (_instance == null)
        {
            _instance = new Configure();
        }
        return _instance;
    }

    private static string appId;
    public string AppId
    {
        get {
            return appId;
        }
    }

    private static string interstitialUnitId;
    public string InterstitialUnitId
    {
        get
        {
            return interstitialUnitId;
        }
    }

    private static string rewardedUnitId;

    public string RewardedUnitId
    {
        get
        {
            return rewardedUnitId;
        }
    }

    private static string offerwallUnitId;

    public string OfferwallUnitId
    {
        get
        {
            return offerwallUnitId;
        }
    }

    private static string bannerUnitId;
    public string BannerUnitId
    {
        get
        {
            return bannerUnitId;
        }
    }

    private static string nativeUnitId;
    public string NativeUnitId
    {
        get
        {
            return nativeUnitId;
        }
    }

    private static string nativeBannerUnitId;
    public string NativeBannerUnitId
    {
        get
        {
            return nativeBannerUnitId;
        }
    }

    public string GetLogs()
    {
        string log = "";
        foreach(string str in logInfos)
        {
            log += str + "\n\n";
        }
        return log;
    }

    private static ArrayList logInfos;
    public ArrayList LogInfos
    {
        get
        {
            return logInfos;
        }
    }

    public void ShowLog(string logStr)
    {
        logInfos.Add(logStr);
        Debug.LogWarning(logStr);
    }

    private Dictionary<string, string> mainCustomMap;
    public Dictionary<string, string> MainCustomMap
    {
        get
        {
            return mainCustomMap;
        }
    }

    private bool useAdCustomMap;
    public bool UseAdCustomMap
    {
        get
        {
            return useAdCustomMap;
        }
        set
        {
            useAdCustomMap = value;
            if(useAdCustomMap)
            {
                PlayerPrefs.SetInt("tp.demo.UseAdCustomMap", 1);
            }
            else
            {
                PlayerPrefs.SetInt("tp.demo.UseAdCustomMap", 0);
            }
        }
    }


    private bool autoLoad;
    public bool AutoLoad
    {
        get
        {
            return autoLoad;
        }
        set
        {
            autoLoad = value;
            if (autoLoad)
            {
                PlayerPrefs.SetInt("tp.demo.AutoLoad", 1);
            }
            else
            {
                PlayerPrefs.SetInt("tp.demo.AutoLoad", 0);
            }
        }
    }

    private bool simplifyListener;
    public bool SimplifyListener
    {
        get
        {
            return simplifyListener;
        }
        set
        {
            simplifyListener = value;
            if (simplifyListener)
            {
                PlayerPrefs.SetInt("tp.demo.SimplifyListener", 1);
            }
            else
            {
                PlayerPrefs.SetInt("tp.demo.SimplifyListener", 0);
            }
        }
    }

    private static string bannerSceneId;
    public string BannerSceneId
    {
        get
        {
            return bannerSceneId;
        }
    }

    private static string interstitialSceneId;
    public string InterstitialSceneId
    {
        get
        {
            return interstitialSceneId;
        }
    }

    private static string rewardVideoSceneId;
    public string RewardVideoSceneId
    {
        get
        {
            return rewardVideoSceneId;
        }
    }

    private static string nativeSceneId;
    public string NativeSceneId
    {
        get
        {
            return nativeSceneId;
        }
    }

    private static string nativeBannerSceneId;
    public string NativeBannerSceneId
    {
        get
        {
            return nativeBannerSceneId;
        }
    }

    public Configure()
    {
        logInfos = new ArrayList();
        mainCustomMap = new Dictionary<string, string>();

#if UNITY_IOS
        // appId = "7A8EC9F31CC99CBAEAD35868A9DF37F1";
        // interstitialUnitId = "51F8E9A8C8C0A48AAC027E7284DDC216";
        // rewardedUnitId = "BBFCEAE498A151F9B3BA314F79B571AC";
        // offerwallUnitId = "CF44FC197564707D77820715882EEE92";
        // bannerUnitId = "9E56C2D02AAB859FF45CC9BDC6C91C21";
        // nativeUnitId = "293161A28F7C5D6750F97BA97A11FEC8";
        // nativeBannerUnitId = "88BD49596ABC9F70F82FA2C43B0EC54B";

        mainCustomMap.Add("user_id", "test_user_id");
        mainCustomMap.Add("user_age", "19");
        mainCustomMap.Add("segment_id", "1571");
        mainCustomMap.Add("bucket_id", "299");
        mainCustomMap.Add("custom_data", "TestIMP");
        mainCustomMap.Add("channel", "tp_channel");
        mainCustomMap.Add("sub_channel", "tp_sub_channel");

        //测试配置
        appId = "0E3CD076A202D14F4AB0D1D535F7190F";
        interstitialUnitId = "E488AF1C8372A5269614FC46649890DC";
        rewardedUnitId = "1A9D6D870122E7056762EC05B21F797D";
        offerwallUnitId = "D109A3D5BF61B540F1BA63C4DE70B31C";
        bannerUnitId = "363C3FBDD5BAFD550B45A52EAE3341C6";
        nativeUnitId = "79D2FF861FD9F33791E1612708C13DCB";
        nativeBannerUnitId = "281D8267EAEB582924B0A9410B24F407";

        bannerSceneId = "009513B2A78F64";//测试
        interstitialSceneId = "A54829DC948F7D";//测试
        rewardVideoSceneId = "828F88157D28F8";//测试
        nativeSceneId = "2D064EC9EF4106";//测试
        nativeBannerSceneId = "2333333";
#else
        interstitialUnitId = "788E1FCB278B0D7E97282231154458B7";
        rewardedUnitId = "702208A872E622C1729FC621025D4B1D";
        bannerUnitId = "E89A890466180B9215487530A8EB519F";
        nativeUnitId = "04D8F97E539A50D52E01BA0898135E02";
        nativeBannerUnitId = "697C4F408EC710DEB2DD0703E7222B86";
        offerwallUnitId = "423EB7FF56537295851D3359633F0182";

        mainCustomMap.Add("user_id", "test_user_id");
        mainCustomMap.Add("user_age", "19");
        mainCustomMap.Add("custom_data", "TestIMP");
        mainCustomMap.Add("channel", "tp_channel");
        mainCustomMap.Add("sub_channel", "tp_sub_channel");

        appId = "44273068BFF4D8A8AFF3D5B11CBA3ADE";

        bannerSceneId = "123";
        interstitialSceneId = "345";//测试
        rewardVideoSceneId = "567";//测试
        nativeSceneId = "789";//测试
        nativeBannerSceneId = "2333333";
#endif
    }
}
