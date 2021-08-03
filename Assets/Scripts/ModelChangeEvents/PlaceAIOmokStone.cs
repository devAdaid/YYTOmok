using System.Drawing;
using AY.Core;

namespace ModelChangeEvents
{
    public class PlaceAIOmokStone : ModelChangeEvent
    {
        public readonly OmokGridPosition Position;
        public readonly OmokStoneColor StoneColor;

        public PlaceAIOmokStone(OmokGridPosition position, OmokStoneColor stoneColor)
        {
            Position = position;
            StoneColor = stoneColor;
        }
    }
}