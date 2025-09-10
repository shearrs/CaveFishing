using Shears;
using Shears.Tweens;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class FishingRod : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Bobber bobber;
        [SerializeField] private Transform castPoint;

        [Header("Cast Force Settings")]
        [SerializeField] private float forwardCastForce = 10f;
        [SerializeField] private float upCastForce = 5f;

        [Header("Tween Settings")]
        [SerializeField] private float releaseRotation = 20f;
        [SerializeField] private float chargeRotation = -24f;
        [SerializeField] private StructTweenData chargeTweenData;
        [SerializeField] private StructTweenData releaseTweenData;

        private Tween tween;

        public void BeginCharging()
        {
            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = chargeRotation;

            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, chargeTweenData);
        }

        public void Cast()
        {
            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = releaseRotation;

            tween?.Dispose();
            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, releaseTweenData);
            tween.AddOnComplete(OnCastTweenComplete);
        }

        private void OnCastTweenComplete()
        {
            Vector3 forward = castPoint.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 force = forwardCastForce * forward + upCastForce * castPoint.up;

            bobber.gameObject.SetActive(true);
            bobber.Cast(castPoint.position, force);
        }
    }
}
