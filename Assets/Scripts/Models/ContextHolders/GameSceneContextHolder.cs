using AY.Core;

namespace Models
{
    public class GameSceneContextHolder : MonoSingleton<GameSceneContextHolder>
    {
        public readonly RpgGame RpgGame;
        public readonly OmokGame OmokGame;

        public GameSceneContextHolder()
        {
            RpgGame = new RpgGame();
            OmokGame = new OmokGame(RpgGame);
        }
    }
}