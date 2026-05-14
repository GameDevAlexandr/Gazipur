using UnityEngine;
using Zenject;

public class ToolTipeCollider : MonoBehaviour
{
    [SerializeField] private string _message;
    [Inject] Tooltipe _tooltipe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _tooltipe.Show(_message);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<PlayerMovement>())
        {
            _tooltipe.Hide();
        }
    }
}