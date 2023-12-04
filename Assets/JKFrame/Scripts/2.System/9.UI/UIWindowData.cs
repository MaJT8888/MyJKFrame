using Sirenix.OdinInspector;
using UnityEngine;
namespace JKFrame
{
    /// <summary>
    /// UIԪ������
    /// </summary>
    public class UIWindowData
    {
        [LabelText("�Ƿ���Ҫ����")] public bool isCache;
        [LabelText("Ԥ����Path��AssetKey")] public string assetPath;
        [LabelText("UI�㼶")] public int layerNum;
        /// <summary>
        /// ���Ԫ�صĴ��ڶ���
        /// </summary>
        [LabelText("����ʵ��")] public UI_WindowBase instance;

        public UIWindowData(bool isCache, string assetPath, int layerNum)
        {
            this.isCache = isCache;
            this.assetPath = assetPath;
            this.layerNum = layerNum;
            instance = null;
        }
    }
}