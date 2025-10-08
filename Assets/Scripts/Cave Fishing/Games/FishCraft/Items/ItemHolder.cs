using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Hotbar hotbar;

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
            HeldItemChanged?.Invoke(slot.Item);
        }
    }
}
