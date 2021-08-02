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

    void Update()
    {
        Debug.Log($"size: {rectTf.rect.size}");
        Debug.Log($"topRight: {rectTf.rect.max}");
        Debug.Log($"real topRight: {topRight.transform.position}");
    }

    public void OnClick(BaseEventData e)
    {
        var eventData = (PointerEventData)e;
        dol.position = eventData.position;


    }
}
