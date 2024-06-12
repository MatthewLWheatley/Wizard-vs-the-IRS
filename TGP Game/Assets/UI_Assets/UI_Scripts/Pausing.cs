using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0f;
        GameObject temp = SetPlayer.m_PlayerRef;
        temp.GetComponent<PlayerMovement>().enabled = false;
    }
    public void UnPause()
    {
        Time.timeScale = 1.0f;
        GameObject temp = SetPlayer.m_PlayerRef;
        temp.GetComponent<PlayerMovement>().enabled = true;
    }
}
