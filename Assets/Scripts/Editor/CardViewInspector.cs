using System.Collections;
using System.Collections.Generic;
using Cards;
using Map;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardView), true)]
public class CardViewInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("DissolveOut"))
            ((CardView) target).DissolveOut();
        if (GUILayout.Button("DissolveIn"))
            ((CardView) target).DissolveIn();
    }
}
