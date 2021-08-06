using System.Collections.Generic;
using AY.Core;
using ModelChangeEvents;

namespace Models
{
    public class RpgGame : Model
    {
        public readonly RpgActor PlayerActor;
        public readonly RpgActor OpponentActor;
        public readonly List<SkilCardType> DrawnCards = new List<SkilCardType>();
        private static readonly int NOMAL_ATTACK_DAMAGE = 5;
        private static readonly int INSTANCE_DEAD_DAMAGE = 9999;

        public bool IsActorDead => PlayerActor.Hp <= 0 || OpponentActor.Hp <= 0;

        private readonly CommonGame _commonGame;

        public RpgGame(CommonGame commonGame)
        {
            _commonGame = commonGame;

            PlayerActor = new RpgActor(100);
            OpponentActor = new RpgActor(100);
        }

        public void Attack(ActorType performer, ActorType target, List<AttackType> attackTypes)
        {
            var targetActor = GetActor(target);
            var damage = CalculateDamage(attackTypes);
            var cards = DrawCard(attackTypes);

            if (damage > 0)
            {
                targetActor.DecreaseHp(damage);
                SendEventDirectly<RpgGameEvents.Attack>(new RpgGameEvents.Attack(performer, target, damage));

                if (IsActorDead)
                {
                    _commonGame.OnGameEnd();
                }
            }
            else if (cards.Count > 0)
            {
                DrawnCards.Clear();
                DrawnCards.AddRange(cards);
                if (performer == ActorType.Player)
                {
                    SendEventDirectly<RpgGameEvents.CardDrawn>(new RpgGameEvents.CardDrawn(DrawnCards));
                }
                else
                {
                    // TODO
                    SelectCard(SkilCardType.Strike);
                }
            }
        }

        public void SelectCard(SkilCardType cardType)
        {
            DrawnCards.Clear();
            if (_commonGame.CurrentActor == ActorType.Player)
            {
                SendEventDirectly<RpgGameEvents.CardSelected>(new RpgGameEvents.CardSelected(cardType));
            }
            _commonGame.OnTurnEnd();
        }

        public RpgActor GetActor(ActorType actorType)
        {
            switch (actorType)
            {
                case ActorType.Player:
                    return PlayerActor;
                case ActorType.Opponent:
                    return OpponentActor;
            }
            return null;
        }

        public int CalculateDamage(List<AttackType> attackTypes)
        {
            if (attackTypes.Count == 1)
            {
                var attackType = attackTypes[0];
                switch (attackType)
                {
                    case AttackType.NormalAttack:
                        UnityEngine.Debug.Log("일반공격");
                        return NOMAL_ATTACK_DAMAGE;
                    case AttackType.ComboAttack:
                        UnityEngine.Debug.Log("콤보공격");
                        return NOMAL_ATTACK_DAMAGE + NOMAL_ATTACK_DAMAGE;
                    case AttackType.InstantDead:
                        UnityEngine.Debug.Log("즉사");
                        return INSTANCE_DEAD_DAMAGE;
                }
            }

            return 0;
        }

        public List<SkilCardType> DrawCard(List<AttackType> attackTypes)
        {
            List<SkilCardType> cards = new List<SkilCardType>();
            foreach (var attackType in attackTypes)
            {
                switch (attackType)
                {
                    case AttackType.DrawNormalCard:
                        {
                            UnityEngine.Debug.Log("일반카드");
                            cards = new List<SkilCardType>() { SkilCardType.Strike, SkilCardType.NextAttackTwice, SkilCardType.DecreaseDefense };
                            break;
                        }
                    case AttackType.DrawRareCard:
                        {
                            UnityEngine.Debug.Log("고급카드");
                            cards = new List<SkilCardType>() { SkilCardType.Strike, SkilCardType.NextAttackTwice, SkilCardType.DecreaseDefense };
                            break;
                        }
                }
            }
            return cards;
        }
    }
}