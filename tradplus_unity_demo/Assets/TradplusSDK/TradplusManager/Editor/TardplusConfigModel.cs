using System;

namespace Tardplus.TradplusEditorManager.Editor
{
    [Serializable]
    public class TardplusConfigModel
    {
        public TardplusConfigiOS iOS;
        public TardplusConfigAndroid Android;
    }

    [Serializable]
    public class TardplusConfigiOS
    {
        public string os;
        public string sdkId;
        public string sdkVersion;
    }

    [Serializable]
    public class TardplusConfigAndroid
    {
        public string os;
        public string sdkId;
        public string sdkVersion;
    }
}

