using System;
using UnityEngine;

namespace Deucarian.UIBinding.Samples.DefinitionWorkflow
{
    /// <summary>Small caller example. The configured scene hosts own services and resource lifetimes.</summary>
    public sealed class UIBindingWorkflow : MonoBehaviour
    {
        [SerializeField] private UIListHost host;
        private System.Collections.Generic.IReadOnlyList<UIListItemHandle<int>> rows;
        private string status = "Ready. Choose an action below.";
        public string Status => status;
        public void Show() { rows = host.SetItems(new[] { 1, 2, 3 }); status = "Bound rows: " + host.Count; }
        public void Remove() { if (rows != null && rows.Count > 0) host.Remove(rows[0]); status = "Bound rows: " + host.Count; }
        public void Clear() { host.Clear(); status = "List cleared."; }
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(24, 24, Math.Min(540, Screen.width - 48), Screen.height - 48), GUI.skin.box);
            GUILayout.Label("UI-Binding — definition workflow");
            GUILayout.Label("Configure a typed row prefab once. The list owns generated views; callers reuse returned row handles to remove items.");
            GUILayout.Space(12);
            if (GUILayout.Button("Show three rows", GUILayout.Height(32))) { try { Show(); } catch (Exception error) { status = error.Message; } }
            if (GUILayout.Button("Remove first row", GUILayout.Height(32))) { try { Remove(); } catch (Exception error) { status = error.Message; } }
            if (GUILayout.Button("Clear", GUILayout.Height(32))) { try { Clear(); } catch (Exception error) { status = error.Message; } }
            GUILayout.Space(12);
            GUILayout.Label(status);
            GUILayout.EndArea();
        }
    }
}
