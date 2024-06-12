using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBarScaling : MonoBehaviour
{

    [SerializeField] private Inventory m_Inventory;
    public int m_InvSlot;
    private float m_Scale =0f;
    private float ReloadTime;
    public void ReloadBar()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        m_Scale = 1;
        //StartCoroutine(C_Reloading());
    }
    private void Update()
    {
        int index = m_Inventory.inventoryList[m_InvSlot];
        ReloadTime = m_Inventory.Items[index].reload;
        this.transform.localScale = new Vector3(ReloadTime, 1, 1);
        if(transform.localScale.x < 0f)
        {
            transform.localScale = new Vector3(0, 1, 1);
        }
    }

    //IEnumerator C_Reloading()
    //{
    //    while(m_Scale > 0f)
    //    {
    //        m_Scale -= rTime;
    //        this.transform.localScale = new Vector3(m_Scale, 1, 1);
    //    }

    //    yield return null;
    //}
}
