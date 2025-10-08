using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingSpot : MonoBehaviour
    {
        [SerializeField] private List<Fish> fish;

        public int FishCount => fish.Count;

        public Fish GetFish()
        {
            return fish[Random.Range(0, fish.Count)];
        }

        public void AddFish(Fish fish)
        {
            this.fish.Add(fish);
        }

        public void RemoveFish(Fish fish)
        {
            this.fish.Remove(fish);
        }
    }
}
