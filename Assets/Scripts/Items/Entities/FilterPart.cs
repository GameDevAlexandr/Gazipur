using UnityEngine;
using static EnumData;

public class FilterPart : ItemObject
{
    [field: SerializeField] public FilterParts Part { get; private set; }
}
