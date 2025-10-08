using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> slots;
        [SerializeField] private CraftingOutputSlot outputSlot;

        private void OnEnable()
        {
            foreach (var slot in slots)
                slot.DataUpdated += OnDataUpdated;
        }

        private void OnDisable()
        {
            foreach (var slot in slots)
                slot.DataUpdated -= OnDataUpdated;
        }

        private void OnDataUpdated()
        {

        }
    }
}
