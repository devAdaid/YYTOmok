namespace AY.Core
{
    public abstract class Presenter<T> : IPresenter<T> where T : IView
    {
        protected T _view;

        public Presenter(T view)
        {
            _view = view;

            SetModels();
            AddModelListeners();

            InitializeView();
        }

        public abstract void AddModelListeners();

        public abstract void RemoveModelListeners();

        public abstract void InitializeView();

        protected abstract void SetModels();
    }
}