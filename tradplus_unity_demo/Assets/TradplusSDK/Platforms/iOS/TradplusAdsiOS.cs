#if UNITY_IOS

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using TradplusSDK.ThirdParty.MiniJSON;

namespace TradplusSDK.iOS
{
    public class TradplusAdsiOS
    {
        private static TradplusAdsiOS _instance;

        public static TradplusAdsiOS Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusAdsiOS();
            }
            return _instance;
        }

        [DllImport("__Internal")]
        private static extern void TradplusCheckCurrentArea();
        public void CheckCurrentArea()
        {
            TradplusCheckCurrentArea();
        }

        [DllImport("__Internal")]
        private static extern void TradplusInitSDK(string appId);
        public void InitSDK(string appId)
        {
            TradplusInitSDK(appId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCustomMap(string customMap);
        public void SetCustomMap(Dictionary<string, string> customMap)
        {
            string customMapString = null;
            if (customMap != null)
            {
                customMapString = Json.Serialize(customMap);
            }
            TradplusSetCustomMap(customMapString);
        }

        [DllImport("__Internal")]
        private static extern string TradplusVersion();
        public string Version()
        {
            return TradplusVersion();
        }

        [DllImport("__Internal")]
        private static extern bool TradplusIsEUTraffic();
        public bool IsEUTraffic()
        {
            return TradplusIsEUTraffic();
        }

        [DllImport("__Internal")]
        private static extern bool TradplusIsCalifornia();
        public bool IsCalifornia()
        {
            return TradplusIsCalifornia();
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetGDPRDataCollection(bool canDataCollection);
        public void SetGDPRDataCollection(bool canDataCollection)
        {
            TradplusSetGDPRDataCollection(canDataCollection);
        }

        [DllImport("__Internal")]
        private static extern int TradplusGetGDPRDataCollection();
        public int GetGDPRDataCollection()
        {
            return TradplusGetGDPRDataCollection();
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCCPADoNotSell(bool canDataCollection);
        public void SetCCPADoNotSell(bool canDataCollection)
        {
            TradplusSetCCPADoNotSell(canDataCollection);
        }

        [DllImport("__Internal")]
        private static extern int TradplusGetCCPADoNotSell();
        public int GetCCPADoNotSell()
        {
            return TradplusGetCCPADoNotSell();
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCOPPAIsAgeRestrictedUser(bool isChild);
        public void SetCOPPAIsAgeRestrictedUser(bool isChild)
        {
            TradplusSetCOPPAIsAgeRestrictedUser(isChild);
        }

        [DllImport("__Internal")]
        private static extern int TradplusGetCOPPAIsAgeRestrictedUser();
        public int GetCOPPAIsAgeRestrictedUser()
        {
            return TradplusGetCOPPAIsAgeRestrictedUser();
        }

        [DllImport("__Internal")]
        private static extern void TradplusShowGDPRDialog();
        public void ShowGDPRDialog()
        {
            TradplusShowGDPRDialog();
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetOpenPersonalizedAd(bool open);
        public void SetOpenPersonalizedAd(bool open)
        {
            TradplusSetOpenPersonalizedAd(open);
        }

        [DllImport("__Internal")]
        private static extern bool TradplusIsOpenPersonalizedAd();
        public bool IsOpenPersonalizedAd()
        {
            return TradplusIsOpenPersonalizedAd();
        }

        [DllImport("__Internal")]
        private static extern void TradplusClearCache(string adUnitId);
        public void ClearCache(string adUnitId)
        {
            TradplusClearCache(adUnitId);
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetAutoExpiration(bool autoCheck);
        public void SetAutoExpiration(bool autoCheck)
        {
            TradplusSetAutoExpiration(autoCheck);
        }

        [DllImport("__Internal")]
        private static extern void TradplusCheckAutoExpiration();
        public void CheckAutoExpiration()
        {
            TradplusCheckAutoExpiration();
        }

        [DllImport("__Internal")]
        private static extern void TradplusSetCnServer(bool onlyCn);
        public void SetCnServer(bool onlyCn)
        {
            TradplusSetCnServer(onlyCn);
        }

        //注册回调
        [DllImport("__Internal")]
        private static extern void TradplusSDKSetCallbacks(
            TPOnInitFinishCallback onInitFinishCallback,
            TPOnDialogClosedCallback onDialogClosedCallback,
            TPOnCurrentAreaSuccessCallback onCurrentAreaSuccessCallback,
            TPOnCurrentAreaFailedCallback onCurrentAreaFailedCallback
        );
        public TradplusAdsiOS()
        {
            TradplusSDKSetCallbacks(
                OnInitFinishCallback,
                OnDialogClosedCallback,
                OnCurrentAreaSuccessCallback,
                OnCurrentAreaFailedCallback
                );
        }

        //OnInitFinish
        public event Action<bool> OnInitFinish;

        internal delegate void TPOnInitFinishCallback(bool success);
        [MonoPInvokeCallback(typeof(TPOnInitFinishCallback))]
        private static void OnInitFinishCallback(bool success)
        {
            if (TradplusAdsiOS.Instance().OnInitFinish != null)
            {
                TradplusAdsiOS.Instance().OnInitFinish(success);
            }
        }

        //OnDialogClosed
        public event Action<int> OnDialogClosed;

        internal delegate void TPOnDialogClosedCallback(int level);
        [MonoPInvokeCallback(typeof(TPOnDialogClosedCallback))]
        private static void OnDialogClosedCallback(int level)
        {
            if (TradplusAdsiOS.Instance().OnDialogClosed != null)
            {
                TradplusAdsiOS.Instance().OnDialogClosed(level);
            }
        }

        //OnCurrentAreaSuccess
        public event Action<bool, bool, bool> OnCurrentAreaSuccess;

        internal delegate void TPOnCurrentAreaSuccessCallback(bool isEu, bool isCn, bool isCa);
        [MonoPInvokeCallback(typeof(TPOnCurrentAreaSuccessCallback))]
        private static void OnCurrentAreaSuccessCallback(bool isEu, bool isCn, bool isCa)
        {
            if (TradplusAdsiOS.Instance().OnCurrentAreaSuccess != null)
            {
                TradplusAdsiOS.Instance().OnCurrentAreaSuccess(isEu,isCn,isCa);
            }
        }

        //OnCurrentAreaFailed
        public event Action<string> OnCurrentAreaFailed;

        internal delegate void TPOnCurrentAreaFailedCallback(string msg);
        [MonoPInvokeCallback(typeof(TPOnCurrentAreaFailedCallback))]
        private static void OnCurrentAreaFailedCallback(string msg)
        {
            if (TradplusAdsiOS.Instance().OnCurrentAreaFailed != null)
            {
                TradplusAdsiOS.Instance().OnCurrentAreaFailed(msg);
            }
        }
    }
}

#endif