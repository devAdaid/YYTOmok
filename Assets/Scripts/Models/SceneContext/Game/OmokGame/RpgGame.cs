using System.Collections.Generic;
using AY.Core;
using ModelChangeEvents;

namespace Models
{
    public class RpgGame : Model
    {
        public readonly RpgActor PlayerActor;
        public readonly RpgActor OpponentActor;
        private static readonly int NOMAL_ATTACK_DAMAGE = 5;
        private static readonly int INSTANCE_DEAD_DAMAGE = 9999;

        public bool IsGameEnd => PlayerActor.Hp <= 0 || OpponentActor.Hp <= 0;

        public RpgGame()
        {
            PlayerActor = new RpgActor(100);
            OpponentActor = new RpgActor(100);
        }

        public void Attack(ActorType performer, ActorType target, List<AttackType> attackTypes)
        {
            var targetActor = GetActor(target);
            var damage = CalculateDamage(attackTypes);
            targetActor.DecreaseHp(damage);
            DrawCard(attackTypes);

            SendEventDirectly<RpgGameEvents.Attack>(new RpgGameEvents.Attack(performer, target, damage));
        }

        public void WinOmok(ActorType actorType)
        {
            // TODO
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

        public void DrawCard(List<AttackType> attackTypes)
        {
            foreach (var attackType in attackTypes)
            {
                switch (attackType)
                {
                    case AttackType.DrawNormalCard:
                        {
                            UnityEngine.Debug.Log("일반카드");
                            break;
                        }
                    case AttackType.DrawRareCard:
                        {
                            UnityEngine.Debug.Log("고급카드");
                            break;
                        }
                }
            }
        }
    }
}