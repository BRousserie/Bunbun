using UnityEditor;
using UnityEngine;

namespace Map
{
    [CustomEditor(typeof(MapManager))]
    public class MapManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("Generate"))
                ((MapManager) target).GenerateNewMap();
        }
    }
}