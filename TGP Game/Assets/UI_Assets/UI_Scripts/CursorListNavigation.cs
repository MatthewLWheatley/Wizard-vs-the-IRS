using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorListNavigation : MonoBehaviour
{
    //list of all the cursor images
    [SerializeField] private GameObject[] m_Cursors;
    [SerializeField] private SetCursor m_SetCursor;
    private int CurrentCursor = 0;
    
    //These two functions simply increment or deincrement the list to display the list of cursors
    public void OnClickRight()
    {
        if(CurrentCursor+1 < m_Cursors.Length)
        {
            CurrentCursor +=1;
        }
        else
        {
            CurrentCursor = 0;
        }
        for (int i = 0; i < m_Cursors.Length; i++)
        {
            m_Cursors[i].gameObject.SetActive(false);
        }
        m_Cursors[CurrentCursor].gameObject.SetActive(true);
        m_SetCursor.CursorSet(CurrentCursor);
    }
    public void OnClickLeft()
    {
        if (CurrentCursor - 1 < 0)
        {
            CurrentCursor = m_Cursors.Length-1;
        }
        else
        {
            CurrentCursor -=1;
        }
        for(int i = 0; i < m_Cursors.Length; i++)
        {
            m_Cursors[i].gameObject.SetActive(false);
        }
        m_Cursors[CurrentCursor].gameObject.SetActive(true);
        m_SetCursor.CursorSet(CurrentCursor);

    }
}
