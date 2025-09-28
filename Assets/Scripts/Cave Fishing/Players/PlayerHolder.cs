using CaveFishing.Items;
using Shears.Detection;
using Shears.Input;
using UnityEngine;

namespace CaveFishing.Players
{
    public class PlayerHolder : MonoBehaviour
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private AreaDetector3D detector;
        [SerializeField] private ItemHolder holder;
        
        private IManagedInput interactInput;
        private int heldItemLayer;
        private int previousLayer;

        private void Awake()
        {
            heldItemLayer = LayerMask.NameToLayer("HeldItem");

            interactInput = inputProvider.GetInput("Interact");
        }

        private void OnEnable()
        {
            interactInput.Performed += OnInteractInput;
        }

        private void OnDisable()
        {
            interactInput.Performed -= OnInteractInput;
        }

        private void OnInteractInput(ManagedInputInfo info)
        {
            if (holder.HeldItem != null)
            {
                Release();
                return;
            }

            if (!detector.Detect())
                return;

            if (detector.TryGetDetection<IItem>(out var item, true))
            {
                Debug.Log("found item");
                Hold(item);
            }
        }

        public void Hold(IItem item)
        {
            holder.Hold(item);

            previousLayer = item.gameObject.layer;
            item.gameObject.layer = heldItemLayer;
        }

        public void Release()
        {
            holder.HeldItem.gameObject.layer = previousLayer;

            holder.Release();
        }
    }
}
