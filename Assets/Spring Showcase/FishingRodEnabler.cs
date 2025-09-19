using UnityEngine;

namespace CaveFishing
{
    public class FishingRodEnabler : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        public void Toggle()
        {
            target.SetActive(!target.activeSelf);
        }

        public void Enable()
        {
            target.SetActive(true);
        }

        public void Disable()
        {
            target.SetActive(false);
        }
    }
}
