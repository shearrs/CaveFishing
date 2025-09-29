using Shears.Input;
using Shears.Tweens;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class ReturnState : FishingRodState
    {
        private readonly IManagedInput castInput;
        private readonly ITweenData tweenData;
        private readonly float releaseRotation;
        private Tween tween;

        public ReturnState(FishingRod fishingRod, IManagedInput castInput, ITweenData tweenData, float releaseRotation) : base(fishingRod)
        {
            Name = "Return State";
            this.castInput = castInput;
            this.tweenData = tweenData;
            this.releaseRotation = releaseRotation;
        }

        protected override void OnEnter()
        {
            castInput.Performed += OnCastInput;

            Vector3 eulerRotation = FishingRod.transform.localEulerAngles;
            eulerRotation.x = releaseRotation;

            tween?.Dispose();
            tween = FishingRod.transform.GetRotateLocalTween(Quaternion.Euler(eulerRotation), true, tweenData);
            tween.AddOnComplete(OnTweenComplete);

            FishingRod.Tween(tween);
        }

        protected override void OnExit()
        {
            castInput.Performed -= OnCastInput;
        }

        protected override void OnUpdate()
        {
        }

        private void OnTweenComplete()
        {
            FishingRod.EnterState<IdleState>();
        }

        private void OnCastInput(ManagedInputInfo info)
        {
            FishingRod.EnterState<ChargeState>();
        }
    }
}
