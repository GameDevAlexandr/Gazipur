using UnityEngine;
using UnityEngine.Events;
using Zenject;
using static EnumData;

public class GameModeManager : MonoBehaviour
{
    public UnityEvent homeModeEvent = new UnityEvent();
    public UnityEvent marketMode = new UnityEvent();
    public System.Action<GameMode> onChangeMode;
    [Inject] DataManager _data;
    public void ChangeMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.home:
                {
                    homeModeEvent?.Invoke();
                    break;
                }

            case GameMode.sell:
                {
                    marketMode?.Invoke();
                    break;
                }
        }
        _data.gameMode = mode;
        onChangeMode?.Invoke(mode);
    }
}
