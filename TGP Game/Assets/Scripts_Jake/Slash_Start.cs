using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash_Start : Weapon_Base
{
    private Animator animator;
    private Camera cam;
    public Collider m_Collider;

    public float m_hitBoxTime = 1.1f;

    [SerializeField] private GameObject sword;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider>();
        cam = Camera.main;
        rTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        reload -= rTime * Time.deltaTime;
        if (reload <= 0)
        {
            reload = 0;
            if (Input.GetMouseButtonDown(fire))
            {
                sword.SetActive(true);
                animator.SetTrigger("Slash");
                source.PlayOneShot(clip);
                m_Collider.enabled = true;
                reload = 1f;
                sword.GetComponent<Sword_Hitbox_Toggle>().hitBoxTime = m_hitBoxTime;
                sword.GetComponent<Damage>().m_level = level;
            }
        }
    }
}
