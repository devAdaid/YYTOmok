using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Presentaions
{
    public class OmokStoneEntry : MonoBehaviour
    {
        [field: SerializeField]
        private StoneColorStaticData _stoneColorStaticData;
        [field: SerializeField]
        private Image _stoneImage;

        public void ApplyStoneColor(OmokGridState stoneType)
        {
            _stoneImage.color = GetStoneColor(stoneType);
        }

        private Color GetStoneColor(OmokGridState stoneColor)
        {
            switch (stoneColor)
            {
                case OmokGridState.Black:
                    return _stoneColorStaticData.BlackStoneColor;
                case OmokGridState.White:
                    return _stoneColorStaticData.WhiteStoneColor;
            }

            Debug.LogError($"지원하지 않는 {nameof(OmokGridState)} 타입: {stoneColor}");
            return Color.clear;
        }
    }
}