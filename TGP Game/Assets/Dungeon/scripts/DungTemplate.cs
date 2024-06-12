using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structures;

public class DungTemplate : MonoBehaviour
{

    public List<Material> m_Materials = new List<Material>();

    public List<List<GameObject>> m_PreFabs = new List<List<GameObject>>();

    public List<GameObject> m_PreFabSpawnRooms = new List<GameObject>();
    public List<GameObject> m_PreFabBossRooms = new List<GameObject>();
    public List<GameObject> m_PreFabItemRooms = new List<GameObject>();
    public List<GameObject> m_PreFab0 = new List<GameObject>();
    public List<GameObject> m_PreFab1 = new List<GameObject>();
    public List<GameObject> m_PreFab2 = new List<GameObject>();
    public List<GameObject> m_PreFab3 = new List<GameObject>();
    public List<GameObject> m_PreFab4 = new List<GameObject>();
    public List<GameObject> m_PreFab5 = new List<GameObject>();
    public List<GameObject> m_PreFab6 = new List<GameObject>();
    public List<GameObject> m_PreFab7 = new List<GameObject>();
    public List<GameObject> m_PreFab8 = new List<GameObject>();
    public List<GameObject> m_PreFab9 = new List<GameObject>();
    public List<GameObject> m_PreFab10 = new List<GameObject>();
    public List<GameObject> m_PreFab11 = new List<GameObject>();
    public List<GameObject> m_PreFab12 = new List<GameObject>();
    public List<GameObject> m_PreFab13 = new List<GameObject>();
    public List<GameObject> m_PreFab14 = new List<GameObject>();
    public List<GameObject> m_PreFab15 = new List<GameObject>();

    public GameObject m_DungeonManager;
    public GameObject m_RoomPreFab;

    public List<GameObject> m_RoomTemplates = new List<GameObject>();

    [SerializeField] List<Room> m_Rooms = new List<Room>();

    int m_currentID = 0;
    public Vector3 m_SpawnLocation = new Vector3(0f, 10f, 0f);

    GameObject m_player;

    int m_BossX;
    int m_BossY;

    public int m_BossID;

    private void Awake()
    {
        //Debug.Log(this.transform.parent.parent.name + " " + this.transform.parent.parent.GetChild(1).gameObject);
        m_player = this.transform.parent.parent.GetChild(1).gameObject;


        m_DungeonManager = this.transform.parent.gameObject;
        m_Rooms = m_DungeonManager.GetComponent<ProceduralGen>().GetRoomList();

        LoadPreFabs();

        Spawn();

        if (this.transform.parent.transform.GetComponent<ProceduralGen>().m_Level >1) 
        {
            this.transform.parent.transform.GetComponent<ProceduralGen>().ChangeHue();
        }

        Deavtivate(m_currentID,0);
    }

    private void LoadPreFabs() 
    {
        m_PreFabs.Add(m_PreFab0);
        m_PreFabs.Add(m_PreFab1);
        m_PreFabs.Add(m_PreFab2);
        m_PreFabs.Add(m_PreFab3);
        m_PreFabs.Add(m_PreFab4);
        m_PreFabs.Add(m_PreFab5);
        m_PreFabs.Add(m_PreFab6);
        m_PreFabs.Add(m_PreFab7);
        m_PreFabs.Add(m_PreFab8);
        m_PreFabs.Add(m_PreFab9);
        m_PreFabs.Add(m_PreFab10);
        m_PreFabs.Add(m_PreFab11);
        m_PreFabs.Add(m_PreFab12);
        m_PreFabs.Add(m_PreFab13);
        m_PreFabs.Add(m_PreFab14);
        m_PreFabs.Add(m_PreFab15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        m_player = this.transform.parent.parent.GetChild(1).transform.gameObject;
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the child GameObject
            GameObject child = transform.GetChild(i).gameObject;

            // Delete the child
            Destroy(child);
        }
        m_Rooms.RemoveRange(0, m_Rooms.Count);
        for (int i = 0; i < m_Rooms.Count; i++) 
        {
            //Debug.Log(i);
        }
        m_RoomTemplates.RemoveRange(0, m_RoomTemplates.Count);
        Destroy(gameObject);
    }

