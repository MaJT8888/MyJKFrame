namespace JKFrame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// 普通类  对象  对象池数据
    /// </summary>
    public class ObjectPoolData
    {
        #region ObjectPoolData持有的数据及初始化方法
        //对象容器
        public Queue<object> PoolQueue;
        //容量限制 -1代表无限
        public int maxCapacity = -1;
        public ObjectPoolData(int capacity = -1)
        {
            maxCapacity = capacity;
            if (maxCapacity == -1) PoolQueue = new Queue<object>();
            else PoolQueue = new Queue<object>(capacity);
        }
        #endregion

        #region ObjectPool数据相关操作
        /// <summary>
        /// 将对象放进对象池
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool PushObj(object obj)
        {
            //检测是不是超过容量
            if (maxCapacity != -1 && PoolQueue.Count >= maxCapacity)
            {
                return false;
            }
            PoolQueue.Enqueue(obj);
            return true;
        }
        /// <summary>
        /// 将对象池缩减  区分重复调用还是一次性缩减到多少
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
        /// 从对象池中获取对象
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