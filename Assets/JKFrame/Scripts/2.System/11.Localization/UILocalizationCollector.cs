using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JKFrame;
using System;

/// <summary>
/// ���ػ��ռ���
/// </summary>
public class UILocalizationCollector : SerializedMonoBehaviour
{
    public LocalizationConfig localizationConfig;
    [TableList]
    public List<UILocalizationData> localizationDataList = new List<UILocalizationData>();
    private Action<object, string> analyzer;
    private void Reset()
    {
        UI_WindowBase window = GetComponent<UI_WindowBase>();
        if (window != null && window.localizationConfig != null)
        {
            localizationConfig = window.localizationConfig;
        }
    }
    private void OnEnable()
    {
        LocalizationSystem.RegisterLanguageEvent(OnUpdateLanguage);
        OnUpdateLanguage(LocalizationSystem.LanguageType);
    }
    private void OnDisable()
    {
        LocalizationSystem.UnregisterLanguageEvent(OnUpdateLanguage);
    }
    private void OnUpdateLanguage(LanguageType type)
    {
        foreach (UILocalizationData item in localizationDataList)
        {
            Analysis(item.component, item.key, type);
        }
    }

    /// <summary>
    /// ��ʼ��������
    /// ��������������������Ͳ�ͬ���ز�ͬ������
    /// </summary>
    /// <param name="analyzer"></param>
    public void InitAnalyzer(Action<object, string> analyzer)
    {
        this.analyzer = analyzer;
    }
    /// <summary>
    /// ���Ȳ����ⲿ�������Ľ�����
    /// ���û��������ڲ��򵥽������������ڱ���������Ѱ�ң����û������ȫ��������Ѱ��
    /// </summary>
    /// <param name="component"></param>
    /// <param name="key"></param>
    /// <param name="languageType"></param>
    public void Analysis(MaskableGraphic component, string key, LanguageType languageType)
    {
        if (component == null) return;
        if (analyzer != null)
        {
            analyzer.Invoke(component, key);
            return;
        }

        // ���ü򵥽���
        if (component is Text)
        {
            LocalizationStringData data = localizationConfig.GetContent<LocalizationStringData>(key, languageType);
            if (data == null) data = LocalizationSystem.GetContent<LocalizationStringData>(key, languageType);
            if (data != null) ((Text)component).text = data.content;
        }
        else if (component is Image)
        {
            LocalizationImageData data = localizationConfig.GetContent<LocalizationImageData>(key, languageType);
            if (data == null) data = LocalizationSystem.GetContent<LocalizationImageData>(key, languageType);
            if (data != null) ((Image)component).sprite = data.content;
        }
    }
}

public class UILocalizationData
{
    public MaskableGraphic component;
    public string key;
}
