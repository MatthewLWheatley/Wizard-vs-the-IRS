using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MimicAI : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround, whatisPlayer;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private float health = 24;
    //private float damage = 8;
    private int m_speed = 4;

    private Animator animator;
    [SerializeField] private GameObject loot;
    [SerializeField] private ParticleSystem Hurt;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip jingle;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip hit;
    [SerializeField] private GameObject DamNumbers;
    [SerializeField] private GameObject HlthPickup;

    char PdamageType;
    private float PlayerDamage;
    int DamageLevel;
    Rigidbody rb;
    bool m_alive = true;

    private bool m_Hidden = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

    }


    void Start()
    {
        int m_difficulty = MenuValues.m_Difficulty;

        if (m_difficulty == 0)//"Easy")
        {
            health = health * 0.5f;
            agent.speed = m_speed * 0.5f;
        }
        else if (m_difficulty == 1)//"Medium")
        {
            health = health * 1f;
            agent.speed = m_speed * 1f;
        }
        else if (m_difficulty == 2)//"Hard")
        {
            health = health * 1.50f;
            agent.speed = m_speed * 1.25f;
        }
        else
        {
            Debug.Log("Difficulty not selected");
            health = health * 1f;
            agent.speed = m_speed * 1f;
        }
    }

    void FixedUpdate()
    {
        if (m_alive)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);  //Checks to see if player is within chase range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer); //Checks to see if player is within attack range

            if (m_Hidden && playerInAttackRange)
            {
                //player.getinput
                animator.SetBool("Spotted", true);
                Invoke(nameof(ExitObjectForm), 2.0f);
            }
            if (!m_Hidden && playerInSightRange) { FleePlayer(); }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Melee")
        {
            PdamageType = collision.gameObject.GetComponent<Damage>().m_damage;
            DamageLevel = collision.gameObject.GetComponent<Damage>().m_level;
           
            TakeDamage();
        }
        if (collision.gameObject.tag == "Ranged")
        {
            PdamageType = collision.gameObject.GetComponent<Damage>().m_damage;
            DamageLevel = collision.gameObject.GetComponent<Damage>().m_level;
            
            TakeDamage();
        }
        if (collision.gameObject.tag == "Special")
        {
            PdamageType = collision.gameObject.GetComponent<Damage>().m_damage;
            DamageLevel = collision.gameObject.GetComponent<Damage>().m_level;
            
            TakeDamage();
        }
        if (collision.gameObject.tag == "Player") // bumper bounce?
        {
            //Vector3 KnockbackDirection = collision.transform.position - transform.position;
            //KnockbackDirection.Normalize();
            //PlayerMovement bump = player.GetComponent<PlayerMovement>();
            //bump.AddImpact(KnockbackDirection);
            // this.GetComponent<Rigidbody>().AddForce((this.transform.position - transform.position).normalized * 160f, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Melee")
        {
            PdamageType = collision.gameObject.GetComponent<Damage>().m_damage;
            DamageLevel = collision.gameObject.GetComponent<Damage>().m_level;
            
            TakeDamage();
        }
        if (collision.gameObject.tag == "Ranged")
        {
            PdamageType = collision.gameObject.GetComponent<Damage>().m_damage;
            DamageLevel = collision.gameObject.GetComponent<Damage>().m_level;
           
            TakeDamage();
        }
        if (collision.gameObject.tag == "Special")
        {
            PdamageType = collision.gameObject.GetComponent<Damage>().m_damage;
            DamageLevel = collision.gameObject.GetComponent<Damage>().m_level;
           
            TakeDamage();
        }
    }

    //private void Patrol()
    //{

    //    if (!walkPointSet) SearchWalkPoint();

    //    if (walkPointSet) agent.SetDestination(walkPoint);

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    if (distanceToWalkPoint.magnitude < 1f)
    //        walkPointSet = false;
    //}
    //private void SearchWalkPoint()
    //{
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //}

    private void FleePlayer()
    {
        animator.SetBool("Moving", true);
        
        agent.SetDestination(transform.position - player.position);

    }

    public void PlayerDamageGet(char damageType)
    {
        if (damageType == 'h')
        {
            PlayerDamage = 6f;
        }
        if (damageType == 'm')
        {
            PlayerDamage = 4f;
        }
        if (damageType == 'l')
        {
            PlayerDamage = 2f;
        }
        if (damageType == 't')
        {
            PlayerDamage = 0.5f;
        }
    }


    public void TakeDamage()
    {
        m_Hidden = false;
        if (source.isPlaying)
        {
            source.Stop();
            PlayAIAudio(hit);
        }
        animator.SetTrigger("Hit");
        rb.velocity = Vector3.zero;
        PlayerDamageGet(PdamageType);
        Hurt.Play();
        
        GameObject DamageNumbers = Instantiate(DamNumbers, transform.position, Quaternion.LookRotation(player.position - transform.position));
        DamageNumbers.GetComponentInChildren<FloatAndFade>().m_DamageDoneText = PlayerDamage.ToString();
        DamageNumbers.GetComponentInChildren<FloatAndFade>().m_IsCritical = false;
        health -= PlayerDamage;
        if (health <= 0f && m_alive)
        {
            //PlayAIAudio(death);
            m_alive = false;
            agent.SetDestination(transform.position);
            animator.SetTrigger("Death");
            this.GetComponent<BoxCollider>().enabled = false;
            Invoke(nameof(DestroyEnemy), 2f);
        }
    }
    //public void TakeCritDamage()
    //{
    //    rb.velocity = Vector3.zero;
    //    //Hurt.Play();
    //    health -= (damage * 1.5f);
    //    GameObject DamageNumbers = Instantiate(DamNumbers, transform.position, Quaternion.LookRotation(player.position - transform.position));
    //    DamageNumbers.GetComponent<FloatAndFade>().m_DamageDoneText = PlayerDamage.ToString();
    //    DamageNumbers.GetComponent<FloatAndFade>().m_IsCritical = true;
    //    if (health <= 0f)
    //    {
    //        //PlayAIAudio(death);
    //        animator.SetTrigger("Death");
    //        Invoke(nameof(DestroyEnemy), 0.5f);
    //    }
    //}
    private void ExitObjectForm()
    {
        m_Hidden = false;
        
    }

        private void DestroyEnemy()
    {
        //Add to drunk meter
        PickUpObject LootID = loot.GetComponentInChildren<PickUpObject>();
        LootID.ID = Random.Range(1, 15);
        Instantiate(loot, transform.position, Quaternion.LookRotation(player.position - transform.position));

        Health m_health = player.GetComponent<Health>();
        m_health.AddHealth(15f);
        GameObject HealthPickup = Instantiate(HlthPickup, transform.position, Quaternion.LookRotation(player.position - transform.position));
        HealthPickup.GetComponentInChildren<HealthPickupAnim>().m_StartPosition = Camera.main.WorldToScreenPoint(transform.position);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() // displays ranges whilst in editor
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }

    public void PlayAIAudio(AudioClip Clip)
    {
        source.PlayOneShot(Clip);

    }

}

