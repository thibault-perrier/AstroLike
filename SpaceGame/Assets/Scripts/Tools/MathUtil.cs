using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class MathUtil
{
    public static Vector2 XZ(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public static Vector3 Average(this List<Vector3> v3list)
    {
        return v3list.Aggregate((a, v3) => a + v3) / v3list.Count;
    }
}
