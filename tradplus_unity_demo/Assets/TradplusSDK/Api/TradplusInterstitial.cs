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
using UnityEngine;

namespace TradplusSDK.Api
{

    public class TPInterstitial:
#if UNITY_EDITOR
     TPSdkUnityInterstitial
#elif UNITY_IOS
     TradplusInterstitialiOS
#elif UNITY_ANDROID
     TradplusInterstitialAndroid
#else
     TPSdkUnityInterstitial
#endif
    { }

    ///<summary>
    ///附加参数 isAutoLoad：是否自动加载, 默认true; customMap：流量分组; localParams：特殊参数，仅Android支持
    ///</summary>
    public class TPInterstitialExtra
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

        public TPInterstitialExtra()
        {
        }
    }

    public class TradplusInterstitial
    {
        private static TradplusInterstitial _instance;

        public static TradplusInterstitial Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusInterstitial();
            }
            return _instance;
        }


        ///<summary>
        ///加载广告 adUnitId：广告位ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadInterstitialAd(string adUnitId, TPInterstitialExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnInterstitialLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnInterstitialLoaded(adUnitId, adInfo);
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPInterstitialExtra();
            }
            TPInterstitial.Instance().LoadInterstitialAd(adUnitId, extra);
#endif
        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void ShowInterstitialAd(string adUnitId,string sceneId = "")
        {
            TPInterstitial.Instance().ShowInterstitialAd(adUnitId, sceneId);
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool InterstitialAdReady(string adUnitId)
        {
            return TPInterstitial.Instance().InterstitialAdReady(adUnitId);
        }

        ///<summary>
        ///进入广告场景 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void EntryInterstitialAdScenario(string adUnitId, string sceneId = "")
        {
            TPInterstitial.Instance().EntryInterstitialAdScenario(adUnitId,sceneId);
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
            TPInterstitial.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
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
        public event Action<string, Dictionary<string, object>> OnInterstitialLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialClosed;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialOneLayerLoadFailed;


//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialStartLoad;

        ///<summary>
        ///Bidding开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialBiddingStart;

        ///<summary>
        ///Bidding结束 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterstitialBiddingEnd;

        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnInterstitialIsLoading;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialOneLayerLoaded;

        ///<summary>
        ///视频播放开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialVideoPlayStart;

        ///<summary>
        ///视频播放结束 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterstitialVideoPlayEnd;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnInterstitialAllLoaded;

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



        public TradplusInterstitial()
        {
            TPInterstitial.Instance().OnInterstitialLoaded += (adunit, adInfo) =>
            {
                if (this.OnInterstitialLoaded != null)
                {
                    this.OnInterstitialLoaded(adunit, adInfo);
                }
            };

            TPInterstitial.Instance().OnInterstitialLoadFailed += (adunit, error) =>
            {
                if (this.OnInterstitialLoadFailed != null)
                {
                    this.OnInterstitialLoadFailed(adunit, error);
                }
            };

            TPInterstitial.Instance().OnInterstitialImpression += (adunit, adInfo) =>
            {
                if (this.OnInterstitialImpression != null)
                {
                    this.OnInterstitialImpression(adunit, adInfo);
                }
            };

            TPInterstitial.Instance().OnInterstitialShowFailed += (adunit, adInfo, error) =>
            {
                if (this.OnInterstitialShowFailed != null)
                {
                    this.OnInterstitialShowFailed(adunit, adInfo, error);
                }
            };

            TPInterstitial.Instance().OnInterstitialClicked += (adunit, adInfo) =>
            {
                if (this.OnInterstitialClicked != null)
                {
                    this.OnInterstitialClicked(adunit, adInfo);
                }
            };


            TPInterstitial.Instance().OnInterstitialClosed += (adunit, adInfo) =>
            {
                if (this.OnInterstitialClosed != null)
                {
                    this.OnInterstitialClosed(adunit, adInfo);
                }
            };


            TPInterstitial.Instance().OnInterstitialStartLoad += (adunit, adInfo) =>
            {
                if (this.OnInterstitialStartLoad != null)
                {
                    this.OnInterstitialStartLoad(adunit, adInfo);
                }
            };

            TPInterstitial.Instance().OnInterstitialBiddingStart += (adunit, adInfo) =>
            {
                if (this.OnInterstitialBiddingStart != null)
                {
                    this.OnInterstitialBiddingStart(adunit, adInfo);
                }
            };
            
            TPInterstitial.Instance().OnInterstitialBiddingEnd += (adunit, adInfo, error) =>
            {
                if (this.OnInterstitialBiddingEnd != null)
                {
                    this.OnInterstitialBiddingEnd(adunit, adInfo, error);
                }
            };

            TPInterstitial.Instance().OnInterstitialIsLoading += (adunit) =>
            {
                if (this.OnInterstitialIsLoading != null)
                {
                    this.OnInterstitialIsLoading(adunit);
                }
            };

            TPInterstitial.Instance().OnInterstitialOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnInterstitialOneLayerStartLoad != null)
                {
                    this.OnInterstitialOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPInterstitial.Instance().OnInterstitialOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnInterstitialOneLayerLoaded != null)
                {
                    this.OnInterstitialOneLayerLoaded(adunit, adInfo);
                }
            };

            TPInterstitial.Instance().OnInterstitialOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnInterstitialOneLayerLoadFailed != null)
                {
                    this.OnInterstitialOneLayerLoadFailed(adunit, adInfo, error);
                }
            };

            TPInterstitial.Instance().OnInterstitialVideoPlayStart += (adunit, adInfo) =>
            {
                if (this.OnInterstitialVideoPlayStart != null)
                {
                    this.OnInterstitialVideoPlayStart(adunit, adInfo);
                }
            };

            TPInterstitial.Instance().OnInterstitialVideoPlayEnd += (adunit, adInfo) =>
            {
                if (this.OnInterstitialVideoPlayEnd != null)
                {
                    this.OnInterstitialVideoPlayEnd(adunit, adInfo);
                }
            };

            TPInterstitial.Instance().OnInterstitialAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnInterstitialAllLoaded != null)
                {
                    this.OnInterstitialAllLoaded(adunit, isSuccess);
                }
            };

#if UNITY_ANDROID
            TPInterstitial.Instance().OnDownloadStart += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadStart != null)
                {
                    this.OnDownloadStart(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPInterstitial.Instance().OnDownloadUpdate += (adunit, adInfo, totalBytes, currBytes, fileName, appName, progress) =>
            {
                if (this.OnDownloadUpdate != null)
                {
                    this.OnDownloadUpdate(adunit, adInfo, totalBytes, currBytes, fileName, appName, progress);
                }
            };


            TPInterstitial.Instance().OnDownloadPause += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadPause != null)
                {
                    this.OnDownloadPause(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPInterstitial.Instance().OnDownloadFinish += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFinish != null)
                {
                    this.OnDownloadFinish(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPInterstitial.Instance().OnDownloadFailed += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFailed != null)
                {
                    this.OnDownloadFailed(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPInterstitial.Instance().OnInstalled += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
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

