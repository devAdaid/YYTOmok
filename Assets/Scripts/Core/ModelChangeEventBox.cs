using System;
using System.Collections.Generic;

namespace AY.Core
{
    public class ModelChangeEventBox
    {
        private readonly Dictionary<Type, ModelChangeEvent> _changeEvents = new Dictionary<Type, ModelChangeEvent>();

        public void AddEvent<T>(T changeEvent) where T : ModelChangeEvent
        {
            _changeEvents.Add(typeof(T), changeEvent);
        }

        public void ClearEvents()
        {
            _changeEvents.Clear();
        }

        public bool TryGetEvent<T>(out T changeEvent) where T : ModelChangeEvent
        {
            if (_changeEvents.TryGetValue(typeof(T), out var eventFound))
            {
                changeEvent = eventFound as T;
                return true;
            }

            changeEvent = null;
            return false;
        }
    }
}