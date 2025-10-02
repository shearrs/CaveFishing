using Shears.Tweens;
using UnityEngine;

namespace CaveFishing.Players
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter character;

        private Vector3 scale;
        private Tween crouchTween;
        private TweenData crouchTweenData;

        private void Awake()
        {
            scale = transform.localScale;

            var tweenData = character.CrouchTweenData;
            crouchTweenData = new TweenData(tweenData.Duration + 0.1f, tweenData.ForceFinalValue, tweenData.Loops, tweenData.LoopMode, EasingFunction.Ease.EaseOutBack);
        }

        private void OnEnable()
        {
            character.Crouched += OnCrouched;
            character.Uncrouched += OnUncrouched;
        }

        private void OnDisable()
        {
            character.Crouched -= OnCrouched;
            character.Uncrouched -= OnUncrouched;
        }

        private void OnCrouched()
        {
            var startScale = transform.localScale;
            var endScale = scale;
            endScale.y *= 0.5f;

            void update(float t)
            {
                transform.localScale = Vector3.LerpUnclamped(startScale, endScale, t);
            }

            crouchTween.Dispose();
            crouchTween = TweenManager.DoTween(update, crouchTweenData);
        }

        private void OnUncrouched()
        {
            var startScale = transform.localScale;
            var endScale = scale;

            void update(float t)
            {
                transform.localScale = Vector3.LerpUnclamped(startScale, endScale, t);
            }

            crouchTween.Dispose();
            crouchTween = TweenManager.DoTween(update, crouchTweenData);
        }
    }
}
