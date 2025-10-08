using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Cave Fishing/FishCraft/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Block block;
        [SerializeField] private Texture2D texture;

        public string Name => itemName;
        public Sprite Sprite => sprite;
        public Block Block => block;
        public bool IsBlock => block != null;
        public Texture2D Texture => texture;
    }
}
