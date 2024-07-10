using System;
using TradplusSDK.Android;
using System.Collections.Generic;
#if UNITY_EDITOR
using TradplusSDK.Unity;
#elif UNITY_ANDROID
using TradplusSDK.Android;
#else
#endif

namespace TradplusSDK.Api
{
#if UNITY_EDITOR
    public class TPInterActive : TPSdkUnityInterActive
#elif UNITY_ANDROID
    public class TPInterActive : TradplusInterActiveAndroid
#else
    public class TPInterActive
#endif
    { }

    ///<summary>
    ///附加参数 x:展示坐标 x; y:展示坐标 y; width:广告宽度 默认50; height:广告高度 默认50 ;customMap：流量分组; localParams：特殊参数，仅Android支持;
    ///</summary>
    public class TPInterActiveExtra
    {
        ///<summary>
        ///是否需要简易回调
        ///</summary>
        public bool isSimpleListener;

        ///<summary>
        ///互动广告展示坐标 x，默认 0
        ///</summary>
        public float x;

        ///<summary>
        ///互动广告展示坐标 y,，默认 0
        ///</summary>
        public float y;

        ///<summary>
        ///互动广告展示 width，默认 50
        ///</summary>
        public float width;

        ///<summary>
        ///互动广告展示 height，默认 50
        ///</summary>
        public float height;

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

        public TPInterActiveExtra()
        {
            width = 50;
            height = 50;
        }
    }

    public class TradplusInterActive
    {

        private static TradplusInterActive _instance;

        public static TradplusInterActive Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusInterActive();
            }
            return _instance;
        }

        ///<summary>
        ///加载广告 adUnitId：广告位ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadInterActiveAd(string adUnitId, TPInterActiveExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnInterActiveLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnInterActiveLoaded(adUnitId, adInfo);
            }
#elif  UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPInterActiveExtra();
            }
            TPInterActive.Instance().LoadInterActiveAd(adUnitId, extra);
#endif

        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID；
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void ShowInterActiveAd(string adUnitId, string sceneId = "")
        {
#if UNITY_ANDROID
            TPInterActive.Instance().ShowInterActiveAd(adUnitId, sceneId);
#endif
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool InterActiveAdReady(string adUnitId)
        {
#if UNITY_ANDROID || UNITY_EDITOR
            return TPInterActive.Instance().InterActiveAdReady(adUnitId);
#else
            return false;
#endif
        }

        ///<summary>
        ///隐藏广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void HideInterActive(string adUnitId)
        {
#if UNITY_ANDROID
            TPInterActive.Instance().HideInterActive(adUnitId);
#endif
        }

        ///<summary>
        ///显示已隐藏了的广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DisplayInterActive(string adUnitId)
        {
#if UNITY_ANDROID
            TPInterActive.Instance().DisplayInterActive(adUnitId);
#endif
        }


        ///<summary>
        ///移除广告
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void DestroyInterActive(string adUnitId)
        {
#if UNITY_ANDROID
            TPInterActive.Instance().DestroyInterActive(adUnitId);
#endif
        }

        ///<summary>
        ///开发者可在展示前通过此接口设置透传的adInfo信息。
        ///透传信息可以在广告展示后的相关回调的adInfo中获取。
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="customAdInfo">自定义透传的adInfo信息</param>
        public void SetCustomAdInfo(string adUnitId, Dictionary<string, string> customAdInfo)
        {
#if UNITY_ANDROID
            TPInterActive.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
#endif
        }

        //接口回调

        //常用回调接口

        ///<summary>
        ///加载成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterActiveShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveClosed;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterActiveOneLayerLoadFailed;

//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveStartLoad;

        ///<summary>
        ///Bidding开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveBiddingStart;

        ///<summary>
        ///Bidding结束 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnInterActiveBiddingEnd;

        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnInterActiveIsLoading;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveOneLayerLoaded;

        ///<summary>
        ///视频播放开始 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveVideoPlayStart;

        ///<summary>
        ///视频播放结束 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnInterActiveVideoPlayEnd;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnInterActiveAllLoaded;


        public TradplusInterActive()
        {
#if UNITY_ANDROID
            TPInterActive.Instance().OnInterActiveLoaded += (adunit, adInfo) =>
            {
                if (this.OnInterActiveLoaded != null)
                {
                    this.OnInterActiveLoaded(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveLoadFailed += (adunit, error) =>
            {
                if (this.OnInterActiveLoadFailed != null)
                {
                    this.OnInterActiveLoadFailed(adunit, error);
                }
            };

            TPInterActive.Instance().OnInterActiveImpression += (adunit, adInfo) =>
            {
                if (this.OnInterActiveImpression != null)
                {
                    this.OnInterActiveImpression(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveShowFailed += (adunit, adInfo, error) =>
            {
                if (this.OnInterActiveShowFailed != null)
                {
                    this.OnInterActiveShowFailed(adunit, adInfo, error);
                }
            };

            TPInterActive.Instance().OnInterActiveClicked += (adunit, adInfo) =>
            {
                if (this.OnInterActiveClicked != null)
                {
                    this.OnInterActiveClicked(adunit, adInfo);
                }
            };


            TPInterActive.Instance().OnInterActiveClosed += (adunit, adInfo) =>
            {
                if (this.OnInterActiveClosed != null)
                {
                    this.OnInterActiveClosed(adunit, adInfo);
                }
            };


            TPInterActive.Instance().OnInterActiveStartLoad += (adunit, adInfo) =>
            {
                if (this.OnInterActiveStartLoad != null)
                {
                    this.OnInterActiveStartLoad(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveBiddingStart += (adunit, adInfo) =>
            {
                if (this.OnInterActiveBiddingStart != null)
                {
                    this.OnInterActiveBiddingStart(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveBiddingEnd += (adunit, adInfo, error) =>
            {
                if (this.OnInterActiveBiddingEnd != null)
                {
                    this.OnInterActiveBiddingEnd(adunit, adInfo, error);
                }
            };

            TPInterActive.Instance().OnInterActiveIsLoading += (adunit) =>
            {
                if (this.OnInterActiveIsLoading != null)
                {
                    this.OnInterActiveIsLoading(adunit);
                }
            };

            TPInterActive.Instance().OnInterActiveOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnInterActiveOneLayerStartLoad != null)
                {
                    this.OnInterActiveOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnInterActiveOneLayerLoaded != null)
                {
                    this.OnInterActiveOneLayerLoaded(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnInterActiveOneLayerLoadFailed != null)
                {
                    this.OnInterActiveOneLayerLoadFailed(adunit, adInfo, error);
                }
            };

            TPInterActive.Instance().OnInterActiveVideoPlayStart += (adunit, adInfo) =>
            {
                if (this.OnInterActiveVideoPlayStart != null)
                {
                    this.OnInterActiveVideoPlayStart(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveVideoPlayEnd += (adunit, adInfo) =>
            {
                if (this.OnInterActiveVideoPlayEnd != null)
                {
                    this.OnInterActiveVideoPlayEnd(adunit, adInfo);
                }
            };

            TPInterActive.Instance().OnInterActiveAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnInterActiveAllLoaded != null)
                {
                    this.OnInterActiveAllLoaded(adunit, isSuccess);
                }
            };
#endif
        }
    }
}

