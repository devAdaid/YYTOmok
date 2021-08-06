using AY.Core;
using ModelChangeEvents;
using Models;

namespace Presentations
{
    public class RpgCardSelectPresenter : Presenter<IRpgCardSelectView>, IListenModel
    {
        private RpgGame _rpgGame;

        #region Presenter
        public RpgCardSelectPresenter(RpgCardSelectView view) : base(view) { }

        protected override void SetModels()
        {
            _rpgGame = GameSceneContextHolder.Instance.RpgGame;
        }

        public override void AddModelListeners()
        {
            _rpgGame.AddListener(this);
        }

        public override void RemoveModelListeners()
        {
            _rpgGame.RemoveListener(this);
        }

        public void OnDataUpdated(ModelChangeEventBox eventBox)
        {
            if (eventBox.TryGetEvent<RpgGameEvents.CardDrawn>(out var cardDrawnEvent))
            {
                _view.SetEnable(true);
                _view.ApplyCards(cardDrawnEvent.Cards);
            }

            if (eventBox.TryGetEvent<RpgGameEvents.CardSelected>(out var cardSelectd))
            {
                _view.SetEnable(false);
            }
        }

        public override void InitializeView()
        {
            SetEnable(false);
        }
        #endregion

        #region To View
        public void SetEnable(bool enable)
        {
            _view.SetEnable(enable);
        }
        #endregion

        #region From View
        public void SelectCard(SkilCardType cardType)
        {
            _rpgGame.SelectCard(cardType);
        }
        #endregion
    }
}