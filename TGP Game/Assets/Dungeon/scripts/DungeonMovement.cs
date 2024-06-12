using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structures;

public class DungeonMovement : MonoBehaviour
{
    [SerializeField] bool m_DoorEntered = false;
    [SerializeField] float m_ReEnterTime = 0.25f;
    float m_LastTimeEntered;
    public List<Room> m_Rooms;
    public int m_CurrentID = 0;
    public int m_BoosRoomID = 0;
    public int RoomExit;

    public GameObject parent;

    List<int> m_RoomsEntered;
    int m_NextRoom;
    // Start is called before the first frame update
    void Start()
    { 
        m_RoomsEntered = new List<int>();
        m_RoomsEntered.Add(0); 
        parent = this.transform.parent.GetComponent<LevelManager>().m_DungeonManger.transform.GetComponent<ProceduralGen>().m_DungTemplate; 
        m_Rooms = this.transform.parent.GetComponent<LevelManager>().m_DungeonManger.transform.GetComponent<ProceduralGen>().m_Rooms;
        m_CurrentID = 0; 
    }
    private void Awake() 
    { 
        m_RoomsEntered = new List<int>();
        m_RoomsEntered.Add(0); 
        parent = this.transform.parent.GetComponent<LevelManager>().m_DungeonManger.transform.GetComponent<ProceduralGen>().m_DungTemplate; 
        m_Rooms = this.transform.parent.GetComponent<LevelManager>().m_DungeonManger.transform.GetComponent<ProceduralGen>().m_Rooms; m_CurrentID = 0;
    }


    private void OnDestroy()
    {
        m_Rooms = null;
        parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - m_LastTimeEntered > m_ReEnterTime)
        {
            m_DoorEntered = false;
            m_LastTimeEntered = Time.time;

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        //Debug.Log("collided"+ collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Door") && !m_DoorEntered)
        {
            parent = this.transform.parent.GetComponent<LevelManager>().m_DungeonManger.transform.GetComponent<ProceduralGen>().m_DungTemplate;
            m_Rooms = this.transform.parent.GetComponent<LevelManager>().m_DungeonManger.transform.GetComponent<ProceduralGen>().m_Rooms;
            this.transform.GetComponent<CharacterController>().enabled = false;
            this.transform.GetComponent<CharacterController>().enabled = false;
            

            //Debug.Log(this.transform.parent.GetComponent<LevelManager>().m_DungeonManger.transform.GetComponent<ProceduralGen>().m_Rooms.Count);
            //Debug.Log("Door"+ m_Rooms.Count + " " + m_CurrentID);
            m_DoorEntered = true;
            
            int t_X = m_Rooms[m_CurrentID].m_X;
            int t_Y = m_Rooms[m_CurrentID].m_Y;

            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(0, 0, 1), out hit, .25f))
            {
                //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(0, 0, 1), Color.red, 10f);
                //Debug.Log(" up " + hit.transform.gameObject.tag);
                RoomExit = 1;
                t_X += 1;
            }

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(.5f, 0, 0), out hit, .25f)) 
            {
                //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(.5f, 0, 0), Color.green, 10f);
                //Debug.Log(" right " + hit.transform.gameObject.tag);
                t_Y += 1;
                RoomExit = 2;
            }

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(0, 0, -.5f), out hit, .25f)) 
            {

                //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(0, 0, -.5f), Color.black, 10f);
                //Debug.Log(" down " + hit.transform.gameObject.tag);
                RoomExit = 4;
                t_X -= 1;
            }

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(-.5f, 0, 0), out hit, .25f))
            {
                //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), new Vector3(-.5f, 0, 0), Color.blue, 10f);
                //Debug.Log(" left " + hit.transform.gameObject.tag);
                t_Y -= 1;
                RoomExit = 8;
            }

            


            Vector3 pos = transform.position;
            switch (RoomExit)
            {
                case 1:
                    //Debug.Log(RoomExit);
                    this.transform.position += new Vector3(0, 0, 1);
                    //this.transform.GetComponent<PlayerMovement>().Controller.Move(new Vector3(0,0,3));
                    break;
                case 2:
                    //Debug.Log(RoomExit);
                    this.transform.position += new Vector3(1, 0, 0);
                    //this.transform.GetComponent<PlayerMovement>().Controller.Move(new Vector3(3, 0, 0));
                    break;
                case 4:
                    //Debug.Log(RoomExit);
                    this.transform.position += new Vector3(0, 0, -1);
                    //this.transform.GetComponent<PlayerMovement>().Controller.Move(new Vector3(0, 0, -3));
                    break;
                case 8:
                    //Debug.Log(RoomExit);
                    this.transform.position += new Vector3(-1, 0, 0);
                    //this.transform.GetComponent<PlayerMovement>().Controller.Move(new Vector3(-3, 0, 0));
                    break;
            }


            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Drop").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Drop")[i]);
            }

            this.transform.GetComponent<CharacterController>().enabled = true;
            this.transform.GetComponent<CharacterController>().enabled = true;
            foreach (Room room in m_Rooms)
            {
                if (room.m_X == t_X && room.m_Y == t_Y)
                {
                    m_NextRoom = room.m_ID;
                }
            }

            // Health
            Health tempHealth = transform.GetComponent<Health>();

            if (m_RoomsEntered.Contains(m_NextRoom))
            {
                tempHealth.AddHealth(0);
            }
            else
            {
                if (MenuValues.m_Difficulty == 0)
                {
                    tempHealth.AddHealth(tempHealth.Maxhealth);
                }
                else if (MenuValues.m_Difficulty == 1)
                {
                    tempHealth.AddHealth(50);
                }
                else
                {

                }
            }


           // StartCoroutine(tempHealth.C_LoseHealth());

            foreach (Room room in m_Rooms)
            {
                if (room.m_X == t_X && room.m_Y == t_Y)
                {
                    m_CurrentID = room.m_ID;
                    parent.GetComponent<DungTemplate>().Deavtivate(m_CurrentID, RoomExit);
                }
            }

        }
    }
}
