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

        [MenuItem("TradPlus/Documentation")]
        private static void Documentation()
        {
            Application.OpenURL("https://docs.tradplusad.com/docs/doc_u3d/u3d_integration/setting_plugin");
        }

    }
}
