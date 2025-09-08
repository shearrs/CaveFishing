using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private Bobber bobber;
        [SerializeField] private Vector3 castPosition;
        [SerializeField] private float castForce = 5f;

        private Vector3 CastPosition => transform.TransformPoint(castPosition);

        public void Cast()
        {
            bobber.gameObject.SetActive(true);
            bobber.Cast(CastPosition, castForce * transform.forward);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CastPosition, 0.2f);
        }
    }
}
