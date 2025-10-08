using Shears.Input;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class CraftingTableUI : MonoBehaviour
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private Inventory inventory;

        private IManagedInput exitInput;

        private void Awake()
        {
            exitInput = inputProvider.GetInput("Toggle Inventory");
        }

        private void OnEnable()
        {
            SignalShuttle.Register<CraftingTableUsedSignal>(OnCraftingTableUsed);
            exitInput.Performed += OnExitInput;
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<CraftingTableUsedSignal>(OnCraftingTableUsed);
            exitInput.Performed -= OnExitInput;
        }

        private void OnCraftingTableUsed(CraftingTableUsedSignal signal)
        {
            inventory.Open();
        }

        private void OnExitInput(ManagedInputInfo info)
        {
            if (inventory.IsOpen)
            {
                inventory.Close();
                SignalShuttle.Emit(new CraftingTableClosedSignal());
            }
        }
    }
}
