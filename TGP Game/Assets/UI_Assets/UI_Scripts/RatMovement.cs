using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RatMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] m_RatPoints;
    [SerializeField] private float m_RatSpeed;
    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(C_RatPathing());
        audioSource = GetComponent<AudioSource>();

    }

    private void OnMouseDown()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.pitch = (Random.Range(1.0f, 1.2f));
            audioSource.Play();
        }
    }
    IEnumerator C_RatPathing()
    {
        int randNum = Random.Range(0, m_RatPoints.Length);
        Tween moveTween = gameObject.transform.DOMove(m_RatPoints[randNum].transform.position, m_RatSpeed);
        gameObject.transform.LookAt(m_RatPoints[randNum].transform);
        yield return moveTween.WaitForCompletion();
        StartCoroutine(C_RatPathing());
    }
}
