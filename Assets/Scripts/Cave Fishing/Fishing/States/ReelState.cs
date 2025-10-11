using Shears.Tweens;
using System;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class ReelState : FishingRodState
    {
        private readonly Bobber bobber;
        private readonly float chargeRotation;
        private readonly float reelTime;
        private readonly ITweenData tweenData;
        private readonly AudioClip reelClip;
        private Tween tween;

        public event Action FishReeled;

        public ReelState(FishingRod fishingRod, Bobber bobber, float chargeRotation, float reelTime, ITweenData tweenData, AudioClip reelClip) : base(fishingRod)
        {
            Name = "Reel State";
            this.bobber = bobber;
            this.chargeRotation = chargeRotation;
            this.reelTime = reelTime;
            this.tweenData = tweenData;
            this.reelClip = reelClip;
        }

        protected override void OnEnter()
        {
            Vector3 eulerRotation = FishingRod.transform.localEulerAngles;
            eulerRotation.x = chargeRotation;

            FishingRod.StopTween();

            tween.Dispose();
            tween = FishingRod.transform.GetRotateLocalTween(Quaternion.Euler(eulerRotation), true, tweenData);
            tween.AddEvent(reelTime, DisableBobber);
            tween.Completed += EnterReturnState;

            FishingRod.Tween(tween);
            FishingRod.PlayClip(reelClip);

            if (bobber.IsBiting)
            {
                bobber.EndBite();

                FishReeled?.Invoke();
            }
        }

        protected override void OnExit()
        {
            tween.Dispose();
        }

        protected override void OnUpdate()
        {
        }

        private void EnterReturnState()
        {
            FishingRod.EnterState<ReturnState>();
        }

        private void DisableBobber() => bobber.gameObject.SetActive(false);
    }
}
