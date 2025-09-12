using Shears.Beziers;
using UnityEngine;

namespace CaveFishing.Fishing
{
    [DefaultExecutionOrder(100)]
    public class FishingLine : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Bezier bezier;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        [Header("Settings")]
        [SerializeField, Range(2, 32)] private int resolution;
        [SerializeField] private Vector3 endOffset = new(0f, 0.15f, 0f);

        private void Awake()
        {
            bezier.AddPoint(transform.position);
            bezier.AddPoint(transform.position);
            bezier.SetLocalTangent2(0, Vector3.down);
            bezier.SetLocalTangent2(1, Vector3.up * 0.1f);
        }

        private void OnValidate()
        {
            if (lineRenderer == null)
                return;

            lineRenderer.positionCount = resolution;
        }

        private void LateUpdate()
        {
            if (!start.gameObject.activeSelf || !end.gameObject.activeSelf)
            {
                lineRenderer.enabled = false;
                return;
            }

            lineRenderer.enabled = true;

            bezier.SetPosition(0, start.position);
            bezier.SetPosition(1, end.position + endOffset);

            for (int i = 0; i < resolution; i++)
            {
                float t = (float)i / (resolution - 1);
                Vector3 pos = bezier.Sample(t);

                lineRenderer.SetPosition(i, pos);
            }
        }
    }
}
