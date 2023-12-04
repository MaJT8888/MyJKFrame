using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JKFrame
{
    /// <summary>
    /// ������Ϸֻ��һ��Update��LateUpdate��
    /// </summary>
    public class MonoSystem : MonoBehaviour
    {
        private MonoSystem() { }
        private static MonoSystem instance;
        private Action updateEvent;
        private Action lateUpdateEvent;
        private Action fixedUpdateEvent;

        public static void Init()
        {
            instance = JKFrameRoot.RootTransform.GetComponent<MonoSystem>();
            instance.updateEvent = null;
            instance.lateUpdateEvent = null;
            instance.fixedUpdateEvent = null;
        }

        #region �������ں���
        /// <summary>
        /// ���Update����
        /// </summary>
        /// <param name="action"></param>
        public static void AddUpdateListener(Action action)
        {
            instance.updateEvent += action;
        }
        /// <summary>
        /// �Ƴ�Update����
        /// </summary>
        /// <param name="action"></param>
        public static void RemoveUpdateListener(Action action)
        {
            instance.updateEvent -= action;
        }

        /// <summary>
        /// ���LateUpdate����
        /// </summary>
        /// <param name="action"></param>
        public static void AddLateUpdateListener(Action action)
        {
            instance.lateUpdateEvent += action;
        }
        /// <summary>
        /// �Ƴ�LateUpdate����
        /// </summary>
        /// <param name="action"></param>
        public static void RemoveLateUpdateListener(Action action)
        {
            instance.lateUpdateEvent -= action;
        }

        /// <summary>
        /// ���FixedUpdate����
        /// </summary>
        /// <param name="action"></param>
        public static void AddFixedUpdateListener(Action action)
        {
            instance.fixedUpdateEvent += action;
        }
        /// <summary>
        /// �Ƴ�FixedUpdate����
        /// </summary>
        /// <param name="action"></param>
        public static void RemoveFixedUpdateListener(Action action)
        {
            instance.fixedUpdateEvent -= action;
        }

        private void Update()
        {
            updateEvent?.Invoke();
        }
        private void LateUpdate()
        {
            lateUpdateEvent?.Invoke();
        }
        private void FixedUpdate()
        {
            fixedUpdateEvent?.Invoke();
        }
        #endregion

        #region Э��
        private Dictionary<object, List<Coroutine>> coroutineDic = new Dictionary<object, List<Coroutine>>();
        private static ObjectPoolModule poolModule = new ObjectPoolModule();

        /// <summary>
        /// ����һ��Э��
        /// </summary>
        /// <param name="coroutine"></param>
        /// <returns></returns>
        public static Coroutine Start_Coroutine(IEnumerator coroutine)
        {
            return instance.StartCoroutine(coroutine);
        }

        /// <summary>
        /// ����һ��Э�̲��Ұ�ĳ������
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="coroutine"></param>
        /// <returns></returns>
        public static Coroutine Start_Coroutine(object obj, IEnumerator coroutine)
        {
            Coroutine _coroutine = instance.StartCoroutine(coroutine);
            if (!instance.coroutineDic.TryGetValue(obj, out List<Coroutine> coroutineList))
            {
                coroutineList = poolModule.GetObject<List<Coroutine>>();
                if (coroutineList == null) coroutineList = new List<Coroutine>();
                instance.coroutineDic.Add(obj, coroutineList);
            }
            coroutineList.Add(_coroutine);
            return _coroutine;
        }

        /// <summary>
        /// ֹͣһ��Э�̲�����ĳ������
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="routine"></param>
        public static void Stop_Coroutine(object obj, Coroutine routine)
        {
            if (instance.coroutineDic.TryGetValue(obj, out List<Coroutine> coroutineList))
            {
                instance.StopCoroutine(routine);
                coroutineList.Remove(routine);
            }
        }

        /// <summary>
        /// ֹͣһ��Э��
        /// </summary>
        /// <param name="routine"></param>
        public static void Stop_Coroutine(Coroutine routine)
        {
            instance.StopCoroutine(routine);
        }

        /// <summary>
        /// ֹͣĳ�������ȫ��Э��
        /// </summary>
        /// <param name="obj"></param>
        public static void StopAllCoroutine(object obj)
        {
            if (instance.coroutineDic.Remove(obj, out List<Coroutine> coroutineList))
            {
                for (int i = 0; i < coroutineList.Count; i++)
                {
                    instance.StopCoroutine(coroutineList[i]);
                }
                coroutineList.Clear();
                poolModule.PushObject(coroutineList);
            }
        }

        /// <summary>
        /// ����ϵͳȫ��Э�̶���ֹͣ
        /// </summary>
        public static void StopAllCoroutine()
        {
            //ȫ�����ݶ�����Ч
            foreach (var item in instance.coroutineDic.Values)
            {
                item.Clear();
                poolModule.PushObject(item);
            }
            instance.coroutineDic.Clear();
            instance.StopAllCoroutines();
        }
        #endregion
    }

}