using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TempCardStaticData : ScriptableObject
{
    public SkilCardStaticData Strike;
    public SkilCardStaticData Next;
    public SkilCardStaticData Defense;

    public SkilCardStaticData GetSkilCardStaticData(SkilCardType cardType)
    {
        switch (cardType)
        {
            case SkilCardType.Strike:
                return Strike;
            case SkilCardType.NextAttackTwice:
                return Next;
            case SkilCardType.DecreaseDefense:
                return Defense;
        }
        return null;
    }
}
