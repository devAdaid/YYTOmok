using AY.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentaions
{
    public class OmokGameView : View, IOmokGameView
    {
        [SerializeField]
        private OmokViewHelper _viewHelper;
        [SerializeField]
        private OmokStoneEntry _stonePrefab;

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
        public void PlaceStone(int rowIndex, int colIndex, OmokStoneColor stoneColor)
        {
            var position = _viewHelper.GetPosition(rowIndex, colIndex);
            var stoneEntry = Instantiate(_stonePrefab, position, Quaternion.identity, transform);
            stoneEntry.ApplyStoneState(_viewHelper.StoneSize, stoneColor);
        }
        #endregion

        #region To Presenter

        #endregion

        public void OnClicked(BaseEventData e)
        {
            var (rowIndex, colIndex) = _viewHelper.GetGridIndex(e);
            _presenter.PlaceStone(rowIndex, colIndex);
        }
    }
}