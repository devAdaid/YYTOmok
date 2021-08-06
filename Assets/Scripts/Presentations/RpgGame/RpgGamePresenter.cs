using AY.Core;
using ModelChangeEvents;
using Models;

namespace Presentations
{
    public class RpgGamePresenter : Presenter<IRpgGameView>, IListenModel
    {
        private RpgGame _rpgGame;

        #region Presenter
        public RpgGamePresenter(IRpgGameView view) : base(view) { }

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
            if (eventBox.TryGetEvent<RpgGameEvents.Attack>(out var attackEvent))
            {
                _view.Attack(attackEvent.Performer, attackEvent.Target, attackEvent.Damage);
                ApplyHp(attackEvent.Target);
            }
        }

        public override void InitializeView()
        {
            ApplyHp(ActorType.Player);
            ApplyHp(ActorType.Opponent);
        }
        #endregion

        #region To View
        private void ApplyHp(ActorType actorType)
        {
            var targetActor = _rpgGame.GetActor(actorType);
            _view.ApplyHp(actorType, targetActor.Hp, targetActor.MaxHp);
        }
        #endregion

        #region From View

        #endregion
    }
}