using System;
using NUnit.Framework;
using UnityEngine;

namespace Deucarian.UIBinding.Tests
{
    public sealed class UIListHostTests
    {
        [Test]
        public void TypedConfigurationOwnsViewsAndRejectsWrongDataType()
        {
            var root = new GameObject("list", typeof(RectTransform));
            var prefab = new GameObject("item", typeof(RectTransform), typeof(SimpleListTestItem));
            try
            {
                var host = root.AddComponent<UIListHost>();
                host.Configure((RectTransform)root.transform, prefab, (string item) => item);
                host.SetItems(new[] { "a", "b" });
                Assert.That(host.Count, Is.EqualTo(2));
                Assert.Throws<InvalidOperationException>(() => host.SetItems(new[] { 1 }));
                Assert.That(host.Remove("a"), Is.True);
                host.Clear();
                Assert.That(host.Count, Is.Zero);
                Assert.That(root.transform.childCount, Is.Zero);
            }
            finally { UnityEngine.Object.DestroyImmediate(root); UnityEngine.Object.DestroyImmediate(prefab); }
        }
    }
    public sealed class SimpleListTestItem : MonoBehaviour, ISettableItem<string>
    { public void SetData(string data) { name = data; } }
}
