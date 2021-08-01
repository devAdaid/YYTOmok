namespace AY.Core
{
    public interface IPresenter<T> where T : IView
    {
        void AddModelListeners();
        void RemoveModelListeners();
    }
}