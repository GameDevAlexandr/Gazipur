using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] public PlayerState PState {get; private set;} 
    [Inject] public DataManager Data {get; private set;} 
    [Inject] public Inventory Inventory {get; private set;} 
    [Inject] public GameModeManager ModeManager { get; private set; }
    [Inject] public MarketManager Market { get; private set; }
    [Inject] public DialogManager Dialog { get; private set; }
    [Inject] public QuestManager Quest { get; private set; }
}
