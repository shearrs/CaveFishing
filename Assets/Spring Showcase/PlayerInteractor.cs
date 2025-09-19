using Shears.Detection;
using Shears.Input;
using Shears.Interaction;
using UnityEngine;

namespace CaveFishing
{
    public class PlayerInteractor : Interactor<Interactable>
    {
        [Header("Input")]
        [SerializeField] private ManagedInputMap inputMap;

        [Header("Detection")]
        [SerializeField] private AreaDetector3D detector;

        private IManagedInput interactInput;

        private void Awake()
        {
            interactInput = inputMap.GetInput("Interact");
        }

        private void OnEnable()
        {
            interactInput.Performed += OnInteractInput;
        }

        private void OnDisable()
        {
            interactInput.Performed -= OnInteractInput;
        }

        private void OnInteractInput(ManagedInputInfo info) => TryInteract();

        private void TryInteract()
        {
            if (!detector.Detect())
                return;

            for (int i = 0; i < detector.Hits; i++)
            {
                var collider = detector.GetDetection(i);

                if (!collider.TryGetComponent(out Interactable interactable))
                    continue;

                interactable.Accept(this);

                break;
            }
        }

        public override void TypeInteract(Interactable interactable)
        {
            interactable.Invoke();
        }
    }
}
