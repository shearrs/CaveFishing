using Shears;
using Shears.Logging;
using Shears.Tweens;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingRod : SHMonoBehaviourLogger
    {
        public enum State { None, Charging, Casted, Reeling }

        [Header("Components")]
        [SerializeField] private Bobber bobber;
        [SerializeField] private Transform castPoint;

        [Header("Cast Force Settings")]
        [SerializeField] private float minForwardCastForce = 0.5f;
        [SerializeField] private float maxForwardCastForce = 3f;
        [SerializeField] private float minUpCastForce = 0.5f;
        [SerializeField] private float maxUpCastForce = 3f;

        [Header("Fishing Settings")]
        [SerializeField] private float minFishingTime = 5f;
        [SerializeField] private float maxFishingTime = 15f;
        [SerializeField] private float fishCooldownTime = 5f;
        [SerializeField] private float biteTime = 1f;

        [Header("Tween Settings")]
        [SerializeField] private float releaseRotation = 20f;
        [SerializeField] private float chargeRotation = -24f;
        [SerializeField, Range(0f, 1f)] private float releaseTime = 0.85f;
        [SerializeField, Range(0f, 1f)] private float reelTime = 0.85f;
        [SerializeField] private StructTweenData chargeTweenData = new(0.8f);
        [SerializeField] private StructTweenData releaseTweenData = new(0.25f, easingFunction: EasingFunction.Ease.EaseOutBack);
        [SerializeField] private StructTweenData reelTweenData = new(0.4f, easingFunction: EasingFunction.Ease.EaseOutBack);
        [SerializeField] private StructTweenData returnTweenData = new(0.4f, easingFunction: EasingFunction.Ease.EaseOutQuad);

        private Tween tween;
        private State state = State.None;

        public State CurrentState => state;
        public Bobber Bobber => bobber;

        public event Action FishReeled;

        private void Awake()
        {
            bobber.gameObject.SetActive(true);
            bobber.transform.SetParent(null);
            bobber.gameObject.SetActive(false);

            bobber.EnteredWater += OnEnteredWater;
        }

        public void BeginCharging()
        {
            if (state == State.Reeling)
                return;

            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = chargeRotation;

            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, chargeTweenData);
            state = State.Charging;
        }

        public void Cast()
        {
            float chargeProgress = tween.Progress;
            float forwardForce = Mathf.Lerp(minForwardCastForce, maxForwardCastForce, chargeProgress);
            float upForce = Mathf.Lerp(minUpCastForce, maxUpCastForce, chargeProgress);

            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = releaseRotation;

            tween?.Dispose();
            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, releaseTweenData);
            tween.AddEvent(releaseTime, () => OnCastTweenComplete(forwardForce, upForce));
        }

        public void Reel()
        {
            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = chargeRotation;

            tween?.Dispose();
            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, reelTweenData);
            tween.AddEvent(reelTime, OnReelTweenComplete);

            state = State.Reeling;

            StopAllCoroutines();

            if (bobber.IsBiting)
            {
                bobber.EndBite();

                FishReeled?.Invoke();
            }
        }

        private void OnEnteredWater()
        {
            StartCoroutine(IEFish());
        }

        private void OnCastTweenComplete(float forwardForce, float upForce)
        {
            Vector3 forward = castPoint.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 force = forwardForce * forward + upForce * castPoint.up;

            bobber.gameObject.SetActive(true);
            bobber.Cast(castPoint.position, force);

            state = State.Casted;
        }

        private void OnReelTweenComplete()
        {
            bobber.gameObject.SetActive(false);

            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = releaseRotation;

            tween?.Dispose();
            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, returnTweenData);
            tween.AddOnComplete(() => state = State.None);
        }

        private IEnumerator IEFish()
        {
            while (true)
            {
                float fishingTime = UnityEngine.Random.Range(minFishingTime, maxFishingTime);

                yield return CoroutineUtil.WaitForSeconds(fishingTime);

                Log("Begin bite.");

                bobber.BeginBite();

                yield return CoroutineUtil.WaitForSeconds(biteTime);

                Log("End bite.");

                bobber.EndBite();

                yield return CoroutineUtil.WaitForSeconds(fishCooldownTime);

                yield return null;
            }
        }
    }
}
