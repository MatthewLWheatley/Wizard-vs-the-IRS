using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadStart : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(0);
        this.gameObject.SetActive(false);
    }
}

