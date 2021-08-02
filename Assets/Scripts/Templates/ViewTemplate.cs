/*
public class ViewTemplate : View, IXXView
{
    #region View
    protected XXXPresenter _presenter;

    protected override void CreatePresenter()
    {
        _presenter = new XXXPresenter(this);
    }

    protected override void DeletePresenter()
    {
        if (_presenter != null)
        {
            _presenter.RemoveModelListeners();
            _presenter = null;
        }
    }

    protected override void AddUIEvent() {}
    #endregion

    #region From Presenter

    #endregion

    #region To Presenter

    #endregion
}
*/