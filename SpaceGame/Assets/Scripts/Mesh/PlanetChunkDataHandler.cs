using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct GenerationSettings
{

    [Header("Mesh Options")]
    [SerializeField, Range(1, 256)] public int _chunkResolution;
    [SerializeField, Range(1, 1024)] public float _meshSize;
    [SerializeField] public bool _voxelize;

    [Header("Noise Parameters")]
    [SerializeField, Range(1, 16)] public int _octave;
    [SerializeField, Range(0.002f, 1.0f)] public float _lacunarity;
    [SerializeField, Range(0.001f, 1.0f)] public float _persistance;
    [SerializeField] public float _perlinScale;

    [Header("Height Multiplier")]
    [SerializeField] public float _heightMultiplier;
}



public class PlanetChunkDataHandler : MonoBehaviour
{
    [SerializeField] private GenerationSettings settings;

    void OnValidate()
    {
        UpdateParameters();
    }

    public void UpdateParameters()
    {
        PlanetChunk[] chunks = GetComponentsInChildren<PlanetChunk>();

        foreach (PlanetChunk chunk in chunks)
        {
            chunk.GenerateMesh(settings);
        }
    }
}
