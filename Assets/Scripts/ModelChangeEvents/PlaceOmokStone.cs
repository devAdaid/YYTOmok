using System.Drawing;
using AY.Core;

namespace ModelChangeEvents
{
    public class PlaceOmokStone : ModelChangeEvent
    {
        public readonly int RowIndex;
        public readonly int ColIndex;
        public readonly OmokStoneColor StoneColor;

        public PlaceOmokStone(int rowIndex, int colIndex, OmokStoneColor stoneColor)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
            StoneColor = stoneColor;
        }
    }
}