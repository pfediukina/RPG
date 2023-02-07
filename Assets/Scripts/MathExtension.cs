using UnityEngine;

public static class MathExtension
 {
    public static bool IsBetweenRange(float value, float range1, float range2)
    {
        return value >= Mathf.Min(range1, range2) && value <= Mathf.Max(range1, range2);
    }
 }