using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [SerializeField] public GameObject m_DungeonManger;
    public GameObject m_DungeonManagerPrefab;

    [SerializeField] public GameObject m_player;
    [SerializeField] private LevelCounter levelCounterUI;
    [SerializeField] public GameObject m_MiniMap;
    public bool m_Respawning=false;
    // Start is called before the first frame update
    void Start()
    {

        m_DungeonManagerPrefab.GetComponent<ProceduralGen>().m_Level = 1;
        m_DungeonManger = Instantiate(m_DungeonManagerPrefab, this.transform);


    }

    public void ExitLevel()
    {
        //Load new level

        Destroy(m_DungeonManger);
        m_DungeonManagerPrefab.GetComponent<ProceduralGen>().m_Level += 1;
        levelCounterUI.SetLevelUI(m_DungeonManagerPrefab.GetComponent<ProceduralGen>().m_Level);
        m_DungeonManger = Instantiate(m_DungeonManagerPrefab, this.transform);

        m_player.GetComponent<DungeonMovement>().m_CurrentID = 0;
        m_player.gameObject.transform.position = new Vector3(0f,0.03f,0f);

        Destroy(m_MiniMap);

        m_DungeonManger.GetComponent<ProceduralGen>().ChangeHue();
    }

   


    public void RespawnLevel()
    {
        //m_player.SetActive(false);
        //Load new level
        Health tempHealth = m_player.GetComponent<Health>();
        tempHealth.Death = false;

        tempHealth.AddHealth(tempHealth.Maxhealth);
        StartCoroutine(tempHealth.C_LoseHealth());
        Destroy(m_DungeonManger);
        m_Respawning = true;
        m_DungeonManagerPrefab.GetComponent<ProceduralGen>().m_Level = 1;
        m_DungeonManger = Instantiate(m_DungeonManagerPrefab, this.transform);

        levelCounterUI.SetLevelUI(m_DungeonManagerPrefab.GetComponent<ProceduralGen>().m_Level);
        Destroy(m_MiniMap);

        m_player.GetComponent<DungeonMovement>().m_CurrentID = 0;

        m_player.gameObject.transform.position = new Vector3(0f, 0.03f, 0f);

    }

    
}
