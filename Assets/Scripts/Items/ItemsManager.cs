using UnityEngine;
using Zenject;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private ItemObject _itemPrefab;
    [Inject] private DiContainer _container;
    public void DropItem(ItemData item, int count, Vector2 position)
    {
        var obj = _container.InstantiatePrefabForComponent<ItemObject>(_itemPrefab);
        obj.SetData(item, count);
    }
}
