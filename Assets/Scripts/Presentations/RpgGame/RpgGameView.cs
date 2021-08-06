using AY.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Presentations
{
    public class RpgGameView : View, IRpgGameView
    {
        [SerializeField]
        private RpgGameActorEntry _playerEntry;
        [SerializeField]
        private RpgGameActorEntry _opponentEntry;

        #region View
        protected RpgGamePresenter _presenter;

        protected override void CreatePresenter()
        {
            _presenter = new RpgGamePresenter(this);
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
        public void Attack(ActorType performer, ActorType target, int damage)
        {
            // TODO
        }

        public void ApplyHp(ActorType actorType, int hp, int maxHp)
        {
            var actorEntry = GetActorEntry(actorType);
            actorEntry.ApplyHp(hp, maxHp);
        }
        #endregion

        #region To Presenter

        #endregion

        private RpgGameActorEntry GetActorEntry(ActorType actorType)
        {
            switch (actorType)
            {
                case ActorType.Player:
                    return _playerEntry;
                case ActorType.Opponent:
                    return _opponentEntry;
            }
            return null;
        }
    }
}