using Shears.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.QuickClickGame.UI
{
    public class ClickTargetUI : MonoBehaviour
    {
        [SerializeField] private ClickTarget target;
        [SerializeField] private ManagedUIElement button;
        private Canvas canvas;

        private void Awake()
        {
            target.PositionSet += OnPositionSet;
            canvas = GetComponentInParent<Canvas>();
        }

        private void OnPositionSet()
        {
            var position = target.Position;
            var displaySize = canvas.renderingDisplaySize;
            button.transform.position = new Vector2(position.x * displaySize.x, position.y * displaySize.y);
        }
    }
}
