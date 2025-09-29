using Shears.Input;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class IdleState : FishingRodState
    {
        private readonly IManagedInput castInput;

        public IdleState(FishingRod fishingRod, IManagedInput castInput) : base(fishingRod)
        {
            Name = "Idle State";
            this.castInput = castInput;
        }

        protected override void OnEnter()
        {
            castInput.Started += OnCastInputStarted;
        }

        protected override void OnExit()
        {
            castInput.Started -= OnCastInputStarted;
        }

        protected override void OnUpdate()
        {
        }

        private void OnCastInputStarted(ManagedInputInfo info)
        {
            FishingRod.EnterState<ChargeState>();
        }
    }
}
