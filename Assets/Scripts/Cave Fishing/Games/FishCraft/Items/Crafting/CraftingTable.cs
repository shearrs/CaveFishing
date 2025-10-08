using Shears.Input;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private Block block;

        private void OnEnable()
        {
            block.Used += OnBlockUsed;
        }

        private void OnDisable()
        {
            block.Used -= OnBlockUsed;
        }

        private void OnBlockUsed()
        {
            SignalShuttle.Emit(new CraftingTableUsedSignal());
        }
    }
}
