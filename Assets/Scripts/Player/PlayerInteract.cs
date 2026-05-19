using UnityEngine;
using Zenject;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _interactableDistance;
    private InteractObject _selectObject;
    private bool _isSelect;
    [Inject] Control _control;
    private void Start()
    {
        _control.OnSelectObject += SelectObject;
        _control.OnHoldInteract += InteractObject;
    }
    private void SelectObject(InteractObject obj)
    {
        if (_selectObject != obj)
            _isSelect = false;

        if (_selectObject != null && _isSelect) return;

        if (_selectObject != null)
            _selectObject.Select(false);

        _selectObject = obj;

        if (obj && Mathf.Abs(Vector3.Distance(transform.position, obj.transform.position)) <= _interactableDistance)
        {
            _isSelect = true;
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
    private void OnDestroy()
    {
        _control.OnSelectObject -= SelectObject;
        _control.OnHoldInteract -= InteractObject;
    }
}
