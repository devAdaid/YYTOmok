namespace AY.Core
{
    public interface IListenModel
    {
        void OnDataUpdated(ModelChangeEventBox eventBox);
    }
}