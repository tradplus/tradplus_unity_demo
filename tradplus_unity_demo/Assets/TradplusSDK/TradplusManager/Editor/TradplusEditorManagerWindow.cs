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

        private const float networkFieldMinWidth = 180f;
        private const float versionFieldMinWidth = 200f;
        private const float actionFieldWidth = 80f;

        private static GUILayoutOption networkWidthOption = GUILayout.Width(networkFieldMinWidth);
        private static GUILayoutOption versionWidthOption = GUILayout.Width(versionFieldMinWidth);
        private static GUILayoutOption fieldWidth = GUILayout.Width(actionFieldWidth);

        private GUIStyle titleLabelStyle;
        private GUIStyle headerLabelStyle;
        private GUIStyle iconStyle;
        private int iOSPopupIndex;
        private int AndroidPopupIndex;

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
                //CalculateFieldWidth();
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
                TradplusEditorManager.Instance().closeBitCode = GUILayout.Toggle(TradplusEditorManager.Instance().closeBitCode, " Close Bitcode(使用快手时需要关闭)");
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
                //GUILayout.Space(10);
                //TradplusEditorManager.Instance().PackName = DrawTextField("Android PackageName", TradplusEditorManager.Instance().PackName, GUILayout.Width(130));
                GUILayout.Space(10);
                TradplusEditorManager.Instance().AndroidAdmobAppID = DrawTextField("Android Admob AppID", TradplusEditorManager.Instance().AndroidAdmobAppID, GUILayout.Width(130));
                GUILayout.Space(10);
                //TradplusEditorManager.Instance().APPLovinSDKKey = DrawTextField("AppLovin SDK Key", TradplusEditorManager.Instance().APPLovinSDKKey, GUILayout.Width(130));
                //GUILayout.Space(10);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawNetworkList()
        {
            EditorGUILayout.LabelField("Ad Networks", titleLabelStyle);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                DrawRowHeader("Network",true);
                foreach (TardplusNetworkDesc desc in TradplusEditorManager.Instance().networkInfo.networkList)
                {
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
                            //if(Equals(desc.uniqueNetworkId, "n2"))
                            //{
                            //    name = "GoogleAdManager";
                            //}
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
                    EditorGUILayout.LabelField(new GUIContent(currentVersion), versionWidthOption);
                    GUILayout.Space(3);
                    if (TradplusEditorManager.Instance().versionInfo != null)
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
                    GUILayout.Space(3);
                }
                GUILayout.Space(4);

                //iOS
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(5);
                    EditorGUILayout.LabelField(new GUIContent("iOS"), networkWidthOption);
                    string currentVersion = TradplusEditorManager.Instance().configInfo.iOS.sdkVersion;
                    EditorGUILayout.LabelField(new GUIContent(currentVersion), versionWidthOption);
                    GUILayout.Space(3);
                    if (TradplusEditorManager.Instance().versionInfo != null)
                    {
                        iOSPopupIndex = EditorGUILayout.IntPopup(iOSPopupIndex, TradplusEditorManager.Instance().iOSVersionNameList.ToArray(), TradplusEditorManager.Instance().iOSVersionIndexList.ToArray(), versionWidthOption);
                        GUILayout.FlexibleSpace();
                        string version = TradplusEditorManager.Instance().iOSVersionNameList[iOSPopupIndex];
                        GUI.enabled = !Equals(currentVersion, version);
                        if (GUILayout.Button(new GUIContent("Exchange"), fieldWidth))
                        {
                            TradplusEditorManager.Instance().ExChangeSDKVersion(iOSPopupIndex,2);
                        }
                        GUI.enabled = true;
                        GUILayout.Space(20);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(new GUIContent("loading..."), versionWidthOption);
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