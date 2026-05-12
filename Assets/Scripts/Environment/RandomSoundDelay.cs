using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class RandomSoundDelay : MonoBehaviour
{
    [SerializeField] private float minDelay = 3;
    [SerializeField] private float maxDelay = 100;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ScreamRoutine());
    }

    IEnumerator ScreamRoutine()
    {
        float waitTime = Random.Range(minDelay, maxDelay);

        yield return new WaitForSeconds(waitTime);

        if (audioSource.clip != null)
        {
            audioSource.Play();
        }

        StartCoroutine(ScreamRoutine());
    }
}