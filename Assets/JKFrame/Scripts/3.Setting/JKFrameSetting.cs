using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Sirenix.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace JKFrame
{
    /// <summary>
    /// ��Դϵͳ����
    /// </summary>
    public enum ResourcesSystemType
    {
        Resources,
        Addressables
    }
    /// <summary>
    /// �浵ϵͳ����
    /// </summary>
    public enum SaveSystemType
    {
        Binary,
        Json
    }
    /// <summary>
    /// ��ܵ�����
    /// </summary>
    [CreateAssetMenu(fileName = "JKFrameSetting", menuName = "JKFrame/JKFrameSetting")]
    public class JKFrameSetting : SerializedScriptableObject
    {
        [LabelText("��Դ����ʽ")]
#if UNITY_EDITOR
        [OnValueChanged(nameof(SetResourcesSystemType))]
#endif
        public ResourcesSystemType ResourcesSystemType = ResourcesSystemType.Resources;
#if UNITY_EDITOR
        [LabelText("�浵��ʽ"), Tooltip("�޸����ͻᵼ��֮ǰ�Ĵ浵��ʧ"), OnValueChanged(nameof(SetSaveSystemType))]
#endif
        public SaveSystemType SaveSystemType = SaveSystemType.Binary;

        [LabelText("��־����")] public LogSetting LogConfig = new LogSetting();

        [LabelText("UI��������(�����ֶ���д)")]
        public Dictionary<string, UIWindowData> UIWindowDataDic = new Dictionary<string, UIWindowData>();
        /// <summary>
        /// ��־����
        /// </summary>
        public class LogSetting
        {
            [LabelText("������־"), OnValueChanged("EnableLogValueChaged")] public bool enableLog = true;
            [LabelText("д��ʱ��"), OnValueChanged("EnableLogValueChaged")] public bool writeTime = true;
            [LabelText("д���߳�ID"), OnValueChanged("EnableLogValueChaged")] public bool writeThreadID = false;
            [LabelText("д���ջ"), OnValueChanged("EnableLogValueChaged")] public bool writeTrace = true;
            [LabelText("������־�ļ�"), OnValueChanged("EnableLogValueChaged")] public bool enableSave = false;
            [LabelText("��Ҫ�������־����"), HideIf("CheckSaveState"), EnumFlags, OnValueChanged("EnableLogValueChaged")] public JK.Log.LogType saveLogTypes;
            [LabelText("����·��,���persistentDataPath��·��"), HideIf("CheckSaveState"), OnValueChanged("EnableLogValueChaged")] public string savePath = "/Log";
            [LabelText("�Զ�����ļ���"), HideIf("CheckSaveState"), OnValueChanged("EnableLogValueChaged")]
            [InfoBox("�����д����ᵼ��ÿ�α��涼�Ǹ���ʽ�ģ��������д����ÿ���Զ�����Ϊʱ���������ļ�")]
            public string customSaveFileName = string.Empty;
#if UNITY_EDITOR
            public void InitOnEidtor()
            {
                EnableLogValueChaged();
            }

            [Button("����־")]
            private void OpenLog()
            {
                string path = Application.persistentDataPath + savePath;
                path = path.Replace("/", "\\");
                System.Diagnostics.Process.Start("explorer.exe", path);
            }

            private bool CheckSaveState()
            {
                return !enableSave;
            }

            private void EnableLogValueChaged()
            {
                if (enableLog)
                {
                    AddScriptCompilationSymbol("ENABLE_LOG");
                }
                else
                {
                    RemoveScriptCompilationSymbol("ENABLE_LOG");
                }
            }
#endif
        }

#if UNITY_EDITOR
        [Button("����")]
        public void Reset()
        {
            LogConfig = new LogSetting();
            LogConfig.InitOnEidtor();
            SetResourcesSystemType();
            SetSaveSystemType();
            InitUIWindowDataDicOnEditor();
        }

        public void InitOnEditor()
        {
            if (LogConfig != null) LogConfig.InitOnEidtor();
            SetResourcesSystemType();
            InitUIWindowDataDicOnEditor();
        }

        /// <summary>
        /// �޸���Դ����ϵͳ������
        /// </summary>
        public void SetResourcesSystemType()
        {
            switch (ResourcesSystemType)
            {
                case ResourcesSystemType.Resources:
                    RemoveScriptCompilationSymbol("ENABLE_ADDRESSABLES");
                    // ������ԴR.cs���������Ҫɾ��
                    string path = Application.dataPath + "/JKFrame//Scripts/2.System/3.Res/R.cs";
                    if (System.IO.File.Exists(path)) AssetDatabase.DeleteAsset("Assets/JKFrame//Scripts/2.System/3.Res/R.cs");
                    break;
                case ResourcesSystemType.Addressables:
                    AddScriptCompilationSymbol("ENABLE_ADDRESSABLES");
                    break;
            }
        }

        /// <summary>
        /// �޸Ĵ浵ϵͳ������
        /// </summary>
        private void SetSaveSystemType()
        {
            // ��մ浵
            SaveSystem.DeleteAll();
        }

        /// <summary>
        /// ����Ԥ����ָ��
        /// </summary>
        public static void AddScriptCompilationSymbol(string name)
        {
            BuildTargetGroup buildTargetGroup = UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup;
            string group = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!group.Contains(name))
            {
                UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, group + ";" + name);
            }
        }

        /// <summary>
        /// �Ƴ�Ԥ����ָ��
        /// </summary>
        public static void RemoveScriptCompilationSymbol(string name)
        {
            BuildTargetGroup buildTargetGroup = UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup;
            string group = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (group.Contains(name))
            {
                UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, group.Replace(";" + name, string.Empty));
            }
        }

        private void InitUIWindowDataDicOnEditor()
        {
            UIWindowDataDic.Clear();
            // ��ȡ���г���
            System.Reflection.Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            Type baseType = typeof(UI_WindowBase);
            // ��������
            foreach (System.Reflection.Assembly assembly in asms)
            {
                // ���������µ�ÿһ������
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (baseType.IsAssignableFrom(type)
                        && !type.IsAbstract)
                    {
                        var attributes = type.GetCustomAttributes<UIWindowDataAttribute>();
                        foreach (var attribute in attributes)
                        {
                            UIWindowDataDic.Add(attribute.windowKey,
                                new UIWindowData(attribute.isCache, attribute.assetPath, attribute.layerNum));
                        }

                    }
                }
            }
        }
#endif

    }
}