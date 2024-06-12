using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeholderAI : MonoBehaviour
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


    private float health = 12;
    private float damage = 3;
    private int m_speed = 4;
    char PdamageType;
    private float PlayerDamage;
    int DamageLevel;

    bool m_alive = true;

    private Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private ParticleSystem Hurt;
    [SerializeField] private GameObject loot;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip hit;
    [SerializeField] private GameObject DamNumbers;
    [SerializeField] private GameObject HlthPickup;

    Rigidbody rb;
    private LineRenderer line;
    //private RaycastHit Impact;
    [SerializeField] private int m_projectilespeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        
    }


    void Start()
    {

        int m_difficulty = MenuValues.m_Difficulty;
        float level = 1 + (0.1f * LevelCounter.m_CurrentLevel);
        if (MenuValues.m_Difficulty == 0)//"Easy")
        {
            health = (health * 0.5f)* level;
            agent.speed = (m_speed * 0.5f)* level;
        }
        else if (MenuValues.m_Difficulty == 1)//"Medium")
        {
            health = (health * 1f) * level;
            agent.speed = (m_speed * 1f)*level;
        }
        else if (MenuValues.m_Difficulty == 2)//"Hard")
        {
            health = (health * 1.50f)*level;
            agent.speed = (m_speed * 1.2f)*level;
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
           
            TakeCritDamage();
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
            //this.GetComponent<Rigidbody>().AddForce((this.transform.position - transform.position).normalized * 160f, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Melee")
        {
            PdamageType = collision.gameObject.GetComponent<Damage>().m_damage;
            DamageLevel = collision.gameObject.GetComponent<Damage>().m_level;

            TakeCritDamage();
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
        
        line.enabled = false;
        animator.SetBool("Moving", true);
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, player.position);
        line.enabled = true;

        agent.SetDestination(transform.position);
        transform.LookAt(player);
        //agent.transform.position.x = player.position.x;
        if (!alreadyAttacked)
        {
            PlayAIAudio(attack);
            animator.SetTrigger("Attack");
            GameObject spell = Instantiate(projectile, transform.position+new Vector3(0,2,0), transform.rotation);
            spell.GetComponent<Rigidbody>().AddForce((player.transform.position-spell.transform.position).normalized* m_projectilespeed, ForceMode.Impulse);
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
        if(damageType == 'h')
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
            health -= PlayerDamage;
            if (health <= 0f)
            {
                m_alive = false;
                agent.SetDestination(transform.position);
                PlayAIAudio(death);
                animator.SetTrigger("Death");
                this.GetComponent<BoxCollider>().enabled = false;
                Invoke(nameof(DestroyEnemy), 2f);
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
            GameObject DamageNumbers = Instantiate(DamNumbers, transform.position, Quaternion.LookRotation(player.position - transform.position));
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_IsCritical = true;
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_DamageDoneText = PlayerDamage.ToString();
            health -= PlayerDamage;

            if (health <= 0f)
            {
                m_alive = false;
                line.enabled = false;
                agent.SetDestination(transform.position);
                PlayAIAudio(death);
                animator.SetTrigger("Death");
                this.GetComponent<BoxCollider>().enabled = false;
                Invoke(nameof(DestroyEnemy), 2f);
            }
        }
    }

    private void DestroyEnemy()
    {
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
        //Add to drunk meter
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
