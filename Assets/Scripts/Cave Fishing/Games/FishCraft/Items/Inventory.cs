using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> slots = new();

        public IReadOnlyList<InventorySlot> Slots => slots;

        public event Action Opened;
        public event Action Closed;

        public void Open()
        {
            Opened?.Invoke();
        }

        public void Close()
        {
            Closed?.Invoke();
        }

        public void AddItem(Item item, int count = 1)
        {
            InventorySlot firstEmptySlot = null;
            InventorySlot stackableSlot = null;

            foreach (var slot in slots)
            {
                if (slot.Item == null)
                {
                    if (firstEmptySlot == null)
                        firstEmptySlot = slot;

                    continue;
                }
                else if (slot.Item.Data == item.Data && !slot.IsFull())
                {
                    stackableSlot = slot;

                    break;
                }
            }

            if (stackableSlot != null)
                stackableSlot.AddCount(count);
            else if (firstEmptySlot != null)
                firstEmptySlot.SetItem(item, count);
        }
    }
}
