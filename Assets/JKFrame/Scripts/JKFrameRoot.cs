using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JKFrame
{
    using System;
#if UNITY_EDITOR
    using UnityEditor;
    [InitializeOnLoad]
#endif
    [DefaultExecutionOrder(-20)]
    /// <summary>
    /// ��ܸ��ڵ�
    /// </summary>
    public class JKFrameRoot : MonoBehaviour
    {
        private JKFrameRoot() { }
        private static JKFrameRoot Instance;
        public static Transform RootTransform { get; private set; }
        public static JKFrameSetting Setting { get => Instance.FrameSetting; }
        //��ܲ���������ļ�
        [SerializeField] JKFrameSetting FrameSetting;
        private void Awake()
        {
            if (Instance != null && Instance != this)//��ֹEditor�µ�Instance�Ѿ����ڣ�����������
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            RootTransform = transform;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            InitSystems();
        }
        #region System
        private void InitSystems()
        {
            PoolSystem.Init();
            EventSystem.Init();
            MonoSystem.Init();
            AudioSystem.Init();
            UISystem.Init();
            SaveSystem.Init();
            LocalizationSystem.Init();
#if ENABLE_LOG
            JKLog.Init(FrameSetting.LogConfig);
#endif
        }
        #endregion

        private void OnDisable()
        {
#if ENABLE_LOG
            JKLog.Close();
#endif
        }
        #region Editor
#if UNITY_EDITOR
        //�༭��ר���¼�ϵͳ
        public static EventModule EditorEventModule;
        static JKFrameRoot()
        {
            EditorEventModule = new EventModule();
            EditorApplication.update += () =>
            {
                InitForEditor();
            };
        }
        [InitializeOnLoadMethod]
        public static void InitForEditor()
        {
            //��ǰ�Ƿ�Ҫ���в��Ż�׼��������
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }
            if (Instance == null)
            {
                Instance = GameObject.FindObjectOfType<JKFrameRoot>();
                if (Instance == null) return;
                Instance.FrameSetting.InitOnEditor();
                //// ���������д��ڶ�����һ��Show
                //UI_WindowBase[] window = Instance.transform.GetComponentsInChildren<UI_WindowBase>();
                //foreach (UI_WindowBase win in window)
                //{
                //    win.ShowGeneralLogic();
                //}
            }
        }
#endif
        #endregion
    }
}