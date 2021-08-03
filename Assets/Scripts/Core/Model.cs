using System.Collections.Generic;

namespace AY.Core
{
    public abstract class Model : IModel
    {
        protected readonly ModelChangeEventBox _eventBox = new ModelChangeEventBox();
        protected readonly List<IListenModel> _listeners = new List<IListenModel>();

        public void AddListener(IListenModel listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IListenModel listener)
        {
            if (!_listeners.Contains(listener))
            {
                return;
            }

            _listeners.Remove(listener);
        }

        public void AddEvent<T>(T changeEvent) where T : ModelChangeEvent
        {
            _eventBox.AddEvent<T>(changeEvent);
        }

        public void SendEventDirectly<T>(T changeEvent) where T : ModelChangeEvent
        {
            _eventBox.AddEvent<T>(changeEvent);
            SendEvent();
        }

        public void SendEvent()
        {
            foreach (var listener in _listeners)
            {
                listener.OnDataUpdated(_eventBox);
            }
            _eventBox.ClearEvents();
        }
    }
}