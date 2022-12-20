
#if UNITY_2018_2_OR_NEWER && UNITY_ANDROID

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor.Android;

namespace Tardplus.TradplusEditorManager.Editor {
    public class PostProcessBuildAndroid : IPostGenerateGradleAndroidProject
    {
#if UNITY_2019_3_OR_NEWER
        private const string PropertyAndroidX = "android.useAndroidX";
        private const string PropertyJetifier = "android.enableJetifier";
        private const string EnableProperty = "=true";
#endif
        private const string PropertyDexingArtifactTransform = "android.enableDexingArtifactTransform";
        private const string DisableProperty = "=false";
                
        public void OnPostGenerateGradleAndroidProject(string path)
        {
#if UNITY_2019_3_OR_NEWER
            var gradlePropertiesPath = Path.Combine(path, "../gradle.properties");
#else
            var gradlePropertiesPath = Path.Combine(path, "gradle.properties");
#endif
            var gradlePropertiesUpdated = new List<string>();
            if (File.Exists(gradlePropertiesPath)) {
                var lines = File.ReadAllLines(gradlePropertiesPath);
#if UNITY_2019_3_OR_NEWER
                gradlePropertiesUpdated.AddRange(lines.Where(line => !line.Contains(PropertyAndroidX) &&
                !line.Contains(PropertyJetifier) && !line.Contains(PropertyDexingArtifactTransform)));
#else
                gradlePropertiesUpdated.AddRange(lines.Where(line => !line.Contains(PropertyDexingArtifactTransform)));
#endif
            }
#if UNITY_2019_3_OR_NEWER
            gradlePropertiesUpdated.Add(PropertyAndroidX + EnableProperty);
            gradlePropertiesUpdated.Add(PropertyJetifier + EnableProperty);
#endif
            gradlePropertiesUpdated.Add(PropertyDexingArtifactTransform + DisableProperty);
            try
            {
                File.WriteAllText(gradlePropertiesPath, string.Join("\n", gradlePropertiesUpdated.ToArray()) + "\n");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            var manifestPath = Path.Combine(path, "src/main/AndroidManifest.xml");
            XDocument manifest;
            manifest = XDocument.Load(manifestPath);
            var elementManifest = manifest.Element("manifest");
            if (elementManifest == null)
            {
                return;
            }

            var elementApplication = elementManifest.Element("application");
            if (elementApplication == null)
            {
                return;
            }

           
            //<uses-library android:name="org.apache.http.legacy" android:required="false" />
            var descendants = elementApplication.Descendants();

            var metaData = new XElement("uses-library");
            XNamespace androidNamespace = "http://schemas.android.com/apk/res/android";
            //elementApplication.Add(new XAttribute(androidNamespace + "name", "androidx.multidex.MultiDexApplication"));
            metaData.Add(new XAttribute(androidNamespace + "name", "org.apache.http.legacy"));
            metaData.Add(new XAttribute(androidNamespace + "required", false));
            elementApplication.Add(metaData);
            manifest.Save(manifestPath);
        }


        public int callbackOrder
        {
            get { return int.MaxValue; }
        }

    }
}

#endif