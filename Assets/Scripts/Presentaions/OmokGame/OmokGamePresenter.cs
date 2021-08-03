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
            if (eventBox.TryGetEvent<PlaceOmokStone>(out var placeEvent))
            {
                _view.PlaceStone(placeEvent.RowIndex, placeEvent.ColIndex, placeEvent.StoneColor);
            }
        }

        public override void InitializeView()
        {
            //_view.ApplyBoardState(_omokGame.BoardState);
        }
        #endregion

        #region To View

        #endregion

        #region From View
        public void PlaceStone(int rowIndex, int colIndex)
        {
            _omokGame.PlaceStone(_omokGame.CurrentPlayer, rowIndex, colIndex);
        }
        #endregion
    }
}