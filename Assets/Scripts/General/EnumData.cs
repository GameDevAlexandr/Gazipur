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
        glowes, 
        key
    }
    public enum DialogType
    {
        start,
        startTrader,
        motherStart,
        traderAfterBuy,
        motherDisease,
        motherMedecine
    }
    public enum RemarksType
    {        
        noWrench,
        noGrowes,
        noHacksaw,
        inventoryFool,
        noCutters,
        noKey,
        noMask,
        maskReady,
        tooEarly,
        relaxMom,
        filterNeed,
        fewParts,
        foolParts
    }
    public enum FilterParts
    {
        shortTube,
        longTube,
        bascet,
        teleTube,
        support,
        pump,
        filter,
        solarBat,
    }
}
