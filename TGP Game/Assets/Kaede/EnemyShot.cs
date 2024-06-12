using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject shellPrefab;
    public AudioClip sound;
    private int count;

    void Update()
    {
        count += 1;

        // attack/300f
        if (count % 300 == 0)
        {
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // speed of bullet
            shellRb.AddForce(transform.forward * 300);

            // destroy the shell after 5 seconds
            Destroy(shell, 5.0f);
        }
    }
}
