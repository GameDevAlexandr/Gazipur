using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static EnumData;
using DG.Tweening;
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
    [Inject] Sounds _sounds;
    private AudioSource _speaker => _sounds.PlayerSource;

    private bool _isDialog;
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
    private void SetIteration(DialogStructure iteraton, float delay)
    {
        Invoke("SetIteration", delay);
    }
    private void SetIteration(DialogStructure iteraton)
    {
        if (!_isDialog) return;        

        if (iteraton.QuestionVoice)
        {
            if (_speaker.isPlaying)
            {
                DOTween.To(() => _speaker.time, x => _speaker.time = x, _speaker.clip.length, _speaker.clip.length)
            .OnComplete(() =>
            {
                _speaker.clip = iteraton.QuestionVoice;
                _speaker.Play();
            });
            }
            else
            {
                _speaker.clip = iteraton.QuestionVoice;
                _speaker.Play();
            }
        }

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
            if (iteraton.Answer[i].newChain != null)
            {
                _ansverButtons[i].onClick.AddListener(() =>
                {                    
                    if (iteraton.Answer[idx].answerVoice)
                    {
                        _speaker.Stop();
                        _speaker.clip = iteraton.Answer[idx].answerVoice;
                        _speaker.Play();
                    }
                    SetIteration(iteraton.Answer[idx].newChain);
                });
            
            }
            else
            {
                _ansverButtons[i].onClick.AddListener(() =>
                {
                    _modManager.ChangeMode(GameMode.outdors);
                    if (iteraton.Answer[idx].answerVoice)
                    {
                        _speaker.Stop();
                        _speaker.clip = iteraton.Answer[idx].answerVoice;
                        _speaker.Play();
                    }
                });
            }

            if (iteraton.Answer[i].action)
            {
                _ansverButtons[i].onClick.AddListener(() => iteraton.Answer[idx].action.Action(_manager));
            }
        }
    }
    public void StopDialog(bool isStop)
    {
        
    }
}
