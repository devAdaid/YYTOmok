using AY.Core;
using Models;

namespace Presentaions
{
    public class OmokGamePresenter : Presenter<IOmokGameView>, IListenModel<OmokGame>
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

        void IListenModel<OmokGame>.OnDataUpdated()
        {
            _view.ApplyBoardState(_omokGame.BoardState);
        }

        public override void InitializeView()
        {
            _view.ApplyBoardState(_omokGame.BoardState);
        }
        #endregion

        #region To View

        #endregion

        #region From View

        #endregion
    }
}