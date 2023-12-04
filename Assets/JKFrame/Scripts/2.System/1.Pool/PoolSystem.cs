using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JKFrame.GameObjectPoolModule;
using Unity.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JKFrame
{
    /// <summary>
    /// �����ϵͳ
    /// </summary>
    public static class PoolSystem
    {
        #region �����ϵͳ���ݼ���̬���췽��
        /// <summary>
        /// ����ز��������Ϸ�������ƣ����ڽ�������Ҳ�Ž������
        /// </summary>
        public const string PoolLayerGameObjectName = "PoolLayerGameObjectName";
        private static GameObjectPoolModule GameObjectPoolModule;
        private static ObjectPoolModule ObjectPoolModule;
        private static Transform poolRootTransform;
        public static void Init()
        {
            GameObjectPoolModule = new GameObjectPoolModule();
            ObjectPoolModule = new ObjectPoolModule();
            poolRootTransform = new GameObject("PoolRoot").transform;
            poolRootTransform.position = Vector3.zero;
            poolRootTransform.SetParent(JKFrameRoot.RootTransform);
            GameObjectPoolModule.Init(poolRootTransform);
        }
        #endregion

        #region GameObject��������API����ʼ����ȡ�������롢��գ�
        /// <summary>
        /// ��ʼ��һ��GameObject���͵Ķ��������
        /// </summary>
        /// <param name="keyName">��Դ����</param>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="prefab">��дĬ������ʱԤ�ȷ���Ķ���</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        public static void InitGameObjectPool(string keyName, int maxCapacity = -1, GameObject prefab = null, int defaultQuantity = 0)
        {
            GameObjectPoolModule.InitObjectPool(keyName, maxCapacity, prefab, defaultQuantity);
#if UNITY_EDITOR
            if (JKFrameRoot.EditorEventModule != null)
                JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnInitGameObjectPool", keyName, defaultQuantity);
#endif
        }
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="maxCapacity">���������-1��������</param>
        /// <param name="gameObjects">Ĭ��Ҫ�Ž����Ķ�������</param>
        public static void InitGameObjectPool(string keyName, int maxCapacity, GameObject[] gameObjects = null)
        {
            GameObjectPoolModule.InitObjectPool(keyName, maxCapacity, gameObjects);
#if UNITY_EDITOR
            if (JKFrameRoot.EditorEventModule != null)
                JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnInitGameObjectPool", keyName, gameObjects.Length);
#endif
        }
        /// <summary>
        /// ��ʼ������ز���������
        /// </summary>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        /// <param name="prefab">��дĬ������ʱԤ�ȷ���Ķ���</param>
        public static void InitGameObjectPool(GameObject prefab, int maxCapacity = -1, int defaultQuantity = 0)
        {
            InitGameObjectPool(prefab.name, maxCapacity, prefab, defaultQuantity);
        }
        /// <summary>
        /// ��ȡGameObject��û���򷵻�Null
        /// </summary>
        public static GameObject GetGameObject(string keyName, Transform parent = null)
        {
            GameObject go = GameObjectPoolModule.GetObject(keyName, parent);
#if UNITY_EDITOR
            if (go != null && JKFrameRoot.EditorEventModule != null) JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnGetGameObject", keyName, 1);
#endif
            return go;
        }

        /// <summary>
        /// ��ȡGameObject��û���򷵻�Null
        /// T:���
        /// </summary>
        public static T GetGameObject<T>(string keyName, Transform parent = null) where T : Component
        {
            GameObject go = GetGameObject(keyName, parent);
            if (go != null) return go.GetComponent<T>();
            else return null;
        }
        /// <summary>
        /// ��Ϸ������ö������
        /// </summary>
        /// <param name="keyName">������е�key</param>
        /// <param name="obj">���������</param>
        public static bool PushGameObject(string keyName, GameObject obj)
        {
            if (!obj.IsNull())
            {
                bool res = GameObjectPoolModule.PushObject(keyName, obj);
#if UNITY_EDITOR
                if (JKFrameRoot.EditorEventModule != null && res)
                    JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnPushGameObject", keyName, 1);
#endif
                return res;
            }
            else
            {
                JKLog.Error("�����ڽ�Null���ö����");
                return false;

            }

        }

        /// <summary>
        /// ��Ϸ������ö������
        /// </summary>
        /// <param name="obj">���������,���һ�������name��ȷ������ʲô����</param>
        public static bool PushGameObject(GameObject obj)
        {
            return PushGameObject(obj.name, obj);
        }

        /// <summary>
        /// ���ĳ����Ϸ�����ڶ�����е���������
        /// </summary>
        public static void ClearGameObject(string keyName)
        {
            GameObjectPoolModule.Clear(keyName);
#if UNITY_EDITOR
            if (JKFrameRoot.EditorEventModule != null)
                JKFrameRoot.EditorEventModule.EventTrigger<string>("OnClearGameObject", keyName);
#endif
        }
        #endregion

        #region Object��������API(��ʼ����ȡ�������롢���)

        /// <summary>
        /// ��ʼ������ز���������
        /// </summary>
        /// <param name="keyName">��Դ����</param>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        public static void InitObjectPool<T>(string keyName, int maxCapacity = -1, int defaultQuantity = 0) where T : new()
        {
            ObjectPoolModule.InitObjectPool<T>(keyName, maxCapacity, defaultQuantity);
#if UNITY_EDITOR
            if (JKFrameRoot.EditorEventModule != null)
                JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnInitObjectPool", keyName, defaultQuantity);
#endif
        }
        /// <summary>
        /// ��ʼ������ز���������
        /// </summary>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        public static void InitObjectPool<T>(int maxCapacity = -1, int defaultQuantity = 0) where T : new()
        {
            InitObjectPool<T>(typeof(T).FullName, maxCapacity, defaultQuantity);
        }
        /// <summary>
        /// ��ʼ��һ����ͨC#���������
        /// </summary>
        /// <param name="keyName">keyName</param>
        /// <param name="maxCapacity">����������ʱ�ᶪ�������ǽ������أ�-1��������</param>
        public static void InitObjectPool(string keyName, int maxCapacity = -1)
        {
            ObjectPoolModule.InitObjectPool(keyName, maxCapacity);
#if UNITY_EDITOR
            if (JKFrameRoot.EditorEventModule != null)
                JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnInitObjectPool", keyName, 0);
#endif
        }
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        /// <param name="type">��Դ����</param>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        public static void InitObjectPool(System.Type type, int maxCapacity = -1)
        {
            ObjectPoolModule.InitObjectPool(type, maxCapacity);
#if UNITY_EDITOR
            if (JKFrameRoot.EditorEventModule != null)
                JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnInitObjectPool", type.FullName, 0);
#endif
        }
        /// <summary>
        /// ��ȡ��ͨ���󣨷�GameObject��
        /// </summary>
        public static T GetObject<T>() where T : class
        {
            return GetObject<T>(typeof(T).FullName);
        }
        /// <summary>
        /// ��ȡ��ͨ���󣨷�GameObject��
        /// </summary>
        public static T GetObject<T>(string keyName) where T : class
        {
            object obj = GetObject(keyName);
            if (obj == null) return null;
            else return (T)obj;
        }
        /// <summary>
        /// ��ȡ��ͨ���󣨷�GameObject��
        /// </summary>
        public static object GetObject(System.Type type)
        {
            return GetObject(type.FullName);
        }
        /// <summary>
        /// ��ȡ��ͨ���󣨷�GameObject��
        /// </summary>
        public static object GetObject(string keyName)
        {
            object obj = ObjectPoolModule.GetObject(keyName);
#if UNITY_EDITOR
            if (obj != null)
            {
                if (JKFrameRoot.EditorEventModule != null)
                    JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnGetObject", keyName, 1);
            }
#endif
            return obj;
        }
        /// <summary>
        /// ��ͨ���󣨷�GameObject�����ö������
        /// �������ʹ��
        /// </summary>
        public static bool PushObject(object obj)
        {
            return PushObject(obj, obj.GetType().FullName);
        }
        /// <summary>
        /// ��ͨ���󣨷�GameObject�����ö������
        /// ����KeyName���
        /// </summary>
        public static bool PushObject(object obj, string keyName)
        {
            if (obj == null)
            {
                //JKLog.Error("�����ڽ�Null���ö����");
                return false;
            }
            else
            {
                bool res = ObjectPoolModule.PushObject(obj, keyName);
#if UNITY_EDITOR
                if (JKFrameRoot.EditorEventModule != null && res)
                {
                    JKFrameRoot.EditorEventModule.EventTrigger<string, int>("OnPushObject", keyName, 1);
                }
#endif
                return res;
            }
        }
        /// <summary>
        /// ����ĳ��C#��������
        /// </summary>
        public static void ClearObject<T>()
        {
            ClearObject(typeof(T).FullName);
        }
        /// <summary>
        /// ����ĳ��C#��������
        /// </summary>
        public static void ClearObject(System.Type type)
        {
            ClearObject(type.FullName);
        }
        /// <summary>
        /// ����ĳ��C#��������
        /// </summary>
        public static void ClearObject(string keyName)
        {
#if UNITY_EDITOR
            if (JKFrameRoot.EditorEventModule != null)
            {
                JKFrameRoot.EditorEventModule.EventTrigger<string>("OnClearnObject", keyName);
            }
#endif
            ObjectPoolModule.ClearObject(keyName);
        }
        #endregion

        #region ��GameObject��Object�����ͬʱ���õ�API�����ȫ����
        /// <summary>
        /// ���ȫ��
        /// </summary>
        public static void ClearAll(bool clearGameObject = true, bool clearCSharpObject = true)
        {
            if (clearGameObject)
            {
                GameObjectPoolModule.ClearAll();
#if UNITY_EDITOR
                JKFrameRoot.EditorEventModule.EventTrigger("OnClearAllGameObject");
#endif
            }
            if (clearCSharpObject)
            {
                ObjectPoolModule.ClearAll();
#if UNITY_EDITOR
                if (JKFrameRoot.EditorEventModule != null)
                {
                    JKFrameRoot.EditorEventModule.EventTrigger("OnClearAllObject");
                }
#endif
            }
        }
        #endregion


        #region Editor
#if UNITY_EDITOR
        public static Dictionary<string, GameObjectPoolData> GetGameObjectLayerDatas()
        {
            return GameObjectPoolModule.poolDic;
        }
        public static Dictionary<string, ObjectPoolData> GetObjectLayerDatas()
        {
            return ObjectPoolModule.poolDic;
        }
#endif
        #endregion
    }
}