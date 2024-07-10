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
    public class TPRewardVideo :
#if UNITY_EDITOR
        TPSdkUnityRewardVideo
#elif UNITY_IOS
        TradplusRewardVideoiOS
#elif UNITY_ANDROID
        TradplusRewardVideoAndroid
#else
        TPSdkUnityRewardVideo
#endif
    { }

    ///<summary>
    ///附加参数  默认true; userId：服务器奖励验证参数; 如使用服务器奖励验证时此参数必填; customData:服务器奖励验证参数; customMap：流量分组; localParams：特殊参数，仅Android支持
    ///</summary>
    public class TPRewardVideoExtra
    {
        ///<summary>
        ///是否需要简易回调
        ///</summary>
        public bool isSimpleListener;
        ///<summary>
        ///服务器奖励验证参数，如使用服务器奖励验证时此参数必填
        ///</summary>
        public string userId;
        ///<summary>
        ///服务器奖励验证参数
        ///</summary>
        public string customData;
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


        public TPRewardVideoExtra()
        {
        }
    }


    public class TradplusRewardVideo
    {
        private static TradplusRewardVideo _instance;

        public static TradplusRewardVideo Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusRewardVideo();
            }
            return _instance;
        }

        ///<summary>
        ///加载广告 adUnitId：广告位ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadRewardVideoAd(string adUnitId, TPRewardVideoExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnRewardVideoLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnRewardVideoLoaded(adUnitId, adInfo);
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPRewardVideoExtra();
            }
            TPRewardVideo.Instance().LoadRewardVideoAd(adUnitId, extra);

