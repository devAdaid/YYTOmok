using AY.Core;

namespace Presentaions
{
    public interface IOmokGameView : IView
    {
        void ApplyBoardState(OmokGridState[,] boardState);
    }
}