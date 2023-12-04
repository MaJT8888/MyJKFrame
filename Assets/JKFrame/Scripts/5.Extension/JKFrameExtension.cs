using System;
using System.Collections;
using UnityEngine;

namespace JKFrame
{
    public static class JKFrameExtension
    {
        #region ͨ��  
        /// <summary>
        /// ������ȶԱ�
        /// </summary>
        public static bool ArraryEquals(this object[] objs, object[] other)
        {
            if (other == null || objs.GetType() != other.GetType())
            {
                return false;
            }
            if (objs.Length == other.Length)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!objs[i].Equals(other[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        #endregion
        #region GameObject
        public static bool IsNull(this GameObject obj)
        {
            return ReferenceEquals(obj, null);
        }
        #endregion
        #region ��Դ����
        /// <summary>
        /// GameObject��������
        /// </summary>
        public static void GameObjectPushPool(this GameObject go)
        {
            if (go.IsNull())
            {
                JKLog.Error("���������������");
            }
            else
            {
                PoolSystem.PushGameObject(go);
            }

        }

        /// <summary>
        /// GameObject��������
        /// </summary>
        public static void GameObjectPushPool(this Component com)
        {
            GameObjectPushPool(com.gameObject);
        }

        /// <summary>
        /// ��ͨ��Ž�����
        /// </summary>
        public static void ObjectPushPool(this object obj)
        {
            PoolSystem.PushObject(obj);
        }
        #endregion
        #region Mono

        /// <summary>
        /// ���Update����
        /// </summary>
        public static void AddUpdate(this object obj, Action action)
        {
            MonoSystem.AddUpdateListener(action);
        }
        /// <summary>
        /// �Ƴ�Update����
        /// </summary>
        public static void RemoveUpdate(this object obj, Action action)
        {
            MonoSystem.RemoveUpdateListener(action);
        }

        /// <summary>
        /// ���LateUpdate����
        /// </summary>
        public static void AddLateUpdate(this object obj, Action action)
        {
            MonoSystem.AddLateUpdateListener(action);
        }
        /// <summary>
        /// �Ƴ�LateUpdate����
        /// </summary>
        public static void RemoveLateUpdate(this object obj, Action action)
        {
            MonoSystem.RemoveLateUpdateListener(action);
        }

        /// <summary>
        /// ���FixedUpdate����
        /// </summary>
        public static void AddFixedUpdate(this object obj, Action action)
        {
            MonoSystem.AddFixedUpdateListener(action);
        }
        /// <summary>
        /// �Ƴ�Update����
        /// </summary>
        public static void RemoveFixedUpdate(this object obj, Action action)
        {
            MonoSystem.RemoveFixedUpdateListener(action);
        }

        public static Coroutine StartCoroutine(this object obj, IEnumerator routine)
        {
            return MonoSystem.Start_Coroutine(obj, routine);
        }

        public static void StopCoroutine(this object obj, Coroutine routine)
        {
            MonoSystem.Stop_Coroutine(obj, routine);
        }

        /// <summary>
        /// �ر�ȫ��Э�̣�ע��ֻ��رյ��ö���������Э��
        /// </summary>
        /// <param name="obj"></param>
        public static void StopAllCoroutine(this object obj)
        {
            MonoSystem.StopAllCoroutine(obj);
        }

        #endregion
    }
}