using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class ItemInteractor : MonoBehaviour
    {
        [SerializeField] private InventorySlot holdSlot;

        private void OnEnable()
        {
            SignalShuttle.Register<InventorySlotSelectedSignal>(OnInventorySlotSelected);
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<InventorySlotSelectedSignal>(OnInventorySlotSelected);
        }

        private void OnInventorySlotSelected(InventorySlotSelectedSignal signal)
        {
            if (signal.AlternativeSelection)
                AltSelect(signal.Slot);
            else
                Select(signal.Slot);
        }

        private void Select(InventorySlot slot)
        {
            bool bothHaveItems = slot.Item != null && holdSlot.Item != null;

            if (bothHaveItems && slot.Item.Data == holdSlot.Item.Data)
                StackItems(slot);
            else
                SwapItems(slot);
        }

        private void AltSelect(InventorySlot selectedSlot)
        {
            if (holdSlot.Item == null)
            {
                if (selectedSlot.Item == null)
                    return;

                int takeCount = (int)(0.5f * selectedSlot.Count);
                if (takeCount == 0)
                    takeCount = 1;

                holdSlot.SetItem(selectedSlot.Item, takeCount);
                selectedSlot.RemoveCount(takeCount);
            }
            else if (selectedSlot.Item == null)
            {
                selectedSlot.SetItem(holdSlot.Item, 1);
                holdSlot.RemoveCount(1);
            }
            else if (selectedSlot.Item == holdSlot.Item)
            {
                selectedSlot.AddCount(1);
                holdSlot.RemoveCount(1);
            }
            else
                SwapItems(selectedSlot);
        }

        private void StackItems(InventorySlot selectedSlot)
        {
            selectedSlot.AddCount(holdSlot.Count);
            holdSlot.SetItem(null, 0);
        }

        private void SwapItems(InventorySlot selectedSlot)
        {
            var item = selectedSlot.Item;
            int count = selectedSlot.Count;

            selectedSlot.SetItem(holdSlot.Item, holdSlot.Count);
            holdSlot.SetItem(item, count);
        }
    }
}
