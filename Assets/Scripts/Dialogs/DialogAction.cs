using UnityEngine;
using Zenject;

public abstract class DialogAction : MonoBehaviour
{    
    public abstract void Action(GameManager manager);
}
