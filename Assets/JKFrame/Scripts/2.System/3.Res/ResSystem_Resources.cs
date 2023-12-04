#if ENABLE_ADDRESSABLES == false
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JKFrame
{
    public static class ResSystem
    {
        #region ��ͨclass����
        /// <summary>
        /// ��ȡʵ������ͨClass
        /// ���������Ҫ���棬��Ӷ�����л�ȡ
        /// ��������û�л򷵻�null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : class
        {
            return PoolSystem.GetObject<T>();
        }

        /// <summary>
        /// ��ȡʵ������ͨClass
        /// ���������Ҫ���棬��Ӷ�����л�ȡ
        /// ��������û�л򷵻�null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static T Get<T>(string keyName) where T : class
        {
            return PoolSystem.GetObject<T>(keyName);
        }

        /// <summary>
        /// ��ȡʵ������ͨClass
        /// ���������Ҫ���棬��Ӷ�����л�ȡ
        /// ��������û�л�newһ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetOrNew<T>() where T : class, new()
        {
            T obj = PoolSystem.GetObject<T>();
            if (obj == null) obj = new T();
            return obj;
        }

        /// <summary>
        /// ��ȡʵ������ͨClass
        /// ���������Ҫ���棬��Ӷ�����л�ȡ
        /// ��������û�л�newһ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetOrNew<T>(string keyName) where T : class, new()
        {
            T obj = PoolSystem.GetObject<T>(keyName);
            if (obj == null) obj = new T();
            return obj;
        }

        /// <summary>
        /// ж����ͨ����������ʹ�ö���صķ�ʽ
        /// </summary>
        /// <param name="obj"></param>
        public static void PushObjectInPool(System.Object obj)
        {
            PoolSystem.PushObject(obj);
        }

        /// <summary>
        /// ��ͨ���󣨷�GameObject�����ö������
        /// ����keyName���
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="keyName"></param>
        public static void PushObjectInPool(object obj, string keyName)
        {
            PoolSystem.PushObject(obj, keyName);
        }

        /// <summary>
        /// ��ʼ������ز���������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName">��Դ����</param>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        public static void InitObjectPool<T>(string keyName, int maxCapacity = -1, int defaultQuantity = 0) where T : new()
        {
            PoolSystem.InitObjectPool<T>(keyName, maxCapacity, defaultQuantity);
        }
        /// <summary>
        /// ��ʼ������ز���������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        public static void InitObjectPool<T>(int maxCapacity = -1, int defaultQuantity = 0) where T : new()
        {
            PoolSystem.InitObjectPool<T>(maxCapacity, defaultQuantity);
        }

        /// <summary>
        /// ��ʼ��һ����ͨc#���������
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        public static void InitObjectPool(string keyName, int maxCapacity = -1)
        {
            PoolSystem.InitObjectPool(keyName, maxCapacity);
        }
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        /// <param name="type">��Դ����</param>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        public static void InitObjectPool(System.Type type, int maxCapacity = -1)
        {
            PoolSystem.InitObjectPool(type, maxCapacity);
        }
        #endregion

        #region ��Ϸ����
        /// <summary>
        /// ж����Ϸ����������ʹ�ö���صķ�ʽ
        /// </summary>
        /// <param name="gameObject"></param>
        public static void PushGameObjectInPool(GameObject gameObject)
        {
            PoolSystem.PushGameObject(gameObject);
        }
        /// <summary>
        /// ��Ϸ������ö������
        /// </summary>
        /// <param name="keyName">������е�key</param>
        /// <param name="gameObject">���������</param>
        public static void PushGameObjectInPool(string keyName, GameObject gameObject)
        {
            PoolSystem.PushGameObject(keyName, gameObject);
        }

        /// <summary>
        /// ��ʼ��һ��GameObject���͵Ķ��������
        /// </summary>
        /// <param name="keyName">������еı�ʶ</param>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="assetPath">��Դ·��</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        public static void InitGameObjectPool(string keyName, int maxCapacity = -1, string assetPath = null, int defaultQuantity = 0)
        {
            GameObject prefab = LoadAsset<GameObject>(assetPath);
            PoolSystem.InitGameObjectPool(keyName, maxCapacity, prefab, defaultQuantity);
            //UnloadAsset(prefab);
        }
        /// <summary>
        /// ��ʼ��һ��GameObject���͵Ķ��������
        /// </summary>
        /// <param name="maxCapacity">�������ƣ�����ʱ�����ٶ����ǽ������أ�-1��������</param>
        /// <param name="assetPath">��Դ·��</param>
        /// <param name="defaultQuantity">Ĭ����������д��������з����Ӧ�����Ķ���0����Ԥ�ȷ���</param>
        public static void InitGameObjectPool(int maxCapacity = -1, string assetPath = null, int defaultQuantity = 0)
        {
            GameObject prefab = LoadAsset<GameObject>(assetPath);
            PoolSystem.InitGameObjectPool(prefab, maxCapacity, defaultQuantity);
            //UnloadAsset(prefab);
        }

        /// <summary>
        /// ������Ϸ����
        /// ���Զ������Ƿ��ڶ�����д���
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static GameObject InstantiateGameObject(Transform parent, string keyName)
        {
            GameObject go;
            go = PoolSystem.GetGameObject(keyName, parent);
            if (!go.IsNull()) return go;
            GameObject prefab = LoadAsset<GameObject>(keyName);
            if (!prefab.IsNull())
            {
                go = GameObject.Instantiate(prefab, parent);
                go.name = keyName;
                //UnloadAsset(prefab);
            }
            return go;
        }
        /// <summary>
        /// ������Ϸ����
        /// ���Զ������Ƿ��ڶ�����д���
        /// </summary>
        /// <param name="assetPath">��Դ·��</param>
        /// <param name="parent">������</param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static GameObject InstantiateGameObject(string assetPath, Transform parent = null, string keyName = null)
        {
            string assetName = GetAssetNameByPath(assetPath);
            GameObject go;
            if (keyName == null) go = PoolSystem.GetGameObject(assetName, parent);
            else go = PoolSystem.GetGameObject(keyName, parent);
            if (!go.IsNull()) return go;

            GameObject prefab = LoadAsset<GameObject>(assetPath);
            if (!prefab.IsNull())
            {
                go = GameObject.Instantiate(prefab, parent);
                go.name = keyName != null ? keyName : assetName;
                //UnloadAsset(prefab);
            }
            return go;
        }

        /// <summary>
        /// ������Ϸ���岢��ȡ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static T InstantiateGameObject<T>(Transform parent, string keyName) where T : UnityEngine.Component
        {
            GameObject go = InstantiateGameObject(parent, keyName);
            if (!go.IsNull())
            {
                return go.GetComponent<T>();
            }
            return null;
        }
        /// <summary>
        /// ������Ϸ���岢��ȡ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static T InstantiateGameObject<T>(string assetPath, Transform parent = null, string keyName = null) where T : UnityEngine.Component
        {
            GameObject go = InstantiateGameObject(assetPath, parent, keyName);
            if (!go.IsNull())
            {
                return go.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// �첽ʵ������Ϸ����
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="callBack"></param>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        public static void InstantiateGameObjectAsync(string assetPath, Action<GameObject> callBack = null, Transform parent = null, string keyName = null)
        {
            //�и�·����ȡʵ�ʵ���Դ����
            string assetName = GetAssetNameByPath(assetPath);
            GameObject go;
            if (keyName == null) go = PoolSystem.GetGameObject(assetName, parent);
            else go = PoolSystem.GetGameObject(keyName, parent);
            //�������
            if (!go.IsNull())
            {
                callBack?.Invoke(go);
                return;
            }
            //��ͨ�������
            MonoSystem.Start_Coroutine(DoInstantiateGameObjectAsync(assetPath, callBack, parent));
        }

        /// <summary>
        /// �첽ʵ������Ϸ���岢��ȡ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <param name="callBack"></param>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        public static void InstantiateGameObjectAsync<T>(string assetPath, Action<T> callBack = null, Transform parent = null, string keyName = null) where T : UnityEngine.Component
        {
            string assetName = GetAssetNameByPath(assetPath);
            //�����ֵ�����
            GameObject go;
            if (keyName == null) go = PoolSystem.GetGameObject(assetName, parent);
            else go = PoolSystem.GetGameObject(keyName, parent);
            //�������
            if (!go.IsNull())
            {
                callBack?.Invoke(go.GetComponent<T>());
                return;
            }
            //��ͨ�������
            MonoSystem.Start_Coroutine(DoInstantiateGameObjectAsync<T>(assetPath, callBack, parent));
        }
        static IEnumerator DoInstantiateGameObjectAsync(string assetPath, Action<GameObject> callBack = null, Transform parent = null)
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(assetPath);
            yield return resourceRequest;
            GameObject prefab = resourceRequest.asset as GameObject;
            GameObject go = GameObject.Instantiate<GameObject>(prefab);
            go.name = prefab.name;
            //UnloadAsset(prefab);
            callBack?.Invoke(go);
        }
        static IEnumerator DoInstantiateGameObjectAsync<T>(string assetPath, Action<T> callBack = null, Transform parent = null) where T : UnityEngine.Component
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(assetPath);
            yield return resourceRequest;
            GameObject prefab = resourceRequest.asset as GameObject;
            GameObject go = GameObject.Instantiate<GameObject>(prefab);
            go.name = prefab.name;
            //UnloadAsset(prefab);
            callBack?.Invoke(go.GetComponent<T>());
        }
        #endregion

        #region ��ϷAsset
        /// <summary>
        /// ����Unity��Դ   ��AudioClip  Sprite  Ԥ����
        /// </summary>
        /// <typeparam name="T">��Դ����</typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public static T LoadAsset<T>(string assetPath) where T : UnityEngine.Object
        {
            return Resources.Load<T>(assetPath);
        }

        /// <summary>
        /// ͨ��path��ȡ��Դ����
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private static string GetAssetNameByPath(string assetPath)
        {
            return assetPath.Substring(assetPath.LastIndexOf("/") + 1);
        }

        /// <summary>
        /// �첽����Unity��Դ��  AudioClip  Sprite  GameObject(Ԥ����)
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="assetPath">��Դ·��</param>
        /// <param name="callBack">������ɺ�Ļص�</param>
        public static void LoadAssetAsync<T>(string assetPath, Action<T> callBack) where T : UnityEngine.Object
        {
            MonoSystem.Start_Coroutine(DoLoadAssetAsync<T>(assetPath, callBack));
        }
        static IEnumerator DoLoadAssetAsync<T>(string assetPath, Action<T> callBack) where T : UnityEngine.Object
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<T>(assetPath);
            yield return resourceRequest;
            callBack?.Invoke(resourceRequest.asset as T);
        }

        /// <summary>
        /// ����ָ��·����������Դ
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public static UnityEngine.Object[] LoadAssets(string assetPath)
        {
            return Resources.LoadAll(assetPath);
        }

        /// <summary>
        /// ����ָ��·����������Դ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public static T[] LoadAssets<T>(string assetPath) where T : UnityEngine.Object
        {
            return Resources.LoadAll<T>(assetPath);
        }

        /// <summary>
        /// ж����Դ
        /// </summary>
        /// <param name="assetToUnload"></param>
        public static void UnloadAsset(UnityEngine.Object assetToUnload)
        {
            Resources.UnloadAsset(assetToUnload);
        }

        /// <summary>
        /// ж��ȫ��δʹ�õ���Դ
        /// </summary>
        public static void UnloadUnuseAssets()
        {
            Resources.UnloadUnusedAssets();
        }
        #endregion
    }
}
#endif