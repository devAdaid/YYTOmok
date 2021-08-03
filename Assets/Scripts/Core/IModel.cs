namespace AY.Core
{
    public interface IModel
    {
        void AddListener(IListenModel listener);
        void RemoveListener(IListenModel listener);
    }
}