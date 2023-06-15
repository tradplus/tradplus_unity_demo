#if UNITY_IOS

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Tardplus.TradplusEditorManager.Editor
{
    public class TradplusIOSSetting : MonoBehaviour
    {
        [PostProcessBuildAttribute(45)]
        private static void PostProcessBuild_iOS(BuildTarget target, string buildPath)
        {
            if (target == BuildTarget.iOS)
            {
                string replaceString = "";
                string[] paths = AssetDatabase.FindAssets("TPPodInfo");
                foreach (string item in paths)
                {
                    string path = AssetDatabase.GUIDToAssetPath(item);
                    if (path.EndsWith("TPPodInfo.json"))
                    {
                        string json = File.ReadAllText(path);
                        Debug.Log("json " + json);
                        TardplusSaveIOSPodInfo saveInfo = JsonUtility.FromJson<TardplusSaveIOSPodInfo>(json);
                        replaceString += saveInfo.podStr + "\n";
                    }
                }

                if(replaceString.Length > 0)
                {
                    replaceString = "target 'Unity-iPhone' do \n" + replaceString;
                    string path = buildPath + "/Podfile";
                    string Podfile = File.ReadAllText(path);
                    //Podfile = Podfile.Replace("target 'Unity-iPhone' do", replaceString);
                    string keyStr = "target 'Unity-iPhone' do";
                    if (Podfile.Contains(keyStr))
                    {
                        Podfile = Podfile.Replace(keyStr, replaceString);
                    }
                    else
                    {
                        Podfile += replaceString + "end";
                    }
                    File.WriteAllText(path,Podfile);
                }
            }

            AddSetting(buildPath);
            if (TradplusEditorManager.Instance().closeBitCode)
            {
                CloseBitCode(buildPath);
            }
        }

        private static void AddSetting(string buildPath)
        {

            string infoPlistPath = Path.Combine(buildPath, "./Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(infoPlistPath));
            PlistElementDict rootDict = plist.root;

            if (TradplusEditorManager.Instance().openHttp)
            {
                PlistElementDict transportSecurity = rootDict.CreateDict("NSAppTransportSecurity");
                transportSecurity.SetBoolean("NSAllowsArbitraryLoads", true);
            }

            if (TradplusEditorManager.Instance().iOSAdmobAppID.Length > 0)
            {
                rootDict.SetString("GADApplicationIdentifier", TradplusEditorManager.Instance().iOSAdmobAppID);
            }

            if (TradplusEditorManager.Instance().IDFAInfo.Length > 0)
            {
                rootDict.SetString("NSUserTrackingUsageDescription", TradplusEditorManager.Instance().IDFAInfo);
            }

            if (TradplusEditorManager.Instance().SKAdNetworkList)
            {
                Dictionary<string, string> list = GetSKAdNetworkList();
                if(list != null)
                {
                    PlistElementArray SKAdNetworkItems = rootDict.CreateArray("SKAdNetworkItems");
                    foreach (string key in list.Keys)
                    {
                        PlistElementDict item = SKAdNetworkItems.AddDict();
                        item.SetString("SKAdNetworkIdentifier", key);
                    }
                }
            }

            File.WriteAllText(infoPlistPath, plist.WriteToString());
        }

        private static void CloseBitCode(string buildPath)
        {
            string projectPath = Path.Combine(buildPath, "./Unity-iPhone.xcodeproj/project.pbxproj");

            PBXProject pbxProject = new PBXProject();
            pbxProject.ReadFromFile(projectPath);

            string target = pbxProject.GetUnityMainTargetGuid();
            pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            target = pbxProject.GetUnityFrameworkTargetGuid();
            pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            pbxProject.WriteToFile(projectPath);
        }

        private static Dictionary<string, string> GetSKAdNetworkList()
        {
            string[] paths = AssetDatabase.FindAssets("TPNetWorkInfo");
            Dictionary<string, string> list = new Dictionary<string, string>();
            foreach (string item in paths)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
                if (path.EndsWith("TPNetWorkInfo.json"))
                {
                    string json = File.ReadAllText(path);
                    Debug.Log("path" + path + "json " + json);
                    TardplusSaveNetworkInfo saveInfo = JsonUtility.FromJson<TardplusSaveNetworkInfo>(json);
                    string[] filePaths = AssetDatabase.FindAssets(saveInfo.uniqueNetworkId + "_SKAdNetwork_List");
                    if (filePaths.Length > 0)
                    {
                        string filePath = AssetDatabase.GUIDToAssetPath(filePaths[0]);
                        string fileJson = File.ReadAllText(filePath);
                        TardplusSKAdNetworkInfo SKAdNetworkInfo = JsonUtility.FromJson<TardplusSKAdNetworkInfo>(fileJson);
                        foreach (string skadnetwork in SKAdNetworkInfo.skadnetwork_ids)
                        {
                            list[skadnetwork] = "";
                        }
                    }
                }
            }
            if (list.Keys.Count > 0)
            {
                //string saveString = "<key>SKAdNetworkItems</key>\n<array>\n";
                //foreach (string skadnetwork in list.Keys)
                //{
                //    saveString += "<dict>\n<key>SKAdNetworkIdentifier</key>\n<string>" + skadnetwork + "</string>\n</dict>\n";
                //}
                //saveString += "</array>";
                //Debug.Log("saveString " + saveString);
                return list;
            }
            return null;
        }

        //pod install 后对特殊源进行配置修改
        [PostProcessBuildAttribute(55)]
        private static void PostConfigureProcessBuild_iOS(BuildTarget target, string buildPath)
        {
            string debug = buildPath + "/Pods/Target Support Files/Pods-Unity-iPhone/Pods-Unity-iPhone.debug.xcconfig";
            string release = buildPath + "/Pods/Target Support Files/Pods-Unity-iPhone/Pods-Unity-iPhone.release.xcconfig";
            string releaseforprofiling = buildPath + "/Pods/Target Support Files/Pods-Unity-iPhone/Pods-Unity-iPhone.releaseforprofiling.xcconfig";
            string releaseforrunning = buildPath + "/Pods/Target Support Files/Pods-Unity-iPhone/Pods-Unity-iPhone.releaseforrunning.xcconfig";
            string[] pathArray = new string[] { debug, release, releaseforprofiling, releaseforrunning };

            //KSAdSDK 快手 动态库无法直接添加在UnityFramework v3.3.27
            string KSAdSDKPath = buildPath + "/Pods/Target Support Files/KSAdSDK/";
            DirectoryInfo pathDir = new DirectoryInfo(KSAdSDKPath);
            //确认pod是否有快手
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                "\"${PODS_ROOT}/KSAdSDK\"",
                "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/KSAdSDK\""};

                string[] frameworkArray = new string[] {
                "-framework \"KSAdSDK\""};
                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray);
            }

            //百度 其资源包需要添加在主工程目录下
            string BuaiduPath = buildPath + "/Pods/Target Support Files/BaiduMobAdSDK/";
            pathDir = new DirectoryInfo(BuaiduPath);
            //确认pod是否有百度
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                "\"${PODS_ROOT}/BaiduMobAdSDK\""};

                string[] frameworkArray = new string[] {
                "-framework \"BaiduMobAdSDK\""};

                string[] sysFrameworkArray = new string[] {
                "CoreLocation.framework"};
                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray, sysFrameworkArray);
            }

            //smaato
            string SmattoPath = buildPath + "/Pods/Target Support Files/smaato-ios-sdk/";
            pathDir = new DirectoryInfo(SmattoPath);
            //确认pod是否有smaato
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                    "\"${PODS_ROOT}/smaato-ios-sdk\"",
                    "\"${PODS_ROOT}/smaato-ios-sdk/vendor\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/Banner\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/Core\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/Interstitial\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/Native\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/OpenMeasurement\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/Outstream\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/RewardedAds\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/RichMedia\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/smaato-ios-sdk/Modules/Video\"",
                };

                string[] frameworkArray = new string[] {
                    "-framework \"OMSDK_Smaato\"",
                    "-framework \"SmaatoSDKBanner\"",
                    "-framework \"SmaatoSDKCore\"",
                    "-framework \"SmaatoSDKInterstitial\"",
                    "-framework \"SmaatoSDKNative\"",
                    "-framework \"SmaatoSDKOpenMeasurement\"",
                    "-framework \"SmaatoSDKOutstream\"",
                    "-framework \"SmaatoSDKRewardedAds\"",
                    "-framework \"SmaatoSDKRichMedia\"",
                    "-framework \"SmaatoSDKVideo\"",
                };

                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray);
            }

            //Verve v2.14.0
            string VerveSDKPath = buildPath + "/Pods/Target Support Files/HyBid/";
            pathDir = new DirectoryInfo(VerveSDKPath);
            //确认pod是否有Verve
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                    "\"${PODS_CONFIGURATION_BUILD_DIR}/HyBid\"",
                    "\"${PODS_ROOT}/HyBid/PubnativeLite/PubnativeLite/OMSDK-1.3.29\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/HyBid/Core\""
                };

                string[] frameworkArray = new string[] {
                    "-framework \"HyBid\"",
                    "-framework \"OMSDK_Pubnativenet\"",
                };
                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray);
            }

            //Ogury v2.1.0
            string OgurySDKPath = buildPath + "/Pods/Target Support Files/OgurySdk/";
            pathDir = new DirectoryInfo(OgurySDKPath);
            //确认pod是否有Ogury
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                    "\"${PODS_ROOT}/OguryAds\"",
                    "\"${PODS_ROOT}/OguryChoiceManager\"",
                    "\"${PODS_ROOT}/OguryCore\"",
                    "\"${PODS_ROOT}/OgurySdk\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/OguryAds\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/OguryAds/OMID\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/OguryChoiceManager\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/OguryCore\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/OgurySdk\"",
                };

                string[] frameworkArray = new string[] {
                    "-framework \"OMSDK_Ogury\"",
                    "-framework \"OguryAds\"",
                    "-framework \"OguryChoiceManager\"",
                    "-framework \"OguryCore\"",
                    "-framework \"OgurySdk\"",
                };
                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray);
            }

            //Bigo
            string BigoSDKPath = buildPath + "/Pods/Target Support Files/BigoADS/";
            pathDir = new DirectoryInfo(BigoSDKPath);
            //确认pod是否有Bigo
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                    "\"${PODS_ROOT}/BigoADS/BigoADS\"",
                    "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/BigoADS\"",
                };

                string[] frameworkArray = new string[] {
                    "-framework \"BigoADS\"",
                    "-framework \"OMSDK_Bigosg\"",
                };
                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray);
            }

            //Fyber v8.2.0
            string FyberSDKPath = buildPath + "/Pods/Target Support Files/Fyber_Marketplace_SDK/";
            pathDir = new DirectoryInfo(FyberSDKPath);
            //确认pod是否有Fyber
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                "\"${PODS_ROOT}/Fyber_Marketplace_SDK/IASDKCore\"",
                "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/Fyber_Marketplace_SDK\""};

                string[] frameworkArray = new string[] {
                "-framework \"IASDKCore\""};
                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray);
            }

            //Start.io v4.9.1
            string StartIOSDKPath = buildPath + "/Pods/Target Support Files/StartAppSDK/";
            pathDir = new DirectoryInfo(StartIOSDKPath);
            //确认pod是否有Start.io
            if (pathDir.Exists)
            {
                string[] sdkPathArray = new string[] {
                "\"${PODS_ROOT}/StartAppSDK\"",
                "\"${PODS_XCFRAMEWORKS_BUILD_DIR}/StartAppSDK\"",
            };

                string[] frameworkArray = new string[] {
                "-framework \"StartApp\""
            };
                removeSetting(pathArray, sdkPathArray, frameworkArray);
                AddFrameworkPath(buildPath, sdkPathArray, frameworkArray);
            }
        }



        private static void AddFrameworkPath(string buildPath, string[] sdkPathArray, string[] frameworkArray = null, string[] sysFrameworkArray = null)
        {
            PBXProject pbxProject = new PBXProject();
            string projectPath = Path.Combine(buildPath, "./Unity-iPhone.xcodeproj/project.pbxproj");
            pbxProject.ReadFromFile(projectPath);
            string target = pbxProject.GetUnityFrameworkTargetGuid();

            foreach (string sdkPath in sdkPathArray)
            {
                pbxProject.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", sdkPath);
            }
            if (frameworkArray != null)
            {
                foreach (string sdkPath in frameworkArray)
                {
                    pbxProject.AddBuildProperty(target, "OTHER_LDFLAGS", sdkPath);
                }
            }
            if (sysFrameworkArray != null)
            {
                foreach (string sysFramework in sysFrameworkArray)
                {
                    pbxProject.AddFrameworkToProject(target, sysFramework, false);
                }
            }
            pbxProject.WriteToFile(projectPath);
        }

        private static void removeSetting(string[] pathArray, string[] sdkPathArray, string[] frameworkArray)
        {
            foreach (string fliePath in pathArray)
            {
                if (System.IO.File.Exists(fliePath))
                {
                    string fileData = File.ReadAllText(fliePath);
                    foreach (string sdkPath in sdkPathArray)
                    {
                        fileData = fileData.Replace(sdkPath, "");
                    }
                    foreach (string framework in frameworkArray)
                    {
                        fileData = fileData.Replace(framework, "");
                    }
                    File.WriteAllText(fliePath, fileData);
                }
            }
        }
    }
}

#endif