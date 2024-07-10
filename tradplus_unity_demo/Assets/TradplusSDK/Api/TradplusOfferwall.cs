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
    public class TPOfferwall :
#if UNITY_EDITOR
        TPSdkUnityOfferwall
#elif UNITY_IOS
        TradplusOfferwalliOS
#elif UNITY_ANDROID
        TradplusOfferwallAndroid
#else
        TPSdkUnityOfferwall
#endif
    { }

    ///<summary>
    ///附加参数 isAutoLoad：是否自动加载, 默认true; customMap：流量分组; localParams：特殊参数，仅Android支持
    ///</summary>
    public class TPOfferwallExtra
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

        public TPOfferwallExtra()
        {
        }
    }

    public class TradplusOfferwall
    {
        private static TradplusOfferwall _instance;

        public static TradplusOfferwall Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusOfferwall();
            }
            return _instance;
        }

        ///<summary>
        ///加载广告 adUnitId：广告位ID；extra：附加参数
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="extra">附加参数</param> 
        public void LoadOfferwallAd(string adUnitId, TPOfferwallExtra extra = null)
        {
#if UNITY_EDITOR
            if (this.OnOfferwallLoaded != null)
            {
                Dictionary<string, object> adInfo = new Dictionary<string, object>();
                this.OnOfferwallLoaded(adUnitId, adInfo);
            }
#elif  UNITY_IOS || UNITY_ANDROID
            if (extra == null)
            {
                extra = new TPOfferwallExtra();
            }
            TPOfferwall.Instance().LoadOfferwallAd(adUnitId, extra);
#endif  
        }

        ///<summary>
        ///展示广告 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void ShowOfferwallAd(string adUnitId, string sceneId = "")
        {
            TPOfferwall.Instance().ShowOfferwallAd(adUnitId, sceneId);
        }

        ///<summary>
        ///是否有ready广告 adUnitId：广告位ID；
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public bool OfferwallAdReady(string adUnitId)
        {
            return TPOfferwall.Instance().OfferwallAdReady(adUnitId);
        }

        ///<summary>
        ///进入广告场景 adUnitId：广告位ID；sceneId：广告场景ID
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="sceneId">广告场景ID</param>
        public void EntryOfferwallAdScenario(string adUnitId, string sceneId = "")
        {
            TPOfferwall.Instance().EntryOfferwallAdScenario(adUnitId, sceneId);
        }

        ///<summary>
        ///设置积分墙用户ID adUnitId：广告位ID；userId：用户ID
        ///需要设置回调 OnOfferwallSetUserIdFinish 来获取设置结果
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="userId">用户ID</param>
        public void SetUserId(string adUnitId,string userId)
        {
            TPOfferwall.Instance().SetUserId(adUnitId, userId);
        }

        ///<summary>
        ///查询当前用户积分墙积分 adUnitId：广告位ID
        ///需要设置回调 OnCurrencyBalanceSuccess & OnCurrencyBalanceFailed 来获取查询结果
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        public void GetCurrencyBalance(string adUnitId)
        {
            TPOfferwall.Instance().GetCurrencyBalance(adUnitId);
        }

        ///<summary>
        ///扣除用户积分墙积分 adUnitId：广告位ID；count：积分数量
        ///需要设置回调 OnSpendCurrencySuccess & OnSpendCurrencyFailed 来获取扣除是否成功及扣除后的积分数量
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="count">积分数量</param>
        public void SpendBalance(string adUnitId, int count)
        {
            TPOfferwall.Instance().SpendBalance(adUnitId, count);
        }

        ///<summary>
        ///增加用户积分墙积分 adUnitId：广告位ID；count：积分数量
        ///需要设置回调 OnAwardCurrencySuccess & OnAwardCurrencyFailed 来获取添加是否成功及扣除后的积分数量
        ///</summary>
        ///<param name="adUnitId">广告位ID</param>
        ///<param name="count">积分数量</param>
        public void AwardBalance(string adUnitId, int count)
        {
            TPOfferwall.Instance().AwardBalance(adUnitId, count);
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
            TPOfferwall.Instance().SetCustomAdInfo(adUnitId, customAdInfo);
        }

        ///<summary>
        ///开发者在 OnApplicationQuit 生命周期时调用关闭回调
        ///仅iOS支持
        ///</summary>
        public void ClearCallback()
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            TPOfferwall.Instance().ClearCallback();
#endif
        }

        //接口回调

        //常用回调接口

        ///<summary>
        ///加载成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallLoaded;

        ///<summary>
        ///加载失败 string adUnitId,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallLoadFailed;

        ///<summary>
        ///展示成功 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallImpression;

        ///<summary>
        ///展示失败 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnOfferwallShowFailed;

        ///<summary>
        ///点击 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallClicked;

        ///<summary>
        ///广告关闭 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallClosed;

        ///<summary>
        ///每层waterfall加载失败时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>, Dictionary<string, object>> OnOfferwallOneLayerLoadFailed;

