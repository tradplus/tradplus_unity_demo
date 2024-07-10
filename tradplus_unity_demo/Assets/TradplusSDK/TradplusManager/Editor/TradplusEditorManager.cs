
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System;
using TradplusSDK.ThirdParty.MiniJSON;
using TradplusSDK.Api;

namespace Tardplus.TradplusEditorManager.Editor
{
    public class TradplusEditorManager
    {
        private static TradplusEditorManager _instance;

        public static string[] specialPodArray = { "KSAdSDK", "OgurySdk", "HyBid", "smaato-ios-sdk", "BaiduMobAdSDK", "MaioSDK-v2", "AmazonPublisherServicesSDK", "TradPlusKwaiAdsSDK" };

        public static TradplusEditorManager Instance()
        {
            if (_instance == null)
            {
                _instance = new TradplusEditorManager();
            }
            return _instance;
        }

        public TardplusConfigModel configInfo;

        private string savePath = Application.dataPath + "/TradplusSDK/Networks/";
        private string mainfestPath = Application.dataPath + "/Plugins/Android/";
        private string configPath;

        private TardplusNetworkModel iOS_NetworkInfo;
        private TardplusNetworkModel Android_NetworkInfo;
        public TardplusSDKVersionInfo versionInfo;

        public List<string> iOSVersionNameList;
        public List<int> iOSVersionIndexList;
        public bool iOSNoVersionError;
        public bool iOSUseVersionOff;

        public List<string> AndroidVersionNameList;
        public List<int> AndroidVersionIndexList;
        public bool AndroidNoVersionError;
        public bool AndroidUseVersionOff;

        public TardplusNetworkInfo networkInfo = new TardplusNetworkInfo();

        private bool _SKAdNetworkList;

        public bool SKAdNetworkList
        {
            get
            {
                return _SKAdNetworkList;
            }
            set
            {
                _SKAdNetworkList = value;
            }
        }

        private bool _closeBitCode;

        public bool closeBitCode
        {
            get
            {
                return _closeBitCode;
            }
            set
            {
                _closeBitCode = value;
            }
        }

        private bool _openHttp;
        public bool openHttp
        {
            get
            {
                return _openHttp;
            }
            set
            {
                _openHttp = value;
            }
        }

        private string _iOSAdmobAppID;
        public string iOSAdmobAppID
        {
            get
            {
                return _iOSAdmobAppID;
            }
            set
            {
                _iOSAdmobAppID = value;
            }
        }

        private string _AndroidAdmobAppID;
        public string AndroidAdmobAppID
        {
            get
            {
                return _AndroidAdmobAppID;
            }
            set
            {
                _AndroidAdmobAppID = value;
            }
        }


        private string _APPLovinSDKKey;
        public string APPLovinSDKKey
        {
            get
            {
                return _APPLovinSDKKey;
            }
            set
            {
                _APPLovinSDKKey = value;
            }
        }

        private string _IDFAInfo;
        public string IDFAInfo
        {
            get
            {
                return _IDFAInfo;
            }
            set
            {
                _IDFAInfo = value;
            }
        }

        public void SaveData()
        {
            TardplusLocalData saveData = new TardplusLocalData();
            saveData.IDFAInfo = _IDFAInfo;
            saveData.APPLovinSDKKey = _APPLovinSDKKey;
            saveData.AndroidAdmobAppID = _AndroidAdmobAppID;
            saveData.iOSAdmobAppID = _iOSAdmobAppID;
            saveData.closeBitCode = _closeBitCode;
            saveData.openHttp = _openHttp;
            saveData.SKAdNetworkList = _SKAdNetworkList;
            string saveJson = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.dataPath + "/TradplusSDK/TradplusManager/Editor/" + "TPSaveData.json", saveJson);
        }

        public void Clear()
        {
            netState = 0;
            configInfo = null;
            iOS_NetworkInfo = null;
            Android_NetworkInfo = null;
            networkInfo = new TardplusNetworkInfo();
        }

