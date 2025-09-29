using Shears.StateMachineGraphs;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public abstract class FishingRodState : State
    {
        private readonly FishingRod fishingRod;

        protected FishingRod FishingRod => fishingRod;

        public FishingRodState(FishingRod fishingRod)
        {
            this.fishingRod = fishingRod;
        }
    }
}
