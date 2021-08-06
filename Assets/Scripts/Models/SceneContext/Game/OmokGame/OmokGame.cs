
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
        public int PlaceCount { get; private set; }
        private OmokGridPosition _playerLastPosition = OmokGridPosition.INVALID;
        private OmokGridPosition _opponentLastPosition = OmokGridPosition.INVALID;

        private readonly CommonGame _commonGame;
        private readonly RpgGame _rpgGame;

        private static readonly OmokStoneColor FIRST_ACTOR_COLOR = OmokStoneColor.Black;

        public OmokGame(CommonGame commonGame, RpgGame rpgGame)
        {
            _commonGame = commonGame;
            _rpgGame = rpgGame;

            BoardState = new OmokStoneColor[Define.OMOK_COUNT, Define.OMOK_COUNT];
            PlaceCount = 0;

            switch (commonGame.CurrentActor)
            {
                case ActorType.Player:
                    {
                        PlayerColor = FIRST_ACTOR_COLOR;
                        break;
                    }
                case ActorType.Opponent:
                    {
                        PlayerColor = FIRST_ACTOR_COLOR.GetOpponentColor();
                        break;
                    }
            }
        }

        public void PlacePlayerStone(OmokGridPosition position)
        {
            DoPlaceStone(position, ActorType.Player);
        }

        public void PlaceOpponentStone()
        {
            var position = GetAutoPlacePosition();
            DoPlaceStone(position, ActorType.Opponent);
        }

        private void DoPlaceStone(OmokGridPosition position, ActorType actorType)
        {
            if (actorType != _commonGame.CurrentActor)
            {
                return;
            }

            if (!CanPlaceStone(position))
            {
                return;
            }

            var stoneColor = GetOmokStoneColor(actorType);
            BoardState[position.Row, position.Col] = stoneColor;
            PlaceCount += 1;

            var lastPlacePosition = GetLastPlacePosition(actorType);
            var attackTypes = GetAttackTypes(position, stoneColor, lastPlacePosition);

            _rpgGame.Attack(_commonGame.CurrentActor, _commonGame.NotCurrentActor, attackTypes);

            //TODO
            if (actorType == ActorType.Player)
            {
                _playerLastPosition = position;
            }
            else
            {
                _opponentLastPosition = position;
            }

            if (_commonGame.IsGameEnd)
            {
                // pass
            }
            else if (IsBoardFull())
            {
                _commonGame.OnGameEnd();
            }
            else if (_rpgGame.DrawnCards.Count > 0)
            {
                // pass
            }
            else
            {
                _commonGame.OnTurnEnd();
            }

            SendEventDirectly<OmokGameEvents.PlaceStone>(new OmokGameEvents.PlaceStone(position, stoneColor));
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

        public OmokGridPosition GetLastPlacePosition(ActorType actorType)
        {
            switch (actorType)
            {
                case ActorType.Player:
                    return _playerLastPosition;
                case ActorType.Opponent:
                    return _opponentLastPosition;
            }

            UnityEngine.Debug.LogError($"{actorType}은 지원되지 않는 {nameof(ActorType)} 타입");
            return OmokGridPosition.INVALID;
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

        private List<AttackType> GetAttackTypes(OmokGridPosition position, OmokStoneColor stoneColor, OmokGridPosition lastPlacePosition)
        {
            var horizontalCount = GetHorizontalSameCount(position, stoneColor);
            var verticalCount = GetVerticalSameCount(position, stoneColor);
            var mainDiagonalCount = GetMainDiagonalSameCont(position, stoneColor);
            var antiDiagonalCount = GetAntiDiagonalSameCont(position, stoneColor);

            var attacks = new List<AttackType>();
            var counts = new List<int>() { horizontalCount, verticalCount, mainDiagonalCount, antiDiagonalCount };
            foreach (var count in counts)
            {
                switch (count)
                {
                    case 1:
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            attacks.Add(AttackType.DrawNormalCard);
                            break;
                        }
                    case 4:
                        {
                            attacks.Add(AttackType.DrawRareCard);
                            break;
                        }
                    case 5:
                        return new List<AttackType>() { AttackType.InstantDead };
                }
            }

            if (attacks.Count == 0)
            {
                if (lastPlacePosition.IsValid() && position.IsNeighbor(lastPlacePosition))
                {
                    attacks.Add(AttackType.ComboAttack);
                }
                else
                {
                    attacks.Add(AttackType.NormalAttack);
                }
            }

            return attacks;
        }

        private int GetHorizontalSameCount(OmokGridPosition position, OmokStoneColor stoneColor)
        {
            // 가로 탐색
            int horizontalCount = 1;
            for (int col = position.Col - 1; col >= 0; --col)
            {
                if (!IncreaseCountIfSameColor(position.Row, col, stoneColor, ref horizontalCount))
                {
                    break;
                }
            }
            for (int col = position.Col + 1; col < Define.OMOK_COUNT; ++col)
            {
                if (!IncreaseCountIfSameColor(position.Row, col, stoneColor, ref horizontalCount))
                {
                    break;
                }
            }
            return horizontalCount;
        }

        private int GetVerticalSameCount(OmokGridPosition position, OmokStoneColor stoneColor)
        {
            // 세로 탐색
            int verticalCount = 1;
            for (int row = position.Row - 1; row >= 0; --row)
            {
                if (!IncreaseCountIfSameColor(row, position.Col, stoneColor, ref verticalCount))
                {
                    break;
                }
            }
            for (int row = position.Row + 1; row < Define.OMOK_COUNT; ++row)
            {
                if (!IncreaseCountIfSameColor(row, position.Col, stoneColor, ref verticalCount))
                {
                    break;
                }
            }
            return verticalCount;
        }

        private int GetMainDiagonalSameCont(OmokGridPosition position, OmokStoneColor stoneColor)
        {
            int mainDiagonalCount = 1;
            for (int row = position.Row - 1, col = position.Col - 1; row >= 0 && col >= 0; --row, --col)
            {
                if (!IncreaseCountIfSameColor(row, col, stoneColor, ref mainDiagonalCount))
                {
                    break;
                }
            }
            for (int row = position.Row + 1, col = position.Col + 1; row < Define.OMOK_COUNT && col < Define.OMOK_COUNT; ++row, ++col)
            {
                if (!IncreaseCountIfSameColor(row, col, stoneColor, ref mainDiagonalCount))
                {
                    break;
                }
            }
            return mainDiagonalCount;
        }

        private int GetAntiDiagonalSameCont(OmokGridPosition position, OmokStoneColor stoneColor)
        {
            int antiDiagonalCount = 1;
            for (int row = position.Row - 1, col = position.Col + 1; row >= 0 && col < Define.OMOK_COUNT; --row, ++col)
            {
                if (!IncreaseCountIfSameColor(row, col, stoneColor, ref antiDiagonalCount))
                {
                    break;
                }
            }
            for (int row = position.Row + 1, col = position.Col - 1; row < Define.OMOK_COUNT && col >= 0; ++row, --col)
            {
                if (!IncreaseCountIfSameColor(row, col, stoneColor, ref antiDiagonalCount))
                {
                    break;
                }
            }
            return antiDiagonalCount;
        }

        private bool IncreaseCountIfSameColor(int row, int col, OmokStoneColor stoneColor, ref int counter)
        {
            var targetColor = BoardState[row, col];
            var result = targetColor == stoneColor;
            if (result) counter += 1;
            return result;
        }
    }
}