        //本地默认配置
        public TradplusEditorManager()
        {
            _openHttp = true;
            _closeBitCode = false;
            _SKAdNetworkList = false;
            _iOSAdmobAppID = "";
            _AndroidAdmobAppID = "";
            _APPLovinSDKKey = "";
            _IDFAInfo = "";
            string[] paths = AssetDatabase.FindAssets("TPSaveData");
            foreach (string item in paths)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
                if (path.EndsWith("TPSaveData.json"))
                {
                    string json = File.ReadAllText(path);
                    TardplusLocalData saveData = JsonUtility.FromJson<TardplusLocalData>(json);
                    _openHttp = saveData.openHttp;
                    closeBitCode = saveData.closeBitCode;
                    SKAdNetworkList = saveData.SKAdNetworkList;
                    _iOSAdmobAppID = saveData.iOSAdmobAppID;
                    _AndroidAdmobAppID = saveData.AndroidAdmobAppID;
                    _APPLovinSDKKey = saveData.APPLovinSDKKey;
                    _IDFAInfo = saveData.IDFAInfo;
                    Debug.Log("TPSaveData path " + path);
                    break;
                }
            }
        }
        List<TPVersion> androidVersions;
        //获取SDK版本列表
        private void GetSDKVersionInfo()
        {
            iOSNoVersionError = false;
            AndroidNoVersionError = false;

            WWWForm form = new WWWForm();
            form.AddField("os", "0");
            TardplusCoroutine.StartCoroutine("https://docs.tradplusad.com/api/sdk/list", form, (callback =>
            {
                if (callback == null || callback == "") return;
                TardplusSDKVersionInfo tempInfo = JsonUtility.FromJson<TardplusSDKVersionInfo>(callback);
                if (tempInfo.code == 200)
                {
                    versionInfo = tempInfo;

                    iOSVersionNameList = new List<string>();
                    List<TPVersion> iOSVersionItemList = new List<TPVersion>();
                    iOSVersionIndexList = new List<int>();
                    int i = 0;
                    iOSUseVersionOff = true;
                    int iOSSdkId = 0;
                    int.TryParse(configInfo.iOS.sdkId, out iOSSdkId);
                    foreach (TPVersion item in versionInfo.data.iosVersions)
                    {
                        if (item.extra_info != null && item.extra_info.Length > 0)
                        {
                            Dictionary<string, object> extraInfo = Json.Deserialize(item.extra_info) as Dictionary<string, object>;
                            if (extraInfo != null && extraInfo.ContainsKey("u3d_version"))
                            {
                                string u3dVersionStr = extraInfo["u3d_version"].ToString();
                                string[] u3dVersionList = u3dVersionStr.Split(';');
                                if (u3dVersionList != null && u3dVersionList.Length > 0)
                                {
                                    if (u3dVersionList.Contains(TradplusAds.PluginVersion))
                                    {
                                        int sdkId = 0;
                                        if (int.TryParse(item.sdkId, out sdkId))
                                        {
                                            if (sdkId == iOSSdkId)
                                            {
                                                iOSUseVersionOff = false;
                                            }
                                            iOSVersionNameList.Add(item.version);
                                            iOSVersionIndexList.Add(i);
                                            iOSVersionItemList.Add(item);
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (iOSVersionNameList.Count == 0)
                    {
                        iOSNoVersionError = true;
                    }
                    versionInfo.data.iosVersions = iOSVersionItemList.ToArray();

                    AndroidVersionNameList = new List<string>();
                    AndroidVersionIndexList = new List<int>();
                    i = 0;
                    androidVersions = new List<TPVersion>();
                    AndroidUseVersionOff = true;
                    int AndroidSdkId = 0;
                    int.TryParse(configInfo.Android.sdkId, out AndroidSdkId);
                    foreach (TPVersion item in versionInfo.data.androidVersions)
                    {
                        if (item.isAndroidX == 1)
                        {
                            if (item.extra_info != null && item.extra_info.Length > 0)
                            {
                                Dictionary<string, object> extraInfo = Json.Deserialize(item.extra_info) as Dictionary<string, object>;
                                if (extraInfo != null && extraInfo.ContainsKey("u3d_version"))
                                {
                                    string u3dVersionStr = extraInfo["u3d_version"].ToString();
                                    string[] u3dVersionList = u3dVersionStr.Split(';');
                                    if (u3dVersionList != null && u3dVersionList.Length > 0)
                                    {
                                        if (u3dVersionList.Contains(TradplusAds.PluginVersion))
                                        {
                                            int sdkId = 0;
                                            if (int.TryParse(item.sdkId, out sdkId))
                                            {
                                                if (sdkId == AndroidSdkId)
                                                {
                                                    AndroidUseVersionOff = false;
                                                }
                                                AndroidVersionNameList.Add(item.version);
                                                AndroidVersionIndexList.Add(i);
                                                androidVersions.Add(item);
                                                i++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (AndroidVersionNameList.Count == 0)
                    {
                        AndroidNoVersionError = true;
                    }
                    versionInfo.data.androidVersions = androidVersions.ToArray();

                    Debug.Log("callback versionInfo: " + JsonUtility.ToJson(versionInfo));
                }
                else
                {
                    Debug.Log("sdk Version List Download Error");
                }
            }));
        }

        Regex mvnReg = new Regex(@"https?:\/\/[^\'\""]+(?=[\'\""])", RegexOptions.IgnoreCase);
        Regex impReg = new Regex(@"implementation(?:\'|\""|\()(?:\s*\')?[\""]?([^\:\""\']+\:[^\:]+\:[^' |\"" |\)]+)(?:\'|\""|)\)?", RegexOptions.IgnoreCase);
        Regex pkgReg = new Regex(@"(package|android:name|android:value)=\""([^\""]+)", RegexOptions.IgnoreCase);

        private void SaveGradleConfig(string[] gradleList, string manifestCode, string savePath, TardplusNetworkDesc desc)
        {
            string gradleConfig = "<dependencies>\n<androidPackages>\n^temp^pp</androidPackages>\n</dependencies>\n";
            string packageConfigs = "";
            string mavenConfigs = "";
            if (desc == null) return;
            foreach (string str in gradleList)
            {
                if (str == null) continue;
                if (str.Contains("http"))
                {
                    Match mvnMatch = mvnReg.Match(str.Trim());
                    if (mvnMatch.Success)
                    {
                        mavenConfigs += "<repositories>\n<repository>^temp</repository>\n</repositories>\n".Replace("^temp", mvnMatch.Groups[0].Value);
                    }
                }
                else if (str.Contains("implementation")) {
                    Match impnMatch = impReg.Match(str.Replace(" ", ""));
                    if (impnMatch.Success)
                    {
                        string tmpAnPkg = "<androidPackage spec=\"^temp\">\n</androidPackage>\n";
                        packageConfigs += tmpAnPkg.Replace("^temp", impnMatch.Groups[1].Value);
                    }
                }
            }


            gradleConfig = gradleConfig.Replace("^temp", packageConfigs).Replace("^pp", mavenConfigs).Replace("^pp", "");
            File.WriteAllText(savePath + "Dependencies.xml", gradleConfig);
            AssetDatabase.Refresh();

            if (string.IsNullOrEmpty(manifestCode)) return;
            string tmpName = desc.uniqueNetworkId.ToLower();

            string temPath = mainfestPath + "/" + tmpName + ".androidlib/";

            manifestCode = manifestCode.Replace(pkgReg.Match(manifestCode).Groups[2].Value, "com.tradplus." + tmpName);
            if (Directory.Exists(temPath))
            {
                Directory.Delete(temPath, true);
            }
            Directory.CreateDirectory(temPath);
            File.WriteAllText(temPath + "AndroidManifest.xml", manifestCode);
            File.WriteAllText(temPath + "project.properties", "target=android-28\n" + "android.library = true");
            AssetDatabase.Refresh();
        }

        private int GetIntVersion(string version)
        {
            int start = version.IndexOf('\'') + 1;
            int end = version.LastIndexOf('\'');
            string tempVersion = version.Substring(start, end - start);
            string[] array = tempVersion.Split('.');
            if(array.Length > 0)
            {
                tempVersion = "" + array[0];
            }
            if (array.Length > 1)
            {
                string temp = array[1];
                if(temp.Length == 1)
                {
                    tempVersion += "0" + temp;
                }
                else {
                    tempVersion += temp;
                }
            }
            if (array.Length > 2)
            {
                string temp = array[2];
                if (temp.Length == 1)
                {
                    tempVersion += "0" + temp;
                }
                else
                {
                    tempVersion += temp;
                }
            }
            return int.Parse(tempVersion);
        }

        //iOS保存pod配置到本地文件
        private void SavePodConfig(string[] podList, string savePath, TardplusNetworkDesc desc)
        {
            string podConfig = "<dependencies>\n<iosPods>\n";
            foreach (string str in podList)
            {
                string[] array = str.Split(',');
                if (array.Length == 2)
                {
                    string name = array[0];
                    int start = name.IndexOf('\'') + 1;
                    int end = name.LastIndexOf('\'');
                    name = name.Substring(start, end - start);
                    if (specialPodArray.Contains(name))
                    {
                        TardplusSaveIOSPodInfo podInfo = new TardplusSaveIOSPodInfo();
                        podInfo.nameEn = desc.nameEn;
                        podInfo.podStr = str;
                        string json = JsonUtility.ToJson(podInfo);
                        File.WriteAllText(savePath + "TPPodInfo.json", json);
                    }
                    //Fyber >=v820 <827 为动态库
                    else if(Equals("Fyber_Marketplace_SDK", name))
                    {
                        string version = array[1];
                        start = version.IndexOf('\'') + 1;
                        end = version.LastIndexOf('\'');
                        version = version.Substring(start, end - start);
                        string tempVersion = version;
                        tempVersion = tempVersion.Replace(".","");
                        if(tempVersion.Length > 3)
                        {
                            tempVersion = tempVersion.Substring(0,3);
                        }
                        if (int.Parse(tempVersion) >= 820 && int.Parse(tempVersion) < 827)
                        {
                            TardplusSaveIOSPodInfo podInfo = new TardplusSaveIOSPodInfo();
                            podInfo.nameEn = desc.nameEn;
                            podInfo.podStr = str;
                            string json = JsonUtility.ToJson(podInfo);
                            File.WriteAllText(savePath + "TPPodInfo.json", json);
                        }
                        else
                        {
                            podConfig += "<iosPod name =\"" + name + "\" version=\"" + version + "\"/>\n";
                        }
                    }
                    //Tapjoy v13.3.0+ 为动态库
                    else if (Equals("TapjoySDK", name))
                    {
                        string version = array[1];
                        start = version.IndexOf('\'') + 1;
                        end = version.LastIndexOf('\'');
                        version = version.Substring(start, end - start);
                        string tempVersion = version;
                        tempVersion = tempVersion.Replace(".", "");
                        if (tempVersion.Length > 4)
                        {
                            tempVersion = tempVersion.Substring(0, 4);
                        }
                        if (int.Parse(tempVersion) >= 1330)
                        {
                            TardplusSaveIOSPodInfo podInfo = new TardplusSaveIOSPodInfo();
                            podInfo.nameEn = desc.nameEn;
                            podInfo.podStr = str;
                            string json = JsonUtility.ToJson(podInfo);
                            File.WriteAllText(savePath + "TPPodInfo.json", json);
                        }
                        else
                        {
                            podConfig += "<iosPod name =\"" + name + "\" version=\"" + version + "\"/>\n";
                        }
                    }
                    //Bigo v4.1.1+  版本无动态库
                    else if (Equals("BigoADS", name))
                    {
                        string version = array[1];
                        start = version.IndexOf('\'') + 1;
                        end = version.LastIndexOf('\'');
                        version = version.Substring(start, end - start);
                        string tempVersion = version;
                        tempVersion = tempVersion.Replace(".", "");
                        if (tempVersion.Length > 3)
                        {
                            tempVersion = tempVersion.Substring(0, 3);
                        }
                        if (int.Parse(tempVersion) >= 411)
                        {
                            podConfig += "<iosPod name =\"" + name + "\" version=\"" + version + "\"/>\n";
                        }
                        else
                        {
                            TardplusSaveIOSPodInfo podInfo = new TardplusSaveIOSPodInfo();
                            podInfo.nameEn = desc.nameEn;
                            podInfo.podStr = str;
                            string json = JsonUtility.ToJson(podInfo);
                            File.WriteAllText(savePath + "TPPodInfo.json", json);
                        }
                    }
                    //Start.io 4.9.1 版本为动态库
                    else if (Equals("StartAppSDK", name))
                    {
                        string version = array[1];
                        start = version.IndexOf('\'') + 1;
                        end = version.LastIndexOf('\'');
                        version = version.Substring(start, end - start);
                        string tempVersion = version;
                        tempVersion = tempVersion.Replace(".", "");
                        if (tempVersion.Length > 3)
                        {
                            tempVersion = tempVersion.Substring(0, 3);
                        }
                        if (int.Parse(tempVersion) == 491)
                        {
                            TardplusSaveIOSPodInfo podInfo = new TardplusSaveIOSPodInfo();
                            podInfo.nameEn = desc.nameEn;
                            podInfo.podStr = str;
                            string json = JsonUtility.ToJson(podInfo);
                            File.WriteAllText(savePath + "TPPodInfo.json", json);
                        }
                        else
                        {
                            podConfig += "<iosPod name =\"" + name + "\" version=\"" + version + "\"/>\n";
                        }
                    }
                    //AppLovin >= 12.4.1
                    else if (Equals("AppLovinSDK", name))
                    {
                        string version = array[1];
                        if (GetIntVersion(version) >= 120401)
                        {
                            TardplusSaveIOSPodInfo podInfo = new TardplusSaveIOSPodInfo();
                            podInfo.nameEn = desc.nameEn;
                            podInfo.podStr = str;
                            string json = JsonUtility.ToJson(podInfo);
                            File.WriteAllText(savePath + "TPPodInfo.json", json);
                        }
                        else
                        {
                            podConfig += "<iosPod name =\"" + name + "\" version=\"" + version + "\"/>\n";
                        }
                    }
                    //InMobi >= 10.7.2
                    else if (Equals("InMobiSDK", name))
                    {
                        string version = array[1];
                        start = version.IndexOf('\'') + 1;
                        end = version.LastIndexOf('\'');
                        version = version.Substring(start, end - start);
                        string tempVersion = version;
                        tempVersion = tempVersion.Replace(".", "");
                        if (int.Parse(tempVersion) >= 1072)
                        {
                            TardplusSaveIOSPodInfo podInfo = new TardplusSaveIOSPodInfo();
                            podInfo.nameEn = desc.nameEn;
                            podInfo.podStr = str;
                            string json = JsonUtility.ToJson(podInfo);
                            File.WriteAllText(savePath + "TPPodInfo.json", json);
                        }
                        else
                        {
                            podConfig += "<iosPod name =\"" + name + "\" version=\"" + version + "\"/>\n";
                        }
                    }
                    else if (!Equals("TradPlusAdSDK", name))
                    {
                        string version = array[1];
                        start = version.IndexOf('\'') + 1;
                        end = version.LastIndexOf('\'');
                        version = version.Substring(start, end - start);
                        podConfig += "<iosPod name =\"" + name + "\" version=\"" + version + "\"/>\n";
                    }
                }
            }
            podConfig += "</iosPods>\n</dependencies>";
            Debug.Log(podConfig);
            File.WriteAllText(savePath + "Dependencies.xml", podConfig);
        }




        private string GetSavePath(int os, TardplusNetworkDesc desc)
        {
            string path = savePath;

            if (os == 1)
            {
                path += "Android/";
            }
            else
            {
                path += "iOS/";
            }
            string uniqueNetworkId = desc.uniqueNetworkId.ToLower().Replace(" ", "");
            path += uniqueNetworkId + "/Editor/";
            return path;
        }

        //获取配置信息
        public void GetNetworkConfig(string sdkId,string networkId,int os, TardplusNetworkDesc desc, bool refresh = true)
        {
            WWWForm form = new WWWForm();
            form.AddField("os",os);
            form.AddField("sdkId", sdkId);
            form.AddField("isUnity", 0);
            if(os == 1)
            {
                //Andorid
                form.AddField("isNogradle",0);
            }
            else
            {
                //iOS
                form.AddField("isCocoapod", 1);
            }
            form.AddField("networkId",networkId);
            TardplusCoroutine.StartCoroutine("https://docs.tradplusad.com/api/tool/package", form, (callback =>
            {
                TardplusNetworkConfig callbackConfig = JsonUtility.FromJson<TardplusNetworkConfig>(callback);
                Debug.Log("gradleCodecallback : " + JsonUtility.ToJson(callbackConfig));
                if(callbackConfig.code == 200)
                {
                    //创建目录
                    string path = GetSavePath(os,desc);
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path,true);
                    }
                    Directory.CreateDirectory(path);

                    //添加配置文件
                    if (os == 1)
                    {
                        //Andorid
                        string gradleCode = callbackConfig.data.gradleCode;
                        string manifestCode = callbackConfig.data.manifestCode;
                        gradleCode = gradleCode.Replace("\r", "").Trim();
                        if (gradleCode != null)
                        {
                            string[] gradleList = gradleCode.Split('\n');
                            SaveGradleConfig(gradleList, manifestCode, path, desc);
                            desc.android_install_version = desc.android_version;
                            desc.android_install_sdk_version = configInfo.Android.sdkVersion;
                            desc.android_hasInstall = true;
                            desc.android_update = false;
                        }
                        else
                        {
                            Debug.LogError("no find gradleCode : " + JsonUtility.ToJson(callbackConfig));
                        }
                    }
                    else
                    {
                        //iOS
                        string cocoapodCode = callbackConfig.data.cocoapodCode;
                        cocoapodCode = cocoapodCode.Replace("\r", "");
                        if (cocoapodCode != null)
                        {
                            string[] podList = cocoapodCode.Split('\n');
                            SavePodConfig(podList, path, desc);
                            desc.ios_install_version = desc.ios_version;
                            desc.ios_install_sdk_version = configInfo.iOS.sdkVersion;
                            desc.ios_hasInstall = true;
                            desc.ios_update = false;
                        }
                        else
                        {
                            Debug.LogError("no find cocoapodCode : " + JsonUtility.ToJson(callbackConfig));
                        }
                    }

                    string netWrokInfo = saveNetWorkInfo(desc, os);
                    File.WriteAllText(path + "TPNetWorkInfo.json", netWrokInfo);
                    if (refresh)
                    {
                        AssetDatabase.Refresh();
                    }
                }
                else
                {
                    Debug.Log("NetworkConfig Download Error");
                }
            }));
        }

        //更新SDK版本
        public void ExChangeSDKVersion(int index, int os)
        {
            TPVersion[] versionArray;
            if (os == 1)
            {
                //Andorid
                versionArray = versionInfo.data.androidVersions;
            }
            else
            {
                //iOS
                versionArray = versionInfo.data.iosVersions;
            }
            TPVersion version = versionArray[index];
            WWWForm form = new WWWForm();
            form.AddField("os", os);
            form.AddField("sdkId", version.sdkId);
            TardplusCoroutine.StartCoroutine("https://docs.tradplusad.com/api/sdk/config", form, (callback =>
            {
                if (callback == null)
                {
                    Debug.Log("exchange Error");
                    return;
                }
                TardplusNetworkModel networkModel = JsonUtility.FromJson<TardplusNetworkModel>(callback);
                //版本的Netwrok列表获取完成
                UpdateSDKInfo(version,networkModel, os);
            }));
        }

        private void UpdateSDKInfo(TPVersion version, TardplusNetworkModel networkModel,int os)
        {
            if(os == 1)
            {
                AndroidUseVersionOff = false;
                configInfo.Android.sdkId = version.sdkId;
                configInfo.Android.sdkVersion = version.version;
                Android_NetworkInfo = networkModel;
                foreach (Network network in Android_NetworkInfo.data.networks)
                {
                    networkInfo.AddNetwork(network, 1);
                }
                string json = JsonUtility.ToJson(configInfo);
                File.WriteAllText(configPath, json);
                //保存配置
                string gradleConfig = "<dependencies>\n<androidPackages>\n<androidPackage spec=\"com.tradplusad:tradplus:" + version.version + "\"/>\n</androidPackages>\n</dependencies>";
                string[] paths = AssetDatabase.FindAssets("TPEditorGradleDependencies");
                foreach (string item in paths)
                {
                    string path = AssetDatabase.GUIDToAssetPath(item);
                    if (path.EndsWith("TPEditorGradleDependencies.xml"))
                    {
                        File.WriteAllText(path, gradleConfig);
                    }
                }
            }
            else
            {
                iOSUseVersionOff = false;
                configInfo.iOS.sdkId = version.sdkId;
                configInfo.iOS.sdkVersion = version.version;
                iOS_NetworkInfo = networkModel;
                foreach (Network network in iOS_NetworkInfo.data.networks)
                {
                    networkInfo.AddNetwork(network, 2);
                }
                string json = JsonUtility.ToJson(configInfo);
                File.WriteAllText(configPath, json);
                //保存pod配置
                string podConfig = "<dependencies>\n<iosPods>\n<iosPod name=\"TradPlusAdSDK\" version=\"" + version.version + "\"/>\n</iosPods>\n</dependencies>";
                string[] paths = AssetDatabase.FindAssets("TPEditorPodDependencies");
                foreach (string item in paths)
                {
                    string path = AssetDatabase.GUIDToAssetPath(item);
                    if (path.EndsWith("TPEditorPodDependencies.xml"))
                    {
                        File.WriteAllText(path, podConfig);
                    }
                }
            }

            GetHisData();
            AddDefNetwork();
            networkInfo.UpdateHisNetWorkInfo();
        }

        public string saveNetWorkInfo(TardplusNetworkDesc desc ,int os)
        {
            TardplusSaveNetworkInfo saveInfo = new TardplusSaveNetworkInfo();
            saveInfo.os = os;
            saveInfo.nameEn = desc.nameEn;
            saveInfo.uniqueNetworkId = desc.uniqueNetworkId;
            if (os == 1)
            {
                saveInfo.version = desc.android_version;
                saveInfo.sdkVersion = configInfo.Android.sdkVersion;
            }
            else
            {
                //iOS
                saveInfo.version = desc.ios_version;
                saveInfo.sdkVersion = configInfo.iOS.sdkVersion;
            }
            return JsonUtility.ToJson(saveInfo);
        }

        //移除
        public void removeNetwork(string networkId, int os, TardplusNetworkDesc desc,bool refresh = true)
        {
            string path = GetSavePath(os, desc);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                //Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            if (os == 1)
            {
                //Andorid
                desc.android_hasInstall = false;
                desc.android_update = false;
                desc.android_install_version = "Not Installed";

                string tempPath = mainfestPath + desc.uniqueNetworkId.ToLower() + ".androidlib";
                if (tempPath!="" && Directory.Exists(tempPath))
                {
                    FileUtil.DeleteFileOrDirectory(tempPath);
                    File.Delete(tempPath + ".meta");
                }
            }
            else
            {
                //iOS
                desc.ios_hasInstall = false;
                desc.ios_update = false;
                desc.ios_install_version = "Not Installed";
            }
            if(refresh)
            {
                AssetDatabase.Refresh();
            }
        }

    

        //0 = 加载中， 1=完成， 2=失败
        public int netState;

        //获取配置及中介列表
        public void GetConfig()
        {
            configPath = "";
            string[] paths = AssetDatabase.FindAssets("tradplus_manager");
            foreach (string item in paths)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
                if (path.EndsWith("tradplus_manager.json"))
                {
                    configPath = path;
                    Debug.Log("configPath " + configPath);
                    break;
                }
            }

            if (configPath.Length > 0)
            {
                string json = File.ReadAllText(configPath);
                Debug.Log("json " + json);
                configInfo = JsonUtility.FromJson<TardplusConfigModel>(json);
            }
            else
            {
                Debug.LogError("not find tradplus_manager.json");
                return;
            }

            GetSDKVersionInfo();

            //iOS
            WWWForm form = new WWWForm();
            form.AddField("os", configInfo.iOS.os);
            form.AddField("sdkId", configInfo.iOS.sdkId);
            TardplusCoroutine.StartCoroutine("https://docs.tradplusad.com/api/sdk/config", form, (callback =>
            {
                if (callback == null)
                {
                    netState = 2;
                    return;
                }
                iOS_NetworkInfo = JsonUtility.FromJson<TardplusNetworkModel>(callback);
                Debug.Log("ios callback : " + JsonUtility.ToJson(iOS_NetworkInfo));
                LoadFinish();
            }));

            //Android
            form = new WWWForm();
            form.AddField("os", configInfo.Android.os);
            form.AddField("sdkId", configInfo.Android.sdkId);
            TardplusCoroutine.StartCoroutine("https://docs.tradplusad.com/api/sdk/config", form, (callback =>
            {
                if (callback == null)
                {
                    netState = 2;
                    return;
                }
                Android_NetworkInfo = JsonUtility.FromJson<TardplusNetworkModel>(callback);
                Debug.Log("Android callback : " + JsonUtility.ToJson(Android_NetworkInfo));
                LoadFinish();
            }));
        }

        private void LoadFinish()
        {
            if(netState == 0 && iOS_NetworkInfo != null && Android_NetworkInfo != null)
            {
                //Android
                if (Android_NetworkInfo != null && Android_NetworkInfo.data != null && Android_NetworkInfo.data.networks != null)
                {
                    foreach (Network network in Android_NetworkInfo.data.networks)
                    {
                        networkInfo.AddNetwork(network, 1);
                    }
                }
                //iOS
                if(iOS_NetworkInfo != null && iOS_NetworkInfo.data != null && iOS_NetworkInfo.data.networks != null)
                {
                    foreach (Network network in iOS_NetworkInfo.data.networks)
                    {
                        networkInfo.AddNetwork(network, 2);
                    }
                }
                networkInfo.networkList.Sort((x,y)=> { return x.nameEn.CompareTo(y.nameEn); });
                netState = 1;
                //获取本地历史数据
                GetHisData();
                //添加默认
                AddDefNetwork();
                TradplusEditorManagerWindow.RefreshManager();
            }
        }

        private void AddDefNetwork()
        {
            //foreach (TardplusNetworkDesc desc in networkInfo.networkList)
            //{
            //    if (Equals(desc.uniqueNetworkId.ToLower(), "n40"))
            //    {
            //        if(desc.has_Android)
            //        {
            //            string sdkId = configInfo.Android.sdkId;
            //            GetNetworkConfig(sdkId, desc.android_networkId, 1, desc);
            //        }
            //        if (desc.has_iOS)
            //        {
            //            string sdkId = configInfo.iOS.sdkId;
            //            GetNetworkConfig(sdkId, desc.ios_networkId, 2, desc);
            //        }
            //    }
            //}
        }

        //获取本地已配置信息
        private void GetHisData()
        {
            string[] paths = AssetDatabase.FindAssets("TPNetWorkInfo");
            foreach (string item in paths)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
                if (path.EndsWith("TPNetWorkInfo.json"))
                {
                    string json = File.ReadAllText(path);
                    Debug.Log("json " + json);
                    TardplusSaveNetworkInfo saveInfo = JsonUtility.FromJson<TardplusSaveNetworkInfo>(json);
                    networkInfo.CheckHisInfo(saveInfo);
                }
            }
        }
    }
}

