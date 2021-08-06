public enum ActorType
{
    Invalid,
    Player,
    Opponent,
}

public static class ActorTypeExtend
{
    public static ActorType GetOpponentActor(this ActorType actorType)
    {
        switch (actorType)
        {
            case ActorType.Player:
                return ActorType.Opponent;
            case ActorType.Opponent:
                return ActorType.Player;
        }
        return ActorType.Invalid;
    }

    public static string GetText(this ActorType actorType)
    {
        switch (actorType)
        {
            case ActorType.Player:
                return "나";
            case ActorType.Opponent:
                return "상대";
        }
        return string.Empty;
    }
}