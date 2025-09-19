using Shears.Tweens;
using System.Collections;
using UnityEngine;

namespace CaveFishing
{
    public class SpringButton : MonoBehaviour
    {
        private enum Mode { Teleport, Lerp, Tween }

        [SerializeField] private Transform moveTarget;
        [SerializeField] private Mode mode;
        [SerializeField] private bool test;

        [Header("Positions")]
        [SerializeField] private Vector3 offPosition;
        [SerializeField] private Vector3 onPosition;

        [Header("Movement Settings")]
        [SerializeField] private float moveTime = 0.5f;
        [SerializeField] private StructTweenData tweenData;

        private bool isEnabled = false;
        private Tween tween;

        private void Update()
        {
            if (test)
            {
                test = false;

                Toggle();
            }
        }

        public void Toggle()
        {
            if (isEnabled)
                Disable();
            else
                Enable();
        }

        public void Enable()
        {
            isEnabled = true;

            MoveTo(onPosition);
        }

        public void Disable()
        {
            isEnabled = false;

            MoveTo(offPosition);
        }

        private void MoveTo(Vector3 pos)
        {
            StopAllCoroutines();
            tween?.Dispose();

            if (mode == Mode.Teleport)
                moveTarget.localPosition = pos;
            else if (mode == Mode.Lerp)
                StartCoroutine(IEMoveTo(moveTarget.parent.TransformPoint(pos)));
            else if (mode == Mode.Tween)
                tween = moveTarget.DoMoveTween(moveTarget.parent.TransformPoint(pos), tweenData);
        }

        private IEnumerator IEMoveTo(Vector3 position)
        {
            Vector3 start = moveTarget.position;
            float elapsedTime = 0;
            
            while (elapsedTime < moveTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / moveTime;

                moveTarget.position = Vector3.Lerp(start, position, t);

                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (moveTarget == null)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(moveTarget.parent.TransformPoint(onPosition), 0.25f);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(moveTarget.parent.TransformPoint(offPosition), 0.25f);
        }
    }
}
