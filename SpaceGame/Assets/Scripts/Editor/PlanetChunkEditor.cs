// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(PlanetChunk))]
// public class PlanetChunkEditor : Editor
// {
//     private Vector3 oldPos;

//     public override void OnInspectorGUI()
//     {
//         PlanetChunk chunk = target as PlanetChunk;

//         EditorGUI.BeginChangeCheck();

//         base.OnInspectorGUI();

//         if (EditorGUI.EndChangeCheck() || oldPos != chunk.transform.position)
//             chunk.GenerateMesh();

//         oldPos = chunk.transform.position;

//         if (GUILayout.Button("Regenerate Mesh")) chunk.GenerateMesh();
//     }
// }
