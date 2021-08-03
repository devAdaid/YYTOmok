using AY.Core;

namespace Presentaions
{
    public interface IOmokGameView : IView
    {
        void PlaceStone(int rowIndex, int colIndex, OmokStoneColor stoneColor);
    }
}