using UnityEngine;

namespace CaveFishing.Fishing
{
    [DefaultExecutionOrder(100)]
    public class FishingLine : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        [Header("Settings")]
        [SerializeField] private Vector3 endOffset = new(0f, 0.15f, 0f);

        private void LateUpdate()
        {
            if (!start.gameObject.activeSelf || !end.gameObject.activeSelf)
            {
                lineRenderer.enabled = false;
                return;
            }

            lineRenderer.enabled = true;

            Vector3 startPos = start.position;
            Vector3 startT = startPos + (Vector3.down);
            Vector3 endPos = end.position + endOffset;

            Vector3 t1 = Vector3.Lerp(startPos, startT, 0.5f);
            Vector3 t2 = Vector3.Lerp(startT, endPos, 0.5f);
            Vector3 t3 = Vector3.Lerp(t1, t2, 0.5f);

            lineRenderer.SetPosition(0, start.position);
            lineRenderer.SetPosition(1, t3);
            lineRenderer.SetPosition(2, end.position + endOffset);
        }
    }
}
