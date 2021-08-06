using System;
using AY.Core;
using ModelChangeEvents;

namespace Models
{
    public class CommonGame : Model
    {
        public ActorType CurrentActor { get; private set; }
        public ActorType NotCurrentActor => CurrentActor.GetOpponentActor();
        public int TurnCount { get; private set; }
        public bool IsGameEnd { get; private set; }

        public CommonGame()
        {
            TurnCount = 1;

            var rand = new Random();
            var randIndex = rand.Next(2);
            switch (randIndex)
            {
                case 0:
                    {
                        CurrentActor = ActorType.Player;
                        break;
                    }
                case 1:
                    {
                        CurrentActor = ActorType.Opponent;
                        break;
                    }
            }
        }

        public void OnGameEnd()
        {
            IsGameEnd = true;
        }

        public void OnTurnEnd()
        {
            ChangeActor();
            TurnCount += 1;
        }

        private void ChangeActor()
        {
            CurrentActor = CurrentActor.GetOpponentActor();
            SendEventDirectly<CommonGameEvents.ActorChanged>(new CommonGameEvents.ActorChanged(CurrentActor));
        }
    }
}