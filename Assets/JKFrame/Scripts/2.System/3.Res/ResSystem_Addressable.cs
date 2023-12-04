#if ENABLE_ADDRESSABLES
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;

namespace JKFrame
{
    public static class ResSystem
    {
        #region ��ͨclass����
        /// <summary>
        /// ��ȡʵ������ͨclass
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
        /// <param name="keyName">������е�����</param>
        /// <returns></returns>
        public static T GetOrNew<T>(string keyName) where T : class, new()
        {
            T obj = PoolSystem.GetObject<T>(keyName);
            if (obj == null) obj = new T();
            return null;
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
        /// ����KeyName���
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
        /// <param name="maxCapacity"></param>
        /// <param name="defaultQuantity"></param>
        public static void InitObjectPool<T>(int maxCapacity = -1, int defaultQuantity = 0) where T : new()
        {
            PoolSystem.InitObjectPool<T>(maxCapacity, defaultQuantity);
        }
        /// <summary>
        /// ��ʼ��һ����ͨC#���������
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="maxCapacity"></param>
        public static void InitObjectPool(string keyName, int maxCapacity = -1)
        {
            PoolSystem.InitObjectPool(keyName, maxCapacity);
        }
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        /// <param name="type">��Դ����</param>
        /// <param name="maxCapacity"></param>
        public static void InitObjectPool(System.Type type, int maxCapacity = -1)
        {
            PoolSystem.InitObjectPool(type, maxCapacity);
        }
        #endregion

        #region ��Ϸ����
        /// <summary>
        /// ��ʼ��һ��GameObject���͵Ķ��������
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="maxCapacity"></param>
        /// <param name="assetName">ab��Դ����</param>
        /// <param name="defaultQuantity"></param>
        public static void InitGameObjectPoolForKeyName(string keyName, int maxCapacity = -1, string assetName = null, int defaultQuantity = 0)
        {
            if (defaultQuantity <= 0 || assetName == null)
            {
                PoolSystem.InitGameObjectPool(keyName, maxCapacity, null, 0);
            }
            else
            {
                GameObject[] gameObjects = new GameObject[defaultQuantity];
                for (int i = 0; i < defaultQuantity; i++)
                {
                    gameObjects[i] = Addressables.InstantiateAsync(assetName).WaitForCompletion();
                }
                PoolSystem.InitGameObjectPool(keyName, maxCapacity, gameObjects);
            }
        }
        /// <summary>
        /// ��ʼ������ز���������
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="maxCapacity"></param>
        /// <param name="defaultQuantity"></param>
        public static void InitGameObjectPoolForAssetName(string assetName, int maxCapacity = -1, int defaultQuantity = 0)
        {
            InitGameObjectPoolForKeyName(assetName, maxCapacity, assetName, defaultQuantity);
        }

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
        /// <param name="keyName"></param>
        /// <param name="gameObject"></param>
        public static void PushGameObjectInPool(string keyName, GameObject gameObject)
        {
            PoolSystem.PushGameObject(keyName, gameObject);
        }

        /// <summary>
        /// ������Ϸ����
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <param name="autoRelease">��������ʱ�����Զ�ȥ����һ��Addressables.Release</param>
        /// <returns></returns>
        public static GameObject InstantiateGameObject(Transform parent, string keyName, bool autoRelease = true)
        {
            GameObject go;
            go = PoolSystem.GetGameObject(keyName, parent);
            if (go.IsNull() == false) return go;
            else
            {
                go = Addressables.InstantiateAsync(keyName, parent).WaitForCompletion();
                if (autoRelease)
                {
                    go.transform.OnReleaseAddressableAsset(AutomaticReleaseAssetAction);
                }
                go.name = keyName;
            }
            return go;
        }

        /// <summary>
        /// �첽������Ϸ����
        /// ���Զ�����������Ƿ��������������򷵻ض�����е�
        /// </summary>
        /// <param name="parent">������</param>
        /// <param name="keyName">������еķ�������</param>
        /// <param name="callBack">������ɺ�Ļص�</param>
        /// <param name="autoRelease">��������ʱ�����Զ�ȥ����һ��Addressables.Release</param>
        public static void InstantiateGameObjectAsync(Transform parent, string keyName, Action<GameObject> callBack, bool autoRelease = true)
        {
            GameObject go;
            go = PoolSystem.GetGameObject(keyName, parent);
            if (go.IsNull() == false) callBack?.Invoke(go);
            else
            {
                Addressables.InstantiateAsync(keyName, parent).Completed += (handle) =>
                {
                    OnInstantiateGameObjectAsyncCompleted(handle, callback, keyName, autoRelease);
                };
            }
        }

        private static void OnInstantiateGameObjectAsyncCompleted(AsyncOperationHandle<GameObject> handle, Action<GameObject> callBack, string gameObjectName, bool autoRelease = true)
        {
            handle.Result.name = gameObjectName;
            if (autoRelease)
            {
                handle.Result.transform.OnReleaseAddressableAsset(AutomaticReleaseAssetAction);
            }
            callBack?.Invoke(handle.Result);
        }

        private static void OnInstantiateGameObjectAsyncCompleted<T>(AsyncOperationHandle<GameObject> handle, Action<T> callback, string gameObjectName, bool autoRelease = true) where T : Component
        {
            handle.Result.name = gameObjectName;
            if (autoRelease)
            {
                handle.Result.transform.OnReleaseAddressableAsset(AutomaticReleaseAssetAction);
            }
            callback?.Invoke(handle.Result.GetComponent<T>());
        }

        /// <summary>
        /// ������Ϸ����
        /// ���Զ�����������Ƿ��������������򷵻ض�����е�
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <param name="autoRelease"></param>
        /// <returns></returns>
        public static GameObject InstantiateGameObject(string assetName, Transform parent = null, string keyName = null, bool autoRelease = true)
        {
            GameObject go;
            if (keyName == null) go = PoolSystem.GetGameObject(assetName, parent);
            else go = PoolSystem.GetGameObject(keyName, parent);
            if (go.IsNull() == false) return go;
            else
            {
                go = Addressables.InstantiateAsync(assetName, parent).WaitForCompletion();
                if (autoRelease)
                {
                    go.transform.OnReleaseAddressableAsset(AutomaticReleaseAssetAction);
                }
                go.name = keyName != null ? keyName : assetName;
            }
            return go;
        }

        /// <summary>
        /// �첽������Ϸ����
        /// ���Զ�����������Ƿ��������������򷵻ض�����е�
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <param name="autoRelease"></param>
        public static void InstantiateGameObjectAsync(string assetName, Action<GameObject> callback, Transform parent = null, string keyName = null, bool autoRelease = true)
        {
            GameObject go;
            if (keyName == null) go = PoolSystem.GetGameObject(assetName, parent);
            else go = PoolSystem.GetGameObject(keyName, parent);

            if (go.IsNull() == false) callback?.Invoke(go);
            else
            {
                Addressables.InstantiateAsync(assetName, parent).Completed += (handle) =>
                {
                    OnInstantiateGameObjectAsyncCompleted(handle, callback, keyName != null ? keyName : assetName, autoRelease);
                };
            }
        }

        /// <summary>
        /// ������Ϸ���岢��ȡ���
        /// ���Զ�����������Ƿ��������������򷵻ض�����е�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <param name="autoRelease"></param>
        /// <returns></returns>
        public static T InstantiateGameObject<T>(Transform parent, string keyName, bool autoRelease = true) where T : Component
        {
            GameObject go = InstantiateGameObject(parent, keyName, autoRelease);
            if (go.IsNull() == false) return go.GetComponent<T>();
            else return null;
        }

        /// <summary>
        /// ������Ϸ���岢��ȡ���
        /// ���Զ�����������Ƿ��������������򷵻ض�����е�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <param name="autoRelease"></param>
        /// <returns></returns>
        public static T InstantiateGameObject<T>(string assetName, Transform parent = null, string keyName = null, bool autoRelease = true) where T : Component
        {
            GameObject go = InstantiateGameObject(assetName, parent, keyName, autoRelease);
            if (go.IsNull() == false) return go.GetComponent<T>();
            else return null;
        }

        /// <summary>
        /// �Զ��ͷ���Դ�¼��������¼�����
        /// </summary>
        /// <param name="go"></param>
        private static void AutomaticReleaseAssetAction(GameObject go)
        {
            Addressables.ReleaseInstance(go);
        }

        /// <summary>
        /// �첽������Ϸ���岢��ȡ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        /// <param name="parent"></param>
        /// <param name="keyName"></param>
        /// <param name="autoRelease"></param>
        public static void InstantiateGameObjectAsync<T>(string assetName, Action<T> callback = null, Transform parent = null, string keyName = null, bool autoRelease = true) where T : Component
        {
            GameObject go;
            if (keyName == null) go = PoolSystem.GetGameObject(assetName, parent);
            else go = PoolSystem.GetGameObject(keyName, parent);
            //���������
            if (!go.IsNull())
            {
                if (autoRelease) go.transform.OnReleaseAddressableAsset(AutomaticReleaseAssetAction);
                callback?.Invoke(go.GetComponent<T>());
                return;
            }
            //��ͨ�������
            Addressables.InstantiateAsync(assetName, parent).Completed += (handle) =>
            {
                OnInstantiateGameObjectAsyncCompleted<T>(handle, callback, keyName != null ? keyName : assetName, autoRelease);
            };
        }
        #endregion

        #region ��ϷAsset
        /// <summary>
        /// ����Unity��Դ  ��AudioClip  Sprite Ԥ����
        /// Ҫע�⣬��Դ����ʹ��ʱ����Ҫ����һ��Release
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            return Addressables.LoadAssetAsync<T>(assetName).WaitForCompletion();
        }

