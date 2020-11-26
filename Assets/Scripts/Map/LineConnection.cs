using System;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class LineConnection
    {
        public LineRenderer lineRenderer;
        public MapNode from;
        public MapNode to;

        public LineConnection(LineRenderer lineRenderer, MapNode from, MapNode to)
        {
            this.lineRenderer = lineRenderer;
            this.from = from;
            this.to = to;
        }

        public void SetColor(Color color)
        {
            for (int keyIndex = 0; keyIndex < lineRenderer.colorGradient.colorKeys.Length; keyIndex++)
                lineRenderer.colorGradient.colorKeys[keyIndex].color = color;
        }
    }
}