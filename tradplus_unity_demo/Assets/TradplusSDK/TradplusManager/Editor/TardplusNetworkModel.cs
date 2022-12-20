using System;

namespace Tardplus.TradplusEditorManager.Editor
{
    [Serializable]
    public class TardplusNetworkModel
    {
        public int code;
        public string msg;
        public NetworkInfoData data;
    }

    [Serializable]
    public class NetworkInfoData
    {
        public Network[] networks;
    }

    [Serializable]
    public class Network
    {
        public string networkId;
        public string version;
        public string nameCn;
        public string nameEn;
        public string adTypes;
        public string isHeaderBidding;
        public string uniqueNetworkId;
        public int region;
    }

}