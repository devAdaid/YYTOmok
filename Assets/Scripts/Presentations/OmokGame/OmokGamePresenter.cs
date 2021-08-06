using AY.Core;
using ModelChangeEvents;
using Models;
using UnityEngine;

namespace Presentations
{
    public class OmokGamePresenter : Presenter<IOmokGameView>, IListenModel
    {
        private CommonGame _commonGame;
        private OmokGame _omokGame;

        #region Presenter
        public OmokGamePresenter(IOmokGameView view) : base(view) { }

        protected override void SetModels()
        {
            _commonGame = GameSceneContextHolder.Instance.CommonGame;
            _omokGame = GameSceneContextHolder.Instance.OmokGame;
        }

        public override void AddModelListeners()
        {
            _commonGame.AddListener(this);
            _omokGame.AddListener(this);
        }

        public override void RemoveModelListeners()
        {
            _commonGame.RemoveListener(this);
            _omokGame.RemoveListener(this);
        }

        public void OnDataUpdated(ModelChangeEventBox eventBox)
        {
            if (eventBox.TryGetEvent<OmokGameEvents.PlaceStone>(out var placeEvent))
            {
                _view.PlaceStone(placeEvent.Position, placeEvent.StoneColor);
                ApplyState();
            }

            if (eventBox.TryGetEvent<CommonGameEvents.ActorChanged>(out var actorChangeEvent))
            {
                CheckAndApplyOpponentTurn();
            }
        }

        public override void InitializeView()
        {
            ApplyState();
            CheckAndApplyOpponentTurn();
        }
        #endregion

        #region To View
        private void ApplyState()
        {
            _view.ApplyState(_commonGame.CurrentActor, _omokGame.GetOmokStoneColor(_commonGame.CurrentActor), _commonGame.TurnCount);
        }

        private void CheckAndApplyOpponentTurn()
        {
            if (!_omokGame.IsGameEnd
                && _commonGame.CurrentActor == ActorType.Opponent
                && !_omokGame.IsBoardFull())
            {
                _view.WaitForOpponent();
            }
        }
        #endregion

        #region From View
        public void PlacePlayerStone(OmokGridPosition position)
        {
            _omokGame.PlacePlayerStone(position);
        }

        public void RequestOpponentPlaceStone()
        {
            _omokGame.PlaceOpponentStone();
        }
        #endregion
    }
}