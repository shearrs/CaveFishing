using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class CaveFishingSpot : MonoBehaviour
    {
        [SerializeField] private FishingSpot fishingSpot;
        [SerializeField] private Fish unlockableFish;

        private void OnEnable()
        {
            SignalShuttle.Register<FishCaughtSignal>(OnFishCaught);
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<FishCaughtSignal>(OnFishCaught);
        }

        private void OnFishCaught(FishCaughtSignal signal)
        {
            fishingSpot.RemoveFish(signal.Fish);

            if (fishingSpot.FishCount == 0)
                fishingSpot.AddFish(unlockableFish);
        }
    }
}
