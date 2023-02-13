using UnityEngine;

public static class VectorExtensions
{
    /// <summary>
    /// Return an inversion of the given vector
    /// </summary>
    public static Vector3 Invert(this Vector3 vec) => new Vector3(1f / vec.x, 1f / vec.y, 1f / vec.z);
}