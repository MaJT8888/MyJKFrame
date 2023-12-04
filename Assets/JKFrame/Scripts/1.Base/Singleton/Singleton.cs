namespace JKFrame
{
    /// <summary>
    /// ����ģʽ�Ļ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : Singleton<T>, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}