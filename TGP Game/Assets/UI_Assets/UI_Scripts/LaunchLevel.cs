using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchLevel : MonoBehaviour
{
    [SerializeField] private GameObject m_LoadingScreen;
    [SerializeField] private GameObject m_LevelScreen;
    [SerializeField] private GameObject m_HUD;
    [SerializeField] private GameObject m_Camera;
    [SerializeField] private Inventory m_Inventory;
    [SerializeField] private SetPlayer m_DeathScreen;
    [SerializeField] private int m_WaitTime = 5;
    public void LevelLoad(int SceneIndex)
    {
        m_LoadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync((SceneIndex),LoadSceneMode.Additive);
        StartCoroutine(C_LoadingWaitTime());
    }
    IEnumerator C_LoadingWaitTime()
    {
        m_Camera.SetActive(false);
        yield return new WaitForSeconds(m_WaitTime);
        m_LoadingScreen.SetActive(false);
        m_LevelScreen.SetActive(false);
        m_HUD.SetActive(true);
        m_Inventory.SetPlayer();
        m_DeathScreen.GetPlayer();
    }
    public void UnloadLevel(int SceneIndex)
    {
        SceneManager.UnloadScene(SceneIndex);
    }
}
