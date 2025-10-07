using UnityEngine;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private GameObject rootContainer;

        private void OnEnable()
        {
            inventory.Opened += OnOpened;
            inventory.Closed += OnClosed;
        }

        private void OnDisable()
        {
            inventory.Opened -= OnOpened;
            inventory.Closed -= OnClosed;
        }

        private void OnOpened()
        {
            rootContainer.SetActive(true);
        }

        private void OnClosed()
        {
            rootContainer.SetActive(false);
        }
    }
}
