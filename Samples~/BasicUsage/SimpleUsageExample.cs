using UnityEngine;

namespace Deucarian.UIBinding.Samples.SimpleUsage
{
    public sealed class SimpleUsageExample : MonoBehaviour
    {
        [SerializeField] private UIListHost inventory;
        public System.Collections.Generic.IReadOnlyList<UIListItemHandle<InventoryItem>> ShowItems(InventoryItem[] items) => inventory.SetItems(items);
        public void RemoveItem(UIListItemHandle<InventoryItem> item) => inventory.Remove(item);
        public sealed class InventoryItem { public string Id; public string Name; }
    }
}
