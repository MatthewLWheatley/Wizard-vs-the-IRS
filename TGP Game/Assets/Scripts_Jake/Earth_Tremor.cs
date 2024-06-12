using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Tremor : Weapon_Base
{
    private Animator animator;
    public GameObject m_SprayPrefab;
    private Camera cam;

    private Vector3 projectileOrigin;
    public float X = 0f, Y = 1f, Z = 2f;

    //tracks reload time
    //how long it takes the spell to reload
    [SerializeField] private ParticleSystem tremor;

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

                tempRef.GetComponent<Damage>().m_level = level;

                animator.SetTrigger("Cast");
                reload = 1f;
                if (tremor != null)
                    tremor.Play();
            }
        }
    }
}
