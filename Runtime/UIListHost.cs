using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deucarian.UIBinding
{
    /// <summary>Owns generated list items. Register the typed prefab binding and key selector once.</summary>
    [DisallowMultipleComponent]
    public sealed class UIListHost : MonoBehaviour
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private GameObject itemPrefab;
        private IListBinding binding;
        private bool destroyed;
        public int Count => binding?.Count ?? 0;

        public void Configure<T>(Func<T, string> keySelector, IUIBindingItemVisual<string, T> visual = null) =>
            Configure(container, itemPrefab, keySelector, visual);
        public void Configure<T>(RectTransform parent, GameObject prefab, Func<T, string> keySelector,
            IUIBindingItemVisual<string, T> visual = null)
        {
            if (destroyed) throw new ObjectDisposedException(nameof(UIListHost));
            if (binding != null) throw new InvalidOperationException("The list is already configured.");
            binding = new ListBinding<T>(new UIBindingContainer<T, string>(parent, prefab, keySelector, visual));
        }
        public void SetItems<T>(IEnumerable<T> items)
        {
            if (destroyed) throw new ObjectDisposedException(nameof(UIListHost));
            if (!(binding is ListBinding<T> typed))
                throw new InvalidOperationException("Configure this list for the supplied item type first.");
            typed.Container.SetItems(items);
        }
        public bool Remove(string id) => Binding.Remove(id);
        public void Clear() => Binding.Clear();
        private IListBinding Binding => binding ?? throw new InvalidOperationException("Configure this list first.");
        private void OnDestroy() { destroyed = true; binding?.Clear(); binding = null; }

        private interface IListBinding { int Count { get; } bool Remove(string id); void Clear(); }
        private sealed class ListBinding<T> : IListBinding
        {
            public ListBinding(UIBindingContainer<T, string> container) { Container = container; }
            public UIBindingContainer<T, string> Container { get; }
            public int Count => Container.Count;
            public bool Remove(string id) => Container.Remove(id);
            public void Clear() => Container.Clear();
        }
    }
}
