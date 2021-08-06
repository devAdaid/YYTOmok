using AY.Core;

namespace Models
{
    public class RpgGame : Model
    {
        public readonly RpgActor PlayerActor;
        public readonly RpgActor OpponentActor;
        private static readonly int NOMAL_ATTACK_DAMAGE = 5;

        public RpgGame()
        {
            PlayerActor = new RpgActor(100);
            OpponentActor = new RpgActor(100);
        }

        public void Attack(ActorType performer, ActorType target)
        {
            var targetActor = GetActor(target);
            var damage = CalculateDamage();

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

        public int CalculateDamage()
        {
            return NOMAL_ATTACK_DAMAGE;
        }
    }
}