using UnityEngine;

public class BlueprintData : MonoBehaviour
{
    [field: SerializeField] int Index;
    [field: SerializeField] ItemStruct[] Components;
    [field: SerializeField] ItemStruct Result;
    public struct ItemStruct
    {
        public ItemData item;
        public int count;
    }
}
