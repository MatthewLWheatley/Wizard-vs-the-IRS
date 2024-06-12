using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structures;
using UnityEngine.Android;

public class ProceduralGen : MonoBehaviour
{

    public int m_Level = 0;

    public int m_LevelSize;
    [SerializeField] int m_DoneRooms = 0;

    [SerializeField] GameObject m_DungPreFab;
    [SerializeField] public GameObject m_DungTemplate;
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_Camera;
    [SerializeField] GameObject m_MiniMap;

    public byte[,] m_LevelArray;

    public List<Room> m_Rooms = new List<Room>();

    bool m_tabPressed = false;


    public int m_BossId;

    private List<GameObject> allChildren = new List<GameObject>();

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        Run();
        Spawn();
    }

    //NOT MY CODE SO IM NOT DELETING
    //private void Update()
    //{
    //    // if tab is pressed regen the dungeon you are in
    //    if (Input.GetKeyDown(KeyCode.Tab) && !m_tabPressed) 
    //    {
    //        this.transform.parent.gameObject.GetComponent<LevelManager>().ExitLevel();

    //        ////makes it so this can be only pressed once so it oesnt run a whole load of times
    //        //m_tabPressed = true;

    //        ////cleans up re-runs the generation process and the spawns the rooms into the map
    //        //Clean();
    //        //Run();
    //        //Spawn();
    //    }
    //    if (Input.GetKeyUp(KeyCode.Tab)) m_tabPressed=false;
    //}

    /// <summary>
    /// Clean memory of all the variables so it can be re ran
    /// </summary>
    void Clean() 
    {
        //gets the dungeonTemplate child so it can me cleaned and deleted
        GameObject child = this.transform.GetChild(0).gameObject;
        Destroy(child);

        m_DoneRooms = 0;
        m_Rooms.RemoveRange(0, m_Rooms.Count);
        m_Rooms = new List<Room>();
        m_LevelArray = new byte[0, 0];


        Destroy(this.transform.parent.GetComponent<LevelManager>().m_player);
        Destroy(this.transform.parent.GetComponent<LevelManager>().m_MiniMap);
    }


    /// <summary>
    /// creates the object that spawns and handles all the rooms
    /// </summary>
    void Spawn() 
    {
        
        
        //TODO: change name for this but that might mean fxing other scripts
        m_DungTemplate = Instantiate(m_DungPreFab, this.transform);
        m_DungTemplate.name = "DungTemplate";

        m_BossId = m_DungTemplate.GetComponent<DungTemplate>().m_BossID;
        this.transform.parent.GetComponent<LevelManager>().m_MiniMap = Instantiate(m_MiniMap, this.transform.parent);
    }

    public void Run()
    {
        Init();

        GenMap();

        CleanMap();

        //PrintArray();

        Debug.Log(transform.parent.GetComponent<LevelManager>().m_Respawning);
        if (m_Level == 1 && !transform.parent.GetComponent<LevelManager>().m_Respawning)
        {
            Debug.Log(transform.parent.GetComponent<LevelManager>().m_Respawning);
            this.transform.parent.GetComponent<LevelManager>().m_player = Instantiate(m_player, this.transform.parent); 
        }
        this.transform.parent.GetComponent<LevelManager>().m_player.GetComponent<DungeonMovement>().m_Rooms = m_Rooms;
        transform.parent.GetComponent<LevelManager>().m_Respawning = false;


        //for(int i = 0; i < this.transform.parent.childCount; i++) 
        //{
        //    if (this.transform.parent.GetChild(i).tag) 
        //    { }
        //}

        //m_CMvcam = GameObject.FindGameObjectWithTag("VCam");
        //GameObject temp = this.transform.parent.GetComponent<LevelManager>().m_player.gameObject.transform.GetChild(this.transform.parent.GetComponent<LevelManager>().m_player.transform.childCount-1).gameObject;

        //m_CMvcam.GetComponent<Cinemachine.CinemachineConfiner>().m_BoundingVolume = temp.GetComponent<BoxCollider>();

        //m_TargetGroup = GameObject.FindGameObjectWithTag("TargetGroup");
        //m_TargetGroup.GetComponent<Cinemachine.CinemachineTargetGroup>().m_Targets[0].target = this.transform.parent.GetComponent<LevelManager>().m_player.transform;
        m_Camera = this.transform.parent.transform.GetChild(0).gameObject;

        m_Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = this.transform.parent.GetComponent<LevelManager>().m_player.transform;


    }



    /// <summary>
    /// initiates values needed for generation
    /// </summary>
    void Init()
    {
        int t_Min = ((m_Level + 1) * 2 + 5);
        int t_Max = m_Level + 5;
        //Random rnd = new Random();
        m_LevelSize = Random.Range(t_Min,t_Max);

        if (true) { 
        //switch (m_Level)
        //{
        //    case 0:
        //        //if level 1 min rooms is 10 and max is 15
        //        m_LevelSize = Random.Range(7,12);
        //        break;
        //    case 1:
        //        //if level 2 min rooms is 16 max is 20
        //        m_LevelSize = Random.Range(9, 14);
        //        break;
        //    case 2:
        //        //if level 3 min rooms is 21 max is 25
        //        m_LevelSize = Random.Range(11, 16);
        //        break;
        //    default:
        //        //if level isnt set cause something messed up or extralevels its set to 25
        //        m_LevelSize = 40;
        //        break;
        //}
        }

        m_LevelArray = new byte[21, 21];

        for (int i = 0; i < m_LevelArray.GetLength(0); i++)
        {
            for (int j = 0; j < m_LevelArray.GetLength(1); j++)
            {
                m_LevelArray[i, j] = 0;
            }
        }

        int temp = 0;
        temp = Random.Range(0, 5);

        switch (temp)
        {
            case 1:
                m_LevelArray[10, 10] = 15;
                break;
            case 2:
                m_LevelArray[10, 10] = 7;
                break;
            case 3:
                m_LevelArray[10, 10] = 14;
                break;
            case 4:
                m_LevelArray[10, 10] = 11;
                break;
            case 5:
                m_LevelArray[10, 10] = 13;
                break;
            default:
                m_LevelArray[10, 10] = 15;
                break;
        }

        Room room = new Room(m_DoneRooms, m_LevelArray[10, 10], 10, 10);
        m_Rooms.Add(room);
        ////Debug.Log(m_Rooms[m_Rooms.Count - 1].m_X);
        m_DoneRooms++;
    }

    /// <summary>
    /// prints the text based array to the debug console so you can do some checks,
    /// it only shows the amount of exits in hexadeciamal now the presets
    /// </summary>
    void PrintArray()
    {
        string buffer = "";
        //for easier visualization ill use a single digit hex output 
        for (int i = 0; i < m_LevelArray.GetLength(1); i++)
        {
            
            for (int j = 0; j < m_LevelArray.GetLength(0); j++)
            {
                buffer += m_LevelArray[j, i].ToString("X1") + " ";
                
            }
            buffer += "\n";
        }
        Debug.Log(buffer);
    }

    /// <summary>
    /// Does the inital generation step where it creates the simple open eneded map, to be cleaned later
    /// </summary>
    void GenMap()
    {
        while (m_DoneRooms < m_LevelSize)
        {
            for (int i = 0; i <  m_Rooms.Count; i++)
            {
                int t_x = m_Rooms[i].m_X;
                int t_y = m_Rooms[i].m_Y;
                byte t_value = m_LevelArray[t_x, t_y];

                byte t_Surround = CheckAvailabilty(t_x, t_y, t_value);
                GenRoom(t_x, t_y, t_Surround);

                if (m_DoneRooms > m_LevelSize) break;
            }
        }
    }

    /// <summary>
    /// Checks the room exits for a specific place in the array
    /// TODO:Replace and remove the need of the 2d array
    /// </summary>
    public byte CheckAvailabilty(int t_x, int t_y, byte t_value)
    {
        byte temp = 0;
        //check if its in bounds
        if ((t_y - 1) >= 0 && (t_y + 1) < m_LevelArray.GetLength(1) && (t_x - 1) >= 0 && (t_x + 1) < m_LevelArray.GetLength(0))
        {
            //check if there is a 1 in this byte
            if ((t_value & 0b0000_0001) != 0)
            {
                if (m_LevelArray[t_x, t_y - 1] == 0)
                {
                    temp += 1;
                }
            }

            //check if there is a 4 in this byte
            if ((t_value & 0b0000_0100) != 0)
            {
                if (m_LevelArray[t_x, t_y + 1] == 0)
                {
                    temp += 4;
                }
            }

            //check if there is a 2 in this byte
            if ((t_value & 0b0000_0010) != 0)
            {
                if (m_LevelArray[t_x + 1, t_y] == 0)
                {
                    temp += 2;
                }

            }

            //check if there is a 8 in this byte
            if ((t_value & 0b0000_1000) != 0)
            {
                if (m_LevelArray[t_x - 1, t_y] == 0)
                {
                    temp += 8;
                }

            }
        }

        return temp;
    }

    /// <summary>
    /// Creates a room based on the rooms around it
    /// TODO:Replace and remove the need of the 2d array
    /// </summary>
    void GenRoom(int t_x, int t_y, byte t_Surround)
    {
        //Random t_Rnd = new Random();
        //array holding all the possible exists
        byte[] t_ExitsValues = { 0b0000_0001, 0b0000_0010, 0b0000_0100, 0b0000_1000 };
        
        //for possible exits go through them all
        for (int i = 0; i < t_ExitsValues.Length; i++)
        {
            //if current root room has this exit 
            if ((t_Surround & t_ExitsValues[i]) != 0)
            {
                bool t_ExtraExit = false;
                byte t_GenedRoom = 0;

                while (!t_ExtraExit)
                {
                    if (Random.Range(0, 2) == 1 && i != 2)
                    {
                        t_GenedRoom += 1;
                        t_ExtraExit = true;
                    }
                    if (Random.Range(0, 2) == 1 && i != 3)
                    {
                        t_GenedRoom += 2;
                        t_ExtraExit = true;
                    }
                    if (Random.Range(0, 2) == 1 && i != 0)
                    {
                        t_GenedRoom += 4;
                        t_ExtraExit = true;
                    }
                    if (Random.Range(0, 2) == 1 && i != 1)
                    {
                        t_GenedRoom += 8;
                        t_ExtraExit = true;
                    }
                }

                Room room;

                switch (i)
                {
                    case 0:
                        t_GenedRoom += 4;
                        m_LevelArray[t_x, t_y - 1] = t_GenedRoom;
                        room = new Room(m_DoneRooms, t_GenedRoom, (byte)(t_x), (byte)(t_y - 1));
                        m_Rooms.Add(room);
                        m_DoneRooms++;
                        break;

                    case 1:
                        t_GenedRoom += 8;
                        m_LevelArray[t_x + 1, t_y] = t_GenedRoom;
                        room = new Room(m_DoneRooms, t_GenedRoom, (byte)(t_x + 1), (byte)(t_y));
                        m_Rooms.Add(room);
                        m_DoneRooms++;
                        break;

                    case 2:
                        t_GenedRoom += 1;
                        m_LevelArray[t_x, t_y + 1] = t_GenedRoom;
                        room = new Room(m_DoneRooms, t_GenedRoom, (byte)(t_x), (byte)(t_y + 1));
                        m_Rooms.Add(room);
                        m_DoneRooms++;
                        break;

                    case 3:
                        t_GenedRoom += 2;
                        m_LevelArray[t_x - 1, t_y] = t_GenedRoom;
                        room = new Room(m_DoneRooms, t_GenedRoom, (byte)(t_x - 1), (byte)(t_y));
                        m_Rooms.Add(room);
                        m_DoneRooms++;
                        break;
                    
                }
            }
        }
    }

    /// <summary>
    /// This Takes the m_Rooms  
    /// </summary>
    void CleanMap()
    {

        for (int i = 0; i < m_Rooms.Count; i++)
        {
            Room t_Room = m_Rooms[i];
            if ((t_Room.m_Exits & 0b0000_0001) != 0)
            {
                if (!((m_LevelArray[t_Room.m_X, t_Room.m_Y - 1] & 0b0000_0100) != 0))
                {
                    t_Room.m_Exits -= 1;
                }
            }
            if ((t_Room.m_Exits & 0b0000_0010) != 0)
            {
                if (!((m_LevelArray[t_Room.m_X + 1, t_Room.m_Y] & 0b0000_1000) != 0))
                {
                    t_Room.m_Exits -= 2;
                }
            }
            if ((t_Room.m_Exits & 0b0000_0100) != 0)
            {
                if (!((m_LevelArray[t_Room.m_X, t_Room.m_Y + 1] & 0b0000_0001) != 0))
                {
                    t_Room.m_Exits -= 4;
                }
            }
            if ((t_Room.m_Exits & 0b0000_1000) != 0)
            {
                if (!((m_LevelArray[t_Room.m_X - 1, t_Room.m_Y] & 0b0000_0010) != 0))
                {
                    t_Room.m_Exits -= 8;
                }

            }



            m_LevelArray[t_Room.m_X, t_Room.m_Y] = t_Room.m_Exits;
            m_Rooms[i] = t_Room;

        }
    }


    /// <summary>
    /// GetRoomList does what it says 
    /// </summary>
    public List<Room> GetRoomList() 
    {
        return m_Rooms;
    }


    public void ChangeHue()
    {
        allChildren.Clear();
        FindChildren(transform);
        
        int red = Random.Range(20, 80);
        int green = Random.Range(20, 80);
        int blue = Random.Range(20, 80);

        foreach (GameObject child in allChildren)
        {
            MeshRenderer meshRenderer;
            if (child.TryGetComponent<MeshRenderer>(out meshRenderer))
            {
                meshRenderer.material.color =  Color.red * red / 100 + Color.green * green / 100 + Color.blue * blue / 100;
            }
        }

    }

    void FindChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            
            Transform child = parent.GetChild(i);
            if (child.transform.position.y>-3) 
            {
                allChildren.Add(child.gameObject);
                FindChildren(child);
            }
        }
    }

    IEnumerator DiscoTime(GameObject child, Color start, Color end, Color OG) 
    {
        MeshRenderer meshRenderer;
        if (child.TryGetComponent<MeshRenderer>(out meshRenderer) && child.gameObject.activeInHierarchy)
        {
            float elapsedTime = 0f;
            while (elapsedTime < 7f)
            {
                meshRenderer.material.color = Color.Lerp(start, end, Mathf.PingPong(elapsedTime, .5f));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            meshRenderer.material.color = OG;
        }
    }

    public void DiscoSetUp() 
    {
        Color t_Color = Color.white;
        allChildren.Clear();
        allChildren.Clear();
        FindChildren(transform);
        foreach (GameObject child in allChildren)
        {
            MeshRenderer meshRenderer;
            TextMesh textMesh;
            if (child.TryGetComponent<MeshRenderer>(out meshRenderer) && !child.TryGetComponent<TextMesh>(out textMesh))
            {
                try
                {
                    t_Color = meshRenderer.material.color;
                    int red = Random.Range(20, 80);
                    int green = Random.Range(20, 80);
                    int blue = Random.Range(20, 80);
                    Color t_Start = Color.red * red / 100 + Color.green * green / 100 + Color.blue * blue / 100;

                    red = Random.Range(20, 80);
                    green = Random.Range(20, 80);
                    blue = Random.Range(20, 80);
                    Color t_End = Color.red * red / 100 + Color.green * green / 100 + Color.blue * blue / 100;

                    StartCoroutine(DiscoTime(child, t_Start, t_End, t_Color));
                }
                catch (System.Exception e)
                {

                }
            }
        }
    }
}



