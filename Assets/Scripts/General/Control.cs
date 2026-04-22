using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour
{
    public static Action<Vector2> OnMouseDownInObject;
    public static Action<InteractObject> OnSelectObject;
    public static Action OnInteractObject;  

    private void Update()
    {
        InteractObject iObject = null;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (iObject = hit.collider.GetComponent<InteractObject>()) { }
            }
        }        
        if (Input.GetKey(KeyCode.E))
        {
            OnInteractObject?.Invoke();
        }
        OnSelectObject?.Invoke(iObject);
    }
}
