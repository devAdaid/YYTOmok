using AY.Core;

namespace ModelChangeEvents
{
    public class OmokGameEvents
    {
        public class PlaceOpponentStone : ModelChangeEvent
        {
            public readonly OmokGridPosition Position;
            public readonly OmokStoneColor StoneColor;

            public PlaceOpponentStone(OmokGridPosition position, OmokStoneColor stoneColor)
            {
                Position = position;
                StoneColor = stoneColor;
            }
        }
        public class PlacePlayerStone : ModelChangeEvent
        {
            public readonly OmokGridPosition Position;
            public readonly OmokStoneColor StoneColor;
            public readonly bool IsBoardFull;

            public PlacePlayerStone(OmokGridPosition position, OmokStoneColor stoneColor, bool isBoardFull)
            {
                Position = position;
                StoneColor = stoneColor;
                IsBoardFull = isBoardFull;
            }
        }
    }
}