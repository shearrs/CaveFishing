using UnityEngine;

namespace CaveFishing.Fishing
{
    [DefaultExecutionOrder(100)]
    public class FishingLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private Vector3 endOffset = new(0f, 0.15f, 0f);

        private void LateUpdate()
        {
            if (!start.gameObject.activeSelf || !end.gameObject.activeSelf)
            {
                lineRenderer.enabled = false;
                return;
            }

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, start.position);
            lineRenderer.SetPosition(1, end.position + endOffset);
        }
    }
}
