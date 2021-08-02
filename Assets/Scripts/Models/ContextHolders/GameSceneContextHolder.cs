using AY.Core;

namespace Models
{
    public class GameSceneContextHolder : MonoSingleton<GameSceneContextHolder>
    {
        public readonly OmokGame OmokGame;
    }
}