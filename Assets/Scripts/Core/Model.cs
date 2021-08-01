using System.Collections.Generic;

namespace AY.Core
{
    public abstract class Model<T> : IModel<T> where T : IModel<T>
    {
        protected List<IListenModel<T>> _listeners = new List<IListenModel<T>>();

        public void AddListener(IListenModel<T> listener)
        {
            if (_listeners == null)
            {
                _listeners = new List<IListenModel<T>>();
            }

            _listeners.Add(listener);
        }

        public void RemoveListener(IListenModel<T> listener)
        {
            if (_listeners == null)
            {
                _listeners = new List<IListenModel<T>>();
            }

            _listeners.Remove(listener);
        }

        protected virtual void NotifyChange()
        {
            foreach (var listener in _listeners)
            {
                listener.OnDataUpdated();
            }
        }
    }
}