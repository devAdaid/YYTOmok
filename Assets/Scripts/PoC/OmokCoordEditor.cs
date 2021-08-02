using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OmokCoord))]
public class OmokCoordEditor : Editor
{
    private static int Count = 19;
    private OmokCoord omok => (OmokCoord)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    public void OnSceneGUI()
    {
        var sizeHalf = (omok.topRight.position - omok.rectTf.position).x * omok.padRatio;
        var size = 2 * sizeHalf;

        //var rect = new Rect(omok.rectTf.position.x - sizeHalf, omok.rectTf.position.y - sizeHalf, size, size);
        //Handles.DrawSolidRectangleWithOutline(rect, Color.clear, Color.red);

        var xMin = omok.rectTf.position.x - sizeHalf;
        var xMax = omok.rectTf.position.x + sizeHalf;
        var yMin = omok.rectTf.position.y - sizeHalf;
        var yMax = omok.rectTf.position.y + sizeHalf;

        Handles.color = Color.red;

        var bottomLeftPos = new Vector3(xMin, yMin, 0f);
        float gridSize = size / Count;

        // 가로선
        for (int i = 0; i < Count + 1; i++)
        {
            var startPos = bottomLeftPos;
            startPos.y += gridSize * i;
            var endPos = startPos;
            endPos.x += size;
            DrawLine(startPos, endPos);
        }

        // 세로선
        for (int i = 0; i < Count + 1; i++)
        {
            var startPos = bottomLeftPos;
            startPos.x += gridSize * i;
            var endPos = startPos;
            endPos.y += size;
            DrawLine(startPos, endPos);
        }
    }

    private void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        Handles.DrawLine(startPos, endPos, 2f);
    }
}
