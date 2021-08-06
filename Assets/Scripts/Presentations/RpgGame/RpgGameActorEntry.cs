using System.Collections;
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
        [SerializeField]
        private TMP_Text _damageText;
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

        public void ShowDamageFloater(int damage)
        {
            StartCoroutine(DamageAnim(damage));
        }

        private IEnumerator DamageAnim(int damage)
        {
            _damageText.text = $"-{damage}";
            yield return new WaitForSeconds(0.5f);
            _damageText.text = "";
        }
    }
}