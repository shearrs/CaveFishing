using Shears;
using TMPro;
using UnityEngine;

namespace CaveFishing.Games.FishTypingGame.UI
{
    public class WordFishUI : MonoBehaviour
    {
        [SerializeField] private WordFish fish;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Range<float> movementRange;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            fish.ProgressUpdated += OnProgressUpdated;
            fish.WordUpdated += OnWordUpdated;
        }

        private void OnDisable()
        {
            fish.ProgressUpdated -= OnProgressUpdated;
            fish.WordUpdated -= OnWordUpdated;
        }

        private void OnProgressUpdated(float progress)
        {
            Vector2 position = rectTransform.localPosition;
            position.x = Mathf.Lerp(movementRange.Max, movementRange.Min, progress);

            rectTransform.localPosition = position;
        }

        private void OnWordUpdated(string word)
        {
            textMesh.text = word;
        }    
    }
}
