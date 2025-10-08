using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public readonly struct InventoryOpenedSignal : ISignal
    {
        private readonly Inventory inventory;

        public Inventory Inventory => inventory;

        public InventoryOpenedSignal(Inventory inventory)
        {
            this.inventory = inventory;
        }
    }
}
