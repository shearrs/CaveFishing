using UnityEngine;
using Shears.UI;
using System.Collections.Generic;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class HotbarUI : MonoBehaviour
    {
        [SerializeField] private Hotbar hotbar;
        [SerializeField] private GameObject container;
        [SerializeField] private List<InventorySlotUI> slots;

        private readonly Dictionary<InventorySlot, InventorySlotUI> slotsToUI = new();
        private bool isEnabled = false;

        private void Awake()
        {
            foreach (var slot in slots)
                slotsToUI[slot.Slot] = slot;
        }

        private void OnEnable()
        {
            hotbar.Enabled += OnEnabled;
            hotbar.Disabled += OnDisabled;
            hotbar.SelectedSlotChanged += OnSelectedSlotChanged;
        }

        private void OnDisable()
        {
            hotbar.Enabled -= OnEnabled;
            hotbar.Disabled -= OnDisabled;
            hotbar.SelectedSlotChanged -= OnSelectedSlotChanged;
        }

        private void OnEnabled()
        {
            if (isEnabled)
                return;

            container.SetActive(true);

            isEnabled = true;
        }

        private void OnDisabled()
        {
            if (!isEnabled)
                return;

            container.SetActive(false);

            isEnabled = false;
        }

        private void OnSelectedSlotChanged(InventorySlot slot)
        {
            var slotUI = slotsToUI[slot];

            ManagedUIEventSystem.Focus(slotUI.Element);
        }
    }
}
