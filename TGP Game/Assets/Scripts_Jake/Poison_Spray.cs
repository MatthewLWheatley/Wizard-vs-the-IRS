using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Spray : Weapon_Base
{
    private Animator animator;
    public GameObject m_SprayPrefab;
    private Camera cam;

    private Vector3 projectileOrigin;
    public float X = 0f, Y = 1f, Z = 2f;

    //tracks reload time

    //triggers the 3 sprays
    [SerializeField] private ParticleSystem spray1;
    [SerializeField] private ParticleSystem spray2;
    [SerializeField] private ParticleSystem spray3;

    void Start()
    {
        cam = Camera.main;
        //used to decide where the projectile spawns in
        projectileOrigin = new Vector3(X, Y, Z);
        animator = GetComponent<Animator>();
        level = 1;
        rTime = 0.25f;
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
                //-transfrom.forward makes it face the right way
                Quaternion sprayRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);

                //creates the projectile
                GameObject tempRef = Instantiate<GameObject>(m_SprayPrefab, transform.position + (transform.forward * cam.nearClipPlane * 2f) + projectileOrigin, sprayRotation);

                tempRef.GetComponentInChildren<Damage>().m_level = level;

                animator.SetTrigger("Cast");
                reload = 1f;
                spray1.Play();
                spray2.Play();
                spray3.Play();
            }
        }
    }
}
