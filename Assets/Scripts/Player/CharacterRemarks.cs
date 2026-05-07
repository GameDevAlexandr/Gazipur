using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static EnumData;
using DG.Tweening;
using NaughtyAttributes;

public class CharacterRemarks : MonoBehaviour
{
    [SerializeField] private CanvasGroup _cGroup;
    [SerializeField] private Text _remarkText;
    [SerializeField] private RemarkData[] _remarks;

    private Tween _tween;
    [System.Serializable]
    public class RemarkData
    {
        [TextArea] public string remark;
        public bool isMultiRemark;
        [ShowIf("isMultiRemark"), AllowNesting, TextArea] public string remarkAnyTime;
        public int chance;
        public float showTime = 3;
        public RemarksType type;
        public bool isOneTime;
        [HideInInspector] public bool hasBeen;
    }
    public bool StartRemark(RemarksType remark)
    {
        var rem = System.Array.Find(_remarks,i => i.type == remark);

        if (rem == null) return false;

        int rnd = Random.Range(0, 100);

        if (rem.chance < rnd)
            return false;

        if (rem.isOneTime)
            rem.chance = 0;        

        if (!rem.isMultiRemark)
        {
            _remarkText.text = rem.remark;
        }
        else
        {
            _remarkText.text = rem.hasBeen?rem.remarkAnyTime:rem.remark;
        }
        rem.hasBeen = true;
        _tween?.Kill();
        _tween = _cGroup.DOFade(1, 0.5f).OnComplete(() =>
         {
             _tween = _cGroup.DOFade(0, 0.5f).SetDelay(rem.showTime);
         });
        return true;
    }
}
