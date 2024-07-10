using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using TradplusSDK.Api;

namespace Tardplus.TradplusEditorManager.Editor
{
    public class TradplusEditorManagerWindow : EditorWindow
    {
        private static readonly Vector2 windowMinSize = new Vector2(800, 750);
        private static float windowWidth = 800;

        private Vector2 scrollPosition;

        private const float networkFieldMinWidth = 200f;
        private const float versionFieldMinWidth = 200f;
        private const float actionFieldWidth = 80f;

        private static GUILayoutOption networkWidthOption = GUILayout.Width(networkFieldMinWidth);
        private static GUILayoutOption versionWidthOption = GUILayout.Width(versionFieldMinWidth);
        private static GUILayoutOption fieldWidth = GUILayout.Width(actionFieldWidth);

        private static GUILayoutOption noVersionWidthOption = GUILayout.Width(400f);

        private GUIStyle titleLabelStyle;
        private GUIStyle headerLabelStyle;
        private GUIStyle iconStyle;
        private GUIStyle errorLabelStyle;
        private int iOSPopupIndex;
        private int AndroidPopupIndex;

        string searchText;
        string showSearchText;

        static TradplusEditorManagerWindow manager;

        public static void ShowManager()
        {
            manager = GetWindow<TradplusEditorManagerWindow>(utility: true, title: "TradPlusManager", focus: true);
            manager.minSize = windowMinSize;
            manager.maxSize = windowMinSize;

        }

        public static void RefreshManager()
        {
            manager.Repaint();
        }

