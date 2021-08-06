using AY.Core;

namespace Presentations
{
    public interface IRpgGameView : IView
    {
        void Attack(ActorType performer, ActorType target, int damage);
        void ApplyHp(ActorType performer, int hp, int maxHp);
        void ApplyColor(ActorType actorType, OmokStoneColor stoneColor);
    }
}