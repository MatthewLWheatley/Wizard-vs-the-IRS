using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structures;

public class DungMiniMap : MonoBehaviour
{
    public GameObject m_levelManager;
    public GameObject m_Canvas;
    public GameObject m_MapRoom;
    List<GameObject> m_MapRooms;
    public List<Room> m_Rooms;
    bool m_mPressed = false;
    bool m_mapOpen = false;
    public List<Sprite> m_RoomSprites;
    public List<Sprite> m_RoomCompleteSprites;
    public GameObject m_Player;
    int m_CurrentRoomID;
    int m_PastRoomID = -1;
    public GameObject m_PlayerIcon;
    public GameObject m_BossIcon;
    public GameObject t_BossObject;
    public GameObject[] Enemys;
    public float m_MapScale;
    public float m_PlayerIconScale;
    public float m_BossIconScale;
    int m_BossRoomId;

    // Start is called before the first frame update
    void Awake()
    {
        m_levelManager = this.transform.parent.gameObject;
        GameObject manager = m_levelManager.GetComponent<LevelManager>().m_DungeonManger;
        m_Rooms = manager.GetComponent<ProceduralGen>().m_Rooms;
        LoadBossRoom();
        LoadMap();
        GameObject t_PlayerObject = Instantiate(m_PlayerIcon, m_Canvas.transform);
        t_PlayerObject.transform.position += new Vector3(((m_Rooms[m_CurrentRoomID].m_Y - 10) * 50), ((m_Rooms[m_CurrentRoomID].m_X - 10) * 50), 1);
        m_Canvas.SetActive(false);
    }

    void LoadMap()
    {
        
        m_MapRooms = new List<GameObject>(m_Rooms.Count);
        for (int i = 0; i < m_Rooms.Count; i++)
        {
            Room room = m_Rooms[i];
            GameObject t_RoomObject = Instantiate(m_MapRoom, m_Canvas.transform);
            t_RoomObject.transform.position += new Vector3(((m_Rooms[i].m_Y - 10) * 50 * m_MapScale), ((m_Rooms[i].m_X - 10) * 50 * m_MapScale), 0);
            t_RoomObject.transform.localScale *= m_MapScale; 
            t_RoomObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            if (i == m_BossRoomId)
            {
                t_RoomObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = m_RoomSprites[m_Rooms[i].m_Exits];
                t_BossObject = Instantiate(m_BossIcon, m_Canvas.transform);
                t_BossObject.transform.position += new Vector3(((m_Rooms[m_BossRoomId].m_Y - 10) * 50 * m_MapScale), ((m_Rooms[m_BossRoomId].m_X - 10) * 50 * m_MapScale), 1);
                t_BossObject.SetActive(false);
            }
            else
            {
                t_RoomObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = m_RoomSprites[m_Rooms[i].m_Exits];
            }
            m_MapRooms.Add(t_RoomObject);
            m_MapRooms[i].SetActive(false);
            
        }
    }

    void LoadBossRoom()
    {
        for (int i = 0; i < this.transform.parent.transform.childCount; i++)
        {
            if (this.transform.parent.GetChild(i).tag == "DungeonManager")
            {
                GameObject manager = m_levelManager.GetComponent<LevelManager>().m_DungeonManger;
                m_BossRoomId = manager.GetComponent<ProceduralGen>().m_BossId;
            }
        }
    }

    void LoadPlayer()
    {
        for (int i = 0; i < this.transform.parent.transform.childCount; i++)
        {
            if (this.transform.parent.GetChild(i).tag == "Player")
            {
                m_Player = this.transform.parent.GetChild(i).gameObject;
                m_CurrentRoomID = this.transform.parent.transform.GetChild(i).gameObject.transform.GetComponent<DungeonMovement>().m_CurrentID;
            }
        }
    }

    void DrawPlayer()
    {
        LoadPlayer();

        if (m_PastRoomID != m_CurrentRoomID)
        {
            for (int i = 0; i < m_Canvas.transform.childCount; i++)
            {
                if (m_Canvas.transform.GetChild(i).tag == "PlayerIcon")
                { 
                    m_PastRoomID = m_CurrentRoomID;
                    Destroy(m_Canvas.transform.GetChild(i).gameObject);
                    GameObject t_PlayerObject = Instantiate(m_PlayerIcon, m_Canvas.transform);
                    t_PlayerObject.transform.position += new Vector3(((m_Rooms[m_CurrentRoomID].m_Y - 10) * 50 * m_MapScale), ((m_Rooms[m_CurrentRoomID].m_X - 10) * 50 * m_MapScale), 1);
                    t_PlayerObject.transform.localScale *= m_PlayerIconScale;
                    break;
                }

            }
        }
    }

    void DrawMap()
    {
        m_MapRooms[m_CurrentRoomID].SetActive(true);
        if (m_CurrentRoomID == m_BossRoomId)
        {
            t_BossObject.SetActive(true);
        }
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //Health.Enemys = Enemys.Length;
        if (Enemys.Length == 0)
        {
            Health.t_HealthDecay = 0;
            m_MapRooms[m_CurrentRoomID].transform.GetComponent<UnityEngine.UI.Image>().sprite = m_RoomCompleteSprites[m_Rooms[m_CurrentRoomID].m_Exits];
        }
        else
        {
            Health.t_HealthDecay = Health.HealthDecay;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !m_mPressed)
        {
            m_mPressed = true;
            if (m_mapOpen)
            {
                m_Canvas.SetActive(false);
                m_mapOpen = false;
            }
            else
            {
                m_Canvas.SetActive(true);
                m_mapOpen = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab)) m_mPressed = false;
        DrawPlayer();
        DrawMap();
    }
}
