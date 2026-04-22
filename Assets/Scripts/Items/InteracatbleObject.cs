using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class InteractObject : MonoBehaviour
{
    private Outline _outline; 
    private void Start()
    {
        _outline.GetComponent<Outline>();
    }
    public void Select(bool isSelect) => _outline.enabled = isSelect;
    public abstract void Intearct();
}

