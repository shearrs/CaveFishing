using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class CraftingResultSlot : MonoBehaviour
    {
        [SerializeField] private InventoryCraftingTable table;
        [SerializeField] private Image spriteImage;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            table.RecipeChanged += OnRecipeChanged;
            table.Inventory.Closed += OnInventoryClosed;
        }

        private void OnDisable()
        {
            table.RecipeChanged -= OnRecipeChanged;
            table.Inventory.Closed -= OnInventoryClosed;
        }

        public void Select()
        {
            table.Craft();
        }

        private void OnRecipeChanged(IRecipe recipe)
        {
            if (recipe == null)
            {
                spriteImage.enabled = false;
                textMesh.enabled = false;
            }
            else
            {
                spriteImage.enabled = true;
                textMesh.enabled = true;
                spriteImage.sprite = recipe.Result.Sprite;

                string count = recipe.Count > 1 ? recipe.Count.ToString() : "";

                textMesh.text = count;
            }
        }

        private void OnInventoryClosed()
        {
            OnRecipeChanged(null);
        }
    }
}
