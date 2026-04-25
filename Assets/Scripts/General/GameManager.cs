using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] public PlayerState PState {get; private set;} 
    [Inject] public DataManager Data {get; private set;} 
    [Inject] public Inventory Inventory {get; private set;} 
}
