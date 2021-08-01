namespace AY.Core
{
    public interface IListenModel<T> where T : IModel<T>
    {
        void OnDataUpdated();
    }
}