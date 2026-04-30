using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class SharpObject : MonoBehaviour
{
    [SerializeField] private int _damage;
    [Inject] PlayerState _player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            _player.TakeDamage(_damage);
        }
    }
}
