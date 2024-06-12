using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] GameObject cursor;

    void Update()
    {
        transform.LookAt(cursor.transform);
    }
}
