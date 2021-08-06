
using System.Collections.Generic;
using AY.Core;
using ModelChangeEvents;
using UnityEngine;

namespace Models
{
    public class OmokGame : Model
    {
        public OmokActorType PlayerActor { get; private set; } = OmokActorType.Black;
        public OmokActorType OpponentActor => PlayerActor.GetOpponentActor();
        public OmokActorType CurrentOmokActor { get; private set; } = OmokActorType.Black;
        public OmokStoneColor[,] BoardState { get; private set; } = new OmokStoneColor[Define.OMOK_COUNT, Define.OMOK_COUNT];
        public int TurnCount { get; private set; } = 1;

        public void PlacePlayerStone(OmokGridPosition position)
        {
            if (!IsPlayerTurn())
            {
                return;
            }

            if (!CanPlaceStone(position))
            {
                return;
            }

            var stoneColor = GetOmokStoneColor(PlayerActor);
            DoPlaceStone(position, stoneColor);

            SendEventDirectly<OmokGameEvents.PlacePlayerStone>(new OmokGameEvents.PlacePlayerStone(position, stoneColor, IsBoardFull()));
        }

        public void PlaceOpponentStone()
        {
            if (IsPlayerTurn())
            {
                return;
            }

            var position = GetOpponentPlacePosition();
            if (!CanPlaceStone(position))
            {
                return;
            }

            var stoneColor = GetOmokStoneColor(OpponentActor);
            DoPlaceStone(position, stoneColor);

            SendEventDirectly<OmokGameEvents.PlaceOpponentStone>(new OmokGameEvents.PlaceOpponentStone(position, stoneColor));
        }

        private void DoPlaceStone(OmokGridPosition position, OmokStoneColor stoneColor)
        {
            BoardState[position.Row, position.Col] = stoneColor;
            CurrentOmokActor = CurrentOmokActor.GetOpponentActor();
            TurnCount += 1;
        }

        public bool IsPlayerTurn()
        {
            return CurrentOmokActor == PlayerActor;
        }

        private bool CanPlaceStone(int rowIndex, int colIndex)
        {
            return CanPlaceStone(new OmokGridPosition(rowIndex, colIndex));
        }

        private bool CanPlaceStone(OmokGridPosition position)
        {
            if (!position.IsValid())
            {
                return false;
            }

            if (BoardState[position.Row, position.Col] != OmokStoneColor.Empty)
            {
                return false;
            }

            return true;
        }

        private OmokStoneColor GetOmokStoneColor(OmokActorType player)
        {
            switch (player)
            {
                case OmokActorType.Black:
                    return OmokStoneColor.Black;
                case OmokActorType.White:
                    return OmokStoneColor.White;
            }

            Debug.LogError($"{player}은 지원되지 않는 {nameof(OmokActorType)} 타입");
            return OmokStoneColor.Empty;
        }

        private bool IsBoardFull()
        {
            return TurnCount < (Define.OMOK_COUNT * Define.OMOK_COUNT);
        }

        private OmokGridPosition GetOpponentPlacePosition()
        {
            // TODO: AI 구현
            var candidates = new List<OmokGridPosition>();
            for (int row = 0; row < Define.OMOK_COUNT; ++row)
            {
                for (int col = 0; col < Define.OMOK_COUNT; ++col)
                {
                    var position = new OmokGridPosition(row, col);
                    if (CanPlaceStone(position))
                    {
                        candidates.Add(position);
                    }
                }
            }

            if (candidates.Count == 0)
            {
                return OmokGridPosition.INVALID;
            }

            var randIndex = UnityEngine.Random.Range(0, candidates.Count);
            return candidates[randIndex];
        }
    }
}