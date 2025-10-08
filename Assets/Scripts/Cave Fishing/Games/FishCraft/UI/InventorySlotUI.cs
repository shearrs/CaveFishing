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
            var item = slot.Item;

            if (item == null || item.Data == null)
            {
                spriteImage.enabled = false;
                textMesh.text = string.Empty;
            }
            else
            {
                string count = slot.Count == 1 ? string.Empty : slot.Count.ToString();

                spriteImage.enabled = true;
                spriteImage.sprite = item.Data.Sprite;
                textMesh.text = count;
            }
        }
    }
}
