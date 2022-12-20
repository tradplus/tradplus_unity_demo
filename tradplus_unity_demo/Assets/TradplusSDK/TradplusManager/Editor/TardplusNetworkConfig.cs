using System;

namespace Tardplus.TradplusEditorManager.Editor
{
    [Serializable]
    public class TardplusNetworkConfig
    {
        public int code;
        public string msg;
        public NetworkData data;
    }

    [Serializable]
    public class NetworkData
    {
        public string cocoapodCode;
        public string fileUrl;
        public string gradleCode;
        public string manifestCode;
        public string obfuscatedCode;
    }
}