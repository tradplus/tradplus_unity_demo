using UnityEditor;
using UnityEngine;

namespace Tardplus.TradplusEditorManager.Editor
{
    public class TardplusMenuItems
    {
        [MenuItem("TradPlus/TradPlusManger")]
        private static void TradplusMangerWindow()
        {
            TradplusEditorManagerWindow.ShowManager();
        }

        [MenuItem("TradPlus/Documentation/Access Document")]
        private static void Documentation()
        {
            Application.OpenURL("https://docs.tradplusad.com/docs/doc_u3d/u3d_integration/setting_plugin");
        }

        [MenuItem("TradPlus/Documentation/DownLoad Plugin")]
        private static void PluginDownLoad()
        {
            Application.OpenURL("https://docs.tradplusad.com/docs/doc_u3d/u3d_download");
        }

        [MenuItem("TradPlus/Documentation/iOS ChangeLog")]
        private static void iOSChangeLog()
        {
            Application.OpenURL("https://docs.tradplusad.com/docs/doc_u3d/change_log/unity_ios_changelog");
        }

        [MenuItem("TradPlus/Documentation/Android ChangeLog")]
        private static void AndroidChangeLog()
        {
            Application.OpenURL("https://docs.tradplusad.com/docs/doc_u3d/change_log/unity_android_changelog");
        }
    }
}
