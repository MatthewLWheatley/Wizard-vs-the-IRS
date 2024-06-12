using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unload : MonoBehaviour
{
    public void Unloading()
    {
        SceneManager.UnloadScene(0);
    }
}
