using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OmokCoord : MonoBehaviour
{
    public float padRatio = 0.95f;
    public RectTransform dol;
    public RectTransform topRight;

    public Camera cam;
    public RectTransform rectTf;

    public float BoardSizeHalf => (topRight.position - rectTf.position).x * padRatio;
    public float BoardSize => 2 * BoardSizeHalf;
    public float GridSize => BoardSize / Count;
    public float XMin => rectTf.position.x - BoardSizeHalf;
    public float XMax => rectTf.position.x + BoardSizeHalf;
    public float YMin => rectTf.position.y - BoardSizeHalf;
    public float YMax => rectTf.position.y + BoardSizeHalf;
    private static int Count = 19;

    public void OnClick(BaseEventData e)
    {
        var eventData = (PointerEventData)e;
        dol.position = eventData.position;
        var (row, col) = GetGridIndex(dol.position);
        Debug.Log($"Pos: {dol.position} Index: ({row}, {col})");
        Debug.Log($"X({XMin},{XMax}) Y({YMin},{YMax})");
        dol.position = GetPosition(row, col);
    }

    private (int rowIndex, int colIndex) GetGridIndex(Vector3 position)
    {
        var colIndex = -1;
        if (position.x < XMin)
        {
            colIndex = 0;
        }
        else if (position.x > XMax)
        {
            colIndex = Count - 1;
        }
        else
        {
            colIndex = (int)((position.x - XMin) / GridSize);
        }

        var rowIndex = -1;
        if (position.y < YMin)
        {
            rowIndex = 0;
        }
        else if (position.y > YMax)
        {
            rowIndex = Count - 1;
        }
        else
        {
            rowIndex = (int)((position.y - YMin) / GridSize);
        }

        return (rowIndex, colIndex);
    }

    private Vector3 GetPosition(int rowIndex, int colIndex)
    {
        var xPos = XMin + GridSize * colIndex;
        var yPos = YMin + GridSize * rowIndex;
        xPos += GridSize / 2;
        yPos += GridSize / 2;
        return new Vector3(xPos, yPos, 0f);
    }
}
