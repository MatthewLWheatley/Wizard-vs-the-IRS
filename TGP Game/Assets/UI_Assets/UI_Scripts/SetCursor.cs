using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField] private Texture2D[] m_Cursors;
    [Tooltip("This is the Focal point of the cursor")]
    [SerializeField] private Vector2[] m_Hotspots;

    public void CursorSet(int index)
    {
        Cursor.SetCursor(m_Cursors[index], m_Hotspots[index], CursorMode.Auto);
        if(index == 0)
        {
            Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        }
        
    }
}
