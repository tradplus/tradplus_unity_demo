using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Tardplus.TradplusEditorManager.Editor
{
    [Serializable]
    public class TardplusSaveIOSPodInfo
    {
        public string nameEn;
        public string podStr;
    }

    [Serializable]
    public class TardplusSaveNetworkInfo
    {
        public int os;
        public string sdkVersion;
        public string nameEn;
        public string version;
        public string uniqueNetworkId;
    }

    [Serializable]
    public class TardplusNetworkDesc
    {
        public string ios_networkId;
        public string android_networkId;
        public string android_version;
        public string ios_version;
        public string android_install_version;
        public string ios_install_version;
        public string android_install_sdk_version;
        public string ios_install_sdk_version;
        public bool ios_hasInstall;
        public bool ios_update;
        public bool android_hasInstall;
        public bool android_update;
        public string nameCn;
        public string nameEn;
        public bool has_iOS;
        public bool has_Android;
        public string uniqueNetworkId;
        public int region;

        public TardplusNetworkDesc()
        {
            android_install_version = "Not Installed";
            ios_install_version = "Not Installed";
        }
    }

    [Serializable]
    public class TardplusSKAdNetworkInfo
    {
        public string platform;
        public string url;
        public string last_check;
        public string uid;
        public List<string> skadnetwork_ids;
    }

    public class TardplusNetworkInfo
    {
        public List<TardplusNetworkDesc> networkList = new List<TardplusNetworkDesc>();
        //os: 0=android; 1=iOS;
        public void AddNetwork(Network network, int os)
        {
            TardplusNetworkDesc desc = null;
            foreach (TardplusNetworkDesc item in networkList)
            {
                string itemUniqueNetworkId = item.uniqueNetworkId.ToLower().Replace(" ","");
                string networkUniqueNetworkId = network.uniqueNetworkId.ToLower().Replace(" ", "");
                if (Equals(itemUniqueNetworkId, networkUniqueNetworkId))
                {
                    desc = item;
                    break;
                }
            }

            if (desc == null)
            {
                desc = new TardplusNetworkDesc();
                //MTG国内特殊处理
                if (Equals(network.uniqueNetworkId, "c18"))
                {
                    desc.nameEn = network.nameEn + "-CN 国内";
                }
                else if (Equals(network.uniqueNetworkId, "c27"))
                {
                    desc.nameEn = "Cross-CN 国内";
                }
                else if (Equals(network.uniqueNetworkId, "c41"))
                {
                    desc.nameEn = network.nameEn + "-CN 国内";
                }
                else
                {
                    desc.nameEn = network.nameEn;
                }
                desc.nameCn = network.nameCn;
                desc.region = network.region;
                desc.uniqueNetworkId = network.uniqueNetworkId;
                networkList.Add(desc);
            }

            if(os == 1)
            {
                desc.android_networkId = network.networkId;
                desc.android_version = "Android - " + network.version;
                desc.has_Android = true;
            }
            else
            {
                desc.ios_networkId = network.networkId;
                desc.ios_version = "iOS - "+ network.version;
                desc.has_iOS = true;
            }
        }

        public void CheckHisInfo(TardplusSaveNetworkInfo saveInfo)
        {
            foreach (TardplusNetworkDesc desc in networkList)
            {
                string itemUniqueNetworkId = desc.uniqueNetworkId.ToLower().Replace(" ", "");
                string networkUniqueNetworkId = saveInfo.uniqueNetworkId.ToLower().Replace(" ", "");
                if (Equals(itemUniqueNetworkId, networkUniqueNetworkId))
                {
                    //移除ADX
                    if (Equals(itemUniqueNetworkId, "n40"))
                    {
                        TradplusEditorManager.Instance().removeNetwork(networkUniqueNetworkId, saveInfo.os, desc);
                        return;
                    }
                    if (saveInfo.os == 1)
                    {
                        desc.android_update = false;
                        desc.has_Android = true;
                        desc.android_hasInstall = true;
                        desc.android_install_version = saveInfo.version;
                        desc.android_install_sdk_version = saveInfo.sdkVersion;
                        if (!Equals(desc.android_version,desc.android_install_version))
                        {
                            desc.android_update = true;
                        }
                        if (!Equals(saveInfo.sdkVersion, TradplusEditorManager.Instance().configInfo.Android.sdkVersion))
                        {
                            desc.android_update = true;
                        }
                    }
                    else
                    {
                        desc.ios_update = false;
                        desc.has_iOS = true;
                        desc.ios_hasInstall = true;
                        desc.ios_install_version = saveInfo.version;
                        desc.ios_install_sdk_version = saveInfo.sdkVersion;
                        if (!Equals(desc.ios_version, desc.ios_install_version))
                        {
                            desc.ios_update = true;
                        }
                        if (!Equals(saveInfo.sdkVersion, TradplusEditorManager.Instance().configInfo.iOS.sdkVersion))
                        {
                            desc.ios_update = true;
                        }
                    }
                    return;
                }
            }
        }

        //更新配置
        public void UpdateHisNetWorkInfo()
        {
            foreach (TardplusNetworkDesc desc in networkList)
            {
                if(desc.ios_hasInstall)
                {
                    string sdkId = TradplusEditorManager.Instance().configInfo.iOS.sdkId;
                    TradplusEditorManager.Instance().GetNetworkConfig(sdkId, desc.ios_networkId, 2, desc,false);
                }
                if (desc.android_hasInstall)
                {
                    string sdkId = TradplusEditorManager.Instance().configInfo.Android.sdkId;
                    TradplusEditorManager.Instance().GetNetworkConfig(sdkId, desc.android_networkId, 1, desc, false);
                }
            }
            AssetDatabase.Refresh();
        }


    }
}
