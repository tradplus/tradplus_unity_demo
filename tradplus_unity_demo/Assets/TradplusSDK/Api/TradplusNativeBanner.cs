using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using TradplusSDK.Unity;
#elif UNITY_IOS
using TradplusSDK.iOS;
#elif UNITY_ANDROID
using TradplusSDK.Android;
#else
using TradplusSDK.Unity;
#endif

namespace TradplusSDK.Api
{
    public class TPNativeBanner :
#if UNITY_EDITOR
        TPSdkUnityNativeBanner
#elif UNITY_IOS
        TradplusNativeBanneriOS
#elif UNITY_ANDROID
        TradplusNativeBannerAndroid
#else
        TPSdkUnityNativeBanner
#endif

    { }

    ///<summary>
    ///附加参数 closeAutoShow:是否关闭自动展示;x:展示坐标 x; y:展示坐标 y; width:广告宽度 默认320; height:广告高度 默认50; adPosition:屏幕位置定位（x=0 y=0 时生效）,默认TopLeft ;customMap：流量分组; localParams：特殊参数，仅Android支持;
    ///</summary>
    public class TPNativeBannerExtra
    {
        ///<summary>
        ///是否需要简易回调
        ///</summary>
        public bool isSimpleListener;
        ///<summary>
        ///是否关闭自动展示
        ///</summary>
        public bool closeAutoShow;
        ///<summary>
        ///是否关闭自动销毁
        ///仅Android支持
        ///</summary>
        public bool closeAutoDestroy;
        ///<summary>
        ///原生横幅广告展示坐标 x，默认 0
        ///</summary>
        public float x;

        ///<summary>
        ///原生横幅广告展示坐标 y,，默认 0
        ///</summary>
        public float y;

        ///<summary>
        ///原生横幅广告展示 width，默认 全屏
        ///</summary>
        public float width;

        ///<summary>
        ///原生横幅广告展示 height，默认 50
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
        ///自定义模版名称
        ///</summary>
        public string className;

        ///<summary>
        ///自定义背景色 例如：#FFFFFF(仅iOS支持)
        ///</summary>
        public string backgroundColor;

        public bool openAutoLoadCallback;

        public float maxWaitTime;

        public TPNativeBannerExtra()
        {
            width = 0;
            height = 50;
            adPosition = TradplusBase.AdPosition.TopLeft;
            backgroundColor = "";
        }
    }

    public class TradplusNativeBanner
    {
        private static TradplusNativeBanner _instance;

        public static TradplusNativeBanner Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusNativeBanner();
            }
            return _instance;
        }

        ///<summary>
        ///加载广告 adUnitId：广告位ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadNativeBannerAd(string adUnitId, string sceneId = "", TPNativeBannerExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnNativeBannerLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnNativeBannerLoaded(adUnitId, adInfo);
            }

#elif UNITY_IOS || UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPNativeBannerExtra();
            }
            TPNativeBanner.Instance().LoadNativeBannerAd(adUnitId, sceneId, extra);
#endif
        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID
        ///nativebanner默认开启自动展示，一般不需要使用此接口。
        ///当TPNativeBannerExtra 中的 closeAutoShow 设置为ture时关闭自动展示。
        ///开发者可以在加载成功回调后调用此接口进行nativebanner展示。
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void ShowNativeBannerAd(string adUnitId, string sceneId = "")
        {
            TPNativeBanner.Instance().ShowNativeBannerAd(adUnitId, sceneId);
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool NativeBannerAdReady(string adUnitId)
        {
            return TPNativeBanner.Instance().NativeBannerAdReady(adUnitId);
        }

        ///<summary>
        ///进入广告场景 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void EntryNativeBannerAdScenario(string adUnitId, string sceneId = "")
        {
            TPNativeBanner.Instance().EntryNativeBannerAdScenario(adUnitId, sceneId);
        }

        ///<summary>
        ///隐藏广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void HideNativeBanner(string adUnitId)
        {
            TPNativeBanner.Instance().HideNativeBanner(adUnitId);
        }

        ///<summary>
        ///显示已隐藏了的广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DisplayNativeBanner(string adUnitId)
        {
            TPNativeBanner.Instance().DisplayNativeBanner(adUnitId);
        }


        ///<summary>
        ///移除广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DestroyNativeBanner(string adUnitId)
        {
            TPNativeBanner.Instance().DestroyNativeBanner(adUnitId);
        }

        ///<summary>
        ///v1.0.1 新增。
        ///开发者可在展示前通过此接口设置透传的adInfo信息。
        ///透传信息可以在广告展示后的相关回调的adInfo中获取。
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="customAdInfo">自定义透传的adInfo信息</param>
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPNativeBanner.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }

        ///<summary>
        ///开发者在 OnApplicationQuit 生命周期时调用关闭回调
        ///仅iOS支持
        ///</summary>
        public void ClearCallback()
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            TPNativeBanner.Instance().ClearCallback();
#endif
        }

        //接口回调

        //常用回调接口

        ///<summary>
        ///加载成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerClosed;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerOneLayerLoadFailed;

