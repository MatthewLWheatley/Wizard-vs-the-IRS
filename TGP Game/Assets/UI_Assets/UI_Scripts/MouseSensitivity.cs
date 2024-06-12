using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] private Slider m_Slider;
    public void SetMouseSens()
    {
        MenuValues.m_MouseSensitivity = m_Slider.value;
    }
}
