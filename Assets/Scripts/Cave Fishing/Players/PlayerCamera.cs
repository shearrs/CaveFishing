using Shears.Input;
using Shears.Tweens;
using Shears.Cameras;
using UnityEngine;

namespace CaveFishing.Players
{
    [RequireComponent(typeof(ManagedCamera), typeof(FirstPersonCameraState))]
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ManagedInputMap inputMap;
        [SerializeField] private PlayerCharacter character;

        [Header("Settings")]
        [SerializeField] private float sensitivity = 0.125f;
        [SerializeField] private Vector3 offset = new(0f, 1.5f, 0f);
        [SerializeField] private float headBobHeight = 0.05f;
        [SerializeField] private TweenData headBobTweenData = new(0.25f, loops: -1, loopMode:LoopMode.PingPong);

        private float headBobOffset;

        private ManagedCamera managedCamera;
        private FirstPersonCameraState firstPersonState;

        private Tween crouchTween;
        private Tween headBobTween;
        private ITweenData crouchTweenData;
        private readonly ITweenData headBobCancelTweenData = new TweenData(0.15f);

        private void OnValidate()
        {
            var camera = GetComponent<ManagedCamera>();
            var firstPersonState = GetComponent<FirstPersonCameraState>();

            camera.InputMap = inputMap;

            if (!camera.HasState(firstPersonState))
                camera.AddState(firstPersonState);

            if (character != null)
                firstPersonState.Target = character.transform;
            else
                firstPersonState.Target = null;

            firstPersonState.Sensitivity = sensitivity;
            firstPersonState.Offset = offset;
        }

        private void Awake()
        {
            managedCamera = GetComponent<ManagedCamera>();
            firstPersonState = GetComponent<FirstPersonCameraState>();

            var tweenData = character.CrouchTweenData;
            crouchTweenData = new TweenData(tweenData.Duration + 0.1f, tweenData.ForceFinalValue, tweenData.Loops, tweenData.LoopMode, EasingFunction.Ease.EaseOutBack);
        }

        private void Start()
        {
            managedCamera.SetState(null);
        }

        private void OnEnable()
        {
            character.BeganMoving += OnBeganMoving;
            character.EndedMoving += OnEndedMoving;
            character.Crouched += OnCrouched;
            character.Uncrouched += OnUncrouched;
        }

        private void OnDisable()
        {
            character.BeganMoving -= OnBeganMoving;
            character.EndedMoving -= OnEndedMoving;
            character.Crouched -= OnCrouched;
            character.Uncrouched -= OnUncrouched;
        }

        private void OnDestroy()
        {
            Destroy(managedCamera);
            Destroy(firstPersonState);
        }

        private void OnBeganMoving()
        {
            var startOffset = headBobOffset;

            void update(float t)
            {
                headBobOffset = Mathf.LerpUnclamped(startOffset, headBobHeight, t);

                firstPersonState.OffsetModifier = new Vector3(0f, headBobOffset, 0f);
            }

            headBobTween?.Dispose();
            headBobTween = TweenManager.DoTween(update, headBobTweenData);
        }

        private void OnEndedMoving()
        {
            var startOffset = headBobOffset;

            void update(float t)
            {
                headBobOffset = Mathf.LerpUnclamped(startOffset, 0f, t);

                firstPersonState.OffsetModifier = new Vector3(0f, headBobOffset, 0f);
            }

            headBobTween?.Dispose();
            headBobTween = TweenManager.DoTween(update, headBobCancelTweenData);
        }

        private void OnCrouched()
        {
            var startOffset = firstPersonState.Offset;

            var crouchOffset = offset;
            crouchOffset.y *= 0.5f;

            void update(float t) => firstPersonState.Offset = Vector3.LerpUnclamped(startOffset, crouchOffset, t);

            crouchTween?.Dispose();
            crouchTween = TweenManager.DoTween(update, crouchTweenData);
        }

        private void OnUncrouched()
        {
            var startOffset = firstPersonState.Offset;

            void update(float t) => firstPersonState.Offset = Vector3.LerpUnclamped(startOffset, offset, t);

            crouchTween?.Dispose();
            crouchTween = TweenManager.DoTween(update, crouchTweenData);
        }
    }
}
