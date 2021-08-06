using AY.Core;

namespace Models
{
    public class GameSceneContextHolder : MonoSingleton<GameSceneContextHolder>
    {
        public readonly CommonGame CommonGame;
        public readonly RpgGame RpgGame;
        public readonly OmokGame OmokGame;

        public GameSceneContextHolder()
        {
            CommonGame = new CommonGame();
            RpgGame = new RpgGame(CommonGame);
            OmokGame = new OmokGame(CommonGame, RpgGame);
        }
    }
}