using Shears.Input;
using Shears.Signals;
using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private ItemData testItem;
        [SerializeField] private ItemData stringItem;

        private IManagedInput toggleInput;
        private bool isEnabled = false;

        private void Awake()
        {
            toggleInput = inputProvider.GetInput("Toggle Inventory");
        }

        private void OnDisable()
        {


            Disable();
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            SignalShuttle.Register<CraftingTableUsedSignal>(OnCraftingTableOpened);
            SignalShuttle.Register<CraftingTableClosedSignal>(OnCraftingTableClosed);

            toggleInput.Performed += OnToggleInput;
            inventory.AddItem(new(testItem), 12);
            inventory.AddItem(new(stringItem), 2);

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            SignalShuttle.Deregister<CraftingTableUsedSignal>(OnCraftingTableOpened);
            SignalShuttle.Deregister<CraftingTableClosedSignal>(OnCraftingTableClosed);

            toggleInput.Performed -= OnToggleInput;

            isEnabled = false;
        }

        private void OnCraftingTableOpened(CraftingTableUsedSignal signal)
        {
            toggleInput.Performed -= OnToggleInput;
        }

        private void OnCraftingTableClosed(CraftingTableClosedSignal signal)
        {
            toggleInput.Performed += OnToggleInput;
        }

        private void OnToggleInput(ManagedInputInfo info)
        {
            inventory.Toggle();
        }
    }
}
