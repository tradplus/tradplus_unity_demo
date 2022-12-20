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

    public void ShowLog(string logStr)
    {
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
        mainCustomMap = new Dictionary<string, string>();

#if UNITY_IOS
        appId = "7A8EC9F31CC99CBAEAD35868A9DF37F1";
        interstitialUnitId = "51F8E9A8C8C0A48AAC027E7284DDC216";
        rewardedUnitId = "BBFCEAE498A151F9B3BA314F79B571AC";
        offerwallUnitId = "CF44FC197564707D77820715882EEE92";
        bannerUnitId = "9E56C2D02AAB859FF45CC9BDC6C91C21";
        nativeUnitId = "293161A28F7C5D6750F97BA97A11FEC8";
        nativeBannerUnitId = "88BD49596ABC9F70F82FA2C43B0EC54B";

        mainCustomMap.Add("user_id", "test_user_id");
        mainCustomMap.Add("user_age", "19");
        mainCustomMap.Add("segment_id", "1571");
        mainCustomMap.Add("bucket_id", "299");
        mainCustomMap.Add("custom_data", "TestIMP");
        mainCustomMap.Add("channel", "tp_channel");
        mainCustomMap.Add("sub_channel", "tp_sub_channel");

        bannerSceneId = "009513B2A78F64";
        interstitialSceneId = "A54829DC948F7D";
        rewardVideoSceneId = "828F88157D28F8";
        nativeSceneId = "2D064EC9EF4106";
        nativeBannerSceneId = "2323113";
#else
        interstitialUnitId = "E609A0A67AF53299F2176C3A7783C46D";
        rewardedUnitId = "39DAC7EAC046676C5404004A311D1DB1";
        bannerUnitId = "A24091715B4FCD50C0F2039A5AF7C4BB";
        nativeUnitId = "DDBF26FBDA47FBE2765F1A089F1356BF";
        nativeBannerUnitId = "9F4D76E204326B16BD42FA877AFE8E7D";
        offerwallUnitId = "4F7F1B9288B2FD513C8549A4A9F5D60F";

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
