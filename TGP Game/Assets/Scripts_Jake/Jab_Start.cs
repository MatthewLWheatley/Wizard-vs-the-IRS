using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab_Start : Weapon_Base
{
    private Animator animator;
    private Camera cam;


    public float m_hitBoxTime = 0.4f;
    [SerializeField] private GameObject sword;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        level = 1;
        rTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //this whole thing just triggers the animator and tracks reload time
        reload -= rTime * Time.deltaTime;
        if (reload <= 0)
        {
            reload = 0;
            if (Input.GetMouseButtonDown(fire))
            {
                sword.SetActive(true);
                animator.SetTrigger("Jab");
                source.PlayOneShot(clip);
                reload = 1f;
                sword.GetComponent<Sword_Hitbox_Toggle>().hitBoxTime = m_hitBoxTime;
                sword.GetComponent<Damage>().m_level = level;
            }
        }
    }
}
