using UnityEngine;
using System.Collections;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace Tardplus.TradplusEditorManager.Editor {

    public class PreAdmobProcessBuildAndroid :
#if UNITY_2018_1_OR_NEWER
        IPreprocessBuildWithReport
#else
        IPreprocessBuild
#endif
    {
#if UNITY_2018_1_OR_NEWER
        public void OnPreprocessBuild(BuildReport report)
#else
        public void OnPreprocessBuild(BuildTarget target, string path)
#endif
        {
            var appId = TradplusEditorManager.Instance().AndroidAdmobAppID;

            if (string.IsNullOrEmpty(appId)) return;
          
            var manifestPath = Path.Combine(Application.dataPath, "Plugins/Android/n2.androidlib/AndroidManifest.xml");
            XDocument manifest;

            try
            {
                manifest = XDocument.Load(manifestPath);
            }
            catch (IOException)
            {
                return;
            }

            var elementManifest = manifest.Element("manifest");

            if (elementManifest == null) return;

            var elementApplication = elementManifest.Element("application");

            if (elementApplication == null) return;


            var adMobMetaData = elementApplication.Descendants().Last(element => element.Name.LocalName.Equals("meta-data"));
            XNamespace androidNamespace = "http://schemas.android.com/apk/res/android";

            if (!adMobMetaData.FirstAttribute.Name.Namespace.Equals(androidNamespace) ||
                !adMobMetaData.FirstAttribute.Name.LocalName.Equals("name") ||
                !adMobMetaData.FirstAttribute.Value.Equals("com.google.android.gms.ads.APPLICATION_ID"))
            {
                return;
            }

            var lastAttribute = adMobMetaData.LastAttribute;
            if (!lastAttribute.Name.LocalName.Equals("value"))
            {
                return;
            }

            lastAttribute.Value = appId;
            manifest.Save(manifestPath);

        }

        public int callbackOrder
        {
            get { return 0; }
        }
    }

}