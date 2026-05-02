using UnityEngine;

public static class EnumData 
{
    public enum GameMode
    {
        outdors,
        trade,
        inventory,
        dialog,
        craft,
        storage,
        menu, 
        die
    }
    public enum ToolsType
    {
        bag,
        wrench,
        hacksaw,
        mask,
        crowbar,
        glowes
    }
    public enum DialogType
    {
        start,
        startTrader,
        matherStart
    }
}
