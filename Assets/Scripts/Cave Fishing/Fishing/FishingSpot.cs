using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingSpot : MonoBehaviour
    {
        [SerializeField] private Fish[] fish;

        public Fish GetFish()
        {
            return fish[Random.Range(0, fish.Length)];
        }
    }
}
