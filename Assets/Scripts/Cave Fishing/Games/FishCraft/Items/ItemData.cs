using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Cave Fishing/FishCraft/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Texture2D texture;

        public string Name => itemName;
        public Sprite Sprite => sprite;
        public Texture2D Texture => texture;
    }
}
