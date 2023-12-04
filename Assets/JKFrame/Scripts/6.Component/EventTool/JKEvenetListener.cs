using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace JKFrame
{
    /// <summary>
    /// �¼�����,ö�ٱ�������int�������������Զ����¼���ֻ��Ҫö�ٵ�ֵ���·����ظ�����
    /// </summary>
    public enum JKEventType
    {
        OnMouseEnter = -10001,
        OnMouseExit = -10002,
        OnClick = -10003,
        OnClickDown = -10004,
        OnClickUp = -10005,
        OnDrag = -10006,
        OnBeginDrag = -10007,
        OnEndDrag = -10008,
        OnCollisionEnter = -10009,
        OnCollisionStay = -10010,
        OnCollisionExit = -10011,
        OnCollisionEnter2D = -10012,
        OnCollisionStay2D = -10013,
        OnCollisionExit2D = -10014,
        OnTriggerEnter = -10015,
        OnTriggerStay = -10016,
        OnTriggerExit = -10017,
        OnTriggerEnter2D = -10018,
        OnTriggerStay2D = -10019,
        OnTriggerExit2D = -10020,
        OnReleaseAddressableAsset = -10021,
        OnDestroy = -10022,
    }

    public interface IMouseEvent : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    { }

    /// <summary>
    /// �¼�����
    /// ������� ��ꡢ��ײ���������¼�
    /// </summary>
    public class JKEventListener : MonoBehaviour, IMouseEvent
    {
        private static ObjectPoolModule poolModul = new ObjectPoolModule();
        #region �ڲ��ࡢ�ӿڵ�
        /// <summary>
        /// ���йؼ��ֵ����ݣ���Ҫ���ڽ�������÷���������
        /// </summary>
        private class JKEventListenerData
        {
            public Dictionary<int, IJKEventListenerEventInfos> eventInfoDic = new Dictionary<int, JKEventListener.IJKEventListenerEventInfos>();
        }


        private interface IJKEventListenerEventInfo<T>
        {
            void TriggerEvent(T eventData);
            void Destory();
        }

        /// <summary>
        /// ĳ���¼���һ���¼������ݰ�װ��
        /// </summary>
        private class JKEventListenerEventInfo<T, TEventArg> : IJKEventListenerEventInfo<T>
        {
            // T���¼�����Ĳ�����PointerEventData��Collision��
            // object[]:�¼��Ĳ���
            public Action<T, TEventArg> action;
            public TEventArg arg;
            public void Init(Action<T, TEventArg> action, TEventArg args = default(TEventArg))
            {
                this.action = action;
                this.arg = args;
            }
            public void Destory()
            {
                this.action = null;
                this.arg = default(TEventArg);
                poolModul.PushObject(this);
            }
            public void TriggerEvent(T eventData)
            {
                action?.Invoke(eventData, arg);
            }
        }


        /// <summary>
        /// ĳ���¼���һ���¼������ݰ�װ�ࣨ�޲Σ�
        /// </summary>
        private class JKEventListenerEventInfo<T> : IJKEventListenerEventInfo<T>
        {
            // T���¼�����Ĳ�����PointerEventData��Collision��
            // object[]:�¼��Ĳ���
            public Action<T> action;
            public void Init(Action<T> action)
            {
                this.action = action;
            }
            public void Destory()
            {
                this.action = null;
                poolModul.PushObject(this);
            }
            public void TriggerEvent(T eventData)
            {
                action?.Invoke(eventData);
            }
        }


        interface IJKEventListenerEventInfos
        {
            void RemoveAll();

        }

        /// <summary>
        /// һ���¼������ݰ�װ���ͣ��������JKEventListenerEventInfo
        /// </summary>
        private class JKEventListenerEventInfos<T> : IJKEventListenerEventInfos
        {
            // ���е��¼�
            private List<IJKEventListenerEventInfo<T>> eventList = new List<IJKEventListenerEventInfo<T>>();


            /// <summary>
            /// ����¼� �޲�
            /// </summary>
            public void AddListener(Action<T> action)
            {
                JKEventListenerEventInfo<T> info = poolModul.GetObject<JKEventListenerEventInfo<T>>();
                if (info == null) info = new JKEventListenerEventInfo<T>();
                info.Init(action);
                eventList.Add(info);
            }

            /// <summary>
            /// ����¼� �в�
            /// </summary>
            public void AddListener<TEventArg>(Action<T, TEventArg> action, TEventArg args = default(TEventArg))
            {
                JKEventListenerEventInfo<T, TEventArg> info = poolModul.GetObject<JKEventListenerEventInfo<T, TEventArg>>();
                if (info == null) info = new JKEventListenerEventInfo<T, TEventArg>();
                info.Init(action, args);
                eventList.Add(info);
            }

            public void TriggerEvent(T evetData)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].TriggerEvent(evetData);
                }
            }


            /// <summary>
            /// �Ƴ��¼����޲Σ�
            /// ͬһ������+����ע�����Σ�������θ÷���ֻ���Ƴ�һ���¼�
            /// </summary>
            public void RemoveListener(Action<T> action)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    JKEventListenerEventInfo<T> eventInfo = eventList[i] as JKEventListenerEventInfo<T>;
                    if (eventInfo == null) continue; // ���Ͳ���

                    // �ҵ�����¼����鿴�Ƿ����
                    if (eventInfo.action.Equals(action))
                    {
                        // �Ƴ�
                        eventInfo.Destory();
                        eventList.RemoveAt(i);
                        return;
                    }
                }
            }

            /// <summary>
            /// �Ƴ��¼����вΣ�
            /// ͬһ������+����ע�����Σ�������θ÷���ֻ���Ƴ�һ���¼�
            /// </summary>
            public void RemoveListener<TEventArg>(Action<T, TEventArg> action, TEventArg args = default(TEventArg))
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    JKEventListenerEventInfo<T, TEventArg> eventInfo = eventList[i] as JKEventListenerEventInfo<T, TEventArg>;
                    if (eventInfo == null) continue; // ���Ͳ���

                    // �ҵ�����¼����鿴�Ƿ����
                    if (eventInfo.action.Equals(action))
                    {
                        // �Ƴ�
                        eventInfo.Destory();
                        eventList.RemoveAt(i);
                        return;
                    }
                }
            }

            /// <summary>
            /// �Ƴ�ȫ����ȫ���Ž������
            /// </summary>
            public void RemoveAll()
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].Destory();
                }
                eventList.Clear();
                poolModul.PushObject(this);
            }
        }

        #endregion

        private JKEventListenerData data;
        private JKEventListenerData Data
        {
            get
            {
                if (data == null)
                {
                    data = poolModul.GetObject<JKEventListenerData>();
                    if (data == null) data = new JKEventListenerData();
                }
                return data;
            }
        }

        #region �ⲿ�ķ���
        /// <summary>
        /// ����޲��¼� 
        /// </summary>
        public void AddListener<T>(int eventTypeInt, Action<T> action)
        {
            if (Data.eventInfoDic.TryGetValue(eventTypeInt, out IJKEventListenerEventInfos info))
            {
                ((JKEventListenerEventInfos<T>)info).AddListener(action);
            }
            else
            {
                JKEventListenerEventInfos<T> infos = poolModul.GetObject<JKEventListenerEventInfos<T>>();
                if (infos == null) infos = new JKEventListenerEventInfos<T>();
                infos.AddListener(action);
                Data.eventInfoDic.Add(eventTypeInt, infos);
            }
        }

        /// <summary>
        /// ����¼����вΣ�
        /// </summary>
        public void AddListener<T, TEventArg>(int eventTypeInt, Action<T, TEventArg> action, TEventArg args)
        {
            if (Data.eventInfoDic.TryGetValue(eventTypeInt, out IJKEventListenerEventInfos info))
            {
                ((JKEventListenerEventInfos<T>)info).AddListener(action, args);
            }
            else
            {
                JKEventListenerEventInfos<T> infos = poolModul.GetObject<JKEventListenerEventInfos<T>>();
                if (infos == null) infos = new JKEventListenerEventInfos<T>();
                infos.AddListener(action, args);
                Data.eventInfoDic.Add(eventTypeInt, infos);
            }
        }


        /// <summary>
        /// ����¼����޲Σ�
        /// </summary>
        public void AddListener<T>(JKEventType eventType, Action<T> action)
        {
            AddListener((int)eventType, action);
        }
        /// <summary>
        /// ����¼����вΣ�
        /// </summary>
        public void AddListener<T, TEventArg>(JKEventType eventType, Action<T, TEventArg> action, TEventArg args)
        {
            AddListener((int)eventType, action, args);
        }


        /// <summary>
        /// �Ƴ��¼����޲Σ�
        /// </summary>
        public void RemoveListener<T>(int eventTypeInt, Action<T> action)
        {
            if (Data.eventInfoDic.TryGetValue(eventTypeInt, out IJKEventListenerEventInfos info))
            {
                ((JKEventListenerEventInfos<T>)info).RemoveListener(action);
            }
        }
        /// <summary>
        /// �Ƴ��¼����޲Σ�
        /// </summary>
        public void RemoveListener<T>(JKEventType eventType, Action<T> action)
        {
            RemoveListener((int)eventType, action);
        }


        /// <summary>
        /// �Ƴ��¼����вΣ�
        /// </summary>
        public void RemoveListener<T, TEventArg>(int eventTypeInt, Action<T, TEventArg> action)
        {
            if (Data.eventInfoDic.TryGetValue(eventTypeInt, out IJKEventListenerEventInfos info))
            {
                ((JKEventListenerEventInfos<T>)info).RemoveListener(action);
            }
        }
        /// <summary>
        /// �Ƴ��¼����вΣ�
        /// </summary>
        public void RemoveListener<T, TEventArg>(JKEventType eventType, Action<T, TEventArg> action)
        {
            RemoveListener((int)eventType, action);
        }

        /// <summary>
        /// �Ƴ�ĳһ���¼������µ�ȫ���¼�
        /// </summary>
        public void RemoveAllListener(JKEventType eventType)
        {
            RemoveAllListener((int)eventType);
        }

        /// <summary>
        /// �Ƴ�ĳһ���¼������µ�ȫ���¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        public void RemoveAllListener(int eventType)
        {
            if (Data.eventInfoDic.TryGetValue(eventType, out IJKEventListenerEventInfos infos))
            {
                infos.RemoveAll();
                Data.eventInfoDic.Remove(eventType);
            }
        }

        /// <summary>
        /// �Ƴ�ȫ���¼�
        /// </summary>
        public void RemoveAllListener()
        {
            foreach (IJKEventListenerEventInfos infos in Data.eventInfoDic.Values)
            {
                infos.RemoveAll();
            }

            data.eventInfoDic.Clear();
            // ����������������������
            poolModul.PushObject(data);
            data = null;
        }

        #endregion
        /// <summary>
        /// �����¼�
        /// </summary>
        public void TriggerAction<T>(int eventTypeInt, T eventData)
        {
            if (Data.eventInfoDic.TryGetValue(eventTypeInt, out IJKEventListenerEventInfos infos))
            {
                (infos as JKEventListenerEventInfos<T>).TriggerEvent(eventData);
            }
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        public void TriggerAction<T>(JKEventType eventType, T eventData)
        {
            TriggerAction<T>((int)eventType, eventData);
        }

        #region ����¼�
        public void OnPointerEnter(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnMouseEnter, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnMouseExit, eventData);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnBeginDrag, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnDrag, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnEndDrag, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnClick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnClickDown, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TriggerAction(JKEventType.OnClickUp, eventData);
        }
        #endregion

        #region ��ײ�¼�
        private void OnCollisionEnter(Collision collision)
        {
            TriggerAction(JKEventType.OnCollisionEnter, collision);
        }
        private void OnCollisionStay(Collision collision)
        {
            TriggerAction(JKEventType.OnCollisionStay, collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            TriggerAction(JKEventType.OnCollisionExit, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TriggerAction(JKEventType.OnCollisionEnter2D, collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TriggerAction(JKEventType.OnCollisionStay2D, collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            TriggerAction(JKEventType.OnCollisionExit2D, collision);
        }
        #endregion

        #region �����¼�
        private void OnTriggerEnter(Collider other)
        {
            TriggerAction(JKEventType.OnTriggerEnter, other);
        }
        private void OnTriggerStay(Collider other)
        {
            TriggerAction(JKEventType.OnTriggerStay, other);
        }
        private void OnTriggerExit(Collider other)
        {
            TriggerAction(JKEventType.OnTriggerExit, other);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerAction(JKEventType.OnTriggerEnter2D, collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            TriggerAction(JKEventType.OnTriggerStay2D, collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerAction(JKEventType.OnTriggerExit2D, collision);
        }
        #endregion

        #region �����¼�
        private void OnDestroy()
        {
            TriggerAction(JKEventType.OnReleaseAddressableAsset, gameObject);
            TriggerAction(JKEventType.OnDestroy, gameObject);

            // �����������ݣ�����һЩ���ݷŻض������
            RemoveAllListener();
        }
        #endregion
    }
}