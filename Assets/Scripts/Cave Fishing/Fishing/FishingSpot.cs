using CaveFishing.Games;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingSpot : MonoBehaviour
    {
        [SerializeField] private Fish[] fish;

        public Minigame GetMinigame()
        {
            var selectFish = fish[Random.Range(0, fish.Length)];

            return MinigameManager.GetMinigame(selectFish.MinigameType);
        }
    }
}
