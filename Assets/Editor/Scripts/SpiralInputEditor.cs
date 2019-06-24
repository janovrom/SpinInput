using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpiralInput))]
public class SpiralInputEditor : Editor
{

    private void OnEnable()
    {
        (target as SpiralInput).transform.hasChanged = false;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        var variableChanged = EditorGUI.EndChangeCheck();
        var input = (target as SpiralInput);

        if (GUILayout.Button("Generate buttons") || variableChanged)
        {
            Debug.Log("Clearing and generating buttons anew.");
            input.transform.hasChanged = false;
            input.RemoveChildren();
            input.CreateButtons();
            input.PlaceButtons();
        }

        if (GUILayout.Button("Clear buttons"))
        {
            input.RemoveChildren();
        }

        if (((target as SpiralInput).transform.hasChanged) && input.transform.childCount > 0)
        {
            input.transform.hasChanged = false;
            input.PlaceButtons();
        }

        if (GUILayout.Button("Flip right vector"))
        {
            input.FlipRightVector();
            input.PlaceButtons();
        }
    }

}
