using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemAI : MonoBehaviour
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


    private float m_health = 40;
    private float m_damage = 3;
    private int m_speed = 2;

    private Animator animator;
    
    [SerializeField] private ParticleSystem Hurt;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip death;
    [SerializeField] private GameObject loot;
    [SerializeField] private AudioClip hit;
    [SerializeField] private GameObject DamNumbers;
    [SerializeField] private GameObject HlthPickup;

    bool m_alive = true;
    char PdamageType;
    private float PlayerDamage;
    int DamageLevel;
    Rigidbody rb;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

    }


    void Start()
    {
        int difficulty = MenuValues.m_Difficulty;//the title class .variablevv
        float level = 1 + (0.1f * LevelCounter.m_CurrentLevel);
        if (difficulty == 0)
        {
            m_health = (m_health * 0.5f)* level;
            m_damage = (m_damage * 0.5f) * level;
            agent.speed = (m_speed * 0.5f) * level;
        }
        else if (difficulty == 1)
        {
            m_health = (m_health * 1f) * level;
            m_damage = (m_damage * 1f) * level;
            agent.speed = (m_speed * 1f) * level;
        }
        else if (difficulty == 2)
        {
            m_health = (m_health * 1.50f) * level;
            m_damage = (m_damage * 1.50f) * level;
            agent.speed = (m_speed * 1.50f) * level;
        }
    }

    void FixedUpdate()
    {
        if (m_alive)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);  //Checks to see if player is within chase range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer); //Checks to see if player is within attack range

            if (!playerInSightRange && !playerInAttackRange) Patrol();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
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

            TakeCritDamage();
        }
        if (collision.gameObject.tag == "Player") // bumper bounce?
        {

            //Vector3 KnockbackDirection = collision.transform.position - transform.position;
            //KnockbackDirection.Normalize();
            //PlayerMovement bump = player.GetComponent<PlayerMovement>();
            //bump.AddImpact(KnockbackDirection);

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
      
            TakeCritDamage();
        }
    }


    private void Patrol()
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    }

    private void ChasePlayer()
    {
        animator.SetBool("Moving", true);
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        //agent.transform.position.x = player.position.x;
        if (!alreadyAttacked)
        {
           
            PlayAIAudio(attack);
            animator.SetTrigger("Attack");
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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
            PlayerDamage = 0.1f;
        }
    }


    public void TakeDamage()
    {
        if (m_alive)
        {
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
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_IsCritical = false;
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_DamageDoneText = PlayerDamage.ToString();
            m_health -= PlayerDamage;
            if (m_health <= 0f)
            {
                m_alive = false;
                agent.SetDestination(transform.position);
                PlayAIAudio(death);
                animator.SetTrigger("Death");
                this.GetComponent<BoxCollider>().enabled = false;
                Invoke(nameof(DestroyEnemy), 2f);
            }
            else
            {
                PlayAIAudio(hit);
                animator.SetTrigger("Hit");

            }
        }
    }
    public void TakeCritDamage()
    {
        if (m_alive)
        {
            if (source.isPlaying)
            {
                source.Stop();
                PlayAIAudio(hit);
            }
                animator.SetTrigger("Hit");

            
            rb.velocity = Vector3.zero;
            PlayerDamageGet(PdamageType);
            PlayerDamage = PlayerDamage * 1.5f;
            Hurt.Play();
            m_health -= PlayerDamage;
            GameObject DamageNumbers = Instantiate(DamNumbers, transform.position, Quaternion.LookRotation(player.position - transform.position));
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_IsCritical = true;
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_DamageDoneText = PlayerDamage.ToString();
            if (m_health <= 0f)
            {
                //PlayAIAudio(death);
                //animator.SetTrigger("Death");
                this.GetComponent<BoxCollider>().enabled = false;
                Invoke(nameof(DestroyEnemy), 2f);
            }
        }
    }

    private void DestroyEnemy()
    {
        //Add to drunk meter
        if (Random.Range(0, 8) == 0)
        {
            PickUpObject LootID = loot.GetComponentInChildren<PickUpObject>();
            LootID.ID = Random.Range(0, 13);
            Instantiate(loot, transform.position, Quaternion.LookRotation(player.position - transform.position));
           
        }
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