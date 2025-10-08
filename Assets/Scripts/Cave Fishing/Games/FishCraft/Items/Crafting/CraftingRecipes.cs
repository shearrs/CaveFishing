using Shears;
using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class CraftingRecipes : ProtectedSingleton<CraftingRecipes>
    {
        [SerializeField] private ItemData stickData;
        [SerializeField] private ItemData oakPlanksData;

        private readonly ItemData[] ingredients = new ItemData[9];
        private Recipe2x2[] recipe2x2s;
        private Recipe3x3[] recipe3x3s;

        protected override void Awake()
        {
            base.Awake();

            recipe2x2s = new Recipe2x2[] 
            {
                new(
                    null,           oakPlanksData,
                    oakPlanksData,  null,
                    stickData, 4
                )
            };

            recipe3x3s = new Recipe3x3[]
            {
                new(
                    null,           oakPlanksData,  null,
                    oakPlanksData,  null,           null,
                    null,           null,           null,
                    stickData, 4
                ),
                new(
                    null,           null,           null,
                    null,           oakPlanksData,  null,
                    oakPlanksData,  null,           null,
                    stickData, 4
                ),
                new(
                    null,           null,           oakPlanksData,
                    null,           oakPlanksData,  null,
                    null,           null,           null,
                    stickData, 4
                ),
                new(
                    null,           null,           null,
                    null,           null,           oakPlanksData,
                    null,           oakPlanksData,  null,
                    stickData, 4
                ),
            };
        }

        public static bool TryGetRecipe(Item[] itemIngredients, out IRecipe recipe) => Instance.InstTryGetRecipe(itemIngredients, out recipe);
        private bool InstTryGetRecipe(Item[] itemIngredients, out IRecipe recipe)
        {
            Array.Clear(ingredients, 0, 9);
            recipe = null;

            for (int i = 0; i < itemIngredients.Length; i++)
            {
                if (itemIngredients[i] == null)
                    ingredients[i] = null;
                else
                    ingredients[i] = itemIngredients[i].Data;
            }

            if (itemIngredients.Length == 9)
            {
                foreach (var recipe3x3 in recipe3x3s)
                {
                    if (recipe3x3.IsValid(ingredients))
                    {
                        recipe = recipe3x3;
                        Debug.Log("3x3: " + recipe);

                        return true;
                    }
                }
            }
            else if (itemIngredients.Length == 4)
            {
                foreach (var recipe2x2 in recipe2x2s)
                {
                    if (recipe2x2.IsValid(ingredients))
                    {
                        recipe = recipe2x2;
                        Debug.Log("2x2: " + recipe);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
