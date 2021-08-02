
using AY.Core;
using UnityEngine;

namespace Models
{
    public class OmokGame : Model<OmokGame>
    {
        public OmokPlayer CurrentPlayer { get; private set; } = OmokPlayer.Black;
        public OmokGridState[,] BoardState { get; private set; } = new OmokGridState[Define.OMOK_COUNT, Define.OMOK_COUNT];

        public bool PlaceStone(OmokPlayer player, int rowIndex, int colIndex)
        {
            if (player != CurrentPlayer)
            {
                return false;
            }

            if (!CanPlaceStone(rowIndex, colIndex))
            {
                return false;
            }

            var stone = GetOmokStone(player);
            BoardState[rowIndex, colIndex] = stone;
            return true;
        }

        private bool CanPlaceStone(int rowIndex, int colIndex)
        {
            if (!IsValidIndex(rowIndex, colIndex))
            {
                return false;
            }

            if (BoardState[rowIndex, colIndex] != OmokGridState.Empty)
            {
                return false;
            }

            return true;
        }

        private bool IsValidIndex(int rowIndex, int colIndex)
        {
            return rowIndex >= 0
                && rowIndex < Define.OMOK_COUNT
                && colIndex >= 0
                && colIndex < Define.OMOK_COUNT;
        }

        private OmokGridState GetOmokStone(OmokPlayer player)
        {
            switch (player)
            {
                case OmokPlayer.Black:
                    return OmokGridState.Black;
                case OmokPlayer.White:
                    return OmokGridState.White;
            }

            Debug.LogError($"{player}은 지원되지 않는 {nameof(OmokPlayer)} 타입");
            return OmokGridState.Empty;
        }
    }
}