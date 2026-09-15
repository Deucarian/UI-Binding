using UnityEngine;
using UnityEngine.UI;
namespace Deucarian.UIBinding.Samples.DefinitionWorkflow
{
    public sealed class SampleRowView : MonoBehaviour, ISettableItem<int>
    {
        [SerializeField] private Text label;
        public void SetData(int data) { label.text = "Typed row " + data; }
    }
}
