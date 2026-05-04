using UnityEngine;
using UnityEngine.UI;
using static EnumData;

public class FilterBlueprint : MonoBehaviour
{
     [SerializeField] private PartData[] _parts;
    [System.Serializable]
    public class PartData
    {
        public FilterParts part;
        public Image partImage;
        public Toggle checkBox;
    }
    public void AddPart(FilterParts part)
    {
        var prt = System.Array.Find(_parts, i => i.part == part);
        prt.partImage.enabled = true;
        prt.checkBox.isOn = true;
        CheckComplete();
    }
    private void CheckComplete()
    {
        foreach (var p in _parts)
        {
            if (!p.checkBox.isOn)
                return;
        }
    }
}
