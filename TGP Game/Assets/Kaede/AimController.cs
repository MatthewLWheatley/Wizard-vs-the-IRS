using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimController : MonoBehaviour
{
    public Vector3 targetPos;
    public Image aimImage;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //Synchronise mousePosition and Aim object position
        transform.position = Input.mousePosition;

        RaycastHit hit;

        // release the ray from MainCamera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            //get the position which the ray hits
            targetPos = hit.point;
            print(targetPos);

            if (hit.transform.CompareTag("Enemy"))
            {
                // change the colour to red
                aimImage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                // change the colour to white
                aimImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            // change the colour to white
            aimImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}