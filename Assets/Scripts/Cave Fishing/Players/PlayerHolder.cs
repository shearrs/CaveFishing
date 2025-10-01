using CaveFishing.Items;
using Shears;
using Shears.Detection;
using Shears.Input;
using UnityEngine;

namespace CaveFishing.Players
{
    public class PlayerHolder : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private AreaDetector3D detector;
        [SerializeField] private ItemHolder holder;
        [SerializeField] private PlayerFisher fisher;

        [Header("Settings")]
        [SerializeField] private float throwingForce = 50f;
        
        private IManagedInput interactInput;
        private Vector3 itemVelocity = Vector3.zero;
        private Vector3 previousItemPosition = Vector3.zero;
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

        private void Update()
        {
            var item = holder.HeldItem;

            if (item == null)
                return;

            itemVelocity = (item.transform.position - previousItemPosition);
            previousItemPosition = item.transform.position;
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
                Hold(item);
        }

        public void Hold(IItem item)
        {
            holder.Hold(item);

            itemVelocity = Vector3.zero;
            previousItemPosition = item.transform.position;
            previousLayer = item.gameObject.layer;
            item.gameObject.layer = heldItemLayer;
            item.transform.SetLayerOnAllChildren(heldItemLayer);

            fisher.Disable();
        }

        public void Release()
        {
            var item = holder.HeldItem;
            item.gameObject.layer = previousLayer;
            item.transform.SetLayerOnAllChildren(previousLayer);

            holder.Release(new(itemVelocity * throwingForce));

            fisher.Enable();
        }
    }
}
