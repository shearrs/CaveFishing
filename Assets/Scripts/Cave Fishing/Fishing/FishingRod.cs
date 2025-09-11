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
        [SerializeField] private float minForwardCastForce = 0.5f;
        [SerializeField] private float maxForwardCastForce = 3f;
        [SerializeField] private float minUpCastForce = 0.5f;
        [SerializeField] private float maxUpCastForce = 3f;

        [Header("Tween Settings")]
        [SerializeField] private float releaseRotation = 20f;
        [SerializeField] private float chargeRotation = -24f;
        [SerializeField, Range(0f, 1f)] private float releaseTime = 0.85f;
        [SerializeField] private StructTweenData chargeTweenData;
        [SerializeField] private StructTweenData releaseTweenData;

        private Tween tween;

        private void Awake()
        {
            bobber.gameObject.SetActive(true);
            bobber.transform.SetParent(null);
            bobber.gameObject.SetActive(false);
        }

        public void BeginCharging()
        {
            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = chargeRotation;

            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, chargeTweenData);
        }

        public void Cast()
        {
            float chargeProgress = tween.Progress;
            float forwardForce = Mathf.Lerp(minForwardCastForce, maxForwardCastForce, chargeProgress);
            float upForce = Mathf.Lerp(minUpCastForce, maxUpCastForce, chargeProgress);

            Vector3 eulerRotation = transform.localEulerAngles;
            eulerRotation.x = releaseRotation;

            tween?.Dispose();
            tween = transform.DoRotateLocalTween(Quaternion.Euler(eulerRotation), true, releaseTweenData);
            tween.AddEvent(releaseTime, () => OnCastTweenComplete(forwardForce, upForce));
        }

        private void OnCastTweenComplete(float forwardForce, float upForce)
        {
            Vector3 forward = castPoint.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 force = forwardForce * forward + upForce * castPoint.up;

            bobber.gameObject.SetActive(true);
            bobber.Cast(castPoint.position, force);
        }
    }
}
