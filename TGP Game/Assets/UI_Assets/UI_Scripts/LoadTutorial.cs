using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTutorial : MonoBehaviour
{
    public void LoadingTutorial(int index)
    {
        SceneManager.LoadScene(index);
    }
}
