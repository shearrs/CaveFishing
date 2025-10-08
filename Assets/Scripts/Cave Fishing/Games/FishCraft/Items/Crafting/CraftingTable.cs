using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private ItemInteractor interactor;
        [SerializeField] private List<InventorySlot> slots;

        private Item[] items;

        public event Action<IRecipe> RecipeChanged;

        private void Awake()
        {
            items = new Item[slots.Count];
        }

        private void OnEnable()
        {
            foreach (var slot in slots)
                slot.DataUpdated += OnDataUpdated;

            inventory.Closed += OnInventoryClosed;
        }

        private void OnDisable()
        {
            foreach (var slot in slots)
                slot.DataUpdated -= OnDataUpdated;

            inventory.Closed -= OnInventoryClosed;
        }

        public void Craft()
        {
            if (!CraftingRecipes.TryGetRecipe(items, out var recipe))
                return;

            Debug.Log("found recipe");
            interactor.Hold(new Item(recipe.Result), recipe.Count);

            foreach (var slot in slots)
                slot.SetItem(null, 0);
        }

        private void OnInventoryClosed()
        {
            foreach (var slot in slots)
                slot.SetItem(null, 0);
        }

        private void OnDataUpdated()
        {
            Array.Clear(items, 0, items.Length);

            for (int i = 0; i < slots.Count; i++)
                items[i] = slots[i].Item;

            if (!CraftingRecipes.TryGetRecipe(items, out var recipe))
                return;

            RecipeChanged?.Invoke(recipe);
        }
    }
}
