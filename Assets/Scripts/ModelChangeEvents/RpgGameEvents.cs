using System.Collections.Generic;
using AY.Core;
using UnityEngine.UIElements;

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

        public class CardDrawn : ModelChangeEvent
        {
            public readonly List<SkilCardType> Cards;

            public CardDrawn(List<SkilCardType> cards)
            {
                Cards = cards;
            }
        }

        public class CardSelected : ModelChangeEvent
        {
            public readonly SkilCardType CardType;

            public CardSelected(SkilCardType cardType)
            {
                CardType = cardType;
            }
        }
    }
}