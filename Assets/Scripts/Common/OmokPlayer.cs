public enum OmokPlayer
{
    Invalid,
    Black,
    White,
}

public static class OmokPlayerExtend
{
    public static OmokPlayer GetOpponentPlayer(this OmokPlayer player)
    {
        switch (player)
        {
            case OmokPlayer.Black:
                return OmokPlayer.White;
            case OmokPlayer.White:
                return OmokPlayer.Black;
        }
        return OmokPlayer.Invalid;
    }
}