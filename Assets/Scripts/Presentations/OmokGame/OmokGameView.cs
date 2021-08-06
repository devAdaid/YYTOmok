using System.Collections;
using AY.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

namespace Presentations
{
    public class OmokGameView : View, IOmokGameView
    {
        [SerializeField]
        private OmokViewHelper _viewHelper;
        [SerializeField]
        private Transform _boardParent;
        [SerializeField]
        private OmokStoneEntry _stonePrefab;
        [SerializeField]
        private TMP_Text _stateText;

        private bool inputLocked;

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
        public void PlaceStone(OmokGridPosition gridPosition, OmokStoneColor stoneColor)
        {
            var position = _viewHelper.GetWorldPosition(gridPosition);
            var stoneEntry = Instantiate(_stonePrefab, position, Quaternion.identity, _boardParent);
            stoneEntry.ApplyStoneState(_viewHelper.StoneSize, stoneColor);
        }

        public void ApplyState(ActorType currentActor, OmokStoneColor currentActorColor, int turnCount)
        {
            _stateText.text = $"({currentActorColor.GetText()}) {(currentActor.GetText())}의 턴, {turnCount}턴 째";
        }

        public void WaitForOpponent()
        {
            StartCoroutine(WaitOpponentCoro());
        }
        #endregion

        #region To Presenter
        public void OnClicked(BaseEventData e)
        {
            if (inputLocked)
            {
                return;
            }

            var gridPosition = _viewHelper.GetGridPosition(e);
            _presenter.PlacePlayerStone(gridPosition);
        }

        public void OnOpponentWaitEnd()
        {
            _presenter.RequestOpponentPlaceStone();
        }
        #endregion

        private IEnumerator WaitOpponentCoro()
        {
            SetInputLock(true);

            yield return new WaitForSeconds(2f);
            OnOpponentWaitEnd();

            SetInputLock(false);
        }

        private void SetInputLock(bool enable)
        {
            inputLocked = enable;
        }
    }
}