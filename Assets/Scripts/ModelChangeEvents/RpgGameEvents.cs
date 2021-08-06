using AY.Core;

namespace ModelChangeEvents
{
    public class RpgGameEvents
    {
        public class Attack : ModelChangeEvent
        {
            public readonly ActorType Performer;
            public readonly ActorType Target;
            public readonly int Damage;

            public Attack(ActorType performer, ActorType target, int damage)
            {
                Performer = performer;
                Target = target;
                Damage = damage;
            }
        }
    }
}