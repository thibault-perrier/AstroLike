using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenPlanet : MonoBehaviour
{
    [SerializeField] private GenerationSettings settings;

    [Header("Prefabs")]
    [SerializeField] private GameObject _planetChunkPrefab;

    private List<GameObject> _planetChunks;
    public void GeneratePlanet()
    {
        Vector3 planetPos = transform.localPosition;
        Quaternion planetRot = transform.localRotation;

        Vector3 facePos;
        Quaternion faceRot;

        List<Vector3> posOffset = new List<Vector3>
        {
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1)
        };

        List<Vector3> rotList = new List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(180, 0, 0),
            new Vector3(90, 90, 0),
            new Vector3(-90, 90, 0),
            new Vector3(0, 90, 90),
            new Vector3(0, 90, -90)
        };

        // Create 6 meshes for each cube face
        for (int faceIndex = 0; faceIndex < 6; faceIndex++)
        {
            facePos = planetPos + (posOffset[faceIndex] * (settings._meshSize / 2));
            PlanetChunk planetChunk = Instantiate(_planetChunkPrefab, facePos, planetRot, transform).GetComponent<PlanetChunk>();

            planetChunk.transform.Rotate(rotList[faceIndex]);




            planetChunk.GenerateMesh(settings);
            _planetChunks.Add(planetChunk.gameObject);
        }
    }

    public void DestroyPlanet()
    {
        foreach (GameObject go in _planetChunks)
        {
            if (Application.isPlaying)
                Destroy(go);
            else
                DestroyImmediate(go);
        }
    }
}
