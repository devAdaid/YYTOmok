namespace AY.Core
{
    public interface IModel<T> where T : IModel<T>
    {
        void AddListener(IListenModel<T> listener);
    }
}