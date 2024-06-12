using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour
{
    private Animator animator;
    private Camera cam;

    float fireTime = 7.0f;
    float elapsedTime = 0.0f;

    [SerializeField] private string fire;
    [SerializeField] private GameObject sword;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    bool emoting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(fire) && elapsedTime >= fireTime)
        {
            animator.SetTrigger("Emote");
            this.transform.parent.transform.GetComponent<LevelManager>().m_DungeonManger.GetComponent<ProceduralGen>().DiscoSetUp();
            sword.SetActive(false);
            source.PlayOneShot(clip);
            emoting = true;
            elapsedTime = 0.0f;
        }
        else if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            if(emoting)
            {
                source.Stop();
                emoting = false;
                sword.SetActive(true);
            }
        }
        //else if (Input.anyKeyDown)
        //{
        //    //sword.SetActive(true);
        //}
    }
}
