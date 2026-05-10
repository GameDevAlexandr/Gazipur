using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static EnumData;

public class QuestManager : MonoBehaviour
{
    public bool BluePrintIsFound { get; private set; }
    public bool MedecineIsPurchased { get; private set; }

    [SerializeField] private GameObject _filterPanel;
    [SerializeField] private GameObject _blueprintPanel;
    [SerializeField] private GameObject _filterPlace;
    [SerializeField] private GameObject _filterObject;
    [SerializeField] private Toggle _medecineCheckBox;
    [Inject] Inventory _inventory;
    [Inject] DialogManager _dialog;
    [Inject] DataManager _data;
    [Inject] GameModeManager _mode;
    private bool _isStartFind;
    private void Start()
    {
        _filterObject.SetActive(false);
        _blueprintPanel.SetActive(false);
        _filterPlace.SetActive(false);
        _medecineCheckBox.gameObject.SetActive(false);
        _mode.onChangeMode += m =>
        {
            if (m == GameMode.trade)
                _isStartFind = true;
        };
        _inventory.onTakeItem += i =>
         {
             if (_isStartFind && !BluePrintIsFound && _data.gameMode == GameMode.outdors)
             {
                 BluePrintIsFound = true;
                 _filterPanel.SetActive(true);
                 _blueprintPanel.SetActive(true);
                 _dialog.Remarks.StartRemark(RemarksType.foundBlueprint);
                 _filterPlace.SetActive(true);
                 _mode.ChangeMode(GameMode.otherPanels);
             }
         };
    }
   
    public void CloseFilterPanel()
    {
        _dialog.Remarks.StartRemark(RemarksType.closeBlueprint);
        _filterPanel.SetActive(false); 
        _mode.ChangeMode(GameMode.outdors);
    }
    public void HealMother(bool isHeal)
    {
        if (!isHeal)
            _medecineCheckBox.gameObject.SetActive(true);
        else
            _medecineCheckBox.isOn = true;
    }
    public void CompleteFilter()
    {
        _blueprintPanel.SetActive(false);
        _filterPlace.SetActive(true);
    }

}
