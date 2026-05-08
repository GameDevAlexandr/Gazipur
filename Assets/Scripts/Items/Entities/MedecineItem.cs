using UnityEngine;

public class MedecineItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _count;
    public bool Use(GameManager manager)
    {
        //if(manager.Data.gameMode != EnumData.GameMode.dialog && manager.Dialog.Dialog != EnumData.DialogType.motherMedecine)
        //{
        //    return false;
        //}
        //manager.PState.Heal(_count);
        //return true;
        return false;
    }
}
