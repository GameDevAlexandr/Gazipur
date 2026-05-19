using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class RandomSoundDelay : MonoBehaviour
{
    [SerializeField] private float minDelay = 3;
    [SerializeField] private float maxDelay = 100;
    [SerializeField] private AudioClip[] audioClips;

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
            audioSource.PlayOneShot(RandomSound());
        }

        StartCoroutine(ScreamRoutine());
    }

    private AudioClip RandomSound()
    {
        int i = Random.Range(0,audioClips.Length);
        return audioClips[i];
    }
}