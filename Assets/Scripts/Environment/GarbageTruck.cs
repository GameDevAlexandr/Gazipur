using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SphereCollider))]

public class GarbageTruck : MonoBehaviour {

    private AudioSource audioSource;

    private static bool _isGarbagePlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isGarbagePlayed && other.name.Equals("PLAYER"))
        {
            audioSource.Play();
            Debug.Log("Garbage is played");
            Debug.Log(audioSource.isPlaying);
            _isGarbagePlayed = true;
        }
    }
}