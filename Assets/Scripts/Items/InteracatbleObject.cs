using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

[RequireComponent(typeof(Outline))]
public abstract class InteractObject : MonoBehaviour
{
    private Outline _outline;
    [SerializeField] private string _tooltipeText;
    private Tween _tween;
    [Inject] private Tooltipe _tooltipe;
    public virtual void Select(bool isSelect)
    {
        _outline.enabled = isSelect;
        if (_tooltipeText!="")
        {
            if (isSelect)
                _tooltipe.Show(_tooltipeText);
            else
                _tooltipe.Hide();
        }
    }
    private void OnEnable()
    {
        _outline ??= GetComponent<Outline>();
        _outline.enabled = false;
    }
    private void OnDestroy()
    {
        _tooltipe.Hide();
    }
    public abstract void Intearct(bool isDowwn);
}

