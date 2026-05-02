using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public class DangerZone : MonoBehaviour
{
    [Inject] private PlayerState _player;
    [Inject] private Inventory _inventory;
    [SerializeField] private int _damagePerSecond;

    private bool _inZone;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_inventory.HaveTools.Contains(EnumData.ToolsType.mask) &&
            other.GetComponent<PlayerMovement>())
        {
            _inZone = true;
            StartCoroutine(Tic());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!_inventory.HaveTools.Contains(EnumData.ToolsType.mask) &&
            other.GetComponent<PlayerMovement>())
        {
            _inZone = false;
        }
    }
    private IEnumerator Tic()
    {
        while (_inZone)
        {
            yield return new WaitForSeconds(1f);
            _player.TakeDamage(_damagePerSecond);
        }
        
    }
}
