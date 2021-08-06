namespace Models
{
    public class RpgActorStatusEffect
    {
        public readonly ActorStatusEffectType EffectType;
        public readonly int MaxTurn;
        public int RemainTurn { get; private set; }

        public RpgActorStatusEffect(ActorStatusEffectType effectType, int turnCount)
        {
            EffectType = effectType;
            MaxTurn = turnCount;
            RemainTurn = turnCount;
        }

        public void DecreaseTurn()
        {
            RemainTurn -= 1;
        }
    }
}