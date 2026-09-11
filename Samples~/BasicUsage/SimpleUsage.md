# Simple usage

Set the list container and item prefab on UIListHost. The prefab's root implements ISettableItem<InventoryItem>; its SetData method writes the name to your text component. Configure the host once with inventory.Configure<InventoryItem>(item => item.Id). An optional existing IUIBindingItemVisual<string, InventoryItem> supplies selection/hover visuals. There is no reflection or automatic member mapping. SetItems uses the existing keyed container, Remove releases the matching generated view, and host destruction releases all generated items. Other children and the source prefab remain owned by the caller.

Import the **Simple Usage** sample from Unity Package Manager. Its caller script is:

Definition fields now use named, domain-specific keys. Select an existing definition from the Inspector dropdown or pass the same named key in code. Declare each project key once in a marked key set; ordinary caller methods do not accept raw IDs. Generated keys for asset-authored definitions require no asset reference in the caller. Owner-issued selection and row handles represent runtime instances.

```csharp
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
```
