using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync((1), LoadSceneMode.Additive);
    }
}
