using AY.Core;

namespace Models
{
    public class GameSceneContextHolder : MonoSingleton<GameSceneContextHolder>
    {
        public readonly RpgGame RpgGame = new RpgGame();
        public readonly OmokGame OmokGame = new OmokGame();
    }
}