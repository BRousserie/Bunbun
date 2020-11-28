using UnityEngine;

namespace Map
{
    public class DottedLineRenderer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private new Renderer renderer;
        public bool scaleInUpdate;

        private void Start()
        {
            ScaleMaterial();
            enabled = scaleInUpdate;
        }

        public void ScaleMaterial()
        {
            lineRenderer = GetComponent<LineRenderer>();
            renderer = GetComponent<Renderer>();
            UpdateMainTextureScale();
        }

        private void Update()
        {
            UpdateMainTextureScale();
        }

        private void UpdateMainTextureScale()
        {
            renderer.material.mainTextureScale = new Vector2(
                Vector2.Distance(lineRenderer.GetPosition(0),
                    lineRenderer.GetPosition(lineRenderer.positionCount - 1)) / lineRenderer.widthMultiplier, 1);
        }
    }
}