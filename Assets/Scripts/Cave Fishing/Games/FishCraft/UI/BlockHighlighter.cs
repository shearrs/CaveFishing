using UnityEngine;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class BlockHighlighter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Block block;
        [SerializeField] private MeshRenderer highlight;

        [Header("Settings")]
        [SerializeField] private Color startColor = Color.white;
        [SerializeField] private Color endColor = Color.black;

        private void OnEnable()
        {
            block.HoverBegan += OnHoverBegan;
            block.HoverEnded += OnHoverEnded;
            block.BreakAmountUpdated += OnBreakAmountUpdated;
        }

        private void OnDisable()
        {
            block.HoverBegan -= OnHoverBegan;
            block.HoverEnded -= OnHoverEnded;
            block.BreakAmountUpdated -= OnBreakAmountUpdated;
        }

        private void OnHoverBegan()
        {
            highlight.gameObject.SetActive(true);
        }

        private void OnHoverEnded()
        {
            highlight.gameObject.SetActive(false);
        }

        private void OnBreakAmountUpdated(float t)
        {
            highlight.material.color = Color.Lerp(startColor, endColor, t);
        }
    }
}