        /// <summary>
        /// �첽����Unity��Դ AudioClip Sprite GameObject(Ԥ����)
        /// </summary>
        /// <typeparam name="T">��Դ����</typeparam>
        /// <param name="assetName">AB��Դ����</param>
        /// <param name="callBack">�ص�����</param>
        public static void LoadAssetAsync<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            Addressables.LoadAssetAsync<T>(assetName).Completed += (handle) =>
            {
                OnLoadAssetAsyncCompleted<T>(handle, callback);
            };
        }

        private static void OnLoadAssetAsyncCompleted<T>(AsyncOperationHandle<T> handle, Action<T> callback) where T : UnityEngine.Object
        {
            callback?.Invoke(handle.Result);
        }

        /// <summary>
        /// ����ָ��Key��������Դ
        /// ע��:��������ʱ������ͷ���ԴҪ�ͷŵ�handle��ֱ��ȥ�ͷ���Դ����Ч��
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="keyName">һ����lable</param>
        /// <param name="handle">����Releaseʱʹ��</param>
        /// <param name="callBackOnEveryOne">ע�����������ÿһ����Դ�Ļص�</param>
        /// <returns>������Դ</returns>
        public static IList<T> LoadAssets<T>(string keyName, out AsyncOperationHandle<IList<T>> handle, Action<T> callBackOnEveryOne = null) where T : UnityEngine.Object
        {
            handle = Addressables.LoadAssetsAsync<T>(keyName, callBackOnEveryOne, true);
            return handle.WaitForCompletion();
        }

