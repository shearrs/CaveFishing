using Shears.Tweens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.UI
{
    public class FadeInButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
            var color = image.color;
            color.a = 0;
            image.color = color;

            color = backgroundImage.color;
            color.a = 0;
            backgroundImage.color = color;

            color = text.color;
            color.a = 0;
            text.color = color;
        }

        public void FadeIn()
        {
            var color = image.color;
            color.a = 0;
            image.color = color;

            color = backgroundImage.color;
            color.a = 0;
            backgroundImage.color = color;

            color = text.color;
            color.a = 0;
            text.color = color;

            var textTween = TweenManager.DoTween((t) =>
            {
                var color = image.color;
                color.a = t;
                image.color = color;

                color = backgroundImage.color;
                color.a = t;
                backgroundImage.color = color;

                color = text.color;
                color.a = t;
                text.color = color;
            });
        }
    }
}
