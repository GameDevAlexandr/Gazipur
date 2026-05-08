using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static EnumData;

public class DialogManager : MonoBehaviour
{
    public DialogType Dialog { get; private set; }
    [field: SerializeField] public CharacterRemarks Remarks { get; private set; }
    [SerializeField] private Text _questionText;
    [SerializeField] private Button[] _ansverButtons;

    [SerializeField] private DialogData[] _dialogs;

    [System.Serializable]
    public class DialogData
    {
        public DialogType dialogType;
        public DialogStructure iteration;
        public bool isOneTime;
        [HideInInspector] public bool isUsed;
    }
    [Inject] GameManager _manager;
    [Inject] GameModeManager _modManager;

    private void Start()
    {
        StartDialog(DialogType.motherStart);
    }

    public bool StartDialog(DialogType dType)
    {        
        var dialog = _dialogs.Where(i => i.dialogType == dType).ToArray()[0];

        if (dialog.isUsed) return false;

        Dialog = dialog.dialogType;
        dialog.isUsed = dialog.isOneTime;
        SetIteration(dialog.iteration);
        _modManager.ChangeMode(GameMode.dialog);
        return true;
    }
    private void SetIteration(DialogStructure iteraton)
    {
        _questionText.text = iteraton.Question;
        for (int i = 0; i < _ansverButtons.Length; i++)
        {
            if (i >= iteraton.Answer.Length)
            {
                _ansverButtons[i].gameObject.SetActive(false);
                continue;
            }

            _ansverButtons[i].gameObject.SetActive(true);
            _ansverButtons[i].GetComponentInChildren<Text>().text = iteraton.Answer[i].answer;
            _ansverButtons[i].onClick.RemoveAllListeners();

            int idx = i;
            if (iteraton.Answer[i].newChain!=null)
            {                
                _ansverButtons[i].onClick.AddListener(() => SetIteration(iteraton.Answer[idx].newChain));
            }
            else
            {
                _ansverButtons[i].onClick.AddListener(() => _modManager.ChangeMode(GameMode.outdors));
            }

            if (iteraton.Answer[i].action)
            {
                _ansverButtons[i].onClick.AddListener(() => iteraton.Answer[idx].action.Action(_manager));
            }
            
        }
    }
}
