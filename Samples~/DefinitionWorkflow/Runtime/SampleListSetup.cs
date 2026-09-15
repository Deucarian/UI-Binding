using UnityEngine;
namespace Deucarian.UIBinding.Samples.DefinitionWorkflow
{
    [DefaultExecutionOrder(-2000)]
    public sealed class SampleListSetup : MonoBehaviour
    {
        [SerializeField] private UIListHost host;
        private void Awake() => host.Configure<int, int>(item => item);
    }
}
