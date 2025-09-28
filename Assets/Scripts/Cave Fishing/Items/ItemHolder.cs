using UnityEngine;

namespace CaveFishing.Items
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Vector3 holdOffset = Vector3.zero;
        private IItem heldItem;

        public IItem HeldItem => heldItem;

        public void Hold(IItem item)
        {
            if (heldItem != null)
                return;

            heldItem = item;

            heldItem.Hold();

            heldItem.transform.SetParent(transform);
            heldItem.transform.position = transform.TransformPoint(holdOffset);
        }

        public void Release()
        {
            if (heldItem == null)
                return;

            heldItem.transform.SetParent(null);
            heldItem.Release();

            heldItem = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.TransformPoint(holdOffset), 0.15f);
        }
    }
}
