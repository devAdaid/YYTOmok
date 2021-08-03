using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StoneColorStaticData : ScriptableObject
{
    [field: SerializeField]
    public Color BlackStoneColor { get; private set; } = Color.white;
    [field: SerializeField]
    public Color WhiteStoneColor { get; private set; } = Color.white;
}
