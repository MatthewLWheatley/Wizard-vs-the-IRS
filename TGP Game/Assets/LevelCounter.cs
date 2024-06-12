using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    //string m_LevelNum = ProceduralGen.m_Level.ToString();
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI;
    public static int m_CurrentLevel = 1;
    public void SetLevelUI(int LevelNum) 
    {
        m_CurrentLevel = LevelNum;
        m_TextMeshProUGUI.text = "Level: " + LevelNum.ToString();
    }
}
