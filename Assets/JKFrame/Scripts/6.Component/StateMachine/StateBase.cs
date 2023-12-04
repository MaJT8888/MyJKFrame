namespace JKFrame
{
    /// <summary>
    /// ״̬����
    /// </summary>
    public abstract class StateBase
    {
        protected StateMachine stateMachine;

        /// <summary>
        /// ��ʼ���ڲ����ݣ�ϵͳʹ��
        /// </summary>
        /// <param name="stateMachine"></param>
        public void InitInternalData(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        /// <summary>
        /// ��ʼ��״̬
        /// ֻ��״̬��һ�δ���ʱִ��
        /// </summary>
        /// <param name="owner">����</param>
        /// <param name="stateType">״̬����ö�ٵ�ֵ</param>
        /// <param name="stateMachine">����״̬��</param>
        public virtual void Init(IStateMachineOwner owner)
        {
        }

        /// <summary>
        /// ����ʼ��
        /// ����ʹ��ʱ�򣬷Żض����ʱ����
        /// ��һЩ�����ÿգ���ֹ���ܱ�GC
        /// </summary>
        public virtual void UnInit()
        {
            // �Żض����
            this.ObjectPushPool();
        }

        /// <summary>
        /// ״̬����
        /// ÿ�ν��붼��ִ��
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// ״̬�˳�
        /// </summary>
        public virtual void Exit() { }

        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }

        public bool TryGetShareData<T>(string key, out T data)
        {
            return stateMachine.TryGetShareData<T>(key, out data);
        }
        public void AddShareData(string key, object data)
        {
            stateMachine.AddShareData(key, data);
        }
        public void RemoveShareData(string key)
        {
            stateMachine.RemoveShareData(key);
        }
        public void UpdateShareData(string key, object data)
        {
            stateMachine.UpdateShareData(key, data);
        }
        public void CleanShareData()
        {
            stateMachine.CleanShareData();
        }
        public bool ContainsShareData(string key)
        {
            return stateMachine.ContainsShareData(key);
        }
    }
}