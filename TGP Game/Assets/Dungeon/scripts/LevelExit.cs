using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private GameObject ExitCover;
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject prompt;
    private bool m_defeated;

    private void Update()
    {
        if ((Boss !=null && Boss.GetComponent<LichBossAI>().m_Defeated))//|| Boss.GetComponent<MimBossAI>().m_Defeated))
        {
            ExitCover.SetActive(false);
            m_defeated = true;
            //Destroy(ExitCover);
        }
        if (m_defeated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<LevelManager>().ExitLevel();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Display prompt to leave
        //if (!ExitCover)
        //{
        //    prompt.SetActive(true);
        //}
        if (other.gameObject.tag == "Player"&& m_defeated)
        {
            prompt.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (!ExitCover)
        //{
        //    prompt.SetActive(false);
        //}
        if (other.gameObject.tag == "Player"&& !m_defeated)
        {
            prompt.SetActive(false);
         
        }
    }
    
}
