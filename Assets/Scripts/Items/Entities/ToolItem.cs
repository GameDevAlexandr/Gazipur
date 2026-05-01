using UnityEngine;
using static EnumData;

public class ToolItem : ItemObject, IUsebleItem
{
    [field: SerializeField] public ToolsType ToolType;
    public void Use(GameManager manager)
    {
        
    }
}
