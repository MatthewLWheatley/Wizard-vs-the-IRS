using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnToZero : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public void ZeroButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.localPosition = Vector3.zero;
        }
    }

}
