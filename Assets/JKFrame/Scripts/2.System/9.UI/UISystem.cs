using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace JKFrame
{
    /// <summary>
    /// UI���ڵ�
    /// </summary>
    public class UISystem : MonoBehaviour
    {
        private static UISystem instance;
        public static void Init()
        {
            instance = JKFrameRoot.RootTransform.GetComponentInChildren<UISystem>();
        }
        #region �ڲ���
        [Serializable]
        private class UILayer
        {
            public Transform root;
            public bool enableMask = true;
            public Image maskImage;
            private int count = 0;
            public void OnWindowShow()
            {
                count += 1;
                Update();
            }
            public void OnWindowClose()
            {
                count -= 1;
                Update();
            }
            private void Update()
            {
                if (enableMask == false) return;
                maskImage.raycastTarget = count != 0;
                int posIndex = root.childCount - 2;
                maskImage.transform.SetSiblingIndex(posIndex < 0 ? 0 : posIndex);
            }
            public void Reset()
            {
                count = 0;
                Update();
            }
        }
        #endregion
        private static Dictionary<string, UIWindowData> UIWindowDataDic => JKFrameRoot.Setting.UIWindowDataDic;
        [SerializeField] private UILayer[] uiLayers;
        [SerializeField] private RectTransform dragLayer;
        /// <summary>
        /// ��ק�㣬λ������UI�����ϲ�
        /// </summary>
        public static RectTransform DragLayer { get => instance.dragLayer; }
        private static UILayer[] UILayers { get => instance.uiLayers; }

        [SerializeField] GameObject UITipsItemPrefab;
        [SerializeField] private RectTransform UITipsItemParent;

        #region ��̬����/�Ƴ���������
        // UIϵͳ�Ĵ�����������Ҫ������Ԥ����·�����Ƿ񻺴桢��ǰ���ڶ���ʵ������Ҫ��Ϣ
        // Ϊ�˷���ʹ�ã����Դ������ݱ����ȴ����UIWindowDataDic�У�����ͨ��UIϵͳ��ʾ���رյ�


        #endregion
    }

}