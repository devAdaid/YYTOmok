public enum OmokActorType
{
    Invalid,
    Black,
    White,
}

public static class OmokActorExtend
{
    public static OmokActorType GetOpponentActor(this OmokActorType player)
    {
        switch (player)
        {
            case OmokActorType.Black:
                return OmokActorType.White;
            case OmokActorType.White:
                return OmokActorType.Black;
        }
        return OmokActorType.Invalid;
    }
}