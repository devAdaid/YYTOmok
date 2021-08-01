using UnityEngine;

namespace AY.Core
{
    public abstract class View : MonoBehaviour
    {
        private bool _initialized = false;

        protected virtual void Awake()
        {
            AddUIEvent();
        }

        protected abstract void CreatePresenter();
        protected abstract void DeletePresenter();

        private void Initialize()
        {
            if (_initialized)
            {
                Debug.LogWarning("[View] 이미 초기화 되었는데도 Initialize가 호출되었습니다.");
                return;
            }

            CreatePresenter();

            _initialized = true;
        }

        private void OnDestroy()
        {
            DeletePresenter();
        }

        protected abstract void AddUIEvent();
    }
}