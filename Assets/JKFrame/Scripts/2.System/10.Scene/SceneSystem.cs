using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace JKFrame
{
    public static class SceneSystem
    {
        public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, mode);
        }

        public static void LoadScene(int sceneBuildIndex, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneBuildIndex, mode);
        }

        public static Scene LoadScene(string sceneName, LoadSceneParameters loadSceneParameters)
        {
            return SceneManager.LoadScene(sceneName, loadSceneParameters);
        }

        public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters loadSceneParameters)
        {
            return SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
        }

        /// <summary>
        /// �첽���س���
        /// ������ѡ��EventSystem����"LoadingSceneProgress"��"LoadSceneSucceed"���¼�������������
        /// Ҳ����ͨ��callBack����
        /// </summary>
        /// <param name="sceneName">��������</param>
        /// <param name="callBack">�ص�����,ע�⣺ÿ�ν��ȸ��¶������һ��,����Ϊ0-1�Ľ���</param>
        public static void LoadSceneAsync(string sceneName, Action<float> callBack = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            MonoSystem.Start_Coroutine(DoLoadSceneAsync(sceneName, callBack, mode));
        }

        private static IEnumerator DoLoadSceneAsync(string sceneName, Action<float> callBack = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, mode);
            float progress = 0;
            while (progress < 1)
            {
                // �ص����ؽ���
                if (progress != ao.progress)
                {
                    progress = ao.progress;
                    callBack?.Invoke(progress);
                    // �Ѽ��ؽ��ȷַ����¼�����
                    EventSystem.EventTrigger("LoadingSceneProgress", ao.progress);
                    if (progress == 1)
                    {
                        EventSystem.EventTrigger("LoadSceneSucceed");
                        break;
                    }
                }
                yield return CoroutineTool.WaitForFrames();
            }
        }

        /// <summary>
        /// �첽���س���
        /// ������ѡ��EventSystem����"LoadingSceneProgress"��"LoadSceneSucceed"���¼�������������
        /// Ҳ����ͨ��callBack����
        /// </summary>
        /// <param name="sceneBuildIndex">����Index</param>
        /// <param name="callBack">�ص�����</param>
        public static void LoadSceneAsync(int sceneBuildIndex, Action<float> callBack = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            MonoSystem.Start_Coroutine(DoLoadSceneAsync(sceneBuildIndex, callBack, mode));
        }

        private static IEnumerator DoLoadSceneAsync(int sceneBuildIndex, Action<float> callBack = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneBuildIndex, mode);
            float progress = 0;
            while (progress < 1)
            {
                progress = ao.progress;
                callBack?.Invoke(progress);
                // �Ѽ��ؽ��ȷַ����¼�����
                EventSystem.EventTrigger("LoadingSceneProgress", ao.progress);
                if (progress == 1)
                {
                    EventSystem.EventTrigger("LoadSceneSucceed");
                    break;
                }
                yield return CoroutineTool.WaitForFrames();
            }
        }

        /// <summary>
        /// �첽���س�����������ɺ������л�
        /// </summary>
        /// <param name="sceneName">��������</param>
        /// <param name="successCallBack">������ɻص������봫����򳡾��޷��л�</param>
        /// <param name="loadingCallBack">����ʱÿ֡�ص�</param>
        /// <param name="mode">����ģʽ</param>
        public static void LoadSceneAsyncWithoutActive(string sceneName, Action<AsyncOperation> successCallBack,
            Action<float> loadingCallBack = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var ao = SceneManager.LoadSceneAsync(sceneName, mode);
            MonoSystem.Start_Coroutine(DoLoadSceneAsyncWithoutActive(ao, successCallBack, loadingCallBack,
                mode));
        }

        /// <summary>
        /// �첽���س�����������ɺ������л�
        /// </summary>
        /// <param name="sceneIndex">�����������</param>
        /// <param name="successCallBack">������ɻص������봫����򳡾��޷��л�</param>
        /// <param name="loadingCallBack">����ʱÿ֡�ص�</param>
        /// <param name="mode">����ģʽ</param>
        public static void LoadSceneAsyncWithoutActive(int sceneIndex, Action<AsyncOperation> successCallBack,
            Action<float> loadingCallBack = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var ao = SceneManager.LoadSceneAsync(sceneIndex, mode);
            MonoSystem.Start_Coroutine(DoLoadSceneAsyncWithoutActive(ao, successCallBack, loadingCallBack,
                mode));
        }

        private static IEnumerator DoLoadSceneAsyncWithoutActive(AsyncOperation ao,
            Action<AsyncOperation> successCallBack, Action<float> loadingCallBack = null,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            ao.allowSceneActivation = false;
            ao.completed += successCallBack;
            while (!ao.isDone)
            {
                loadingCallBack?.Invoke(ao.progress);
                Debug.Log(ao.progress);
                EventSystem.EventTrigger("LoadingSceneProgress", ao.progress);
                yield return CoroutineTool.WaitForFrames();
            }

            EventSystem.EventTrigger("LoadSceneSucceed");
        }
    }
}

