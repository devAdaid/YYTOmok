using System;

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

    public bool IsNeighbor(OmokGridPosition other)
    {
        if (!this.IsValid() || !other.IsValid())
        {
            return false;
        }

        var rowDiff = Row - other.Row;
        var colDiff = Col - other.Col;

        if (rowDiff == 0
            && Math.Abs(colDiff) == 1)
        {
            return true;
        }

        if (colDiff == 0
            && Math.Abs(rowDiff) == 1)
        {
            return true;
        }

        if (rowDiff == colDiff)
        {
            return true;
        }

        if ((Row + Col) == (other.Row + other.Col))
        {
            return true;
        }

        return false;
    }
}