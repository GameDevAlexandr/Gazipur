using UnityEngine;
using Zenject;

public class LocationChanger : MonoBehaviour
{
    [Inject] private Sounds _sounds;

    private void OnTriggerEnter(Collider other)
    {
        _sounds.ChangeBackground(other.GetComponent<AudioSource>());
    }
}