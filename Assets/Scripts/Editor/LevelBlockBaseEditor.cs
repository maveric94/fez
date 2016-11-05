using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelBlockBase))]
public class LevelBlockBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelBlockBase targetObject = (LevelBlockBase)target;

        targetObject.Size = EditorGUILayout.Vector3Field("Size", targetObject.Size);
        targetObject.MaterialType = (LevelBlockBase.BlockMaterialType)EditorGUILayout.EnumPopup("Material", targetObject.MaterialType);
        targetObject.ColliderEnabled = EditorGUILayout.Toggle("Collider enabled", targetObject.ColliderEnabled);
    }
}