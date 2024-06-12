using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public float MoveSpeed = 2.0f;
    public float DashTime = 0.25f;
    public float DashMultipler = 2.0f;
    public int DashCost = 33;

    public int DodgeCost = 33;
    public float DodgeTime = 0.5f;
    public bool Dodging;

    public float Gravity = 9.8f;
    private float TempGravity;
    private float VSpeed;


    private float KnockbackCount;
    public float KnockbackTime = 1.0f;
    public float KnockbackForce = 2.0f;

    private Camera Camera;
    public Stamina Stamina;
    public Health Health;

    private Animator PlayerAnimator;
    [SerializeField] private float m_SpinDelay;

    //private Vector3 impact = Vector3.zero;
    //private float mass  = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
        Stamina = FindObjectOfType<Stamina>();
        Health = FindObjectOfType<Health>();
        TempGravity = Gravity;
        Dodging = false;
        PlayerAnimator = GetComponent<Animator>();
        //CurrentStamna = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Controller.velocity.x) > 0 || Mathf.Abs(Controller.velocity.z) > 0)// Takes velocity value for x and z axis, makes them positive, then checks if they are above 0
        {
            PlayerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            PlayerAnimator.SetBool("IsMoving", false);
        }

        if (!Health.Death)// Temporay will replace with desory object latter.
        {
            float Speed = MoveSpeed * Time.deltaTime;
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");//Changed to GetAxisRaw so its more precise with keys
            Vector3 VectorInput = new Vector3(x, 0, z).normalized;//Puts Horizontal and vertical input into one vector
                                                                  //Normalized vector so speed is consistent when pressing two different buttons
            if (KnockbackCount <= 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && Stamina.UseStamina(DashCost))// Checks if User has pressed Dash button and if the user has enough stamina to dash
                {
                    StartCoroutine(C_Dash(VectorInput));//Dash Logic
                    VectorInput.y = GroundCheck();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && !Dodging /*Stamina.UseStamina(DodgeCost)*/)
                {
                    StartCoroutine(C_Dodge());
                    VectorInput.y = GroundCheck();
                }
                else
                {
                    
                    VectorInput.y = GroundCheck();
                    Controller.Move(VectorInput * Speed);//Default Movement Code
                }
            }
            else
            {
                KnockbackCount -= Time.deltaTime;
            }
            if (Controller.transform.position.y < -5.0f)// If falls to far Move him back to start point. Need to implemt damage for it.
            {
                Controller.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
            }

            //if (impact.magnitude > 0.2) { Controller.Move(impact * Time.deltaTime); }
            //// consumes the impact energy each cycle:
            //impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
            //if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            //{
            //    Aim();//Aiming Logic
            //}
            Aim(VectorInput);
        }
    }
    private void Aim(Vector3 Move)
    {
        if (!Dodging)
        {
            //if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            //{
                Ray Ray = Camera.ScreenPointToRay(Input.mousePosition);//Find mouse postion and Camerea shots ray though it towards ground.
                if (Physics.Raycast(Ray, out RaycastHit hitInfo))
                {
                    var target = hitInfo.point;//The Target is where the Ray hits
                    transform.LookAt(new Vector3(target.x, transform.position.y, target.z));// Looks at target but Ignores the Y cordinate so charter doesnt look up or down
                }
            //}
            else if ((Mathf.Abs(Move.x) > 0 || Mathf.Abs(Move.z) > 0))// Player stays looking in direction when no keys are pressed
            {
                Move.y = 0.0f;
                transform.LookAt(Move+ transform.position);
                //transform.rotation = Quaternion.LookRotation(Move);
            }
        }
    }
    private IEnumerator C_Dash(Vector3 Input)
    {
        float StartTime = Time.time;
        float CurrentTime = Time.time;
        while (StartTime + DashTime > CurrentTime)//Runs For Dash Time amount of Time
        {
            TempGravity = 0.0f;
            Controller.Move(Input * Time.deltaTime * MoveSpeed * DashMultipler);//Incrases the players Speed by Dash Multipler
            CurrentTime = Time.time;
            yield return null;
        }
        TempGravity = Gravity;
    }
    private IEnumerator C_Dodge()
    {
        float StartTime = Time.time;
        float CurrentTime = Time.time;
        Dodging = true;
        Physics.IgnoreLayerCollision(6, 7, true);//Turns off coilions between the player and enimes during dodge
        while (StartTime + DodgeTime > CurrentTime)// makes player doge fore DodgeTime amount of time
        {
            //Debug.Log("Rot");
            transform.Rotate(0, (360/ DodgeTime) * Time.deltaTime, 0);// Spins player 360 to show there doging can be changed.
            CurrentTime = Time.time;// incrases currentTime so the code only runs for dodgetime amount of time
            yield return null;
        }
        Physics.IgnoreLayerCollision(6, 7, false);//re enables coillions between the player and enemy

        Dodging = false;

    }
    private float GroundCheck()
    {
        if (Controller.isGrounded)
        {
            VSpeed = 0.0f;
            //Debug.Log("Ground");
            return VSpeed;
        }
        else
        {
            VSpeed -= TempGravity * Time.deltaTime;//Sets the verticle speed so Player falls if not grounded
            return VSpeed;
        }
    }
    public void AddImpact(Vector3 dir)//knockback logic
    {
        dir.y = 0.0f;
        KnockbackCount = KnockbackTime;
        if (!Dodging) { Controller.Move(dir * KnockbackForce); }

    }
}