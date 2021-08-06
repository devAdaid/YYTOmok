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

        public void ApplyHp(int hp, int maxHp)
        {
            _hpText.text = $"{hp}/{maxHp}";
        }
    }
}