using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIProjectile : MonoBehaviour
{
    [SerializeField] private float dam_value;
 

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            Health m_health = collision.gameObject.GetComponent<Health>();
            m_health.AddHealth(-dam_value * 5);
            Destroy(gameObject);

        }
        Destroy(gameObject);

    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            Health m_health = collision.gameObject.GetComponent<Health>();
            m_health.AddHealth(-dam_value * 5);
            

        }
        Destroy(gameObject);

    }
}