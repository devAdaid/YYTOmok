using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OmokCoord : MonoBehaviour
{
    public RectTransform dol;
    public RectTransform leftup;

    private Camera cam;
    private RectTransform rectTf;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        rectTf = GetComponent<RectTransform>();
        Debug.Log($"leftup: {leftup.position}");
    }

    public void OnClick(BaseEventData e)
    {
        var eventData = (PointerEventData)e;
        dol.position = eventData.position;
    }
}
