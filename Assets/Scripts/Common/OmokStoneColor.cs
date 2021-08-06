public enum OmokStoneColor
{
    Empty,
    Black,
    White,
}
public static class OmokStoneColorExtend
{
    public static OmokStoneColor GetOpponentColor(this OmokStoneColor player)
    {
        switch (player)
        {
            case OmokStoneColor.Black:
                return OmokStoneColor.White;
            case OmokStoneColor.White:
                return OmokStoneColor.Black;
        }
        return OmokStoneColor.Empty;
    }

    public static string GetText(this OmokStoneColor color)
    {
        switch (color)
        {
            case OmokStoneColor.Black:
                return "흑";
            case OmokStoneColor.White:
                return "백";
        }
        return string.Empty;
    }
}