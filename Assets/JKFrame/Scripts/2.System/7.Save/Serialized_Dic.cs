using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// �����л����ֵ䣬֧�ֶ����ƺ�JsonUtility
/// </summary>
/// <remarks>���ʹ��URP��HDRP��Ⱦ���ߣ��ٷ��ṩ�����л��ֵ�Ϊ<see cref="UnityEngine.Rendering.SerializedDictionary{K,V}"/>"</remarks>
[Serializable]
public class Serialized_Dic<K, V> : ISerializationCallbackReceiver
{
    [SerializeField] private List<K> keyList;
    [SerializeField] private List<V> valueList;

    [NonSerialized] //�����л�  ���ⱨ��
    private Dictionary<K, V> dictionary;

    public Dictionary<K, V> Dictionary
    {
        get => dictionary;
    }

    public Serialized_Dic()
    {
        dictionary = new Dictionary<K, V>();
    }

    public Serialized_Dic(Dictionary<K, V> dictionary)
    {
        this.dictionary = dictionary;
    }

    //���л���ʱ����ֵ���������ݷŽ�list
    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
        OnBeforeSerialize();
    }

    //�����л���ʱ���Զ�����ֵ�ĳ�ʼ��
    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        OnAfterDeserialize();
    }

    /// <summary>
    /// Unity���л�ǰ����
    /// </summary>
    public void OnBeforeSerialize()
    {
        keyList = new List<K>(dictionary.Keys);
        valueList = new List<V>(dictionary.Values);
    }
    /// <summary>
    /// Unity�����л������
    /// </summary>
    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<K, V>();
        for (int i = 0; i < keyList.Count; i++)
            dictionary.Add(keyList[i], valueList[i]);

        keyList.Clear();
        valueList.Clear();
    }
}