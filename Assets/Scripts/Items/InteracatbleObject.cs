using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class InteractObject : MonoBehaviour
{
    private Outline _outline; 
    public virtual void Select(bool isSelect)
    {
        _outline ??= GetComponent<Outline>();
        _outline.enabled = isSelect;
    }
    private void OnEnable()
    {
        Select(false);
    }
    public abstract void Intearct(bool isDowwn);
}

