using System.Linq;
using System.Reflection;
using AY.Core;

namespace AY.Systems
{
    public class ModelHolder : Singleton<ModelHolder>
    {
        // Static Datas

        // Player Datas

        // Scene Contexts
        public InterSceneContext InterSceneContext { get; private set; }

        public ModelHolder()
        {
            // TODO
        }

        public void TryLoadData()
        {
            // TODO
        }
    }
}