using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Presentations
{
    public class OmokStoneEntry : MonoBehaviour
    {
        [field: SerializeField]
        private StoneColorStaticData _stoneColorStaticData;
        [field: SerializeField]
        private Image _stoneImage;
        private RectTransform _rectTf;
        private RectTransform rectTf
        {
            get
            {
                if (_rectTf == null)
                {
                    _rectTf = (RectTransform)transform;
                }
                return _rectTf;
            }
        }

        public void ApplyStoneState(float stoneSize, OmokStoneColor stoneType)
        {
            rectTf.sizeDelta = new Vector2(stoneSize, stoneSize);
            _stoneImage.color = GetStoneColor(stoneType);
        }

        private Color GetStoneColor(OmokStoneColor stoneColor)
        {
            switch (stoneColor)
            {
                case OmokStoneColor.Black:
                    return _stoneColorStaticData.BlackStoneColor;
                case OmokStoneColor.White:
                    return _stoneColorStaticData.WhiteStoneColor;
            }

            Debug.LogError($"지원하지 않는 {nameof(OmokStoneColor)} 타입: {stoneColor}");
            return Color.clear;
        }
    }
}