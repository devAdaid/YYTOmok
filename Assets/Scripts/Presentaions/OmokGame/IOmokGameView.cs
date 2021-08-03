using AY.Core;

namespace Presentaions
{
    public interface IOmokGameView : IView
    {
        void PlaceStone(OmokGridPosition position, OmokStoneColor stoneColor, bool waitAI = false);
    }
}