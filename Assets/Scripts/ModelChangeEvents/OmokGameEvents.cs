using AY.Core;

namespace ModelChangeEvents
{
    public class OmokGameEvents
    {
        public class PlaceStone : ModelChangeEvent
        {
            public readonly OmokGridPosition Position;
            public readonly OmokStoneColor StoneColor;

            public PlaceStone(OmokGridPosition position, OmokStoneColor stoneColor)
            {
                Position = position;
                StoneColor = stoneColor;
            }
        }
    }
}