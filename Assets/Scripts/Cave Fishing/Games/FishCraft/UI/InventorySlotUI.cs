using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private InventorySlot slot;
        [SerializeField] private Image spriteImage;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            slot.DataUpdated += OnDataUpdated;

            OnDataUpdated();
        }

        private void OnDisable()
        {
            slot.DataUpdated -= OnDataUpdated;
        }

        private void OnDataUpdated()
        {
            var sprite = slot.Item.Data.Sprite;
            string count = slot.Count == 0 ? string.Empty : slot.Count.ToString();

            if (sprite == null)
                spriteImage.enabled = false;
            else
                spriteImage.sprite = sprite;

            textMesh.text = count;
        }
    }
}
