# Simple usage

Set the list container and item prefab on UIListHost. The prefab's root implements ISettableItem<InventoryItem>; its SetData method writes the name to your text component. Configure the host once with inventory.Configure<InventoryItem>(item => item.Id). An optional existing IUIBindingItemVisual<string, InventoryItem> supplies selection/hover visuals. There is no reflection or automatic member mapping. SetItems uses the existing keyed container, Remove releases the matching generated view, and host destruction releases all generated items. Other children and the source prefab remain owned by the caller.

Import the **Simple Usage** sample from Unity Package Manager. Its caller script is:

```csharp
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
```
