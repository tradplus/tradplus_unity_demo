using System;
#if UNITY_EDITOR
using TradplusSDK.Unity;
#elif UNITY_IOS
using TradplusSDK.iOS;
#elif UNITY_ANDROID
using TradplusSDK.Android;
#else
using TradplusSDK.Unity;
#endif
using System.Collections.Generic;

namespace TradplusSDK.Api
{
    public class TPBanner :
#if UNITY_EDITOR
    TPSdkUnityBanner
#elif UNITY_IOS
    TradplusBanneriOS
#elif UNITY_ANDROID
    TradplusBannerAndroid
#else
    TPSdkUnityBanner
#endif
    { }

    ///<summary>
    ///附加参数 closeAutoShow:是否关闭自动展示;x:展示坐标 x; y:展示坐标 y; width:广告宽度 默认320; height:广告高度 默认50; adPosition:屏幕位置定位（x=0 y=0 时生效）,默认TopLeft ;customMap：流量分组; localParams：特殊参数，仅Android支持;
    ///</summary>
    public class TPBannerExtra
    {
        ///<summary>
        ///自定义模版名称
        ///</summary>
        public string className;
        ///<summary>
        ///是否需要简易回调
        ///</summary>
        public bool isSimpleListener;
        ///<summary>
        ///是否关闭自动展示
        ///仅Android支持
        ///</summary>
        public bool closeAutoShow;
        ///<summary>
        ///是否关闭自动销毁
        ///仅Android支持
        ///</summary>
        public bool closeAutoDestroy;
        ///<summary>
        ///横幅广告展示坐标 x，默认 0
        ///</summary>
        public float x;

        ///<summary>
        ///横幅广告展示坐标 y,，默认 0
        ///</summary>
        public float y;

        ///<summary>
        ///横幅广告展示 width，默认 320
        ///</summary>
        public float width;

        ///<summary>
        ///横幅广告展示 height，默认 50
        ///</summary>
        public float height;

        ///<summary>
        ///位置定位, x = 0 ,y = 0,使用 adPosition 进行定位。默认TopLeft
        ///</summary>
        public TradplusBase.AdPosition adPosition;

        ///<summary>
        ///流量分组
        ///</summary>
        public Dictionary<string, string> customMap;
        ///<summary>
        ///本地参数
        ///</summary>
        public Dictionary<string, object> localParams;

        ///<summary>
        ///居中模式 仅iOS支持
        ///</summary>
        public TradplusBase.AdContentMode contentMode;

        ///<summary>
        ///自定义背景色 例如：#FFFFFF
        ///</summary>
        public string backgroundColor;

        public bool openAutoLoadCallback;

        public float maxWaitTime;

        public TPBannerExtra()
        {
            width = 0;
            height = 50;
            backgroundColor = "";
            adPosition = TradplusBase.AdPosition.TopLeft;
        }
    }

    public class TradplusBanner
    {

        private static TradplusBanner _instance;

        public static TradplusBanner Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusBanner();
            }
            return _instance;
        }

        ///<summary>
        ///加载广告 adUnitId：广告位ID；sceneId：广告场景ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadBannerAd(string adUnitId, string sceneId = "", TPBannerExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnBannerLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnBannerLoaded(adUnitId, adInfo);
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPBannerExtra();
            }
            TPBanner.Instance().LoadBannerAd(adUnitId, sceneId,extra);
#endif
        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID
        ///banner默认开启自动展示，一般不需要使用此接口。
        ///当TPBannerExtra 中的 closeAutoShow 设置为ture时关闭自动展示。
        ///开发者可以在加载成功回调后调用此接口进行banner展示。
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void ShowBannerAd(string adUnitId, string sceneId = "")
        {
            TPBanner.Instance().ShowBannerAd(adUnitId, sceneId);
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool BannerAdReady(string adUnitId)
        {
            return TPBanner.Instance().BannerAdReady(adUnitId);
        }

        ///<summary>
        ///进入广告场景 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void EntryBannerAdScenario(string adUnitId, string sceneId = "")
        {
            TPBanner.Instance().EntryBannerAdScenario(adUnitId, sceneId);
        }

        ///<summary>
        ///隐藏广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void HideBanner(string adUnitId)
        {
            TPBanner.Instance().HideBanner(adUnitId);
        }

        ///<summary>
        ///显示已隐藏了的广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DisplayBanner(string adUnitId)
        {
            TPBanner.Instance().DisplayBanner(adUnitId);
        }


        ///<summary>
        ///移除广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DestroyBanner(string adUnitId)
        {
            TPBanner.Instance().DestroyBanner(adUnitId);
        }

        ///<summary>
        ///v1.0.1 新增。
        ///开发者可在展示前通过此接口设置透传的adInfo信息。
        ///透传信息可以在广告展示后的相关回调的adInfo中获取。
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="customAdInfo">自定义透传的adInfo信息</param>
        public void SetCustomAdInfo(string adUnitId,Dictionary<string, string> customAdInfo)
        {
            TPBanner.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }

        ///<summary>
        ///开发者在 OnApplicationQuit 生命周期时调用关闭回调
        ///仅iOS支持
        ///</summary>
        public void ClearCallback()
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            TPInterstitial.Instance().ClearCallback();
#endif
        }


        //接口回调

        //常用回调接口

        ///<summary>
        ///加载成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerClosed;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerOneLayerLoadFailed;


