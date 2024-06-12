using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocking : MonoBehaviour
{
    public GameObject m_DoorTrigger;
    public GameObject m_DoorPartcals;

    public GameObject[] Enemys;

    float m_Time;

    private void Start()
    {
        for (int i =0; i < this.transform.childCount;i++) 
        { 
            this.transform.GetChild(i).GetComponent<Collider>().enabled = false;
        }
        StartCoroutine(DisableDoor());

        m_DoorTrigger.SetActive(false);
        m_DoorPartcals.SetActive(true);

        m_Time = Time.time;
    }

    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<Collider>().enabled = false;
        }
        StartCoroutine(DisableDoor());

        m_DoorTrigger.SetActive(false);
        m_DoorPartcals.SetActive(true);

        m_Time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - m_Time > .1f)
        {
            CheckEnemys();
            m_Time = Time.time;
        }
    }

    private IEnumerator DisableDoor() 
    {
        
        //Debug.Log("corutine");
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<Collider>().enabled = true;
        }
    }

    void CheckEnemys() 
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (Enemys.Length == 0) 
        {
            //Debug.Log(Enemys.Length);
            m_DoorTrigger.SetActive(true);
            m_DoorPartcals.SetActive(false);
        }
    }
}
