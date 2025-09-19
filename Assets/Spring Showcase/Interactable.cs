using Shears.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace CaveFishing
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent interacted;

        public void Accept(IInteractor interactor)
        {
            interactor.Interact(this);
        }

        public void Invoke()
        {
            interacted.Invoke();
        }
    }
}
