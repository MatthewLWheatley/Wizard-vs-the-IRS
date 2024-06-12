using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float zoomSpeed = 1;
    [SerializeField] GameObject cursorArea;

    void Update()
    {
        var scroll = Input.mouseScrollDelta.y;
        cursorArea.transform.position += -cursorArea.transform.up * scroll * zoomSpeed;
    }
}