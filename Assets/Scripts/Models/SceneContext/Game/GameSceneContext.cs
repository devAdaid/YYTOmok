using AY.Core;

namespace Models
{
    public class GameSceneContext : Model<GameSceneContext>
    {
        public OmokGame OmokGame;

        public GameSceneContext()
        {
            OmokGame = new OmokGame();
        }

        public void PlaceStone(OmokPlayer player, int rowIndex, int colIndex)
        {
            if (OmokGame.PlaceStone(player, rowIndex, colIndex))
            {
                NotifyChange();
            }
        }
    }
}