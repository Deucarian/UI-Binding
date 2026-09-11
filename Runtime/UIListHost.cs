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

        public void Configure<T, TKey>(Func<T, TKey> keySelector, IUIBindingItemVisual<TKey, T> visual = null) =>
            Configure(container, itemPrefab, keySelector, visual);
        public void Configure<T, TKey>(RectTransform parent, GameObject prefab, Func<T, TKey> keySelector,
            IUIBindingItemVisual<TKey, T> visual = null)
        {
            if (destroyed) throw new ObjectDisposedException(nameof(UIListHost));
            if (binding != null) throw new InvalidOperationException("The list is already configured.");
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (parent == null || prefab == null) throw new InvalidOperationException("UIListHost '" + name + "' needs a container and item prefab. Assign both before configuring the list.");
            binding = new ListBinding<T, TKey>(new UIBindingContainer<T, TKey>(parent, prefab, keySelector, visual), keySelector);
        }
        public IReadOnlyList<UIListItemHandle<T>> SetItems<T>(IEnumerable<T> items) => Typed<T>().SetItems(items);
        public bool Remove<T>(UIListItemHandle<T> handle) => Typed<T>().Remove(handle);
        public void Clear() => Binding.Clear();
        private IListBinding Binding
        {
            get
            {
                if (destroyed) throw new ObjectDisposedException(nameof(UIListHost));
                return binding ?? throw new InvalidOperationException("UIListHost '" + name + "' is not configured. Configure its item prefab and typed key selector during startup.");
            }
        }
        private ITypedListBinding<T> Typed<T>() => Binding as ITypedListBinding<T> ??
            throw new InvalidOperationException("UIListHost '" + name + "' is configured for a different item type. Supply the same item type used in Configure.");
        private void OnDestroy() { destroyed = true; binding?.Clear(); binding = null; }

        private interface IListBinding { int Count { get; } void Clear(); }
        private interface ITypedListBinding<T> : IListBinding
        {
            IReadOnlyList<UIListItemHandle<T>> SetItems(IEnumerable<T> items);
            bool Remove(UIListItemHandle<T> handle);
        }
        private sealed class ListBinding<T, TKey> : ITypedListBinding<T>
        {
            private readonly UIBindingContainer<T, TKey> container;
            private readonly Func<T, TKey> keySelector;
            public ListBinding(UIBindingContainer<T, TKey> container, Func<T, TKey> keySelector)
            { this.container = container; this.keySelector = keySelector; }
            public int Count => container.Count;
            public IReadOnlyList<UIListItemHandle<T>> SetItems(IEnumerable<T> items)
            {
                if (items == null) throw new ArgumentNullException(nameof(items));
                var snapshot = new List<T>(items);
                container.SetItems(snapshot);
                var handles = new List<UIListItemHandle<T>>(snapshot.Count);
                foreach (T item in snapshot)
                {
                    TKey key = keySelector(item);
                    if (container.TryGetItem(key, out var view)) handles.Add(new UIListItemHandle<T>(this, key, view));
                }
                return handles.AsReadOnly();
            }
            public bool Remove(UIListItemHandle<T> handle)
            {
                if (handle == null) throw new ArgumentNullException(nameof(handle), "Use a row handle returned by this list's SetItems call.");
                if (!ReferenceEquals(handle.Owner, this)) throw new InvalidOperationException("This row handle belongs to another UIListHost. Remove it through the list that issued it.");
                var key = (TKey)handle.Key;
                return container.TryGetItem(key, out var current) && ReferenceEquals(handle.View, current) && container.Remove(key);
            }
            public void Clear() => container.Clear();
        }
    }
}
