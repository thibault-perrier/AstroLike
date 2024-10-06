using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenPlanet))]
public class GenPlanetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GenPlanet genPlanet = target as GenPlanet;

        GUILayout.Space(15);

        if (GUILayout.Button("Generate Planet")) genPlanet.GeneratePlanet();
        if (GUILayout.Button("Destroy Planet")) genPlanet.DestroyPlanet();
    }
}
