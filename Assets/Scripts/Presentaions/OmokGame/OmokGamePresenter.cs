using AY.Core;
using ModelChangeEvents;
using Models;

namespace Presentaions
{
    public class OmokGamePresenter : Presenter<IOmokGameView>, IListenModel
    {
        private OmokGame _omokGame;

        #region Presenter
        public OmokGamePresenter(IOmokGameView view) : base(view) { }

        protected override void SetModels()
        {
            _omokGame = GameSceneContextHolder.Instance.OmokGame;
        }

        public override void AddModelListeners()
        {
            _omokGame.AddListener(this);
        }

        public override void RemoveModelListeners()
        {
            _omokGame.RemoveListener(this);
        }

        public void OnDataUpdated(ModelChangeEventBox eventBox)
        {
            if (eventBox.TryGetEvent<PlacePlayerOmokStone>(out var playerPlaceEvent))
            {
                _view.PlaceStone(playerPlaceEvent.Position, playerPlaceEvent.StoneColor, !playerPlaceEvent.IsBoardFull);
                ApplyState();
            }
            if (eventBox.TryGetEvent<PlaceAIOmokStone>(out var aiPlaceEvent))
            {
                _view.PlaceStone(aiPlaceEvent.Position, aiPlaceEvent.StoneColor);
                ApplyState();
            }
        }

        public override void InitializeView()
        {
            //_view.ApplyBoardState(_omokGame.BoardState);
        }
        #endregion

        #region To View
        private void ApplyState()
        {
            _view.ApplyState(_omokGame.CurrentOmokActor, _omokGame.IsPlayerTurn(), _omokGame.TurnCount);
        }
        #endregion

        #region From View
        public void PlacePlayerStone(OmokGridPosition position)
        {
            _omokGame.PlacePlayerStone(position);
        }

        public void RequestAIPlaceStone()
        {
            _omokGame.PlaceAIStone();
        }
        #endregion
    }
}