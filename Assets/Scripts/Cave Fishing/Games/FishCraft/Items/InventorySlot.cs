using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class InventorySlot : MonoBehaviour
    {
        private const int MAX_STACK = 64;

        [SerializeField] private Item item;
        [SerializeField] private int count;

        public Item Item => item;
        public int Count => count;

        public event Action DataUpdated;

        public void SetItem(Item item, int count)
        {
            this.item = item;
            this.count = count;

            DataUpdated?.Invoke();
        }

        public void AddCount(int count) => this.count += count;

        public bool IsFull() => count == MAX_STACK;
    }
}
