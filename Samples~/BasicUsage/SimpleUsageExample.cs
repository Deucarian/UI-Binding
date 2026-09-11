using UnityEngine;

namespace Deucarian.UIBinding.Samples.SimpleUsage
{
    public sealed class SimpleUsageExample : MonoBehaviour
    {
        [SerializeField] private UIListHost inventory;
        public void ShowItems(InventoryItem[] items) => inventory.SetItems(items);
        public void RemoveItem(string id) => inventory.Remove(id);
        public sealed class InventoryItem { public string Id; public string Name; }
    }
}
