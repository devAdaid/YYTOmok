
using AY.Core;
using ModelChangeEvents;
using UnityEngine;

namespace Models
{
    public class OmokGame : Model
    {
        public OmokPlayer PlayerColor { get; private set; } = OmokPlayer.Black;
        public OmokPlayer CurrentPlayer { get; private set; } = OmokPlayer.Black;
        public OmokStoneColor[,] BoardState { get; private set; } = new OmokStoneColor[Define.OMOK_COUNT, Define.OMOK_COUNT];

        public void PlaceStone(OmokPlayer player, int rowIndex, int colIndex)
        {
            if (player != CurrentPlayer)
            {
                return;
            }

            if (!CanPlaceStone(rowIndex, colIndex))
            {
                return;
            }

            var stoneColor = GetOmokStoneColor(player);
            BoardState[rowIndex, colIndex] = stoneColor;
            CurrentPlayer = CurrentPlayer.GetOpponentPlayer();

            SendEventDirectly<PlaceOmokStone>(new PlaceOmokStone(rowIndex, colIndex, stoneColor));
        }

        private bool CanPlaceStone(int rowIndex, int colIndex)
        {
            if (!IsValidIndex(rowIndex, colIndex))
            {
                return false;
            }

            if (BoardState[rowIndex, colIndex] != OmokStoneColor.Empty)
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

        private OmokStoneColor GetOmokStoneColor(OmokPlayer player)
        {
            switch (player)
            {
                case OmokPlayer.Black:
                    return OmokStoneColor.Black;
                case OmokPlayer.White:
                    return OmokStoneColor.White;
            }

            Debug.LogError($"{player}은 지원되지 않는 {nameof(OmokPlayer)} 타입");
            return OmokStoneColor.Empty;
        }
    }
}