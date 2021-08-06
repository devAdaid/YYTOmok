using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentations
{
    public class RpgGameActorEntry : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _hpText;
        [SerializeField]
        private Image _actorImage;
        [field: SerializeField]
        private Sprite _whiteSprite;
        [field: SerializeField]
        private Sprite _blackSprite;

        public void ApplyHp(int hp, int maxHp)
        {
            _hpText.text = $"{hp}/{maxHp}";
        }

        public void ApplyColor(OmokStoneColor stoneColor)
        {
            if (stoneColor == OmokStoneColor.Black)
            {
                _actorImage.sprite = _blackSprite;
            }
            else
            {
                _actorImage.sprite = _whiteSprite;
            }
        }
    }
}