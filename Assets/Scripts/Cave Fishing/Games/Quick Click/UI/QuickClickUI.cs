using Shears;
using Shears.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.QuickClickGame.UI
{
    public class QuickClickUI : MonoBehaviour
    {
        [SerializeField] private QuickClick quickClick;
        [SerializeField] private Image background;
        [SerializeField] private Range<float> positionRange;
        [SerializeField] private StructTweenData tweenData;

        private void OnEnable()
        {
            quickClick.Enabled += OnEnabled;
        }

        private void OnDisable()
        {
            quickClick.Enabled -= OnEnabled;
        }

        private void OnEnabled()
        {
            background.enabled = true;

            void update(float t)
            {
                Vector2 offset = background.rectTransform.offsetMax;
                offset.y = Mathf.LerpUnclamped(-.5f* positionRange.Max, positionRange.Min, t);

                background.rectTransform.offsetMax = offset;
            }

            var tween = TweenManager.DoTween(update, tweenData);
            tween.AddOnComplete(quickClick.StartGame);

            Debug.Log("play tween");
        }
    }
}
