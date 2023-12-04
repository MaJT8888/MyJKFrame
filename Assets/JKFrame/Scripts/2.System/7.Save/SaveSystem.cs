using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace JKFrame
{
    /// <summary>
    /// һ���浵������
    /// </summary>
    [Serializable]
    public class SaveItem
    {
        public int saveID;
        private DateTime lastSaveTime;
        public DateTime LastSaveTime
        {
            get
            {
                if (lastSaveTime == default(DateTime))
                {
                    DateTime.TryParse(lastSaveTimeString, out lastSaveTime);
                }
                return lastSaveTime;
            }
        }
        [SerializeField] private string lastSaveTimeString; // Json��֧��DateTime�������־û���
        public SaveItem(int saveID, DateTime lastSaveTime)
        {
            this.saveID = saveID;
            this.lastSaveTime = lastSaveTime;
            lastSaveTimeString = lastSaveTime.ToString();
        }

        public void UpdateTime(DateTime lastSaveTime)
        {
            this.lastSaveTime = lastSaveTime;
            lastSaveTimeString = lastSaveTime.ToString();
        }
    }

    /// <summary>
    /// �浵ϵͳ
    /// </summary>
    public static class SaveSystem
    {
        #region �浵ϵͳ���浵ϵͳ�����༰�����û��浵�����ô浵����
        /// <summary>
        /// �浵ϵͳ������
        /// </summary>
        [Serializable]
        private class SaveSystemData
        {
            // ��ǰ�Ĵ浵ID
            public int currID = 0;
            // ���д浵���б�
            public List<SaveItem> saveItemList = new List<SaveItem>();
        }

        private static SaveSystemData saveSystemData;

        // �浵�ı���
        private const string saveDirName = "saveData";
        // ���õı��棺1.ȫ�����ݵı��棨�ֱ��ʡ��������ã� 2.�浵�����ñ��档
        // ��������£��浵ϵͳ����ά��
        private const string settingDirName = "setting";

        // �浵�ļ���·��
        private static string saveDirPath;
        private static string settingDirPath;

        // �浵�ж���Ļ����ֵ� 
        // <�浵ID,<�ļ����ƣ�ʵ�ʵĶ���>>
        private static Dictionary<int, Dictionary<string, object>> cacheDic = new Dictionary<int, Dictionary<string, object>>();

#if UNITY_EDITOR
        static SaveSystem()
        {
            Init();
        }
#endif

        // ��ʼ��������
        public static void Init()
        {
            saveDirPath = Application.persistentDataPath + "/" + saveDirName;
            settingDirPath = Application.persistentDataPath + "/" + settingDirName;
#if UNITY_EDITOR
            // 
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }
#endif
            CheckAndCreateDir();
            // ��ʼ��SaveSystemData
            InitSaveSystemData();
        }
        #endregion

        #region ��ȡ��ɾ�������û��浵
        /// <summary>
        /// ��ȡ���д浵
        /// ���µ��������
        /// </summary>
        /// <returns></returns>
        public static List<SaveItem> GetAllSaveItem()
        {
            return saveSystemData.saveItemList;
        }

        /// <summary>
        /// ��ȡ���д浵
        /// �������µ�����ǰ��
        /// </summary>
        /// <returns></returns>
        public static List<SaveItem> GetAllSaveItemByCreatTime()
        {
            List<SaveItem> saveItems = new List<SaveItem>(saveSystemData.saveItemList.Count);

            for (int i = 0; i < saveSystemData.saveItemList.Count; i++)
            {
                saveItems.Add(saveSystemData.saveItemList[saveSystemData.saveItemList.Count - (i + 1)]);
            }
            return saveItems;
        }

        /// <summary>
        /// ��ȡ���д浵
        /// ���¸��µ���������
        /// </summary>
        /// <returns></returns>
        public static List<SaveItem> GetAllSaveItemByUpdateTime()
        {
            List<SaveItem> saveItems = new List<SaveItem>(saveSystemData.saveItemList.Count);
            for (int i = 0; i < saveSystemData.saveItemList.Count; i++)
            {
                saveItems.Add(saveSystemData.saveItemList[i]);
            }
            OrderByUpdateTimeComparer orderBy = new OrderByUpdateTimeComparer();
            saveItems.Sort(orderBy);
            return saveItems;
        }

        private class OrderByUpdateTimeComparer : IComparer<SaveItem>
        {
            public int Compare(SaveItem x, SaveItem y)
            {
                if (x.LastSaveTime > y.LastSaveTime)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// ��ȡ���д浵
        /// ���ܽ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderFunc"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public static List<SaveItem> GetAllSaveItem<T>(Func<SaveItem, T> orderFunc, bool isDescending = false)
        {
            if (isDescending)
            {
                return saveSystemData.saveItemList.OrderByDescending(orderFunc).ToList();
            }
            else
            {
                return saveSystemData.saveItemList.OrderBy(orderFunc).ToList();
            }

        }


        public static void DeleteAllSaveItem()
        {
            if (Directory.Exists(saveDirPath))
            {            // ֱ��ɾ��Ŀ¼
                Directory.Delete(saveDirPath, true);
            }
            CheckAndCreateDir();
            InitSaveSystemData();
        }


        public static void DeleteAll()
        {
            CleanCache();
            DeleteAllSaveItem();
            DeleteAllSetting();
        }
        #endregion

        #region ��������ȡ��ɾ��ĳһ���û��浵
        /// <summary>
        /// ��ȡSaveItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SaveItem GetSaveItem(int id)
        {
            for (int i = 0; i < saveSystemData.saveItemList.Count; i++)
            {
                if (saveSystemData.saveItemList[i].saveID == id)
                {
                    return saveSystemData.saveItemList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// ��ȡSaveItem
        /// </summary>
        /// <param name="saveItem"></param>
        /// <returns></returns>
        public static SaveItem GetSaveItem(SaveItem saveItem)
        {
            GetSaveItem(saveItem.saveID);
            return null;
        }

        /// <summary>
        /// ���һ���浵
        /// </summary>
        /// <returns></returns>
        public static SaveItem CreateSaveItem()
        {
            SaveItem saveItem = new SaveItem(saveSystemData.currID, DateTime.Now);
            saveSystemData.saveItemList.Add(saveItem);
            saveSystemData.currID += 1;
            // ����SaveSystemData д�����
            UpdateSaveSystemData();
            return saveItem;
        }

        /// <summary>
        /// ɾ���浵
        /// </summary>
        /// <param name="saveID"></param>
        public static void DeleteSaveItem(int saveID)
        {
            string itemDir = GetSavePath(saveID, false);
            // ���·������ �� ��Ч
            if (itemDir != null)
            {
                // ������浵�µ��ļ��ݹ�ɾ��
                Directory.Delete(itemDir, true);
            }
            saveSystemData.saveItemList.Remove(GetSaveItem(saveID));
            // �Ƴ�����
            RemoveCache(saveID);
            // ����SaveSystemData д�����
            UpdateSaveSystemData();
        }

        /// <summary>
        /// ɾ���浵
        /// </summary>
        /// <param name="saveItem"></param>
        public static void DeleteSaveItem(SaveItem saveItem)
        {
            DeleteSaveItem(saveItem.saveID);
        }
        #endregion

        #region ���¡���ȡ��ɾ���û��浵����
        /// <summary>
        /// ���û���
        /// </summary>
        /// <param name="saveID">�浵ID</param>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="saveObject">Ҫ����Ķ���</param>
        private static void SetCache(int saveID, string fileName, object saveObject)
        {
            // �����ֵ����Ƿ������SaveID
            if (cacheDic.ContainsKey(saveID))
            {
                // ����浵����û������ļ�
                if (cacheDic[saveID].ContainsKey(fileName))
                {
                    cacheDic[saveID][fileName] = saveObject;
                }
                else
                {
                    cacheDic[saveID].Add(fileName, saveObject);
                }
            }
            else
            {
                cacheDic.Add(saveID, new Dictionary<string, object>() { { fileName, saveObject } });
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveID">�浵ID</param>
        /// <param name="fileName">Ҫ����Ķ���</param>
        /// <returns></returns>
        private static T GetCache<T>(int saveID, string fileName) where T : class
        {
            // �����ֵ����Ƿ������SaveID
            if (cacheDic.ContainsKey(saveID))
            {
                // ����浵����û������ļ�
                if (cacheDic[saveID].ContainsKey(fileName))
                {
                    return cacheDic[saveID][fileName] as T;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// �Ƴ�����
        /// </summary>
        private static void RemoveCache(int saveID)
        {
            cacheDic.Remove(saveID);
        }

        /// <summary>
        /// �Ƴ������е�ĳһ������
        /// </summary>
        private static void RemoveCache(int saveID, string fileName)
        {
            cacheDic[saveID].Remove(fileName);
        }

        public static void CleanCache()
        {
            cacheDic.Clear();
        }
        #endregion

        #region ���桢��ȡ��ɾ���û��浵��ĳһ������
        /// <summary>
        /// �������ĳ���浵��
        /// </summary>
        /// <param name="saveObject">Ҫ����Ķ���</param>
        /// <param name="saveFileName">������ļ�����</param>
        /// <param name="saveID">�浵��ID</param>
        public static void SaveObject(object saveObject, string saveFileName, int saveID = 0)
        {
            // �浵���ڵ��ļ���·��
            string dirPath = GetSavePath(saveID, true);
            // ����Ķ���Ҫ�����·��
            string savePath = dirPath + "/" + saveFileName;
            // ����ı���
            SaveFile(saveObject, savePath);
            // ���´浵ʱ��
            GetSaveItem(saveID).UpdateTime(DateTime.Now);
            // ����SaveSystemData д�����
            UpdateSaveSystemData();

            // ���»���
            SetCache(saveID, saveFileName, saveObject);

        }

        /// <summary>
        /// �������ĳ���浵��
        /// </summary>
        /// <param name="saveObject">Ҫ����Ķ���</param>
        /// <param name="saveFileName">������ļ�����</param>
        /// <param name="saveItem"></param>
        public static void SaveObject(object saveObject, string saveFileName, SaveItem saveItem)
        {
            SaveObject(saveObject, saveFileName, saveItem.saveID);
        }

        /// <summary>
        /// �������ĳ���浵��
        /// </summary>
        /// <param name="saveObject">Ҫ����Ķ���</param>
        /// <param name="saveID">�浵��ID</param>
        public static void SaveObject(object saveObject, int saveID = 0)
        {
            SaveObject(saveObject, saveObject.GetType().Name, saveID);
        }

        /// <summary>
        /// �������ĳ���浵��
        /// </summary>
        /// <param name="saveObject">Ҫ����Ķ���</param>
        /// <param name="saveItem">�浵��ID</param>
        public static void SaveObject(object saveObject, SaveItem saveItem)
        {
            SaveObject(saveObject, saveObject.GetType().Name, saveItem);
        }

        /// <summary>
        /// ��ĳ������Ĵ浵�м���ĳ������
        /// </summary>
        /// <typeparam name="T">Ҫ���ص�ʵ������</typeparam>
        /// <param name="saveFileName">�ļ�����</param>
        /// <param name="saveID">�浵ID</param>
        /// <returns></returns>
        public static T LoadObject<T>(string saveFileName, int saveID = 0) where T : class
        {
            T obj = GetCache<T>(saveID, saveFileName);
            if (obj == null)
            {
                // �浵���ڵ��ļ���·��
                string dirPath = GetSavePath(saveID);
                if (dirPath == null) return null;
                // ����Ķ���Ҫ�����·��
                string savePath = dirPath + "/" + saveFileName;
                obj = LoadFile<T>(savePath);
                SetCache(saveID, saveFileName, obj);
            }
            return obj;
        }

        /// <summary>
        /// ��ĳ������Ĵ浵�м���ĳ������
        /// </summary>
        /// <typeparam name="T">Ҫ���ص�ʵ������</typeparam>
        /// <param name="saveFileName">�ļ�����</param>
        /// <param name="saveItem"></param>
        /// <returns></returns>
        public static T LoadObject<T>(string saveFileName, SaveItem saveItem) where T : class
        {
            return LoadObject<T>(saveFileName, saveItem.saveID);
        }

        /// <summary>
        /// ��ĳ������Ĵ浵�м���ĳ������
        /// </summary>
        /// <typeparam name="T">Ҫ���ص�ʵ������</typeparam>
        /// <param name="saveID">�浵ID</param>
        /// <returns></returns>
        public static T LoadObject<T>(int saveID = 0) where T : class
        {
            return LoadObject<T>(typeof(T).Name, saveID);
        }

        /// <summary>
        /// ��ĳ������Ĵ浵�м���ĳ������
        /// </summary>
        /// <typeparam name="T">Ҫ���ص�ʵ������</typeparam>
        /// <param name="saveItem">�浵��</param>
        /// <returns></returns>
        public static T LoadObject<T>(SaveItem saveItem) where T : class
        {
            return LoadObject<T>(typeof(T).Name, saveItem.saveID);
        }

        /// <summary>
        /// ɾ��ĳ���浵�е�ĳ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveFileName"></param>
        /// <param name="saveID">�浵��ID</param>
        public static void DeleteObject<T>(string saveFileName, int saveID) where T : class
        {
            //��ջ����ж���
            if (GetCache<T>(saveID, saveFileName) != null)
            {
                RemoveCache(saveID, saveFileName);
            }
            // �浵�������ڵ��ļ�·��
            string dirPath = GetSavePath(saveID);
            string savePath = dirPath + "/" + saveFileName;
            //ɾ����Ӧ���ļ�
            File.Delete(savePath);

        }

        /// <summary>
        /// ɾ��ĳ���浵�е�ĳ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveFileName"></param>
        /// <param name="saveItem">�浵��ID</param>
        public static void DeleteObject<T>(string saveFileName, SaveItem saveItem) where T : class
        {
            DeleteObject<T>(saveFileName, saveItem.saveID);
        }

        /// <summary>
        /// ɾ��ĳ���浵�е�ĳ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveID">�浵��ID</param>
        public static void DeleteObject<T>(int saveID) where T : class
        {
            DeleteObject<T>(typeof(T).Name, saveID);
        }

        /// <summary>
        /// ɾ��ĳ���浵�е�ĳ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveItem"></param>
        public static void DeleteObject<T>(SaveItem saveItem) where T : class
        {
            DeleteObject<T>(typeof(T).Name, saveItem.saveID);
        }
        #endregion

        #region ���桢��ȡȫ�����ô浵
        /// <summary>
        /// �������ã�ȫ����Ч�����غ��κ�һ���浵
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadSetting<T>(string fileName) where T : class
        {
            return LoadFile<T>(settingDirPath + "/" + fileName);
        }

        /// <summary>
        /// �������ã�ȫ����Ч�����غ��κ�һ���浵
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadSetting<T>() where T : class
        {
            return LoadSetting<T>(typeof(T).Name);
        }

        /// <summary>
        /// �������ã�ȫ����Ч�����غ��κ�һ���浵
        /// </summary>
        /// <param name="saveObject"></param>
        /// <param name="fileName"></param>
        public static void SaveSetting(object saveObject, string fileName)
        {
            SaveFile(saveObject, settingDirPath + "/" + fileName);
        }

        /// <summary>
        /// �������ã�ȫ����Ч�����غ��κ�һ���浵
        /// </summary>
        /// <param name="saveObject"></param>
        public static void SaveSetting(object saveObject)
        {
            SaveSetting(saveObject, saveObject.GetType().Name);
        }

        public static void DeleteAllSetting()
        {
            if (Directory.Exists(settingDirPath))
            {
                // ֱ��ɾ��Ŀ¼
                Directory.Delete(settingDirPath, true);
            }
            CheckAndCreateDir();
        }
        #endregion

        #region �ڲ����ߺ���
        /// <summary>
        /// ��ȡ�浵ϵͳ����
        /// </summary>
        private static void InitSaveSystemData()
        {
            saveSystemData = LoadFile<SaveSystemData>(saveDirPath + "/SaveSystemData");
            if (saveSystemData == null)
            {
                saveSystemData = new SaveSystemData();
                UpdateSaveSystemData();
            }
        }

        /// <summary>
        /// ���´浵ϵͳ����
        /// </summary>
        private static void UpdateSaveSystemData()
        {
            SaveFile(saveSystemData, saveDirPath + "/SaveSystemData");
        }

        /// <summary>
        /// ���·��������Ŀ¼
        /// </summary>
        private static void CheckAndCreateDir()
        {
            // ȷ��·���Ĵ���
            if (Directory.Exists(saveDirPath) == false)
            {
                Directory.CreateDirectory(saveDirPath);
            }
            if (Directory.Exists(settingDirPath) == false)
            {
                Directory.CreateDirectory(settingDirPath);
            }
        }

        /// <summary>
        /// ��ȡĳ���浵��·��
        /// </summary>
        /// <param name="saveID">�浵��ID</param>
        /// <param name="createDir">������������·�����Ƿ���Ҫ����</param>
        /// <returns></returns>
        private static string GetSavePath(int saveID, bool createDir = true)
        {
            // ��֤�Ƿ���ĳ���浵
            if (GetSaveItem(saveID) == null) throw new Exception("JK:saveID �浵�����ڣ�");

            string saveDir = saveDirPath + "/" + saveID;
            // ȷ���ļ����Ƿ����
            if (Directory.Exists(saveDir) == false)
            {
                if (createDir)
                {
                    Directory.CreateDirectory(saveDir);
                }
                else
                {
                    return null;
                }
            }

            return saveDir;
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="saveObject">����Ķ���</param>
        /// <param name="path">�����·��</param>
        private static void SaveFile(object saveObject, string path)
        {
            switch (JKFrameRoot.Setting.SaveSystemType)
            {
                case SaveSystemType.Binary:
                    IOTool.SaveFile(saveObject, path);
                    break;
                case SaveSystemType.Json:
                    string jsonData = JsonUtility.ToJson(saveObject);
                    IOTool.SaveJson(jsonData, path);
                    break;
            }
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <typeparam name="T">���غ�ҪתΪ������</typeparam>
        /// <param name="path">����·��</param>
        /// <returns></returns>
        private static T LoadFile<T>(string path) where T : class
        {
            switch (JKFrameRoot.Setting.SaveSystemType)
            {
                case SaveSystemType.Binary:
                    return IOTool.LoadFile<T>(path);
                case SaveSystemType.Json:
                    return IOTool.LoadJson<T>(path);
            }
            return null;
        }
        #endregion
    }
}