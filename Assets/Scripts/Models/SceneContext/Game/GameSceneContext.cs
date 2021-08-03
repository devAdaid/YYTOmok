using AY.Core;

namespace Models
{
    public class GameSceneContext
    {
        public readonly OmokGame OmokGame;

        public GameSceneContext()
        {
            OmokGame = new OmokGame();
        }
    }
}