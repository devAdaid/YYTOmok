using System.Drawing;
using AY.Core;

namespace ModelChangeEvents
{
    public class PlacePlayerOmokStone : ModelChangeEvent
    {
        public readonly OmokGridPosition Position;
        public readonly OmokStoneColor StoneColor;
        public readonly bool IsBoardFull;

        public PlacePlayerOmokStone(OmokGridPosition position, OmokStoneColor stoneColor, bool isBoardFull)
        {
            Position = position;
            StoneColor = stoneColor;
            IsBoardFull = isBoardFull;
        }
    }
}