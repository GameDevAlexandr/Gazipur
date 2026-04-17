using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour
{
    public Action<Vector2> OnMouseDown;
    public Action<GameObject> OnSelectObject;
    public Action OnChooseObject;  

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                OnSelectObject?.Invoke(hit.collider.gameObject);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            OnChooseObject?.Invoke();
        }
    }
}
