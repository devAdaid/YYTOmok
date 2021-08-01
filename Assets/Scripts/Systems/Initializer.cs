using System.Collections.Generic;
using AY.Core;
using UnityEngine.Events;

namespace AY.Systems
{
    public class Initializer : MonoSingleton<Initializer>
    {
        public bool Initialized { get; private set; } = false;

        private void Awake()
        {
            Initialized = true;
        }
    }
}