#endif  
        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void ShowRewardVideoAd(string adUnitId, string sceneId = "")
        {
            TPRewardVideo.Instance().ShowRewardVideoAd(adUnitId, sceneId);
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool RewardVideoAdReady(string adUnitId)
        {
            return TPRewardVideo.Instance().RewardVideoAdReady(adUnitId);
        }

        ///<summary>
        ///进入广告场景 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void EntryRewardVideoAdScenario(string adUnitId, string sceneId = "")
        {
            TPRewardVideo.Instance().EntryRewardVideoAdScenario(adUnitId, sceneId);
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
            TPRewardVideo.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }

        ///<summary>
        ///开发者在 OnApplicationQuit 生命周期时调用关闭回调
        ///仅iOS支持
        ///</summary>
        public void ClearCallback()
        {
#if UNITY_EDITOR
            
#elif UNITY_IOS
            TPRewardVideo.Instance().ClearCallback();
#endif
        }

//接口回调

//常用回调接口

        ///<summary>
        ///加载成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoClosed;

        ///<summary>
        ///获得奖励 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoReward;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoOneLayerLoadFailed;

//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoStartLoad;

        ///<summary>
        ///Bidding开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoBiddingStart;

        ///<summary>
        ///Bidding结束 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnRewardVideoBiddingEnd;

        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnRewardVideoIsLoading;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoOneLayerLoaded;

        ///<summary>
        ///视频播放开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoPlayStart;

        ///<summary>
        ///视频播放结束 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnRewardVideoPlayEnd;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnRewardVideoAllLoaded;

// 国内再看一个相关回调(可选)

        ///<summary>
        ///再看一个激励视频 展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnPlayAgainImpression;

        ///<summary>
        ///再看一个激励视频 获得奖励 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnPlayAgainReward;

        ///<summary>
        ///再看一个激励视频 点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnPlayAgainClicked;

        ///<summary>
        ///再看一个激励视频 播放开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnPlayAgainVideoPlayStart;

        ///<summary>
        ///再看一个激励视频 播放结束 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnPlayAgainVideoPlayEnd;

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

        public TradplusRewardVideo()
        {
            TPRewardVideo.Instance().OnRewardVideoLoaded += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoLoaded != null)
                {
                    this.OnRewardVideoLoaded(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoLoadFailed += (adunit, error) =>
            {
                if (this.OnRewardVideoLoadFailed != null)
                {
                    this.OnRewardVideoLoadFailed(adunit, error);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoImpression += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoImpression != null)
                {
                    this.OnRewardVideoImpression(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoShowFailed += (adunit, adInfo, error) =>
            {
                if (this.OnRewardVideoShowFailed != null)
                {
                    this.OnRewardVideoShowFailed(adunit, adInfo, error);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoClicked += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoClicked != null)
                {
                    this.OnRewardVideoClicked(adunit, adInfo);
                }
            };


            TPRewardVideo.Instance().OnRewardVideoClosed += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoClosed != null)
                {
                    this.OnRewardVideoClosed(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoReward += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoReward != null)
                {
                    this.OnRewardVideoReward(adunit, adInfo);
                }
            };


            TPRewardVideo.Instance().OnRewardVideoStartLoad += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoStartLoad != null)
                {
                    this.OnRewardVideoStartLoad(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoBiddingStart += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoBiddingStart != null)
                {
                    this.OnRewardVideoBiddingStart(adunit, adInfo);
                }
            };
            
            TPRewardVideo.Instance().OnRewardVideoBiddingEnd += (adunit, adInfo, error) =>
            {
                if (this.OnRewardVideoBiddingEnd != null)
                {
                    this.OnRewardVideoBiddingEnd(adunit, adInfo, error);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoIsLoading += (adunit) =>
            {
                if (this.OnRewardVideoIsLoading != null)
                {
                    this.OnRewardVideoIsLoading(adunit);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoOneLayerStartLoad != null)
                {
                    this.OnRewardVideoOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoOneLayerLoaded != null)
                {
                    this.OnRewardVideoOneLayerLoaded(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnRewardVideoOneLayerLoadFailed != null)
                {
                    this.OnRewardVideoOneLayerLoadFailed(adunit, adInfo, error);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoPlayStart += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoPlayStart != null)
                {
                    this.OnRewardVideoPlayStart(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoPlayEnd += (adunit, adInfo) =>
            {
                if (this.OnRewardVideoPlayEnd != null)
                {
                    this.OnRewardVideoPlayEnd(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnRewardVideoAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnRewardVideoAllLoaded != null)
                {
                    this.OnRewardVideoAllLoaded(adunit, isSuccess);
                }
            };

            // 国内再看一个相关回调

            TPRewardVideo.Instance().OnPlayAgainImpression += (adunit, adInfo) =>
            {
                if (this.OnPlayAgainImpression != null)
                {
                    this.OnPlayAgainImpression(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnPlayAgainReward += (adunit, adInfo) =>
            {
                if (this.OnPlayAgainReward != null)
                {
                    this.OnPlayAgainReward(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnPlayAgainClicked += (adunit, adInfo) =>
            {
                if (this.OnPlayAgainClicked != null)
                {
                    this.OnPlayAgainClicked(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnPlayAgainVideoPlayStart += (adunit, adInfo) =>
            {
                if (this.OnPlayAgainVideoPlayStart != null)
                {
                    this.OnPlayAgainVideoPlayStart(adunit, adInfo);
                }
            };

            TPRewardVideo.Instance().OnPlayAgainVideoPlayEnd += (adunit, adInfo) =>
            {
                if (this.OnPlayAgainVideoPlayEnd != null)
                {
                    this.OnPlayAgainVideoPlayEnd(adunit, adInfo);
                }
            };

#if UNITY_ANDROID
            TPRewardVideo.Instance().OnDownloadStart += (adunit, adInfo, totalBytes,  currBytes,  fileName,  appName) =>
            {
                if (this.OnDownloadStart != null)
                {
                    this.OnDownloadStart(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPRewardVideo.Instance().OnDownloadUpdate += (adunit, adInfo, totalBytes, currBytes, fileName, appName,progress) =>
            {
                if (this.OnDownloadUpdate != null)
                {
                    this.OnDownloadUpdate(adunit, adInfo, totalBytes, currBytes, fileName, appName,progress);
                }
            };


            TPRewardVideo.Instance().OnDownloadPause += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadPause != null)
                {
                    this.OnDownloadPause(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };


            TPRewardVideo.Instance().OnDownloadFinish += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFinish != null)
                {
                    this.OnDownloadFinish(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPRewardVideo.Instance().OnDownloadFailed += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
            {
                if (this.OnDownloadFailed != null)
                {
                    this.OnDownloadFailed(adunit, adInfo, totalBytes, currBytes, fileName, appName);
                }
            };

            TPRewardVideo.Instance().OnInstalled += (adunit, adInfo, totalBytes, currBytes, fileName, appName) =>
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
