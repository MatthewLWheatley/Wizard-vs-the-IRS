using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichBossAI : MonoBehaviour
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
    [SerializeField] public float m_bosshealth;
    private float m_damage = 8;
    private int m_speed = 5;
    private int currentattack;
    char PdamageType;
    private float PlayerDamage;
    int DamageLevel;

    private Animator animator;
    [SerializeField] private GameObject eldritchBlast;
    [SerializeField] private GameObject eldritchLauncher;
    [SerializeField] private GameObject wisp;
    [SerializeField] private ParticleSystem Hurt;
    [SerializeField] private ParticleSystem Phase;

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

    public GameObject m_SprayPrefab;
    //triggers the 3 sprays
    [SerializeField] private ParticleSystem spray1;
    [SerializeField] private ParticleSystem spray2;
    [SerializeField] private ParticleSystem spray3;
    [SerializeField] private ParticleSystem spray4;
    [SerializeField] private ParticleSystem spray5;

    [SerializeField] private GameObject HealthBar;
    public Vector3 projectileOrigin;
    public float X = 0, Y = 3, Z = 0;
    [SerializeField] private int m_projectilespeed;

    public bool m_Defeated;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        
    }


    void Start()
    {
        m_Defeated = false;
        int m_difficulty = MenuValues.m_Difficulty;
        float level = 1 + (0.1f * LevelCounter.m_CurrentLevel);
        if (m_difficulty == 0)
        {
            m_bosshealth = (m_bosshealth * 0.8f)* level;
            m_damage = (m_damage * 0.8f)* level;
            agent.speed = (m_speed * 0.8f)* level;
        }
        else if (m_difficulty == 1)
        {
            m_bosshealth = (m_bosshealth * 1f)* level;
            m_damage = (m_damage * 1f)* level;
            agent.speed = (m_speed * 0.75f)* level;
        }
        else if (m_difficulty == 2)
        {
            m_bosshealth = (m_bosshealth * 1.50f)* level;
            m_damage = (m_damage * 1.50f)* level;
            agent.speed = (m_speed * 1f)* level;
        }
        else
        {
            Debug.Log("Difficulty not selected");
            m_bosshealth = m_bosshealth * 1f;
            agent.speed = m_speed * 1f;
        }
        initialHealth = m_bosshealth;
        HealthBar.GetComponent<BossHealthScaling>().m_MaxHealth = initialHealth;
        HealthBar.GetComponent<BossHealthScaling>().m_Health = initialHealth;
        //this.GetComponent<HealthBarScaling>().m_MaxHealth = initialHealth;

    }

    void FixedUpdate()
    {
        if(m_alive == true)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);  //Checks to see if player is within chase range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer); //Checks to see if player is within attack range
            //playerInAttackCloseRange = Physics.CheckSphere(transform.position, attackCloseRange, whatisPlayer); //Checks to see if player is within attack range

            
            
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
            
            //else
            //{
            //    if (playerInSightRange && !playerInAttackCloseRange) ChasePlayer();
            //    if (playerInSightRange && playerInAttackCloseRange) AttackPlayer();
            //}
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

    private void ChasePlayer()
    {
        animator.SetBool("Moving", true);
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
       
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
    
            if (Phase2)
            {
                if (!alreadySpecialAttack1)//Square Launcher bounce
                {
                    GameObject EldritchLauncher = Instantiate(eldritchLauncher, transform.position + (transform.forward * 2f) + projectileOrigin, Quaternion.LookRotation(player.position - transform.position));
                    EldritchLauncher.GetComponent<Rigidbody>().AddForce((player.transform.position - EldritchLauncher.transform.position).normalized * m_projectilespeed, ForceMode.Impulse);
                }
                else if (!alreadySpecialAttack2)//Mass Projectile Summon
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Vector3 newRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

                        GameObject EldritchBlast = Instantiate(eldritchBlast, transform.position + (newRotation - transform.position).normalized, Quaternion.LookRotation(player.position - transform.position));
                        EldritchBlast.GetComponent<Rigidbody>().AddForce((player.transform.position - EldritchBlast.transform.position).normalized * m_projectilespeed, ForceMode.Impulse);
                    }
                }
                else // Single shot
                {
                    GameObject spell = Instantiate(eldritchBlast, transform.position + new Vector3(0, 2, 0), transform.rotation);
                    spell.GetComponent<Rigidbody>().AddForce((player.transform.position - spell.transform.position).normalized * m_projectilespeed, ForceMode.Impulse);
                    PlayAIAudio(attack);
                    animator.SetTrigger("Attack1");
                }
                
            }
            else
            {
                animator.SetBool("Moving", false);
                if (!alreadySpecialAttack1)//Poison spray
                {
                    PlayAIAudio(attack);
                    animator.SetTrigger("Attack2");
                    Quaternion sprayRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);

                    //creates the projectile
                    GameObject tempRef = Instantiate<GameObject>(m_SprayPrefab, transform.position + (transform.forward) + new Vector3(0,1,0), sprayRotation);

                    spray1.Play();
                    spray2.Play();
                    spray3.Play();
                    spray4.Play();
                    spray5.Play();
                    alreadySpecialAttack1 = true;
                    Invoke(nameof(ResetSpecialAttack1), CooldownAttack1);
                }
                else if (!alreadySpecialAttack2)//Summon
                {
                    Debug.Log("2");
                    PlayAIAudio(attack);
                    animator.SetTrigger("Summon");
                    GameObject Homingspell = Instantiate(wisp, transform.position + new Vector3(0, 2, 0), transform.rotation);

                    alreadySpecialAttack2 = true;
                    Invoke(nameof(ResetSpecialAttack2), CooldownAttack2);
                }
                else//Range jab
                {
                    //regular melee attack
                   
                    PlayAIAudio(attack);
                    animator.SetTrigger("Attack1");
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

            rb.velocity = Vector3.zero;
            PlayerDamageGet(PdamageType);
            Hurt.Play();
            GameObject DamageNumbers = Instantiate(DamNumbers, transform.position, Quaternion.LookRotation(Vector3.zero));
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_IsCritical = false;
            DamageNumbers.GetComponentInChildren<FloatAndFade>().m_DamageDoneText = PlayerDamage.ToString();
            m_bosshealth -= PlayerDamage;
            HealthBar.GetComponent<BossHealthScaling>().m_Health = m_bosshealth;
            if (m_bosshealth <= initialHealth/2 && !Phase2)
            {
                Phase2 = true;
                attackRange = attackCloseRange;
                Phase.Play();
            }
            if (m_bosshealth <= 0f)
            {
                m_alive = false;
                agent.SetDestination(transform.position);
                PlayAIAudio(death);
                animator.SetTrigger("Death");
                this.GetComponent<BoxCollider>().enabled = false;
                m_Defeated = true;
                Invoke(nameof(DestroyEnemy), 3f);
            }
            else
            {
                source.Stop();
                PlayAIAudio(hit);
                animator.SetTrigger("Hit");

            }
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
       
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() // displays ranges whilst in editor
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackCloseRange);
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
