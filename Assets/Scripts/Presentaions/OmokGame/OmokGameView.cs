using System.Collections;
using AY.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presentaions
{
    public class OmokGameView : View, IOmokGameView
    {
        [SerializeField]
        private OmokViewHelper _viewHelper;
        [SerializeField]
        private OmokStoneEntry _stonePrefab;
        [SerializeField]
        private TMP_Text _stateText;

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

        public void ApplyState(OmokActorType currentActor, bool isPlayer, int turnCount)
        {
            _stateText.text = $"({currentActor.GetText()}) {(isPlayer ? "나" : "상대")}의 턴, {turnCount}턴 째";
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