        /// <summary>
        /// �첽����ָ��Key��������Դ
        /// ע��1:��������ʱ������ͷ���ԴҪ�ͷŵ�handle��ֱ��ȥ�ͷ���Դ����Ч��
        /// ע��2:�ص���ʹ��callBack�еĲ���ʹ��(.Result)���ɷ�����Դ�б�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="keyName">һ����lable</param>
        /// <param name="callBack">������Դ�б��ͳһ�ص���ע�����Ǻܱ�Ҫ�ģ���ΪReleaseʱ��Ҫ���handle</param>
        /// <param name="callBackOnEveryOne">ע�����������ÿһ����Դ�Ļص�,������Null</param>
        public static void LoadAssetsAsync<T>(string keyName, Action<AsyncOperationHandle<IList<T>>> callBack, Action<T> callBackOnEveryOne = null) where T : UnityEngine.Object
        {
            Addressables.LoadAssetsAsync<T>(keyName, callBackOnEveryOne).Completed += callBack;
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="obj">�������</param>
        public static void UnloadAsset<T>(T obj)
        {
            Addressables.Release(obj);
        }

        /// <summary>
        /// ж����Ϊ�������ض�������handle
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="handle"></param>
        public static void UnLoadAssetsHandle<TObject>(AsyncOperationHandle<TObject> handle)
        {
            Addressables.Release(handle);
        }

        /// <summary>
        /// ������Ϸ���岢�ͷ���Դ
        /// </summary>
        public static bool UnloadInstance(GameObject obj)
        {
            return Addressables.ReleaseInstance(obj);
        }
        #endregion
    }
}
#endif