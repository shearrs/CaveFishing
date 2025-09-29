using Shears.Tweens;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class ReturnState : FishingRodState
    {
        private readonly ITweenData tweenData;
        private readonly float releaseRotation;
        private Tween tween;

        public ReturnState(FishingRod fishingRod, ITweenData tweenData, float releaseRotation) : base(fishingRod)
        {
            this.tweenData = tweenData;
            this.releaseRotation = releaseRotation;
        }

        protected override void OnEnter()
        {
            Vector3 eulerRotation = FishingRod.transform.localEulerAngles;
            eulerRotation.x = releaseRotation;

            tween?.Dispose();
            tween = FishingRod.transform.GetRotateLocalTween(Quaternion.Euler(eulerRotation), true, tweenData);
            tween.AddOnComplete(OnTweenComplete);

            FishingRod.Tween(tween);
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        private void OnTweenComplete()
        {
            FishingRod.EnterState<IdleState>();
        }
    }
}
