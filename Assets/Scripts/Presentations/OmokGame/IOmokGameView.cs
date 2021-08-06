using AY.Core;

namespace Presentations
{
    public interface IOmokGameView : IView
    {
        void PlaceStone(OmokGridPosition position, OmokStoneColor stoneColor);
        void ApplyState(ActorType currentActor, OmokStoneColor currentActorColor, int turnCount);
        void WaitForOpponent();
    }
}