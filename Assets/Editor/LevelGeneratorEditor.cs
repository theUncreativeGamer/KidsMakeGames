using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
[CanEditMultipleObjects]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelGenerator theThing = (LevelGenerator)target;

        if(GUILayout.Button("Generate a new level"))
        {
            theThing.GenerateNewLevel();
        }
        if (GUILayout.Button("Destroy all levels"))
        {
            theThing.DestroyAllLevelsImmediately();
        }

    }
}
