using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static EnumData;
using DG.Tweening;

public class CharacterRemarks : MonoBehaviour
{
    [SerializeField] private CanvasGroup _cGroup;
    [SerializeField] private Text _remarkText;
    [SerializeField] private RemarkData[] _remarks;

    private Tween _tween;
    [System.Serializable]
    public class RemarkData
    {
        public string remark;
        public int chance;
        public float showTime = 3;
        public RemarksType type;
    }
    public void StartRemark(RemarksType remark)
    {
        var rem = _remarks.Where(i => i.type == remark).ToArray()[0];
        _remarkText.text = rem.remark;
        _tween?.Kill();
        _tween = _cGroup.DOFade(1, 0.5f).OnComplete(() =>
         {
             _tween = _cGroup.DOFade(0, 0.5f).SetDelay(rem.showTime);
         });
    }
}
