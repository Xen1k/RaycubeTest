using UnityEngine;

public static class ExpansionDataDefaults
{
    /// <summary>
    /// Vector where all the coordiantes are equal to shrinked stated size
    /// </summary>
    public readonly static Vector3 shrinked =
        new Vector3(Slice.shrinkedSize, Slice.shrinkedSize, Slice.shrinkedSize);
    /// <summary>
    /// Vector where all the coordiantes are equal to expanded stated size
    /// </summary>
    public readonly static Vector3 expanded =
        new Vector3(Slice.expandedSize, Slice.expandedSize, Slice.expandedSize);
    /// <summary>
    /// Vector where x equals to shrinked size and y to expanded
    /// </summary>
    public readonly static Vector3 expandedHeight =
        new Vector3(Slice.shrinkedSize, Slice.expandedSize, Slice.shrinkedSize);
    /// <summary>
    /// Vector where x equals to expanded size and y to shrinked
    /// </summary>
    public readonly static Vector3 expandedWidth =
        new Vector3(Slice.expandedSize, Slice.shrinkedSize, Slice.shrinkedSize);
    /// <summary>
    /// Vector where all the coordiantes are equal to default start size
    /// </summary>
    public readonly static Vector3 defaultSize =
        new Vector3(Slice.startSize, Slice.startSize, Slice.startSize);
}
