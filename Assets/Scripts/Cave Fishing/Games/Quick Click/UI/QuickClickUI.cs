using Shears;
using Shears.Input;
using Shears.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.QuickClickGame.UI
{
    public class QuickClickUI : MonoBehaviour
    {
        [SerializeField] private QuickClick quickClick;
        [SerializeField] private Instructor instructor;
        [SerializeField] private Image background;
        [SerializeField] private Range<float> positionRange;
        [SerializeField] private StructTweenData tweenData;

        private Tween tween;

        private void OnEnable()
        {
            quickClick.Enabled += OnEnabled;
            quickClick.Disabled += OnDisabled;
        }

        private void OnDisable()
        {
            quickClick.Enabled -= OnEnabled;
            quickClick.Disabled -= OnDisabled;

            tween.Dispose();
        }

        private void OnEnabled()
        {
            CursorManager.SetCursorVisibility(true);
            CursorManager.SetCursorLockMode(CursorLockMode.None);

            background.enabled = true;

            void update(float t)
            {
                Vector2 offset = background.rectTransform.offsetMax;
                offset.y = Mathf.LerpUnclamped(-positionRange.Max, positionRange.Min, t);

                background.rectTransform.offsetMax = offset;
            }

            tween.Dispose();
            tween = TweenManager.DoTween(update, tweenData);
            tween.AddOnComplete(() => instructor.Instruct(quickClick.StartGame));
        }

        private void OnDisabled()
        {
            void update(float t)
            {
                Vector2 offset = background.rectTransform.offsetMax;
                offset.y = Mathf.LerpUnclamped(positionRange.Min, -positionRange.Max, t);

                background.rectTransform.offsetMax = offset;
            }

            tween.Dispose();
            tween = TweenManager.DoTween(update, tweenData);
            tween.AddOnComplete(quickClick.EndGame);
        }
    }
}
