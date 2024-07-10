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
    public class TPNative :
#if UNITY_EDITOR
        TPSdkUnityNative
#elif UNITY_IOS
        TradplusNativeiOS
#elif UNITY_ANDROID
        TradplusNativeAndroid
#else
        TPSdkUnityNative
#endif
    { }

    ///<summary>
    ///附加参数 isAutoLoad：是否自动加载, 默认true; x:展示坐标 x; y:展示坐标 y; width:广告宽度 默认320; height:广告高度 默认200; adPosition:屏幕位置定位（x=0 y=0 时生效）,默认TopLeft ;customMap：流量分组; localParams：特殊参数，仅Android支持;
    ///</summary>
    public class TPNativeExtra
    {
        ///<summary>
        ///是否需要简易回调
        ///</summary>
        public bool isSimpleListener;

        ///<summary>
        ///原生广告展示坐标 x，默认 0
        ///</summary>
        public float x;

        ///<summary>
        ///原生广告展示坐标 y,，默认 0
        ///</summary>
        public float y;

        ///<summary>
        ///原生广告展示 width，默认 320
        ///</summary>
        public float width;

        ///<summary>
        ///原生广告展示 height，默认 200
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
        ///特殊参数，仅Android支持
        ///</summary>
        public Dictionary<string, object> localParams;

        public bool openAutoLoadCallback;

        public float maxWaitTime;

        public TPNativeExtra()
        {
            width = 320;
            height = 200;
            adPosition = TradplusBase.AdPosition.TopLeft;
        }
    }

    public class TradplusNative
    {

        private static TradplusNative _instance;

        public static TradplusNative Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusNative();
            }
            return _instance;
        }

        ///<summary>
        ///加载广告 adUnitId：广告位ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadNativeAd(string adUnitId, TPNativeExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnNativeLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnNativeLoaded(adUnitId, adInfo);
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPNativeExtra();
            }
            TPNative.Instance().LoadNativeAd(adUnitId, extra);
#endif    
        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID；className：自定义模版名称 设置null时会使用默认
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        ///<param name="className">自定义模版名称</param>
        public void ShowNativeAd(string adUnitId, string sceneId = "", string className = null)
        {
            TPNative.Instance().ShowNativeAd(adUnitId, sceneId, className);
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool NativeAdReady(string adUnitId)
        {
            return TPNative.Instance().NativeAdReady(adUnitId);
        }

        ///<summary>
        ///进入广告场景 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void EntryNativeAdScenario(string adUnitId, string sceneId = "")
        {
            TPNative.Instance().EntryNativeAdScenario(adUnitId, sceneId);
        }

        ///<summary>
        ///隐藏广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void HideNative(string adUnitId)
        {
            TPNative.Instance().HideNative(adUnitId);
        }

        ///<summary>
        ///显示已隐藏了的广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DisplayNative(string adUnitId)
        {
            TPNative.Instance().DisplayNative(adUnitId);
        }


        ///<summary>
        ///移除广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DestroyNative(string adUnitId)
        {
            TPNative.Instance().DestroyNative(adUnitId);
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
            TPNative.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }

        ///<summary>
        ///开发者在 OnApplicationQuit 生命周期时调用关闭回调
        ///仅iOS支持
        ///</summary>
        public void ClearCallback()
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            TPNative.Instance().ClearCallback();
#endif
        }

        //接口回调

        //常用回调接口

        ///<summary>
        ///加载成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeClosed;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeOneLayerLoadFailed;

