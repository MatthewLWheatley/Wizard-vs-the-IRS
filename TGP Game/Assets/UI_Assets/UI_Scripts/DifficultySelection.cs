using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    [SerializeField] private int m_DifficultyNumber;
    public void AssignDifficulty()
    {
        MenuValues.m_Difficulty = m_DifficultyNumber;
    }
}