//功能相关回调

        ///<summary>
        ///查询积分成功 string adUnitId, bool isSuccess 是否设置成功
        ///</summary>
        public event Action<string, bool> OnOfferwallSetUserIdFinish;

        ///<summary>
        ///查询积分成功 string adUnitId, int amount 用户当前积分,string msg
        ///</summary>
        public event Action<string, int, string> OnCurrencyBalanceSuccess;

        ///<summary>
        ///查询积分失败 string adUnitId,string msg
        ///</summary>
        public event Action<string, string> OnCurrencyBalanceFailed;

        ///<summary>
        ///扣除积分成功 string adUnitId, int amount 用户当前积分,string msg
        ///</summary>
        public event Action<string, int, string> OnSpendCurrencySuccess;

        ///<summary>
        ///扣除积分失败 string adUnitId,string msg
        ///</summary>
        public event Action<string, string> OnSpendCurrencyFailed;

        ///<summary>
        ///增加积分成功 string adUnitId, int amount 用户当前积分,string msg
        ///</summary>
        public event Action<string, int, string> OnAwardCurrencySuccess;

        ///<summary>
        ///增加积分失败 string adUnitId,string msg
        ///</summary>
        public event Action<string, string> OnAwardCurrencyFailed;


//广告源维度回调(可选)

        ///<summary>
        ///开始加载 string adUnitId,Dictionary adInfo
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallStartLoad;

        ///<summary>
        ///每层waterfall开始加载时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallOneLayerStartLoad;

        ///<summary>
        ///每层waterfall加载成功时回调 string adUnitId,Dictionary adInfo,Dictionary error
        ///</summary>
        public event Action<string, Dictionary<string, object>> OnOfferwallOneLayerLoaded;

        ///<summary>
        ///加载流程结束 string adUnitId,bool isSuccess 是否有广告源加载成功
        ///</summary>
        public event Action<string, bool> OnOfferwallAllLoaded;


        ///<summary>
        ///IsLoading string adUnitId
        ///</summary>
        public event Action<string> OnOfferwallIsLoading;


        public TradplusOfferwall()
        {
            TPOfferwall.Instance().OnOfferwallLoaded += (adunit, adInfo) =>
            {
                if (this.OnOfferwallLoaded != null)
                {
                    this.OnOfferwallLoaded(adunit, adInfo);
                }
            };

            TPOfferwall.Instance().OnOfferwallLoadFailed += (adunit, error) =>
            {
                if (this.OnOfferwallLoadFailed != null)
                {
                    this.OnOfferwallLoadFailed(adunit, error);
                }
            };

            TPOfferwall.Instance().OnOfferwallImpression += (adunit, adInfo) =>
            {
                if (this.OnOfferwallImpression != null)
                {
                    this.OnOfferwallImpression(adunit, adInfo);
                }
            };

            TPOfferwall.Instance().OnOfferwallShowFailed += (adunit, adInfo,error) =>
            {
                if (this.OnOfferwallShowFailed != null)
                {
                    this.OnOfferwallShowFailed(adunit, adInfo, error);
                }
            };

            TPOfferwall.Instance().OnOfferwallClicked += (adunit, adInfo) =>
            {
                if (this.OnOfferwallClicked != null)
                {
                    this.OnOfferwallClicked(adunit, adInfo);
                }
            };

            TPOfferwall.Instance().OnOfferwallClosed += (adunit, adInfo) =>
            {
                if (this.OnOfferwallClosed != null)
                {
                    this.OnOfferwallClosed(adunit, adInfo);
                }
            };

            TPOfferwall.Instance().OnOfferwallStartLoad += (adunit, adInfo) =>
            {
                if (this.OnOfferwallStartLoad != null)
                {
                    this.OnOfferwallStartLoad(adunit, adInfo);
                }
            };

            TPOfferwall.Instance().OnOfferwallOneLayerStartLoad += (adunit, adInfo) =>
            {
                if (this.OnOfferwallOneLayerStartLoad != null)
                {
                    this.OnOfferwallOneLayerStartLoad(adunit, adInfo);
                }
            };

            TPOfferwall.Instance().OnOfferwallOneLayerLoaded += (adunit, adInfo) =>
            {
                if (this.OnOfferwallOneLayerLoaded != null)
                {
                    this.OnOfferwallOneLayerLoaded(adunit, adInfo);
                }
            };


            TPOfferwall.Instance().OnOfferwallOneLayerLoadFailed += (adunit, adInfo, error) =>
            {
                if (this.OnOfferwallOneLayerLoadFailed != null)
                {
                    this.OnOfferwallOneLayerLoadFailed(adunit, adInfo, error);
                }
            };
            
            TPOfferwall.Instance().OnOfferwallAllLoaded += (adunit, isSuccess) =>
            {
                if (this.OnOfferwallAllLoaded != null)
                {
                    this.OnOfferwallAllLoaded(adunit, isSuccess);
                }
            };

            TPOfferwall.Instance().OnOfferwallIsLoading += (adunit) =>
            {
                if (this.OnOfferwallIsLoading != null)
                {
                    this.OnOfferwallIsLoading(adunit);
                }
            };

            TPOfferwall.Instance().OnOfferwallSetUserIdFinish += (adunit, isSuccess) =>
            {
                if (this.OnOfferwallSetUserIdFinish != null)
                {
                    this.OnOfferwallSetUserIdFinish(adunit, isSuccess);
                }
            };


            TPOfferwall.Instance().OnCurrencyBalanceSuccess += (adunit, amount, msg) =>
            {
                if (this.OnCurrencyBalanceSuccess != null)
                {
                    this.OnCurrencyBalanceSuccess(adunit, amount, msg);
                }
            };

            TPOfferwall.Instance().OnCurrencyBalanceFailed += (adunit, msg) =>
            {
                if (this.OnCurrencyBalanceFailed != null)
                {
                    this.OnCurrencyBalanceFailed(adunit, msg);
                }
            };

            TPOfferwall.Instance().OnSpendCurrencySuccess += (adunit, amount, msg) =>
            {
                if (this.OnSpendCurrencySuccess != null)
                {
                    this.OnSpendCurrencySuccess(adunit, amount, msg);
                }
            };

            TPOfferwall.Instance().OnSpendCurrencyFailed += (adunit, msg) =>
            {
                if (this.OnSpendCurrencyFailed != null)
                {
                    this.OnSpendCurrencyFailed(adunit, msg);
                }
            };

            TPOfferwall.Instance().OnAwardCurrencySuccess += (adunit, amount, msg) =>
            {
                if (this.OnAwardCurrencySuccess != null)
                {
                    this.OnAwardCurrencySuccess(adunit, amount, msg);
                }
            };

            TPOfferwall.Instance().OnAwardCurrencyFailed += (adunit, msg) =>
            {
                if (this.OnAwardCurrencyFailed != null)
                {
                    this.OnAwardCurrencyFailed(adunit, msg);
                }
            };

        }
    }
}