//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerStartLoad;

        ///<summary>
        ///Bidding开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerBiddingStart;

        ///<summary>
        ///Bidding结束 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBannerBiddingEnd;

        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnNativeBannerIsLoading;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBannerOneLayerLoaded;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnNativeBannerAllLoaded;


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

        public TradplusNativeBanner()
        {
            TPNativeBanner.Instance().OnNativeBannerLoaded += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerLoaded != null)
                {
                    this.OnNativeBannerLoaded(adunit, adInfo);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerLoadFailed += (adunit, error) =>
            {
                if (this.OnNativeBannerLoadFailed != null)
                {
                    this.OnNativeBannerLoadFailed(adunit, error);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerImpression += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerImpression != null)
                {
                    this.OnNativeBannerImpression(adunit, adInfo);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerShowFailed += (adunit, adInfo, error) =>
            {
                if (this.OnNativeBannerShowFailed != null)
                {
                    this.OnNativeBannerShowFailed(adunit, adInfo, error);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerClicked += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerClicked != null)
                {
                    this.OnNativeBannerClicked(adunit, adInfo);
                }
            };


            TPNativeBanner.Instance().OnNativeBannerClosed += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerClosed != null)
                {
                    this.OnNativeBannerClosed(adunit, adInfo);
                }
            };


            TPNativeBanner.Instance().OnNativeBannerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerStartLoad != null)
                {
                    this.OnNativeBannerStartLoad(adunit, adInfo);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerBiddingStart += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerBiddingStart != null)
                {
                    this.OnNativeBannerBiddingStart(adunit, adInfo);
                }
            };
            
            TPNativeBanner.Instance().OnNativeBannerBiddingEnd += (adunit, adInfo, error) =>
            {
                if (this.OnNativeBannerBiddingEnd != null)
                {
                    this.OnNativeBannerBiddingEnd(adunit, adInfo, error);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerIsLoading += (adunit) =>
            {
                if (this.OnNativeBannerIsLoading != null)
                {
                    this.OnNativeBannerIsLoading(adunit);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerOneLayerStartLoad != null)
                {
                    this.OnNativeBannerOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnNativeBannerOneLayerLoaded != null)
                {
                    this.OnNativeBannerOneLayerLoaded(adunit, adInfo);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnNativeBannerOneLayerLoadFailed != null)
                {
                    this.OnNativeBannerOneLayerLoadFailed(adunit, adInfo, error);
                }
            };

            TPNativeBanner.Instance().OnNativeBannerAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnNativeBannerAllLoaded != null)
                {
                    this.OnNativeBannerAllLoaded(adunit, isSuccess);
                }
            };

#if UNITY_ANDROID
            TPNativeBanner.Instance().OnDownloadStart += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadStart != null)
                {
                    this.OnDownloadStart(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPNativeBanner.Instance().OnDownloadUpdate += (adunit, adInfo, totalBytes, currBytes, fileName, appName, progress) =>
            {
                if (this.OnDownloadUpdate != null)
                {
                    this.OnDownloadUpdate(adunit, adInfo, totalBytes, currBytes, fileName, appName, progress);
                }
            };


            TPNativeBanner.Instance().OnDownloadPause += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadPause != null)
                {
                    this.OnDownloadPause(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPNativeBanner.Instance().OnDownloadFinish += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFinish != null)
                {
                    this.OnDownloadFinish(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPNativeBanner.Instance().OnDownloadFailed += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFailed != null)
                {
                    this.OnDownloadFailed(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPNativeBanner.Instance().OnInstalled += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
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