//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeStartLoad;

        ///<summary>
        ///Bidding开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeBiddingStart;

        ///<summary>
        ///Bidding结束 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnNativeBiddingEnd;

        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnNativeIsLoading;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeOneLayerLoaded;

        ///<summary>
        ///视频播放开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayStart;

        ///<summary>
        ///视频播放结束 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnNativeVideoPlayEnd;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnNativeAllLoaded;


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

        public TradplusNative()
        {

            TPNative.Instance().OnNativeLoaded += (adunit, adInfo) =>
            {
                if (this.OnNativeLoaded != null)
                {
                    this.OnNativeLoaded(adunit, adInfo);
                }
            };

            TPNative.Instance().OnNativeLoadFailed += (adunit, error) =>
            {
                if (this.OnNativeLoadFailed != null)
                {
                    this.OnNativeLoadFailed(adunit, error);
                }
            };

            TPNative.Instance().OnNativeImpression += (adunit, adInfo) =>
            {
                if (this.OnNativeImpression != null)
                {
                    this.OnNativeImpression(adunit, adInfo);
                }
            };

            TPNative.Instance().OnNativeShowFailed += (adunit, adInfo, error) =>
            {
                if (this.OnNativeShowFailed != null)
                {
                    this.OnNativeShowFailed(adunit, adInfo, error);
                }
            };

            TPNative.Instance().OnNativeClicked += (adunit, adInfo) =>
            {
                if (this.OnNativeClicked != null)
                {
                    this.OnNativeClicked(adunit, adInfo);
                }
            };


            TPNative.Instance().OnNativeClosed += (adunit, adInfo) =>
            {
                if (this.OnNativeClosed != null)
                {
                    this.OnNativeClosed(adunit, adInfo);
                }
            };


            TPNative.Instance().OnNativeStartLoad += (adunit, adInfo) =>
            {
                if (this.OnNativeStartLoad != null)
                {
                    this.OnNativeStartLoad(adunit, adInfo);
                }
            };

            TPNative.Instance().OnNativeBiddingStart += (adunit, adInfo) =>
            {
                if (this.OnNativeBiddingStart != null)
                {
                    this.OnNativeBiddingStart(adunit, adInfo);
                }
            };
            
            TPNative.Instance().OnNativeBiddingEnd += (adunit, adInfo, error) =>
            {
                if (this.OnNativeBiddingEnd != null)
                {
                    this.OnNativeBiddingEnd(adunit, adInfo, error);
                }
            };

            TPNative.Instance().OnNativeIsLoading += (adunit) =>
            {
                if (this.OnNativeIsLoading != null)
                {
                    this.OnNativeIsLoading(adunit);
                }
            };

            TPNative.Instance().OnNativeOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnNativeOneLayerStartLoad != null)
                {
                    this.OnNativeOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPNative.Instance().OnNativeOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnNativeOneLayerLoaded != null)
                {
                    this.OnNativeOneLayerLoaded(adunit, adInfo);
                }
            };

            TPNative.Instance().OnNativeOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnNativeOneLayerLoadFailed != null)
                {
                    this.OnNativeOneLayerLoadFailed(adunit, adInfo, error);
                }
            };

            TPNative.Instance().OnNativeVideoPlayStart += (adunit, adInfo) =>
            {
                if (this.OnNativeVideoPlayStart != null)
                {
                    this.OnNativeVideoPlayStart(adunit, adInfo);
                }
            };

            TPNative.Instance().OnNativeVideoPlayEnd += (adunit, adInfo) =>
            {
                if (this.OnNativeVideoPlayEnd != null)
                {
                    this.OnNativeVideoPlayEnd(adunit, adInfo);
                }
            };

            TPNative.Instance().OnNativeAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnNativeAllLoaded != null)
                {
                    this.OnNativeAllLoaded(adunit, isSuccess);
                }
            };

#if UNITY_ANDROID
            TPNative.Instance().OnDownloadStart += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadStart != null)
                {
                    this.OnDownloadStart(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPNative.Instance().OnDownloadUpdate += (adunit, adInfo, totalBytes, currBytes, fileName, appName, progress) =>
            {
                if (this.OnDownloadUpdate != null)
                {
                    this.OnDownloadUpdate(adunit, adInfo, totalBytes, currBytes, fileName, appName, progress);
                }
            };


            TPNative.Instance().OnDownloadPause += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadPause != null)
                {
                    this.OnDownloadPause(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPNative.Instance().OnDownloadFinish += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFinish != null)
                {
                    this.OnDownloadFinish(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPNative.Instance().OnDownloadFailed += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFailed != null)
                {
                    this.OnDownloadFailed(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPNative.Instance().OnInstalled += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
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

