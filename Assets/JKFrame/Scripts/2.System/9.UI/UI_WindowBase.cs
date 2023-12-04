using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JKFrame
{
    /// <summary>
    /// ���ڻ���
    /// </summary>
    public class UI_WindowBase : MonoBehaviour
    {
        protected bool uiEnable;
        public bool UIEnable { get => uiEnable; }
        protected int currentLayer;
        public int CurrentLayer { get => currentLayer; }

        // ��������
        public Type Type { get { return this.GetType(); } }

        public bool EnableLocalization => localizationConfig == null;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init() { }

        public void ShowGeneralLogic(int layerNum)
        {
            this.currentLayer = layerNum;
            if (!uiEnable)
            {
                RegisterEventListener();
                // �󶨱��ػ��¼�
                LocalizationSystem.RegisterLanguageEvent(UpdateLanguageGeneralLogic);
            }

            OnShow();
            OnUpdateLanguage(LocalizationSystem.LanguageType);
            uiEnable = true;
        }

        /// <summary>
        /// ��ʾ
        /// </summary>
        public virtual void OnShow() { }

        /// <summary>
        /// �رյĻ����߼�
        /// </summary>
        public void CloseGeneralLogic()
        {
            uiEnable = false;
            UnRegisterEventListener();
            LocalizationSystem.UnregisterLanguageEvent(UpdateLanguageGeneralLogic);
            OnClose();
        }

        /// <summary>
        /// �ر�ʱ����ִ�е�����
        /// </summary>
        public virtual void OnClose() { }

        /// <summary>
        /// ע���¼�
        /// </summary>
        protected virtual void RegisterEventListener() { }

        /// <summary>
        /// ȡ���¼�
        /// </summary>
        protected virtual void UnRegisterEventListener() { }

        #region ���ػ�
        /// <summary>
        /// �����ػ������в�����ָ��keyʱ�����Զ���ȫ�������г���
        /// </summary>
        [SerializeField, LabelText("���ػ�����")]
        public LocalizationConfig localizationConfig;


        private void UpdateLanguageGeneralLogic(LanguageType languageType)
        {
            OnUpdateLanguage(languageType);
        }
        /// <summary>
        /// �����Ը���ʱ
        /// </summary>
        /// <param name="languageType"></param>
        private void OnUpdateLanguage(LanguageType languageType)
        {

        }
        #endregion
    }
}