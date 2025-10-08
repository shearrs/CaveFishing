using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class CraftingResultSlot : MonoBehaviour
    {
        [SerializeField] private CraftingTable table;
        [SerializeField] private Image spriteImage;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            table.RecipeChanged -= OnRecipeChanged;
        }

        private void OnDisable()
        {
            table.RecipeChanged -= OnRecipeChanged;
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
                textMesh.text = recipe.Count.ToString();
            }
        }
    }
}
