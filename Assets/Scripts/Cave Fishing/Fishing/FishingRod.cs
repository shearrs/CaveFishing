using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private Bobber bobber;
        [SerializeField] private Vector3 castPosition;

        private Vector3 CastPosition => transform.TransformPoint(castPosition);

        public void Cast()
        {
            bobber.gameObject.SetActive(true);
            bobber.SetPosition(CastPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CastPosition, 0.35f);
        }
    }
}
