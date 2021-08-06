using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class RpgActor
    {
        public int Hp { get; private set; }
        public readonly int MaxHp;
        public List<RpgActorStatusEffect> StatusEffects = new List<RpgActorStatusEffect>();

        public RpgActor(int maxHp)
        {
            MaxHp = maxHp;
            Hp = maxHp;
        }

        public bool IncreaseHp(int amount)
        {
            var beforeHp = Hp;
            var afterHp = beforeHp + amount;
            if (afterHp > MaxHp)
            {
                afterHp = MaxHp;
            }
            Hp = afterHp;

            return beforeHp != afterHp;
        }

        public bool DecreaseHp(int amount)
        {
            var beforeHp = Hp;
            var afterHp = beforeHp - amount;
            if (afterHp < 0)
            {
                afterHp = 0;
            }
            Hp = afterHp;

            return beforeHp != afterHp;
        }

        public void OnEndTurn()
        {
            foreach (var effect in StatusEffects)
            {
                effect.DecreaseTurn();
            }
            StatusEffects = StatusEffects.Where(effect => effect.RemainTurn > 0).ToList();
        }
    }
}