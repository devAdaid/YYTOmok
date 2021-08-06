using System.Collections;
using System.Collections.Generic;
using AY.Core;
using UnityEngine;

namespace Presentations
{
    public class RpgCardSelectView : View, IRpgCardSelectView
    {
        [SerializeField]
        private GameObject _root;

        [SerializeField]
        private List<RpgCardEntry> _entries;

        #region View
        protected RpgCardSelectPresenter _presenter;

        protected override void CreatePresenter()
        {
            _presenter = new RpgCardSelectPresenter(this);
        }

        protected override void DeletePresenter()
        {
            if (_presenter != null)
            {
                _presenter.RemoveModelListeners();
                _presenter = null;
            }
        }

        protected override void AddUIEvent()
        {
            foreach (var entry in _entries)
            {
                entry.InjectParent(this);
            }
        }
        #endregion

        #region From Presenter
        public void ApplyCards(List<SkilCardType> cards)
        {
            for (int i = 0; i < cards.Count; ++i)
            {
                _entries[i].gameObject.SetActive(true);
                _entries[i].ApplyData(cards[i]);
            }

            for (int i = cards.Count; i < _entries.Count; ++i)
            {
                _entries[i].gameObject.SetActive(false);
            }
        }

        public void SetEnable(bool enable)
        {
            _root.SetActive(enable);
        }
        #endregion

        #region To Presenter
        public void SelectCard(SkilCardType cardType)
        {
            _presenter.SelectCard(cardType);
        }
        #endregion
    }
}