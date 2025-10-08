using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class InventoryCraftingTable : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private ItemInteractor interactor;
        [SerializeField] private List<InventorySlot> slots;

        private Item[] items;

        public Inventory Inventory => inventory;

        public event Action<IRecipe> RecipeChanged;

        private void Awake()
        {
            items = new Item[slots.Count];

            if (interactor == null)
                interactor = FindAnyObjectByType<ItemInteractor>();
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

            var heldItem = interactor.HeldItem;

            if (heldItem != null && heldItem.Data != recipe.Result)
                return;

            if (heldItem != null && heldItem.Data == recipe.Result)
                interactor.AddCount(recipe.Count);
            else
                interactor.Hold(new Item(recipe.Result), recipe.Count);

            foreach (var slot in slots)
            {
                if (slot.Item != null)
                    slot.RemoveCount(1);
            }
        }

        private void OnInventoryClosed()
        {
            foreach (var slot in slots)
            {
                if (slot.Item == null)
                    continue;

                inventory.AddItem(slot.Item, slot.Count);
                slot.SetItem(null, 0);
            }
        }

        private void OnDataUpdated()
        {
            Array.Clear(items, 0, items.Length);

            for (int i = 0; i < slots.Count; i++)
                items[i] = slots[i].Item;

            if (!CraftingRecipes.TryGetRecipe(items, out var recipe))
            {
                RecipeChanged?.Invoke(null);

                return;
            }

            RecipeChanged?.Invoke(recipe);
        }
    }
}
