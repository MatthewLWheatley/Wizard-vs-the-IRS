using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connective_Hitbox : MonoBehaviour
{
    public GameObject m_nextSwipe;
    
    private Camera cam;

    public Vector3 Swipe;
    public float X = -0.1f, Y = 0f, Z = -0.1f;
    bool summoned = false;

    private void Start()
    {
        cam = Camera.main;
        Swipe = new Vector3(X, Y, Z);
    }

    // Update is called once per frame
    void Update()
    {
        if(!summoned)
        {
            if (gameObject.transform.rotation.x <= 180)
                Swipe.x = X - (X * (gameObject.transform.rotation.x / 90));
            else
                Swipe.x = -X + (X * ((gameObject.transform.rotation.x - 180) / 90));

            if (gameObject.transform.rotation.z <= 180)
                Swipe.x = X - (X * (gameObject.transform.rotation.z / 90));
            else
                Swipe.x = -X + (X * ((gameObject.transform.rotation.z - 180) / 90));

            Quaternion baseRotation = Quaternion.LookRotation(transform.forward, Vector3.up);

            Instantiate<GameObject>(m_nextSwipe, transform.position + Swipe, baseRotation);
            summoned = true;
        }
    }
}
