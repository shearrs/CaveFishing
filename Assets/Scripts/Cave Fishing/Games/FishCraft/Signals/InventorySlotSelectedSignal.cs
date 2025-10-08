using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public readonly struct InventorySlotSelectedSignal : ISignal
    {
        private readonly InventorySlot slot;
        private readonly bool alternativeSelection;

        public readonly InventorySlot Slot => slot;
        public readonly bool AlternativeSelection => alternativeSelection;

        public InventorySlotSelectedSignal(InventorySlot slot, bool alternativeSelection)
        {
            this.slot = slot;
            this.alternativeSelection = alternativeSelection;
        }
    }
}
