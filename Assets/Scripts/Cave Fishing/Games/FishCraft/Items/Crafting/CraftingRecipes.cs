using Shears;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class CraftingRecipes : ProtectedSingleton<CraftingRecipes>
    {
        [SerializeField] private ItemData stickData;
        [SerializeField] private ItemData oakPlanksData;

        private IRecipe[] recipes;

        protected override void Awake()
        {
            base.Awake();

            recipes = new IRecipe[]
            {
                new Recipe2x2(  null,           oakPlanksData,
                                oakPlanksData,  null,
                                stickData)
            };
        }

        private bool TryGetRecipe(Item[] ingredients)
        {

        }
    }
}
