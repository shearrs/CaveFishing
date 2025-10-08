using UnityEngine;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class ItemHolderUI : MonoBehaviour
    {
        [SerializeField] private ItemHolder holder;
        [SerializeField] private MeshRenderer block;

        private void OnEnable()
        {
            holder.Enabled += OnEnabled;
            holder.HeldItemChanged += OnHeldItemChanged;

            UpdateVisual(holder.HeldItem);
        }

        private void OnDisable()
        {
            holder.Enabled -= OnEnabled;
            holder.HeldItemChanged -= OnHeldItemChanged;
        }

        private void OnEnabled()
        {
            UpdateVisual(holder.HeldItem);
        }

        private void OnHeldItemChanged(Item item)
        {
            UpdateVisual(item);
        }

        private void UpdateVisual(Item item)
        {
            if (item == null)
                block.gameObject.SetActive(false);
            else
            {
                block.gameObject.SetActive(true);
                block.material.mainTexture = item.Data.Texture;
            }
        }
    }
}
