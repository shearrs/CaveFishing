using UnityEngine;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class ItemHolderUI : MonoBehaviour
    {
        [SerializeField] private ItemHolder holder;
        [SerializeField] private MeshRenderer block;
        [SerializeField] private MeshRenderer heldItem;
        [SerializeField] private Transform heldItemPivot;

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
            if (item == null || item.Data.Texture == null)
            {
                block.gameObject.SetActive(false);
                heldItem.gameObject.SetActive(false);
            }
            else if (item.Data.IsBlock)
            {
                heldItem.gameObject.SetActive(false);
                block.gameObject.SetActive(true);
                block.material.mainTexture = item.Data.Texture;
            }
            else
            {
                block.gameObject.SetActive(false);
                heldItem.gameObject.SetActive(true);
                heldItemPivot.transform.localRotation = Quaternion.Euler(item.Data.DisplayRotation);
                heldItem.material.mainTexture = item.Data.Texture;
            }
        }
    }
}
