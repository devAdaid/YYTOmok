using System.Collections;
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
        public void PlaceStone(OmokGridPosition gridPosition, OmokStoneColor stoneColor, bool waitAI = false)
        {
            var position = _viewHelper.GetWorldPosition(gridPosition);
            var stoneEntry = Instantiate(_stonePrefab, position, Quaternion.identity, transform);
            stoneEntry.ApplyStoneState(_viewHelper.StoneSize, stoneColor);

            if (waitAI)
            {
                StartCoroutine(WaitAITurn());
            }
        }
        #endregion

        #region To Presenter
        public void OnClicked(BaseEventData e)
        {
            var gridPosition = _viewHelper.GetGridPosition(e);
            _presenter.PlacePlayerStone(gridPosition);
        }

        public void OnAIWaitEnd()
        {
            _presenter.RequestAIPlaceStone();
        }
        #endregion

        private IEnumerator WaitAITurn()
        {
            yield return new WaitForSeconds(2f);
            OnAIWaitEnd();
        }
    }
}