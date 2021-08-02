using AY.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentaions
{
    public class OmokGameView : View, IOmokGameView
    {
        [SerializeField]
        private OmokViewHelper _viewHelper;

        #region View
        protected OmokGamePresenter _presenter;

        protected override void CreatePresenter()
        {
            _presenter = new OmokGamePresenter(this);
        }

        protected override void DeletePresenter()
        {
            if (_presenter != null)
            {
                _presenter.RemoveModelListeners();
                _presenter = null;
            }
        }

        protected override void AddUIEvent() { }
        #endregion

        #region From Presenter
        public void ApplyBoardState(OmokGridState[,] boardState)
        {

        }
        #endregion

        #region To Presenter

        #endregion

        public void OnClicked(BaseEventData e)
        {
            var eventData = (PointerEventData)e;
        }
    }
}