        private void Awake()
        {
            errorLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Bold,
            };
            errorLabelStyle.normal.textColor = Color.red;
            titleLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                fixedHeight = 20
            };
            headerLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                fixedHeight = 18
            };
            iconStyle = new GUIStyle(EditorStyles.miniButton)
            {
                fixedWidth = 18,
                fixedHeight = 18,
                padding = new RectOffset(1, 1, 1, 1)
            };
            TradplusEditorManager.Instance().GetConfig();
            searchText = "";
        }

        private void OnDisable()
        {
            
        }

        private void OnDestroy()
        {
            TradplusEditorManager.Instance().SaveData();
            TradplusEditorManager.Instance().Clear();
        }


        private void OnEnable()
        {

        }

        private void OnGUI()
        {
            if (Math.Abs(windowWidth - position.width) > 1)
            {
                windowWidth = position.width;
            }

            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosition, false, false))
            {
                scrollPosition = scrollView.scrollPosition;

                GUILayout.Space(5);
                DrawSDKVersions();
                if (TradplusEditorManager.Instance().netState == 1)
                {
                    GUILayout.Space(5);
                    DrawNetworkList();
                }
                DrawAndroidSetting();
                GUILayout.Space(5);
                DrawIOSSetting();
                GUILayout.Space(20);
            }
        }

        private void DrawIOSSetting()
        {
            EditorGUILayout.LabelField("iOS Setting", titleLabelStyle);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(10);
                EditorGUILayout.LabelField(new GUIContent("User Tracking Usage Description (IDFA)"));
                GUILayout.Space(2);
                TradplusEditorManager.Instance().IDFAInfo = GUILayout.TextField(TradplusEditorManager.Instance().IDFAInfo);
                GUILayout.Space(10);
                TradplusEditorManager.Instance().iOSAdmobAppID = DrawTextField("iOS Admob AppID", TradplusEditorManager.Instance().iOSAdmobAppID, GUILayout.Width(130));
                GUILayout.Space(10);
                TradplusEditorManager.Instance().openHttp = GUILayout.Toggle(TradplusEditorManager.Instance().openHttp, " Open Http Network (一般需开启以支持部分三方源的Http网络请求)");
                GUILayout.Space(10);
                TradplusEditorManager.Instance().closeBitCode = GUILayout.Toggle(TradplusEditorManager.Instance().closeBitCode, " Close Bitcode(使用快手 AppLovin 时需要关闭)");
                GUILayout.Space(10);
                TradplusEditorManager.Instance().SKAdNetworkList = GUILayout.Toggle(TradplusEditorManager.Instance().SKAdNetworkList, " Auto Add SKAdNetworkList(自动导出SKAdNetworkList到Xcode)");
                GUILayout.Space(10);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawAndroidSetting()
        {
            EditorGUILayout.LabelField("Android Setting", titleLabelStyle);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(10);
                TradplusEditorManager.Instance().AndroidAdmobAppID = DrawTextField("Android Admob AppID", TradplusEditorManager.Instance().AndroidAdmobAppID, GUILayout.Width(130));
                GUILayout.Space(10);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawNetworkList()
        {
            EditorGUILayout.LabelField("Ad Networks", titleLabelStyle);
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            this.searchText = GUILayout.TextField(searchText);
            GUILayout.Space(5);
            if (GUILayout.Button(new GUIContent("search"), fieldWidth))
            {
                this.showSearchText = this.searchText;
            }
            GUILayout.Space(30);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                DrawRowHeader("Network",true);
                foreach (TardplusNetworkDesc desc in TradplusEditorManager.Instance().networkInfo.networkList)
                {
                    //if (Equals(desc.uniqueNetworkId.ToLower(), "n40"))
                    //{
                    //    continue;
                    //}
                    if(this.showSearchText != null && this.showSearchText.Length > 0)
                    {
                        string name = desc.nameEn;
                        if (desc.nameCn != null && desc.nameCn.Length > 0)
                        {
                            name = desc.nameEn + "-" + desc.nameCn;
                        }
                        if (!name.ToLower().Contains(this.showSearchText.ToLower()))
                        {
                            continue;
                        }
                    }
                    using (new EditorGUILayout.VerticalScope("box"))
                    {
                        GUILayout.Space(4);
                        bool didShowName = false;
                        if (desc.has_Android)
                        {
                            didShowName = true;
                            string name = desc.nameEn;
                            if(desc.nameCn != null && desc.nameCn.Length > 0)
                            {
                                name = desc.nameEn + "-" + desc.nameCn;
                            }
                            string action = "Install";
                            string version = desc.android_install_version;
                            if (desc.android_hasInstall)
                            {
                                action = "Installed";
                                version += " (" + desc.android_install_sdk_version + ")";
                            }
                            if (desc.android_update)
                            {
                                action = "Update";
                            }
                            DrawRowNetwork(name, desc.android_install_version, desc.android_version, desc.android_networkId, action, 1, desc);
                        } 
                        if (desc.has_iOS)
                        {
                            string name = @"";
                            if (!didShowName)
                            {
                                name = desc.nameEn;
                            }
                            string action = "Install";
                            string version = desc.ios_install_version;
                            if (desc.ios_hasInstall)
                            {
                                action = "Installed";
                                version += " (" + desc.ios_install_sdk_version + ")";
                            }
                            if (desc.ios_update)
                            {
                                action = "Update";
                            }
                            DrawRowNetwork(name, version, desc.ios_version, desc.ios_networkId, action, 2, desc);
                        }
                        GUILayout.Space(4);
                    }
                }
            }

            GUILayout.Space(5);
            GUILayout.EndHorizontal();
        }

        private void DrawSDKVersions()
        {
            EditorGUILayout.LabelField("TradPlus SDK Versions", titleLabelStyle);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                DrawRowHeader("Platform",true);
                DrawRowInfo("Unity Plugin", TradplusAds.PluginVersion, "");
                //Android
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(5);
                    EditorGUILayout.LabelField(new GUIContent("Android"), networkWidthOption);
                    string currentVersion = TradplusEditorManager.Instance().configInfo.Android.sdkVersion;
                    if (TradplusEditorManager.Instance().AndroidNoVersionError)
                    {
                        string info = "当前 "+ currentVersion + " 版本已下架，请至官网更新unitypackage插件包。";
                        EditorGUILayout.LabelField(new GUIContent(info), errorLabelStyle, noVersionWidthOption);
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button(new GUIContent("官网下载"), fieldWidth))
                        {
                            Application.OpenURL("https://docs.tradplusad.com/docs/doc_u3d/u3d_download/");
                        }
                        GUILayout.Space(20);
                    }
                    else
                    {
                        if (TradplusEditorManager.Instance().AndroidUseVersionOff)
                        {
                            string info = "当前 " + currentVersion + " 版本已下架,请更新版本";
                            EditorGUILayout.LabelField(new GUIContent(info), errorLabelStyle, versionWidthOption);
                        }
                        else
                        {
                            EditorGUILayout.LabelField(new GUIContent(currentVersion), versionWidthOption);
                        }
                        GUILayout.Space(3);
                        if (TradplusEditorManager.Instance().versionInfo != null && TradplusEditorManager.Instance().AndroidVersionNameList.Count > 0)
                        {
                            AndroidPopupIndex = EditorGUILayout.IntPopup(AndroidPopupIndex, TradplusEditorManager.Instance().AndroidVersionNameList.ToArray(), TradplusEditorManager.Instance().AndroidVersionIndexList.ToArray(), versionWidthOption);
                            GUILayout.FlexibleSpace();
                            string version = TradplusEditorManager.Instance().AndroidVersionNameList[AndroidPopupIndex];
                            GUI.enabled = !Equals(currentVersion, version);
                            if (GUILayout.Button(new GUIContent("Exchange"), fieldWidth))
                            {
                                TradplusEditorManager.Instance().ExChangeSDKVersion(AndroidPopupIndex, 1);
                            }
                            GUI.enabled = true;
                            GUILayout.Space(20);
                        }
                        else
                        {
                            EditorGUILayout.LabelField(new GUIContent("loading..."), versionWidthOption);
                        }
                    }
                    GUILayout.Space(3);
                }
                GUILayout.Space(4);

                //iOS
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(5);
                    EditorGUILayout.LabelField(new GUIContent("iOS"), networkWidthOption);
                    string currentVersion = TradplusEditorManager.Instance().configInfo.iOS.sdkVersion;
                    if (TradplusEditorManager.Instance().iOSNoVersionError)
                    {
                        string info = "当前 " + currentVersion + " 版本已下架，请至官网更新unitypackage插件包。";
                        EditorGUILayout.LabelField(new GUIContent(info), errorLabelStyle,noVersionWidthOption);
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button(new GUIContent("官网下载"), fieldWidth))
                        {
                            Application.OpenURL("https://docs.tradplusad.com/docs/doc_u3d/u3d_download/");
                        }
                        GUILayout.Space(20);
                    }
                    else
                    {
                        if(TradplusEditorManager.Instance().iOSUseVersionOff)
                        {
                            string info = "当前 " + currentVersion + " 版本已下架,请更新版本";
                            EditorGUILayout.LabelField(new GUIContent(info), errorLabelStyle, versionWidthOption);
                        }
                        else
                        {
                            EditorGUILayout.LabelField(new GUIContent(currentVersion), versionWidthOption);
                        }
                        GUILayout.Space(3);
                        if (TradplusEditorManager.Instance().versionInfo != null)
                        {
                            iOSPopupIndex = EditorGUILayout.IntPopup(iOSPopupIndex, TradplusEditorManager.Instance().iOSVersionNameList.ToArray(), TradplusEditorManager.Instance().iOSVersionIndexList.ToArray(), versionWidthOption);
                            GUILayout.FlexibleSpace();
                            string version = TradplusEditorManager.Instance().iOSVersionNameList[iOSPopupIndex];
                            GUI.enabled = !Equals(currentVersion, version);
                            if (GUILayout.Button(new GUIContent("Exchange"), fieldWidth))
                            {
                                TradplusEditorManager.Instance().ExChangeSDKVersion(iOSPopupIndex, 2);
                            }
                            GUI.enabled = true;
                            GUILayout.Space(20);
                        }
                        else
                        {
                            EditorGUILayout.LabelField(new GUIContent("loading..."), versionWidthOption);
                        }
                    }
                    GUILayout.Space(3);

                }
                GUILayout.Space(4);
            }

            GUILayout.Space(5);
            GUILayout.EndHorizontal();

        }

        private void DrawRowHeader(string name,bool hasAction)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(5);
                EditorGUILayout.LabelField(name, headerLabelStyle, networkWidthOption);
                EditorGUILayout.LabelField("Current Version", headerLabelStyle, versionWidthOption);
                GUILayout.Space(3);
                EditorGUILayout.LabelField("SDK Version", headerLabelStyle, versionWidthOption);
                GUILayout.Space(3);
                if (hasAction)
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Button("Actions", headerLabelStyle, fieldWidth);
                    GUILayout.Space(5);
                }
            }

            GUILayout.Space(4);
        }

        private void DrawRowInfo(string platform, string currentVersion, string latestVersion)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(5);
                EditorGUILayout.LabelField(new GUIContent(platform), networkWidthOption);
                EditorGUILayout.LabelField(new GUIContent(currentVersion), versionWidthOption);
                GUILayout.Space(3);
                EditorGUILayout.LabelField(new GUIContent(latestVersion), versionWidthOption);
                GUILayout.Space(3);
            }
            GUILayout.Space(4);
        }

        private string DrawTextField(string fieldTitle, string text, GUILayoutOption labelWidth, GUILayoutOption textFieldWidthOption = null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(4);
            EditorGUILayout.LabelField(new GUIContent(fieldTitle), labelWidth);
            GUILayout.Space(4);
            text = (textFieldWidthOption == null) ? GUILayout.TextField(text) : GUILayout.TextField(text, textFieldWidthOption);
            GUILayout.Space(4);
            GUILayout.EndHorizontal();
            GUILayout.Space(4);

            return text;
        }

        private void DrawRowNetwork(string platform, string currentVersion, string latestVersion, string networkId, string action,int os, TardplusNetworkDesc desc)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(5);
                EditorGUILayout.LabelField(new GUIContent(platform), networkWidthOption);
                EditorGUILayout.LabelField(new GUIContent(currentVersion), versionWidthOption);
                GUILayout.Space(3);
                EditorGUILayout.LabelField(new GUIContent(latestVersion), versionWidthOption);
                GUILayout.FlexibleSpace();
                string sdkId = "";
                bool enabled = false;
                if (os == 1)
                {
                    //Android
                    sdkId = TradplusEditorManager.Instance().configInfo.Android.sdkId;
                    enabled = !desc.android_hasInstall;
                    if (desc.android_update)
                    {
                        enabled = true;
                    }
                    GUI.enabled = enabled;
                }
                else
                {
                    //iOS
                    sdkId = TradplusEditorManager.Instance().configInfo.iOS.sdkId;
                    enabled = !desc.ios_hasInstall;
                    if(desc.ios_update)
                    {
                        enabled = true;
                    }
                    GUI.enabled = enabled;
                }


                if (GUILayout.Button(new GUIContent(action), fieldWidth))
                {
                    TradplusEditorManager.Instance().GetNetworkConfig(sdkId , networkId, os, desc);
                }

                GUILayout.Space(2);
                if (os == 1)
                {
                    GUI.enabled = !enabled;
                    if (desc.android_update)
                    {
                        GUI.enabled = true;
                    }
                }
                else
                {
                    //iOS
                    GUI.enabled = !enabled;
                    if (desc.ios_update)
                    {
                        GUI.enabled = true;
                    }
                }
                if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash"), iconStyle))
                {
                    TradplusEditorManager.Instance().removeNetwork(networkId, os, desc);
                }
                GUI.enabled = true;
                GUILayout.Space(5);
            }
            GUILayout.Space(4);
        }

    }
}