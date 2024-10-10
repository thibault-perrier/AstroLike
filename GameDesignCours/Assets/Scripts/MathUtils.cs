using UnityEditor;
using UnityEngine;

public static class MathUtils
{
    public static Vector2 GetRandomPosOnAreaWithCorners(float x1, float y1, float x2, float y2)
    {
        // float minX = Mathf.Min(x1, x2);
        // float minY = Mathf.Min(y1, y2);

        // float rangeX = Mathf.Abs(x1 - x2);
        // float rangeY = Mathf.Abs(y1 - y2);


        float randX = Random.Range(x1, x2);
        float randY = Random.Range(y1, y2);

        return new Vector2(randX, randY);
    }
}
