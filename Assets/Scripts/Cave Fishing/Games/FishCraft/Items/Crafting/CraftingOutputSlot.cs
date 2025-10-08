using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class CraftingOutputSlot : MonoBehaviour
    {
        private ItemData outputItem;

        public ItemData OutputItem { get => outputItem; set => SetOutputItem(value); }

        public event Action<ItemData> OutputItemChanged;

        private void SetOutputItem(ItemData data)
        {
            outputItem = data;

            OutputItemChanged?.Invoke(data);
        }
    }
}
