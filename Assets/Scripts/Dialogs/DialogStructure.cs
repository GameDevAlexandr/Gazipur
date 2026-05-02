using UnityEngine;

[CreateAssetMenu(fileName = "DialogIteration", menuName = "MyGame/Dialog", order =0)]
public class DialogStructure : ScriptableObject
{
    [field: SerializeField, TextArea] public string Question { get; private set; }
    [field: SerializeField] public AnswerData[] Answer { get; private set; }
    [System.Serializable]
    public struct AnswerData
    {
        [TextArea] public string answer;
        public DialogStructure newChain;
        public DialogAction action;
    }
}
