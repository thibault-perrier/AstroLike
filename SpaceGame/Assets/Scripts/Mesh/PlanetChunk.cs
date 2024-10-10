using System.Collections.Generic;
using UnityEngine;

public class PlanetChunk : MonoBehaviour
{
    GenerationSettings settings;

    public void GenerateMesh(GenerationSettings settings)
    {
        this.settings = settings;

        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int x = 0; x < settings._chunkResolution; x++)
        {
            for (int z = 0; z < settings._chunkResolution; z++)
            {
                vertices.Add(GetLocalVertexPositionWithHeight(x, z));
                normals.Add(GetNormals(x, z));

                if (x == settings._chunkResolution - 1 || z == settings._chunkResolution - 1) continue;

                int cornerVertex = x + z * settings._chunkResolution;

                triangles.Add(cornerVertex);
                triangles.Add(cornerVertex + 1);
                triangles.Add(cornerVertex + settings._chunkResolution + 1);

                triangles.Add(cornerVertex);
                triangles.Add(cornerVertex + settings._chunkResolution + 1);
                triangles.Add(cornerVertex + settings._chunkResolution);
            }
        }


        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);

        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private Vector3 GetNormals(int x, int z)
    {
        Vector3[,] v = new Vector3[3, 3];

        for (int i = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                v[i, j] = GetLocalVertexPositionWithHeight(x - (i - 1), z - (j - 1));
            }
        }

        List<Vector3> quadNormals = new List<Vector3>();

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector3 t1n = GetTriangleNormal(v[i, j], v[i + 1, j], v[i + 1, j + 1]);
                Vector3 t2n = GetTriangleNormal(v[i, j], v[i + 1, j + 1], v[i, j + 1]);
                quadNormals.Add(GetQuadNormal(t1n, t2n));
            }
        }

        return quadNormals.Average().normalized;
    }

    private Vector3 GetTriangleNormal(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 edge1 = p3 - p1;
        Vector3 edge2 = p2 - p1;

        return Vector3.Cross(edge1, edge2);
    }

    private Vector3 GetQuadNormal(Vector3 n1, Vector3 n2)
    {
        return (n1 + n2) / 2;
    }

    private Vector3 GetLocalVertexPositionWithHeight(int x, int z)
    {
        float step = settings._meshSize / (settings._chunkResolution - 1);
        return new Vector3((x * step) - (settings._meshSize / 2), GetHeightByCoord(x, z), (z * step) - (settings._meshSize / 2));
    }

    private float GetHeightByCoord(int x, int z)
    {
        float height = 0.0f;

        float frequency = settings._perlinScale;
        float amplitude = 1.0f;

        Vector2 worldPos = getVertexWorldPos(x, z);

        for (int currentLayer = 0; currentLayer < settings._octave; currentLayer++)
        {
            height += Mathf.PerlinNoise(worldPos.x * frequency, worldPos.y * frequency) * amplitude;

            frequency /= settings._lacunarity;
            amplitude *= settings._persistance;
        }

        return height * settings._heightMultiplier;
    }


    private Vector2 getVertexWorldPos(int x, int z)
    {
        float step = settings._meshSize / (settings._chunkResolution - 1);
        Vector2 localPos = new Vector2(x * step, z * step);

        if (settings._voxelize)
        {
            localPos.x = (int)localPos.x;
            localPos.y = (int)localPos.y;
        }

        return localPos + transform.position.XZ();
    }
}
