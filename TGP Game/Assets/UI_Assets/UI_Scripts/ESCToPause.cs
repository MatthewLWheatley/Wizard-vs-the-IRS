using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCToPause : MonoBehaviour
{
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private Pausing m_PauseScript;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_PauseMenu.SetActive(!m_PauseMenu.activeSelf);
        }
        if (m_PauseMenu.activeSelf)
        {
            m_PauseScript.Pause();
        }
        else
        {
            m_PauseScript.UnPause();
        }
    }
}
