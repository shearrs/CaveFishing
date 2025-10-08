using CaveFishing.Fishing;
using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Hotbar hotbar;
        [SerializeField] private FishingRod fishingRod;

        public InventorySlot HeldSlot => hotbar.SelectedSlot;
        public Item HeldItem => hotbar.SelectedSlot == null ? null : hotbar.SelectedSlot.Item;

        public event Action Enabled { add => hotbar.Enabled += value; remove => hotbar.Enabled -= value; }
        public event Action Disabled { add => hotbar.Disabled += value; remove => hotbar.Disabled -= value; }
        public event Action<Item> HeldItemChanged;

        private void OnEnable()
        {
            hotbar.SelectedSlotChanged += OnSelectedSlotChanged;
        }

        private void OnDisable()
        {
            hotbar.SelectedSlotChanged -= OnSelectedSlotChanged;
        }

        private void OnSelectedSlotChanged(InventorySlot slot)
        {
            if (slot.Item != null && slot.Item.Data.Name == "Fishing Rod")
                fishingRod.Enable();
            else
                fishingRod.Disable();

            HeldItemChanged?.Invoke(slot.Item);
        }
    }
}
