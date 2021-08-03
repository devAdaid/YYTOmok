public struct OmokGridPosition
{
    public static OmokGridPosition INVALID = new OmokGridPosition(-1, -1);

    public int Row;
    public int Col;

    public OmokGridPosition(int rowIndex, int colIndex)
    {
        Row = rowIndex;
        Col = colIndex;
    }

    public bool IsValid()
    {
        return Row >= 0
            && Row < Define.OMOK_COUNT
            && Col >= 0
            && Col < Define.OMOK_COUNT;
    }
}