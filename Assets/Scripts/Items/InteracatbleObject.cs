using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(Outline))]
public abstract class InteractObject : MonoBehaviour
{
    private Outline _outline;
    [SerializeField] private Text _howUseToolTipe;
    [SerializeField] CanvasGroup _cGroup;

    private Tween _tween;
    public virtual void Select(bool isSelect)
    {
        _outline ??= GetComponent<Outline>();
        _outline.enabled = isSelect;
        if (_cGroup)
        {
            _cGroup.transform.LookAt(Camera.main.transform);
            _tween?.Kill();
            _tween = _cGroup.DOFade(isSelect?1:0, 0.3f);
        }
    }
    private void OnEnable()
    {
        Select(false);
    }
    public abstract void Intearct(bool isDowwn);
}

