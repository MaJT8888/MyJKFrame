using System;
using System.Collections.Generic;

namespace JKFrame
{
    public interface IStateMachineOwner { }

    /// <summary>
    /// ״̬��������
    /// </summary>
    public class StateMachine
    {
        // ��ǰ״̬
        public Type CurrStateType { get; private set; } = null;

        // ��ǰ��Ч�е�״̬
        private StateBase currStateObj;

        // ����
        private IStateMachineOwner owner;

        // ���е�״̬ Key:״̬ö�ٵ�ֵ Value:�����״̬
        private Dictionary<Type, StateBase> stateDic = new Dictionary<Type, StateBase>();

        private Dictionary<string, object> stateShareDataDic;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="owner">����</param>
        /// <param name="enableStateShareData">����״̬�������ݣ�����ע�����װ��Ͳ��������</param>
        /// <typeparam name="T">��ʼ״̬����</typeparam>
        public void Init<T>(IStateMachineOwner owner, bool enableStateShareData = false) where T : StateBase, new()
        {
            this.owner = owner;
            if (enableStateShareData) stateShareDataDic = new Dictionary<string, object>();
            ChangeState<T>();
        }
        /// <summary>
        /// ��ʼ������Ĭ��״̬��״̬��������
        /// </summary>
        /// <param name="owner">����</param>
        public void Init(IStateMachineOwner owner, bool enableStateShareData = false)
        {
            if (enableStateShareData) stateShareDataDic = new Dictionary<string, object>();
            this.owner = owner;
        }

        #region ״̬
        /// <summary>
        /// �л�״̬
        /// </summary>
        /// <typeparam name="T">����Ҫ�л�����״̬�ű�����</typeparam>
        /// <param name="newState">��״̬</param>
        /// <param name="reCurrstate">��״̬�͵�ǰ״̬һ�µ�����£��Ƿ�ҲҪ�л�</param>
        /// <returns></returns>
        public bool ChangeState<T>(bool reCurrstate = false) where T : StateBase, new()
        {
            Type stateType = typeof(T);
            // ״̬һ�£����Ҳ���Ҫˢ��״̬�����л�ʧ��
            if (stateType == CurrStateType && !reCurrstate) return false;

            // �˳���ǰ״̬
            if (currStateObj != null)
            {
                currStateObj.Exit();
                currStateObj.RemoveUpdate(currStateObj.Update);
                currStateObj.RemoveLateUpdate(currStateObj.LateUpdate);
                currStateObj.RemoveFixedUpdate(currStateObj.FixedUpdate);
            }
            // ������״̬
            currStateObj = GetState<T>();
            CurrStateType = stateType;
            currStateObj.Enter();
            currStateObj.AddUpdate(currStateObj.Update);
            currStateObj.AddLateUpdate(currStateObj.LateUpdate);
            currStateObj.AddFixedUpdate(currStateObj.FixedUpdate);

            return true;
        }

        /// <summary>
        /// �Ӷ���ػ�ȡһ��״̬
        /// </summary>
        private StateBase GetState<T>() where T : StateBase, new()
        {
            Type stateType = typeof(T);
            if (stateDic.TryGetValue(stateType, out var st)) return st;
            StateBase state = ResSystem.GetOrNew<T>();
            state.InitInternalData(this);
            state.Init(owner);
            stateDic.Add(stateType, state);
            return state;
        }

        /// <summary>
        /// ֹͣ����
        /// ������״̬���ͷţ�����StateMachineδ�������Թ���
        /// </summary>
        public void Stop()
        {
            // ����ǰ״̬�Ķ����߼�
            if (currStateObj != null)
            {
                currStateObj.Exit();
                currStateObj.RemoveUpdate(currStateObj.Update);
                currStateObj.RemoveLateUpdate(currStateObj.LateUpdate);
                currStateObj.RemoveFixedUpdate(currStateObj.FixedUpdate);
                currStateObj = null;
            }
            CurrStateType = null;
            // ������������״̬���߼�
            foreach (var state in stateDic.Values)
            {
                state.UnInit();
            }
            stateDic.Clear();
        }
        #endregion
        #region ״̬��������
        public bool TryGetShareData<T>(string key, out T data)
        {
            bool res = stateShareDataDic.TryGetValue(key, out object stateData);
            if (res)
            {
                data = (T)stateData;
            }
            else
            {
                data = default(T);
            }
            return res;
        }
        public void AddShareData(string key, object data)
        {
            stateShareDataDic.Add(key, data);
        }
        public bool RemoveShareData(string key)
        {
            return stateShareDataDic.Remove(key);
        }
        public bool ContainsShareData(string key)
        {
            return stateShareDataDic.ContainsKey(key);
        }
        public bool UpdateShareData(string key, object data)
        {
            if (ContainsShareData(key))
            {
                stateShareDataDic[key] = data;
                return true;
            }
            else return false;
        }
        public void CleanShareData()
        {
            stateShareDataDic?.Clear();
        }
        #endregion

        /// <summary>
        /// ���٣�����Ӧ���ͷŵ�StateMachine������
        /// </summary>
        public void Destroy()
        {
            // ��������״̬
            Stop();
            // �����������
            CleanShareData();
            // ����������Դ������
            owner = null;
            // �Ž������
            this.ObjectPushPool();
        }
    }
}