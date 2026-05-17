using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using static EnumData;

public class GameModeManager : MonoBehaviour
{
    public UnityEvent<bool> OnOutdors = new UnityEvent<bool>();
    public UnityEvent<bool> OnTrade = new UnityEvent<bool>();
    public UnityEvent<bool> OnInventory = new UnityEvent<bool>();
    public UnityEvent<bool> OnCraft = new UnityEvent<bool>();
    public UnityEvent<bool> OnStorage = new UnityEvent<bool>();
    public UnityEvent<bool> OnDialog = new UnityEvent<bool>();
    public UnityEvent<bool> OnMenu = new UnityEvent<bool>();
    public UnityEvent<bool> OnDie = new UnityEvent<bool>();
    public UnityEvent<bool> OnOtherPanels = new UnityEvent<bool>();
    public System.Action<GameMode> onChangeMode;

    private Dictionary<GameMode, UnityEvent<bool>> _mods;
    [Inject] DataManager _data;
    [Inject] DialogManager _dialog;

    [Inject]
    private void InitMods()
    {
        Time.timeScale = 1;
        _mods = new Dictionary<GameMode, UnityEvent<bool>> 
        { 
            [GameMode.outdors] = OnOutdors, 
            [GameMode.trade] = OnTrade,
            [GameMode.inventory] = OnInventory,
            [GameMode.craft] = OnCraft,
            [GameMode.storage] = OnStorage,
            [GameMode.dialog] = OnDialog,
            [GameMode.menu] = OnMenu,
            [GameMode.die] = OnDie,
            [GameMode.otherPanels] = OnOtherPanels
        };
        Control.OnEsc += () =>
        {
            if (_data.gameMode == GameMode.outdors)
            {
                ChangeMode(GameMode.menu);
                Time.timeScale = 0;
                return;
            }            
            if (_data.gameMode != GameMode.die) OutDors();            
        };
    }
    public void ChangeMode(GameMode mode)
    {
        if (_data.gameMode == GameMode.trade && mode == GameMode.outdors)
            _dialog.Remarks.StartRemark(RemarksType.rohulHelp);

        _mods[_data.gameMode]?.Invoke(false);
        _data.gameMode = mode;
        onChangeMode?.Invoke(mode);
        _mods[mode]?.Invoke(true);
    }
    public void OutDors()
    {
        Time.timeScale = 1;
        ChangeMode(GameMode.outdors);
    }

}
