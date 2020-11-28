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
            Gradient gradient = lineRenderer.colorGradient;
            GradientColorKey[] colorKeys = gradient.colorKeys;
            
            for (int key = 0; key < colorKeys.Length; key++)
                colorKeys[key].color = color;

            gradient.colorKeys = colorKeys;
            lineRenderer.colorGradient = gradient;
        }
    }
}