    void GenBossRoom()
    {
        int T_CenterX = m_Rooms[0].m_X;
        int T_CenterY = m_Rooms[0].m_Y;
        int T_FurthestX = 11;
        int T_FurthestY = 11;
        int T_LargestDiff = 0;

        for (int i = 0; i < m_Rooms.Count; i++) 
        {
            if (m_Rooms[i].m_Exits == 1 || m_Rooms[i].m_Exits == 2 || m_Rooms[i].m_Exits == 4 || m_Rooms[i].m_Exits == 8)
            {
                int T_XDiff = Mathf.Abs(m_Rooms[i].m_X - T_CenterX);
                int T_YDiff = Mathf.Abs(m_Rooms[i].m_Y - T_CenterY);
                int Diff = T_XDiff + T_YDiff;
                if (Diff > T_LargestDiff)
                {
                    T_LargestDiff = Diff;
                    T_FurthestX = m_Rooms[i].m_X;
                    T_FurthestY = m_Rooms[i].m_Y;
                }
            }
        }
        m_BossX = T_FurthestX;
        m_BossY = T_FurthestY;
        //Debug.Log(furthestX +" "+ furthestY);
    }

    void Spawn()
    {
        m_player = this.transform.parent.parent.GetChild(1).transform.gameObject;
        m_Rooms = m_DungeonManager.GetComponent<ProceduralGen>().GetRoomList();

        GenBossRoom();

        for (int i = 0; i < m_Rooms.Count; i++)
        {
            int preset = Random.Range(0, m_PreFabs[m_Rooms[i].m_Exits].Count);
            GameObject Temp = m_PreFabs[m_Rooms[i].m_Exits][preset];

            //if spawn room make blank
            if (i == 0) 
            {
                switch (m_Rooms[i].m_Exits) 
                {
                    case 15:
                        preset = 0;
                        break;
                    case 14:
                        preset = 1;
                        break;
                    case 13:
                        preset = 2;
                        break;
                    case 11:
                        preset = 3;
                        break;
                    case 7:
                        preset = 4;
                        break;
                }

                Temp = m_PreFabSpawnRooms[preset];
            }

            if (m_Rooms[i].m_X == m_BossX && m_Rooms[i].m_Y == m_BossY)
            {
                switch (m_Rooms[i].m_Exits)
                {
                    case 1:
                        preset = 0;
                        break;
                    case 2:
                        preset = 1;
                        break;
                    case 4:
                        preset = 2;
                        break;
                    case 8:
                        preset = 3;
                        break;
                }
                //Debug.Log("boss room placed");
                Temp = m_PreFabBossRooms[preset];
                m_BossID = i;
            }

            m_RoomTemplates.Add(Temp);
        }

        for (int i = 0; i < m_RoomTemplates.Count; i++)
        {
            float scale = m_RoomTemplates[i].GetComponent<MeshRenderer>().bounds.size.x;


            m_RoomTemplates[i].GetComponent<MeshRenderer>().material = m_Materials[m_Rooms[i].m_Exits];
            m_RoomTemplates[i].GetComponent<Transform>().position = new Vector3((m_Rooms[i].m_Y - 10) * scale, 0, (m_Rooms[i].m_X - 10) * scale);
            m_RoomTemplates[i].gameObject.name = m_Rooms[i].m_Exits + ", ID:" + i.ToString();



            m_RoomTemplates[i] = Instantiate(m_RoomTemplates[i], this.transform);
        }
    }

    public void Deavtivate(int newID, int Direction)
    {
        m_player = this.transform.parent.parent.GetChild(1).gameObject;
        m_currentID = newID;
        for (int i = 0; i < m_Rooms.Count; i++) 
        {
            m_RoomTemplates[i].SetActive(false);
            if (i == m_currentID)
            {
                m_RoomTemplates[i].SetActive(true);
            }
        }

        
    }
}
