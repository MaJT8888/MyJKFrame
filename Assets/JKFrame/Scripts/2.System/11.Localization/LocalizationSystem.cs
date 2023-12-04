using JKFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    private static LocalizationSystem instance;
    private const string OnUpdaterLanguage = "OnUpdaterLanguage";

    /// <summary>
    /// ���ʻ������������ͣ�����ʱ���Զ��ַ������޸� �¼�
    /// </summary>
    public static LanguageType LanguageType
    {
        get { return instance.languageType; }
        set
        {
            instance.languageType = value;
            OnLanguageValueChanged();

        }
    }
    public static void Init()
    {
        instance = JKFrameRoot.RootTransform.GetComponentInChildren<LocalizationSystem>();
    }

    /// <summary>
    /// ȫ�ֵ�����
    /// ��������ʱ�޸Ĵ�����
    /// </summary>
    [SerializeField] private LocalizationConfig globalConfig;

    [SerializeField] private LanguageType languageType;

    private void OnValidate()
    {
        OnLanguageValueChanged();
    }
    public static void OnLanguageValueChanged()
    {
        if (instance == null) return; // Ӧ��û������
        EventSystem.EventTrigger(OnUpdaterLanguage, instance.languageType);
    }

    /// <summary>
    /// ��ȡ���ݣ���������ڻ᷵��Null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="languageType"></param>
    /// <returns></returns>
    public static T GetContent<T>(string key, LanguageType languageType) where T : LocalizationDataBase => instance.GetContentByKey<T>(key, languageType);

    public T GetContentByKey<T>(string key, LanguageType languageType) where T : LocalizationDataBase
    {
        if (globalConfig == null)
        {
            JKLog.Warning("ȱ��globalConfig");
            return null;
        }
        return globalConfig.GetContent<T>(key, languageType);
    }

    public static void RegisterLanguageEvent(Action<LanguageType> action)
    {
        EventSystem.AddEventListener(OnUpdaterLanguage, action);
    }

    public static void UnregisterLanguageEvent(Action<LanguageType> action)
    {
        EventSystem.RemoveEventListener(OnUpdaterLanguage, action);
    }
}
