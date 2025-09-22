using Shears;
using UnityEngine;

namespace CaveFishing.Games
{
    public enum MinigameType { Wordle }

    [System.Serializable]
    internal class MinigameDictionary : SerializableDictionary<MinigameType, Minigame> { }

    public class MinigameManager : ProtectedSingleton<MinigameManager>
    {
        [SerializeField] private MinigameDictionary minigames = new();

        public static Minigame GetMinigame(MinigameType type) => Instance.InstGetMinigame(type);

        private Minigame InstGetMinigame(MinigameType type)
        {
            return minigames[type];
        }
    }
}
