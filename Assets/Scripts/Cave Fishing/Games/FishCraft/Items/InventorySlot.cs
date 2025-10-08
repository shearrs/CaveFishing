using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class InventorySlot : MonoBehaviour
    {
        private const int MAX_STACK = 64;

#if UNITY_EDITOR
        [SerializeField] private string itemName;
#endif
        [SerializeField] private int count;

        private Item item = null;

        public Item Item => item;
        public int Count => count;

        public event Action DataUpdated;

        public void SetItem(Item item, int count)
        {
            this.item = item;
            this.count = count;

            UpdateData();
        }

        public void AddCount(int count)
        {
            this.count += count;

            UpdateData();
        }

        public void RemoveCount(int count)
        {
            this.count -= count;

            if (this.count == 0)
                item = null;

            UpdateData();
        }

        private void UpdateData()
        {
#if UNITY_EDITOR
            if (item == null || item.Data == null)
                itemName = string.Empty;
            else
                itemName = item.Name;
#endif

            DataUpdated?.Invoke();
        }

        public bool IsFull() => count == MAX_STACK;
    }
}
