using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using TradplusSDK.Unity;
#elif UNITY_IOS
using TradplusSDK.iOS;
#elif UNITY_ANDROID
using TradplusSDK.Android;
#elif UNITY_EDITOR
using TradplusSDK.Unity;
#else
using TradplusSDK.Unity;
#endif
using UnityEngine;

namespace TradplusSDK.Api
{
    public class TPSplash :
#if UNITY_EDITOR
        TPSdkUnitySplash
#elif UNITY_IOS
        TradplusSplashiOS
#elif UNITY_ANDROID
        TradplusSplashAndroid
#else
        TPSdkUnitySplash
#endif
    { }

    ///<summary>
    ///附加参数 isAutoLoad：是否自动加载, 默认true; customMap：流量分组; localParams：特殊参数，仅Android支持
    ///</summary>
    public class TPSplashExtra
    {
        ///<summary>
        ///是否需要简易回调
        ///</summary>
        public bool isSimpleListener;
        ///<summary>
        ///流量分组
        ///</summary>
        public Dictionary<string, string> customMap;
        ///<summary>
        ///本地参数
        ///</summary>
        public Dictionary<string, object> localParams;

        public bool openAutoLoadCallback;

        public float maxWaitTime;

        public TPSplashExtra()
        {
        }
    }

    public class TradplusSplash
    {
        private static TradplusSplash _instance;

        public static TradplusSplash Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusSplash();
            }
            return _instance;
        }


        ///<summary>
        ///加载广告 adUnitId：广告位ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadSplashAd(string adUnitId, TPSplashExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnSplashLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnSplashLoaded(adUnitId, adInfo);
            }

#elif UNITY_IOS || UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPSplashExtra();
            }
            TPSplash.Instance().LoadSplashAd(adUnitId, extra);
#endif  
        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        //////<param name="sceneId">广告场景ID</param>
        public void ShowSplashAd(string adUnitId, string sceneId = "")
        {
            TPSplash.Instance().ShowSplashAd(adUnitId, sceneId);
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool SplashAdReady(string adUnitId)
        {
            return TPSplash.Instance().SplashAdReady(adUnitId);
        }

        ///<summary>
        ///进入广告场景 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void EntrySplashAdScenario(string adUnitId, string sceneId = "")
        {
            TPSplash.Instance().EntrySplashAdScenario(adUnitId, sceneId);
        }

        ///<summary>
        ///开发者可在展示前通过此接口设置透传的adInfo信息。
        ///透传信息可以在广告展示后的相关回调的adInfo中获取。
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="customAdInfo">自定义透传的adInfo信息</param>
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
            TPSplash.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }

        ///<summary>
        ///开发者在 OnApplicationQuit 生命周期时调用关闭回调
        ///仅iOS支持
        ///</summary>
        public void ClearCallback()
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            TPSplash.Instance().ClearCallback();
#endif
        }

        //接口回调

        //常用回调接口

        ///<summary>
        ///加载成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashClosed;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashOneLayerLoadFailed;


//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashStartLoad;

        ///<summary>
        ///Bidding开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashBiddingStart;

        ///<summary>
        ///Bidding结束 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnSplashBiddingEnd;

        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnSplashIsLoading;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashOneLayerLoaded;

        ///<summary>
        ///点睛开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutStart;

        ///<summary>
        ///点睛结束 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashZoomOutEnd;

        ///<summary>
        ///跳过 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnSplashSkip;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnSplashAllLoaded;

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



        public TradplusSplash()
        {
            TPSplash.Instance().OnSplashLoaded += (adunit, adInfo) =>
            {
                if (this.OnSplashLoaded != null)
                {
                    this.OnSplashLoaded(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashLoadFailed += (adunit, error) =>
            {
                if (this.OnSplashLoadFailed != null)
                {
                    this.OnSplashLoadFailed(adunit, error);
                }
            };

            TPSplash.Instance().OnSplashImpression += (adunit, adInfo) =>
            {
                if (this.OnSplashImpression != null)
                {
                    this.OnSplashImpression(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashShowFailed += (adunit, adInfo, error) =>
            {
                if (this.OnSplashShowFailed != null)
                {
                    this.OnSplashShowFailed(adunit, adInfo, error);
                }
            };

            TPSplash.Instance().OnSplashClicked += (adunit, adInfo) =>
            {
                if (this.OnSplashClicked != null)
                {
                    this.OnSplashClicked(adunit, adInfo);
                }
            };


            TPSplash.Instance().OnSplashClosed += (adunit, adInfo) =>
            {
                if (this.OnSplashClosed != null)
                {
                    this.OnSplashClosed(adunit, adInfo);
                }
            };


            TPSplash.Instance().OnSplashStartLoad += (adunit, adInfo) =>
            {
                if (this.OnSplashStartLoad != null)
                {
                    this.OnSplashStartLoad(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashBiddingStart += (adunit, adInfo) =>
            {
                if (this.OnSplashBiddingStart != null)
                {
                    this.OnSplashBiddingStart(adunit, adInfo);
                }
            };
            
            TPSplash.Instance().OnSplashBiddingEnd += (adunit, adInfo, error) =>
            {
                if (this.OnSplashBiddingEnd != null)
                {
                    this.OnSplashBiddingEnd(adunit, adInfo, error);
                }
            };

            TPSplash.Instance().OnSplashIsLoading += (adunit) =>
            {
                if (this.OnSplashIsLoading != null)
                {
                    this.OnSplashIsLoading(adunit);
                }
            };

            TPSplash.Instance().OnSplashOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnSplashOneLayerStartLoad != null)
                {
                    this.OnSplashOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnSplashOneLayerLoaded != null)
                {
                    this.OnSplashOneLayerLoaded(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnSplashOneLayerLoadFailed != null)
                {
                    this.OnSplashOneLayerLoadFailed(adunit, adInfo, error);
                }
            };

            TPSplash.Instance().OnSplashZoomOutStart += (adunit, adInfo) =>
            {
                if (this.OnSplashZoomOutStart != null)
                {
                    this.OnSplashZoomOutStart(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashZoomOutEnd += (adunit, adInfo) =>
            {
                if (this.OnSplashZoomOutEnd != null)
                {
                    this.OnSplashZoomOutEnd(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashSkip += (adunit, adInfo) =>
            {
                if (this.OnSplashSkip != null)
                {
                    this.OnSplashSkip(adunit, adInfo);
                }
            };

            TPSplash.Instance().OnSplashAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnSplashAllLoaded != null)
                {
                    this.OnSplashAllLoaded(adunit, isSuccess);
                }
            };
#if UNITY_ANDROID
            TPSplash.Instance().OnDownloadStart += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadStart != null)
                {
                    this.OnDownloadStart(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPSplash.Instance().OnDownloadUpdate += (adunit, adInfo, totalBytes, currBytes, fileName, appName, progress) =>
            {
                if (this.OnDownloadUpdate != null)
                {
                    this.OnDownloadUpdate(adunit, adInfo, totalBytes, currBytes, fileName, appName, progress);
                }
            };


            TPSplash.Instance().OnDownloadPause += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadPause != null)
                {
                    this.OnDownloadPause(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPSplash.Instance().OnDownloadFinish += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFinish != null)
                {
                    this.OnDownloadFinish(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPSplash.Instance().OnDownloadFailed += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFailed != null)
                {
                    this.OnDownloadFailed(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPSplash.Instance().OnInstalled += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
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

