using Shears.Input;
using Shears.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class Hotbar : MonoBehaviour
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private List<InventorySlot> slots;

        private InventorySlot selectedSlot;
        private IManagedInput scrollInput;
        private bool isEnabled = false;

        public InventorySlot SelectedSlot => selectedSlot;

        public event Action Enabled;
        public event Action Disabled;
        public event Action<InventorySlot> SelectedSlotChanged;

        private void Awake()
        {
            scrollInput = inputProvider.GetInput("Scroll Inventory");
        }

        private void OnEnable()
        {
            SignalShuttle.Register<GameEnabledSignal>(OnGameEnabled);
            SignalShuttle.Register<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Register<InventoryClosedSignal>(OnInventoryClosed);
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<GameEnabledSignal>(OnGameEnabled);
            SignalShuttle.Deregister<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Deregister<InventoryClosedSignal>(OnInventoryClosed);
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            scrollInput.Performed += OnScrollInput;

            if (selectedSlot == null)
                SelectSlot(slots[0]);

            isEnabled = true;
            Enabled?.Invoke();
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            scrollInput.Performed -= OnScrollInput;

            isEnabled = false;
            Disabled?.Invoke();
        }

        private void OnInventoryOpened(InventoryOpenedSignal signal)
        {
            Disable();
        }

        private void OnInventoryClosed(InventoryClosedSignal signal)
        {
            Enable();
        }

        private void OnGameEnabled(GameEnabledSignal signal)
        {
            if (signal.Type == MinigameType.FishCraft)
                Enable();
        }

        private void OnScrollInput(ManagedInputInfo info)
        {
            int value = -Mathf.RoundToInt(scrollInput.ReadValue<Vector2>().y);
            int currentIndex = slots.IndexOf(selectedSlot);
            int targetIndex = currentIndex + value;

            if (targetIndex >= slots.Count)
                targetIndex = 0;
            else if (targetIndex < 0)
                targetIndex = slots.Count - 1;

            SelectSlot(slots[targetIndex]);
        }

        private void SelectSlot(InventorySlot slot)
        {
            if (selectedSlot != null)
                selectedSlot.DataUpdated -= OnSelectedSlotUpdated;

            selectedSlot = slot;

            if (selectedSlot != null)
                selectedSlot.DataUpdated += OnSelectedSlotUpdated;

            SelectedSlotChanged?.Invoke(slot);
        }

        private void OnSelectedSlotUpdated()
        {
            SelectedSlotChanged?.Invoke(selectedSlot);
        }
    }
}
