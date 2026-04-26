using UnityEngine;
using Zenject;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _interactableDistance;
    private InteractObject _selectObject;
    private void Start()
    {
        Control.OnSelectObject += SelectObject;
        Control.OnHoldInteract += InteractObject;
    }
    private void SelectObject(InteractObject obj)
    {
        if (_selectObject != null && _selectObject == obj) return;

        if (_selectObject != null)
            _selectObject.Select(false);

        _selectObject = obj;

        if (obj && Mathf.Abs(Vector3.Distance(transform.position, obj.transform.position)) <= _interactableDistance)
        {
           

            if (_selectObject != null)
                _selectObject.Select(true);
        }
    }
    private void InteractObject(bool isDown)
    {
        if (_selectObject != null)
        {
            _selectObject.Intearct(isDown);
        }
    }
}
