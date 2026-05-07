using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static EnumData;

public class QuestManager : MonoBehaviour
{
    public bool BluePrintIsFound { get; private set; }

    [SerializeField] private GameObject _filterPanel;
    [SerializeField] private GameObject _blueprintPanel;
    [SerializeField] private Toggle _medecineCheckBox;
    [Inject] Inventory _inventory;
    [Inject] DialogManager _dialog;
    [Inject] DataManager _data;
    [Inject] GameModeManager _mode;
    private bool _isStartFind;
    private void Start()
    {
        _blueprintPanel.SetActive(false);
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

}
