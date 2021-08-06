using System.Collections.Generic;
using AY.Core;

namespace Presentations
{
    public interface IRpgCardSelectView : IView
    {
        void ApplyCards(List<SkilCardType> cards);
        void SetEnable(bool enable);
    }
}