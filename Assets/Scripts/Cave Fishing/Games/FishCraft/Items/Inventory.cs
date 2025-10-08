using Shears.Input;
using Shears.Logging;
using Shears.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class Inventory : SHMonoBehaviourLogger
    {
        [SerializeField] private List<InventorySlot> slots = new();

        private bool isOpen = false;

        public IReadOnlyList<InventorySlot> Slots => slots;

        public event Action Opened;
        public event Action Closed;

        public void Open()
        {
            if (isOpen)
                return;

            isOpen = true;
            CursorManager.SetCursorVisibility(true);
            CursorManager.SetCursorLockMode(CursorLockMode.None);

            Opened?.Invoke();
            SignalShuttle.Emit(new InventoryOpenedSignal());
        }

        public void Close()
        {
            if (!isOpen)
                return;

            isOpen = false;
            CursorManager.SetCursorVisibility(false);
            CursorManager.SetCursorLockMode(CursorLockMode.Locked);

            Closed?.Invoke();
            SignalShuttle.Emit(new InventoryClosedSignal());
        }

        public void Toggle()
        {
            if (isOpen)
                Close();
            else
                Open();
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
            {
                stackableSlot.AddCount(count);
                Log($"Adding {item.Name} to stack slot {stackableSlot}.", SHLogLevels.Verbose, context: stackableSlot);
            }
            else if (firstEmptySlot != null)
            {
                firstEmptySlot.SetItem(item, count);
                Log($"Adding {item.Name} to empty slot {firstEmptySlot}.", SHLogLevels.Verbose, context: firstEmptySlot);
            }
            else
                Log($"Could not find available slot for {item.Name}!", SHLogLevels.Verbose);
        }
    }
}
