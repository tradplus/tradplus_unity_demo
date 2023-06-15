using System;

namespace Tardplus.TradplusEditorManager.Editor
{
    [Serializable]
    public class TPVersion
    {
        public string sdkId;
        public string version;
        public int isAndroidX;
        public String extra_info;
    }

    [Serializable]
    public class TPSDKVersionData
    {
        public TPVersion[] androidVersions;
        public TPVersion[] iosVersions;
    }

    [Serializable]
    public class TardplusSDKVersionInfo
    {
        public int code;
        public string msg;
        public TPSDKVersionData data;
    }

}
