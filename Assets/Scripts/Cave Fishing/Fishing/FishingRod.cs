using Shears;
using Shears.Input;
using Shears.Logging;
using Shears.StateMachineGraphs;
using Shears.Tweens;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingRod : SHMonoBehaviourLogger
    {
        #region Variables
        [Header("Components")]
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private Bobber bobber;
        [SerializeField] private Transform castPoint;

        [FoldoutGroup("Cast Force Settings", 2)]
        [SerializeField] private Range<float> forwardCastRange = new(0.5f, 3f);
        [SerializeField] private Range<float> upCastRange = new(0.5f, 3f);

        [FoldoutGroup("Fishing Settings", 4)]
        [SerializeField] private Range<float> fishingTimeRange = new(5f, 15f);
        [SerializeField] private float fishCooldownTime = 5f;
        [SerializeField] private float biteTime = 1f;
        [SerializeField, ReadOnly, Range(0f, 1f)] private float chargeProgress;

        [FoldoutGroup("Tween Settings", 8)]
        [SerializeField] private float releaseRotation = 20f;
        [SerializeField] private float chargeRotation = -45f;
        [SerializeField, Range(0f, 1f)] private float releaseTime = 0.85f;
        [SerializeField, Range(0f, 1f)] private float reelTime = 0.85f;
        [SerializeField] private StructTweenData chargeTweenData = new(0.8f);
        [SerializeField] private StructTweenData releaseTweenData = new(0.25f, easingFunction: EasingFunction.Ease.EaseOutBack);
        [SerializeField] private StructTweenData reelTweenData = new(0.2f, easingFunction: EasingFunction.Ease.EaseOutQuad);
        [SerializeField] private StructTweenData returnTweenData = new(1f, easingFunction: EasingFunction.Ease.EaseOutQuad);

        private IManagedInput castInput;
        private Tween tween;

        public Bobber Bobber => bobber;
        internal float ChargeProgress { get => chargeProgress; set => chargeProgress = Mathf.Clamp01(value); }

        public event Action FishReeled;
        #endregion

        private void Awake()
        {
            castInput = inputProvider.GetInput("Cast");

            bobber.gameObject.SetActive(true);
            bobber.transform.SetParent(null);
            bobber.gameObject.SetActive(false);

            bobber.EnteredWater += () => EnterState<FishingState>();

            ReelState reelState;

            stateMachine.AddStates(
                new DisableState(this, bobber),
                new IdleState(this, castInput),
                new ChargeState(this, castInput, chargeRotation, chargeTweenData),
                new CastState(this, bobber, castPoint, castInput, forwardCastRange, upCastRange, releaseRotation, releaseTime, releaseTweenData),
                new FishingState(this, bobber, castInput, fishingTimeRange, biteTime, fishCooldownTime),
                reelState = new ReelState(this, bobber, chargeRotation, reelTime, reelTweenData),
                new ReturnState(this, castInput, returnTweenData, releaseRotation)
            );

            reelState.FishReeled += () => FishReeled?.Invoke();
        }

        public void Enable()
        {
            gameObject.SetActive(true);

            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = releaseRotation;
            transform.localRotation = Quaternion.Euler(eulerRotation);

            EnterState<IdleState>();
        }

        public void Disable()
        {
            EnterState<DisableState>();
        }

        public void EnterState<T>()
        {
            stateMachine.EnterStateOfType<T>();
        }
    
        public void Tween(Tween tween)
        {
            this.tween?.Dispose();

            this.tween = tween;

            tween?.Play();
        }
    }
}
