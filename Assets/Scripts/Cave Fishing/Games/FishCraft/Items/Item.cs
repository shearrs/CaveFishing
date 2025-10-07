using Shears;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    [System.Serializable]
    public class Item
    {
        [SerializeField, ReadOnly] private ItemData data;

        public ItemData Data => data;
        public string Name => data.Name;

        public Item(ItemData data)
        {
            this.data = data;
        }
    }
}
