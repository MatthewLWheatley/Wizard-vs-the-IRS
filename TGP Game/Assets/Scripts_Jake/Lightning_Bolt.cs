using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Bolt : Weapon_Base
{
    private Animator animator;
    public GameObject m_BoltPrefab;
    private Camera cam;

    private Vector3 projectileOrigin;
    public float X = 0f, Y = 1f, Z = 2f;

    public float m_FiringForce = 10f;

    //tracks reload time

    // fire is assigned to choose what button fires the spell

    [SerializeField] private ParticleSystem lightning;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    void Start()
    {
        cam = Camera.main;
        //used to decide where the projectile spawns in
        projectileOrigin = new Vector3(X, Y, Z);
        animator = GetComponent<Animator>();
        level = 1;
        rTime = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        reload -= rTime * Time.deltaTime;
        if (reload <= 0)
        {
            if (Input.GetMouseButton(fire))
            {
                //-transfrom.forward makes it face the right way
                Quaternion boltRotation = Quaternion.LookRotation(transform.forward, Vector3.up);

                //creates the projectile
                GameObject tempRef = Instantiate<GameObject>(m_BoltPrefab, transform.position + (transform.forward * cam.nearClipPlane * 2f) + projectileOrigin, boltRotation);

                Vector3 direction = transform.forward;

                tempRef.GetComponent<Damage>().m_level = level;

                animator.SetTrigger("Cast");
                tempRef.GetComponent<Rigidbody>().AddForce(direction * m_FiringForce, ForceMode.Impulse);
                reload = 1f;
                //starts the particle system
                lightning.Play();
                if(!source.isPlaying)
                    source.PlayOneShot(clip);
            }

            if(reload <= -5)
            {
                reload = 0;
                if (!source.isPlaying)
                    source.Stop();
            }
        }
    }
}
