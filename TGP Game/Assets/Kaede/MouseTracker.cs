using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private Camera cam;
    private Vector3 tempPos;
    Ray ray;
    RaycastHit raycastHit;

    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out raycastHit);

        if (raycastHit.transform.tag == "Ground")
        {
            gameObject.transform.position = raycastHit.point;
            tempPos = raycastHit.point;
        }
        else if (raycastHit.transform.tag != "Ground")
        {
            gameObject.transform.position = tempPos;
        }

    }
}