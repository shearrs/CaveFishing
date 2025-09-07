using Shears.Input;
using Shears.Tweens;
using ShearsLibrary.Cameras;
using UnityEngine;

namespace CaveFishing.Players
{
    [RequireComponent(typeof(ManagedCamera), typeof(FirstPersonCameraState))]
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ManagedInputMap inputMap;
        [SerializeField] private PlayerCharacter playerCharacter;

        [Header("Settings")]
        [SerializeField] private float sensitivity = 0.125f;
        [SerializeField] private Vector3 offset = new(0f, 1.5f, 0f);

        private ManagedCamera managedCamera;
        private FirstPersonCameraState firstPersonState;
        private Tween crouchTween;
        private ITweenData crouchTweenData;

        private void OnValidate()
        {
            var camera = GetComponent<ManagedCamera>();
            var firstPersonState = GetComponent<FirstPersonCameraState>();

            camera.InputMap = inputMap;

            if (!camera.HasState(firstPersonState))
                camera.AddState(firstPersonState);

            if (playerCharacter != null)
                firstPersonState.Target = playerCharacter.transform;
            else
                firstPersonState.Target = null;

            firstPersonState.Sensitivity = sensitivity;
            firstPersonState.Offset = offset;
        }

        private void Awake()
        {
            managedCamera = GetComponent<ManagedCamera>();
            firstPersonState = GetComponent<FirstPersonCameraState>();

            var tweenData = playerCharacter.CrouchTweenData;
            crouchTweenData = new TweenData(tweenData.Duration + 0.1f, tweenData.ForceFinalValue, tweenData.Loops, tweenData.LoopMode, EasingFunction.Ease.EaseOutBack);
        }

        private void OnEnable()
        {
            playerCharacter.Crouched += OnCrouched;
            playerCharacter.Uncrouched += OnUncrouched;
        }

        private void OnDisable()
        {
            playerCharacter.Crouched -= OnCrouched;
            playerCharacter.Uncrouched -= OnUncrouched;
        }

        private void OnDestroy()
        {
            Destroy(managedCamera);
            Destroy(firstPersonState);
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
