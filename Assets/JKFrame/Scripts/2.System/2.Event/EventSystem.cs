using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JKFrame
{
    /// <summary>
    /// �¼�ϵͳ������
    /// </summary>
    public static class EventSystem
    {
        private static EventModule eventModule;
        public static void Init()
        {
            eventModule = new EventModule();
        }
        #region ����¼��ļ���������Ҫ����ĳ���¼���������¼�����ʱ����ִ���㴫�ݹ�����Action
        /// <summary>
        /// ����޲��¼�
        /// </summary>
        public static void AddEventListener(string eventName, Action action)
        {
            eventModule.AddEventListener(eventName, action);
        }

        /// <summary>
        /// ���1�������¼�
        /// </summary>
        public static void AddEventListener<T>(string eventName, Action<T> action)
        {
            eventModule.AddEventListener<Action<T>>(eventName, action);
        }
        /// <summary>
        /// ���2�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���3�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���4�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���5�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���6�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(string eventName, Action<T0, T1, T2, T3, T4, T5> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���7�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���8�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���9�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���10�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���11�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���12�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���13�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T2> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���14�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���15�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        /// <summary>
        /// ���16�������¼�
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            eventModule.AddEventListener(eventName, action);
        }
        #endregion

        #region �����¼�
        /// <summary>
        /// �����޲ε��¼�
        /// </summary>
        public static void EventTrigger(string eventName)
        {
            eventModule.EventTrigger(eventName);
        }
        /// <summary>
        /// ����1���������¼�
        /// </summary>
        public static void EventTrigger<T>(string eventName, T arg)
        {
            eventModule.EventTrigger<T>(eventName, arg);
        }
        /// <summary>
        /// ����2���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1>(string eventName, T0 arg0, T1 arg1)
        {
            eventModule.EventTrigger(eventName, arg0, arg1);
        }
        /// <summary>
        /// ����3���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2>(string eventName, T0 arg0, T1 arg1, T2 arg2)
        {
            eventModule.EventTrigger<T0, T1, T2>(eventName, arg0, arg1, arg2);
        }
        /// <summary>
        /// ����4���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            eventModule.EventTrigger<T0, T1, T2, T3>(eventName, arg0, arg1, arg2, arg3);
        }
        /// <summary>
        /// ����5���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4>(eventName, arg0, arg1, arg2, arg3, arg4);
        }
        /// <summary>
        /// ����6���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5>(eventName, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        /// <summary>
        /// ����7���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        /// <summary>
        /// ����8���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        /// <summary>
        /// ����9���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
        /// <summary>
        /// ����10���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// ����11���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }


        /// <summary>
        /// ����12���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// ����13���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// ����14���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// ����15���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        /// <summary>
        /// ����16���������¼�
        /// </summary>
        public static void EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            eventModule.EventTrigger<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(eventName, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }
        #endregion

        #region ȡ���¼��ļ���
        /// <summary>
        /// �Ƴ��޲ε��¼�����
        /// </summary>
        public static void RemoveEventListener(string eventName, Action action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�1���������¼�����
        /// </summary>
        public static void RemoveEventListener<T>(string eventName, Action<T> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�2���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�3���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�4���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�5���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�6���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5>(string eventName, Action<T0, T1, T2, T3, T4, T5> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�7���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�8���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�9���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�10���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�11���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�12���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�13���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�14���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�15���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        /// <summary>
        /// �Ƴ�16���������¼�����
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            eventModule.RemoveEventListener(eventName, action);
        }
        #endregion

        #region �Ƴ��¼�
        /// <summary>
        /// �Ƴ�/ɾ��һ���¼�
        /// </summary>
        /// <param name="eventName"></param>
        public static void RemoveEvent(string eventName)
        {
            eventModule.RemoveEvent(eventName);
        }
        /// <summary>
        /// ����¼�����
        /// </summary>
        public static void Clear()
        {
            eventModule.Clear();
        }
        #endregion

        #region �����¼�
        /// <summary>
        /// ��������¼��ļ���
        /// ����������T��������Ϊ�¼�����
        /// </summary>
        /// <typeparam name="T">�������ͣ�����Ϊstruct����</typeparam>
        /// <param name="action">�ص�����</param>
        public static void AddTypeEventListener<T>(Action<T> action)
        {
            AddEventListener<T>(nameof(T), action);
        }
        /// <summary>
        /// �Ƴ�/ɾ��һ�������¼�
        /// </summary>
        /// <typeparam name="T">�¼��Ĳ�������</typeparam>
        public static void RemoveTypeEvent<T>()
        {
            RemoveEvent(nameof(T));
        }
        /// <summary>
        /// �Ƴ������¼��ļ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void RemoveTypeEventListener<T>(Action<T> action)
        {
            eventModule.RemoveEventListener(nameof(T), action);
        }
        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        public static void TypeEventTrigger<T>(T arg)
        {
            EventTrigger(nameof(T), arg);
        }
        #endregion
    }
}