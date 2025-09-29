using Shears;
using Shears.Input;
using Shears.Tweens;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class CastState : FishingRodState
    {
        private readonly Bobber bobber;
        private readonly Transform castPoint;
        private readonly IManagedInput castInput;
        private readonly Range<float> forwardCastRange;
        private readonly Range<float> upCastRange;
        private readonly float releaseRotation;
        private readonly float releaseTime;
        private readonly ITweenData tweenData;
        private Tween tween;

        public CastState(FishingRod fishingRod, Bobber bobber, Transform castPoint, 
            IManagedInput castInput, Range<float> forwardCastRange, Range<float> upCastRange, 
            float releaseRotation, float releaseTime, ITweenData tweenData) : base(fishingRod)
        {
            Name = "Cast State";
            this.bobber = bobber;
            this.castPoint = castPoint;
            this.castInput = castInput;
            this.forwardCastRange = forwardCastRange;
            this.upCastRange = upCastRange;
            this.releaseRotation = releaseRotation;
            this.releaseTime = releaseTime;
            this.tweenData = tweenData;
        }

        protected override void OnEnter()
        {
            castInput.Performed += OnCastInput;

            float chargeProgress = FishingRod.ChargeProgress;
            float forwardForce = Mathf.Lerp(forwardCastRange.Min, forwardCastRange.Max, chargeProgress);
            float upForce = Mathf.Lerp(upCastRange.Min, upCastRange.Max, chargeProgress);

            Vector3 eulerRotation = FishingRod.transform.localEulerAngles;
            eulerRotation.x = releaseRotation;

            tween?.Dispose();
            tween = FishingRod.transform.GetRotateLocalTween(Quaternion.Euler(eulerRotation), true, tweenData);
            tween.AddEvent(releaseTime, () => OnReleaseBobberTime(forwardForce, upForce));
            FishingRod.Tween(tween);

            bobber.EnteredWater += OnBobberEnteredWater;
        }

        protected override void OnExit()
        {
            castInput.Performed -= OnCastInput;
        }

        protected override void OnUpdate()
        {
        }

        private void OnReleaseBobberTime(float forwardForce, float upForce)
        {
            Vector3 forward = castPoint.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 force = forwardForce * forward + upForce * castPoint.up;

            bobber.gameObject.SetActive(true);
            bobber.Cast(castPoint.position, force);
        }

        private void OnBobberEnteredWater()
        {
            FishingRod.EnterState<FishingState>();
        }

        private void OnCastInput(ManagedInputInfo info)
        {
            FishingRod.EnterState<ReelState>();
        }
    }
}
