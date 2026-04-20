using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Text _count;

    public void SetAmount(float curren, float max)
    {
        _bar.fillAmount = curren / max;
    }
    public void SetAmountAndValue(float curren, float max)
    {
        SetAmount(curren, max);
        _count.text = ((int)curren).ToString();
    }
    public void SetAmountCurAndMax(float curren, float max)
    {
        SetAmount(curren, max);
        _count.text = (int)curren + "/" + (int)max;
    }
}