//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerStartLoad;

        ///<summary>
        ///Bidding开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerBiddingStart;

        ///<summary>
        ///Bidding结束 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnBannerBiddingEnd;

        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnBannerIsLoading;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnBannerOneLayerLoaded;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnBannerAllLoaded;


//下载监听 快手、穿山甲支持(可选)(仅Android支持)

        ///<summary>
        ///下载开始 long totalBytes, long currBytes, string fileName, string appName
        ///</summary>
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadStart;

        ///<summary>
        ///下载更新 long totalBytes, long currBytes, string fileName, string appName,int progress
        ///</summary>
        public event Action<string, Dictionary<string, object>, long, long, string, string, int> OnDownloadUpdate;

        ///<summary>
        ///下载暂停 long totalBytes, long currBytes, string fileName, string appName
        ///</summary>
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadPause;

        ///<summary>
        ///下载完成 long totalBytes, long currBytes, string fileName, string appName
        ///</summary>
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFinish;

        ///<summary>
        ///下载失败 long totalBytes, long currBytes, string fileName, string appName
        ///</summary>
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnDownloadFailed;

        ///<summary>
        ///安装成功 long totalBytes, long currBytes, string fileName, string appName
        ///</summary>
        public event Action<string, Dictionary<string, object>, long, long, string, string> OnInstalled;



        public TradplusBanner()
        {
            TPBanner.Instance().OnBannerLoaded += (adunit, adInfo) =>
            {
                if (this.OnBannerLoaded != null)
                {
                    this.OnBannerLoaded(adunit, adInfo);
                }
            };

            TPBanner.Instance().OnBannerLoadFailed += (adunit, error) =>
            {
                if (this.OnBannerLoadFailed != null)
                {
                    this.OnBannerLoadFailed(adunit, error);
                }
            };

            TPBanner.Instance().OnBannerImpression += (adunit, adInfo) =>
            {
                if (this.OnBannerImpression != null)
                {
                    this.OnBannerImpression(adunit, adInfo);
                }
            };

            TPBanner.Instance().OnBannerShowFailed += (adunit, adInfo, error) =>
            {
                if (this.OnBannerShowFailed != null)
                {
                    this.OnBannerShowFailed(adunit, adInfo, error);
                }
            };

            TPBanner.Instance().OnBannerClicked += (adunit, adInfo) =>
            {
                if (this.OnBannerClicked != null)
                {
                    this.OnBannerClicked(adunit, adInfo);
                }
            };


            TPBanner.Instance().OnBannerClosed += (adunit, adInfo) =>
            {
                if (this.OnBannerClosed != null)
                {
                    this.OnBannerClosed(adunit, adInfo);
                }
            };


            TPBanner.Instance().OnBannerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnBannerStartLoad != null)
                {
                    this.OnBannerStartLoad(adunit, adInfo);
                }
            };

            TPBanner.Instance().OnBannerBiddingStart += (adunit, adInfo) =>
            {
                if (this.OnBannerBiddingStart != null)
                {
                    this.OnBannerBiddingStart(adunit, adInfo);
                }
            };
            
            TPBanner.Instance().OnBannerBiddingEnd += (adunit, adInfo, error) =>
            {
                if (this.OnBannerBiddingEnd != null)
                {
                    this.OnBannerBiddingEnd(adunit, adInfo, error);
                }
            };

            TPBanner.Instance().OnBannerIsLoading += (adunit) =>
            {
                if (this.OnBannerIsLoading != null)
                {
                    this.OnBannerIsLoading(adunit);
                }
            };

            TPBanner.Instance().OnBannerOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnBannerOneLayerStartLoad != null)
                {
                    this.OnBannerOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPBanner.Instance().OnBannerOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnBannerOneLayerLoaded != null)
                {
                    this.OnBannerOneLayerLoaded(adunit, adInfo);
                }
            };

            TPBanner.Instance().OnBannerOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnBannerOneLayerLoadFailed != null)
                {
                    this.OnBannerOneLayerLoadFailed(adunit, adInfo, error);
                }
            };

            TPBanner.Instance().OnBannerAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnBannerAllLoaded != null)
                {
                    this.OnBannerAllLoaded(adunit, isSuccess);
                }
            };

#if UNITY_ANDROID
            TPBanner.Instance().OnDownloadStart += (adunit,adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadStart != null)
                {
                    this.OnDownloadStart(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPBanner.Instance().OnDownloadUpdate += (adunit, adInfo, totalBytes, currBytes, fileName, appName, progress) =>
            {
                if (this.OnDownloadUpdate != null)
                {
                    this.OnDownloadUpdate(adunit, adInfo, totalBytes, currBytes, fileName, appName, progress);
                }
            };


            TPBanner.Instance().OnDownloadPause += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadPause != null)
                {
                    this.OnDownloadPause(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPBanner.Instance().OnDownloadFinish += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFinish != null)
                {
                    this.OnDownloadFinish(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPBanner.Instance().OnDownloadFailed += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFailed != null)
                {
                    this.OnDownloadFailed(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPBanner.Instance().OnInstalled += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnInstalled != null)
                {
                    this.OnInstalled(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };
#endif
        }
    }
}

