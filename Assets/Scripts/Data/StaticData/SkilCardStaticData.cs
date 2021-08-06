using UnityEngine;

[CreateAssetMenu]
public class SkilCardStaticData : ScriptableObject
{
    [field: SerializeField]
    public SkilCardType CardType { get; private set; }
    [field: SerializeField]
    public Sprite CardSprite { get; private set; }
}
