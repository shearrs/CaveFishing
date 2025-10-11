using Shears;
using Shears.Tweens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.UI
{
    public class MainMenuIntro : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Color startColor;
        [SerializeField] private Image startButtonImage;
        [SerializeField] private Image startButtonBackgroundImage;
        [SerializeField] private TextMeshProUGUI startButtonText;

        [FoldoutGroup("Fade In", 5)]
        [SerializeField] private Range<float> verticalWaveStrengths = new(0.001f, 0.05f);
        [SerializeField] private Range<float> horizontalWaveStrengths = new(0.01f, 0.5f);
        [SerializeField] private Range<float> waveSpeeds = new(1.0f, 2.0f);
        [SerializeField, Min(0f)] private float delayDuration;
        [SerializeField, Min(0f)] private float fadeInDuration;

        private readonly StructTweenData introTweenData = new(4, easingFunction: EasingFunction.Ease.EaseInOutQuad);
        private readonly int verticalWaveStrengthID = Shader.PropertyToID("_Vertical_Wave_Strength");
        private readonly int horizontalWaveStrengthID = Shader.PropertyToID("_Horizontal_Wave_Strength");
        private readonly int verticalWaveSpeedID = Shader.PropertyToID("_Vertical_Wave_Speed");
        private readonly int horizontalWaveSpeedID = Shader.PropertyToID("_Horizontal_Wave_Speed");

        private Material material;
        private CoroutineChain coroutineChain;

        private void Start()
        {
            coroutineChain = CoroutineChain.Create().WithLifetime(this);

            material = Instantiate(image.material);
            image.material = material;

            image.enabled = true;
            image.color = startColor;

            material.SetFloat(verticalWaveStrengthID, verticalWaveStrengths.Max);
            material.SetFloat(horizontalWaveStrengthID, horizontalWaveStrengths.Max);
            material.SetFloat(verticalWaveSpeedID, waveSpeeds.Max);
            material.SetFloat(horizontalWaveSpeedID, waveSpeeds.Max);

            var color = startButtonImage.color;
            color.a = 0;
            startButtonImage.color = color;

            color = startButtonBackgroundImage.color;
            color.a = 0;
            startButtonBackgroundImage.color = color;

            color = startButtonText.color;
            color.a = 0;
            startButtonText.color = color;

            var backgroundTween = TweenManager.CreateTween((t) =>
            {
                image.color = Color.Lerp(startColor, Color.white, t);

                material.SetFloat(verticalWaveStrengthID, verticalWaveStrengths.Lerp(t, true));
                material.SetFloat(horizontalWaveStrengthID, horizontalWaveStrengths.Lerp(t, true));
                material.SetFloat(verticalWaveSpeedID, waveSpeeds.Lerp(t, true));
                material.SetFloat(horizontalWaveSpeedID, waveSpeeds.Lerp(t, true));
            }, introTweenData);

            var textTween = TweenManager.CreateTween((t) =>
            {
                var color = startButtonImage.color;
                color.a = t;
                startButtonImage.color = color;

                color = startButtonBackgroundImage.color;
                color.a = t;
                startButtonBackgroundImage.color = color;

                color = startButtonText.color;
                color.a = t;
                startButtonText.color = color;
            });

            backgroundTween.Completed += textTween.Play;

            coroutineChain
                .WaitForSeconds(delayDuration)
                .Then(() => image.raycastTarget = false)
                .Then(backgroundTween.Play)
                .Run();
        }
    }
}
