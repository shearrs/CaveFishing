using Shears.Input;
using Shears.Tweens;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class ChargeState : FishingRodState
    {
        private readonly IManagedInput castInput;
        private readonly float chargeRotation;
        private readonly ITweenData tweenData;
        private Tween tween;

        public ChargeState(FishingRod fishingRod, IManagedInput castInput, float chargeRotation, ITweenData tweenData) : base(fishingRod)
        {
            this.castInput = castInput;
            this.chargeRotation = chargeRotation;
            this.tweenData = tweenData;
        }

        protected override void OnEnter()
        {
            castInput.Canceled += OnCastInputCanceled;

            Vector3 eulerRotation = FishingRod.transform.localEulerAngles;
            eulerRotation.x = chargeRotation;

            tween?.Dispose();
            tween = FishingRod.transform.GetRotateLocalTween(Quaternion.Euler(eulerRotation), true, tweenData);
            FishingRod.Tween(tween);
        }

        protected override void OnExit()
        {
            castInput.Canceled -= OnCastInputCanceled;
        }

        protected override void OnUpdate()
        {
            if (tween != null)
                FishingRod.ChargeProgress = tween.Progress;
        }

        private void OnCastInputCanceled(ManagedInputInfo info)
        {
            FishingRod.EnterState<CastState>();
        }
    }
}
