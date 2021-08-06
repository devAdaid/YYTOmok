
using System;
using System.Collections.Generic;
using AY.Core;
using ModelChangeEvents;

namespace Models
{
    public class OmokGame : Model
    {
        public readonly OmokStoneColor[,] BoardState;
        public readonly OmokStoneColor PlayerColor;
        public bool IsGameEnd { get; private set; }
        public OmokStoneColor OpponentColor => PlayerColor.GetOpponentColor();
        public ActorType CurrentActor { get; private set; }
        public int TurnCount { get; private set; }
        public int PlaceCount { get; private set; }

        private static readonly OmokStoneColor FIRST_ACTOR_COLOR = OmokStoneColor.Black;

        public OmokGame()
        {
            BoardState = new OmokStoneColor[Define.OMOK_COUNT, Define.OMOK_COUNT];
            TurnCount = 1;
            PlaceCount = 0;

            // 선턴 결정
            var rand = new Random();
            var randIndex = rand.Next(2);
            switch (randIndex)
            {
                case 0:
                    {
                        CurrentActor = ActorType.Player;
                        PlayerColor = FIRST_ACTOR_COLOR;
                        break;
                    }
                case 1:
                    {
                        CurrentActor = ActorType.Opponent;
                        PlayerColor = FIRST_ACTOR_COLOR.GetOpponentColor();
                        break;
                    }
            }
        }

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

            DoPlaceStone(position, PlayerColor);
        }

        public void PlaceOpponentStone()
        {
            if (IsPlayerTurn())
            {
                return;
            }

            var position = GetAutoPlacePosition();
            if (!CanPlaceStone(position))
            {
                return;
            }

            DoPlaceStone(position, OpponentColor);
        }

        private void DoPlaceStone(OmokGridPosition position, OmokStoneColor stoneColor)
        {
            BoardState[position.Row, position.Col] = stoneColor;
            PlaceCount += 1;

            if (IsBoardFull())
            {
                IsGameEnd = true;
            }
            else
            {
                CurrentActor = CurrentActor.GetOpponentActor();
                TurnCount += 1;
            }

            SendEventDirectly<OmokGameEvents.PlaceStone>(new OmokGameEvents.PlaceStone(position, stoneColor));
        }

        public bool IsPlayerTurn()
        {
            return CurrentActor == ActorType.Player;
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

        public OmokStoneColor GetOmokStoneColor(ActorType actorType)
        {
            switch (actorType)
            {
                case ActorType.Player:
                    return PlayerColor;
                case ActorType.Opponent:
                    return OpponentColor;
            }

            UnityEngine.Debug.LogError($"{actorType}은 지원되지 않는 {nameof(ActorType)} 타입");
            return OmokStoneColor.Empty;
        }

        public bool IsBoardFull()
        {
            return PlaceCount >= (Define.OMOK_COUNT * Define.OMOK_COUNT);
        }

        private OmokGridPosition GetAutoPlacePosition()
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