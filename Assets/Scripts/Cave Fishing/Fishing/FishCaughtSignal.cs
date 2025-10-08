using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public readonly struct FishCaughtSignal : ISignal
    {
        private readonly Fish fish;

        public readonly Fish Fish => fish;

        public FishCaughtSignal(Fish fish)
        {
            this.fish = fish;
        }
    }
}
