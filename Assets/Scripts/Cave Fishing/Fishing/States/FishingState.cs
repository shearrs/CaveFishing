using Shears;
using Shears.Input;

namespace CaveFishing.Fishing
{
    public class FishingState : FishingRodState
    {
        private readonly Bobber bobber;
        private readonly IManagedInput castInput;
        private readonly Range<float> fishingTimeRange;

        private readonly Timer fishingTimer = new();
        private readonly Timer biteTimer;
        private readonly Timer fishCooldownTimer;

        public FishingState(FishingRod fishingRod, Bobber bobber, IManagedInput castInput, Range<float> fishingTimeRange, float biteTime, float fishCooldownTime) : base(fishingRod)
        {
            Name = "Fishing State";
            this.bobber = bobber;
            this.fishingTimeRange = fishingTimeRange;
            this.castInput = castInput;
            biteTimer = new(biteTime);
            fishCooldownTimer = new(fishCooldownTime);

            fishingTimer.AddOnComplete(BeginBiteTimer);
            biteTimer.AddOnComplete(BeginFishCooldownTimer);
            fishCooldownTimer.AddOnComplete(BeginFishingTimer);
        }

        protected override void OnEnter()
        {
            castInput.Performed += OnCastInputPerformed;

            fishingTimer.Start();
        }

        protected override void OnExit()
        {
            castInput.Performed -= OnCastInputPerformed;

            fishingTimer.Stop();
            biteTimer.Stop();
            fishCooldownTimer.Stop();
        }

        protected override void OnUpdate()
        {
        }

        private void BeginFishingTimer()
        {
            fishingTimer.Start(fishingTimeRange.Random());
        }

        private void BeginBiteTimer()
        {
            bobber.BeginBite();
            biteTimer.Start();
        }

        private void BeginFishCooldownTimer()
        {
            bobber.EndBite();
            fishCooldownTimer.Start();
        }

        private void OnCastInputPerformed(ManagedInputInfo info)
        {
            FishingRod.EnterState<ReelState>();
        }
    }
}
