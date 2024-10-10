using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static float Normalize(this float val)
    {
        return val > 0.0f ? 1.0f : (val < 0.0f ? -1.0f : 0.0f);
    }
}
