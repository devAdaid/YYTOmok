/*
public class PresenterTemplate : Presenter<IXXView>, IListenModel<XXData>
{
    private XXData _XXData;

    #region Presenter
    public XXPresenter(IXXView view) : base(view) { }

    protected override void SetModels()
    {
        _XXData = ModelHolder.Instance.XXData;
    }

    public override void AddModelListeners()
    {
        _XXData.AddListener(this);
    }

    public override void RemoveModelListeners()
    {
        _XXData.RemoveListener(this);
    }

    void IListenModel<XXData>.OnDataUpdated()
    {

    }

    public override void InitializeView()
    {

    }
    #endregion

    #region To View

    #endregion

    #region From View

    #endregion
}
*/