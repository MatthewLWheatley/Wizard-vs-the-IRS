using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MimBossAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround, whatisPlayer;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    bool alreadySpecialAttack1;
    bool alreadySpecialAttack2;
    public float CooldownAttack1;
    public float CooldownAttack2;
    private bool Phase2 = false;

    //States
    public float sightRange, attackRange, attackCloseRange;
    public bool playerInSightRange, playerInAttackRange, playerInAttackCloseRange;

    private bool m_alive = true;
    public float m_bosshealth = 100;
    private float m_damage = 8;
    private int m_speed = 5;
    private int currentattack;
    char PdamageType;
    private float PlayerDamage;
    int DamageLevel;


    private Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private ParticleSystem Hurt;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip hit;

    [SerializeField] private GameObject loot;
    [SerializeField] private GameObject DamNumbers;
    [SerializeField] private GameObject HlthPickup;
    [SerializeField] private GameObject DthIcon;
    Rigidbody rb;
    private float initialHealth;

    [SerializeField] private GameObject HealthBar;
    public bool m_Defeated = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
    }


    void Start()
    {
        string difficulty = "Medium";//the title class .variablevv
        if (difficulty == "Easy")
        {
            m_bosshealth = m_bosshealth * 0.75f;
            m_damage = m_damage * 0.75f;
            agent.speed = m_speed * 0.75f;
        }
        if (difficulty == "Medium")
        {
            m_bosshealth = m_bosshealth * 1f;
            m_damage = m_damage * 1f;
            agent.speed = m_speed * 0.75f;
        }
        if (difficulty == "Hard")
        {
            m_bosshealth = m_bosshealth * 1.50f;
            m_damage = m_damage * 1.50f;
            agent.speed = m_speed * 0.75f;
        }
    }

    void Update()
    {
        //if(intro)
        //{
        //    agent.SetDestination(transform.position);
        //    Invoke(nameof(EndIntro), 6.0f);
        //}
        //else
        //{
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);  //Checks to see if player is within chase range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer); //Checks to see if player is within attack range


            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        
    
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Melee")
        {
            PlayAIAudio(hit);
            animator.SetTrigger("Hit");
            TakeDamage();
        }
      
    }


    private void ChasePlayer()
    {
        //animator.SetBool("Moving", true);
        agent.SetDestination(player.position);
        

    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            if (!Phase2)
            {
                if (!alreadySpecialAttack1)//Screem
                {
                    PlayAIAudio(attack);
                    animator.SetTrigger("Attack2");

                    Invoke(nameof(ResetSpecialAttack1), CooldownAttack1);
                }
                else if (!alreadySpecialAttack2)//Summon
                {
                    PlayAIAudio(attack);
                    animator.SetTrigger("Summon");
                    GameObject Homingspell = Instantiate(projectile, transform.position + (player.position - transform.position).normalized, Quaternion.LookRotation(player.position - transform.position));
                    GameObject Homingspell1 = Instantiate(projectile, (transform.position + new Vector3(1, 0, 0)).normalized + (player.position - transform.position).normalized, Quaternion.LookRotation(player.position - transform.position));
                    GameObject Homingspell2 = Instantiate(projectile, (transform.position + new Vector3(1, 0, 0)).normalized + (player.position - transform.position).normalized, Quaternion.LookRotation(player.position - transform.position));
                    Invoke(nameof(ResetSpecialAttack2), CooldownAttack2);
                }
                else//bite
                {
                    //regular melee attack
                    
                    PlayAIAudio(attack);
                    animator.SetTrigger("Attack1");
                }
            }
            else
            {
                if (!alreadySpecialAttack1)//Debris fall
                {

                }
                else if (!alreadySpecialAttack2)//MinorHeal Hide
                {

                }
                else // Headbutt
                {

                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void ResetSpecialAttack1()
    {
        alreadySpecialAttack1 = false;
    }
    private void ResetSpecialAttack2()
    {
        alreadySpecialAttack2 = false;
    }
    private void EndIntro()
    {
        //intro = false;
    }

    public void TakeDamage()
    {
        //Hurt.Play();
        m_bosshealth -= m_damage;

        if (m_bosshealth <= 0f)
        {
            //PlayAIAudio(death);
            //animator.SetTrigger("Death");
            m_alive = false;
            Invoke(nameof(DestroyEnemy), 7.5f);
        }
    }
   

    private void DestroyEnemy()
    {
        PickUpObject LootID = loot.GetComponentInChildren<PickUpObject>();
        LootID.ID = Random.Range(1, 14);
        Instantiate(loot, transform.position, Quaternion.LookRotation(player.position - transform.position));
        Health m_health = player.GetComponent<Health>();
        m_health.AddHealth(15f);
        //UI elements to play on death
        GameObject HealthPickup = Instantiate(HlthPickup, transform.position, Quaternion.LookRotation(player.position - transform.position));
        HealthPickup.GetComponentInChildren<HealthPickupAnim>().m_StartPosition = Camera.main.WorldToScreenPoint(transform.position);
        GameObject DeathIcon = Instantiate(DthIcon, transform.position, Quaternion.LookRotation(player.position - transform.position));
        DeathIcon.GetComponentInChildren<DeathIcon>().m_StartPosition = Camera.main.WorldToScreenPoint(transform.position);
        //HealthCopy.Replenish(2.0f);//Add to drunk meter
        m_Defeated = true;
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
