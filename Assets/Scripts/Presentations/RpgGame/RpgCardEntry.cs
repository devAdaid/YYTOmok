using System;
using AY.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Presentations
{
    public class RpgCardEntry : MonoBehaviour
    {
        [SerializeField]
        private Image _cardImage;
        [SerializeField]
        private TempCardStaticData _tempData;

        private RpgCardSelectView _parentView;
        private SkilCardType _cardType;

        public void InjectParent(RpgCardSelectView view)
        {
            _parentView = view;
        }

        public void ApplyData(SkilCardType cardType)
        {
            _cardType = cardType;
            _cardImage.sprite = _tempData.GetSkilCardStaticData(cardType).CardSprite;
        }

        public void OnClick()
        {
            _parentView.SelectCard(_cardType);
        }
    }
}