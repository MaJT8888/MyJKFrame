namespace JKFrame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// ��ͨ��  ����  ���������
    /// </summary>
    public class ObjectPoolData
    {
        #region ObjectPoolData���е����ݼ���ʼ������
        //��������
        public Queue<object> PoolQueue;
        //�������� -1��������
        public int maxCapacity = -1;
        public ObjectPoolData(int capacity = -1)
        {
            maxCapacity = capacity;
            if (maxCapacity == -1) PoolQueue = new Queue<object>();
            else PoolQueue = new Queue<object>(capacity);
        }
        #endregion

        #region ObjectPool������ز���
        /// <summary>
        /// ������Ž������
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool PushObj(object obj)
        {
            //����ǲ��ǳ�������
            if (maxCapacity != -1 && PoolQueue.Count >= maxCapacity)
            {
                return false;
            }
            PoolQueue.Enqueue(obj);
            return true;
        }
        /// <summary>
        /// �����������  �����ظ����û���һ��������������
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public bool DelObj(int len)
        {
            for (int i = 0; i < len; i++)
            {
                PoolQueue.Dequeue();
            }
            return true;
        }
        /// <summary>
        /// �Ӷ�����л�ȡ����
        /// </summary>
        /// <returns></returns>
        public object GetObj()
        {
            return PoolQueue.Dequeue();
        }
        public void Destory(bool pushThisToPool = false)
        {
            PoolQueue.Clear();
            maxCapacity = -1;
            if (pushThisToPool)
            {
                PoolSystem.PushObject(this);
            }
        }
        #endregion
    }
}