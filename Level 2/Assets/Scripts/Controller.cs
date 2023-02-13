using UnityEngine;
using System.Linq;

public class Controller : MonoBehaviour
{
    public Slice[] slices;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ExpandBottomLeft();
        else if (Input.GetKeyDown(KeyCode.G))
            ExpandBottomRight();
        else if (Input.GetKeyDown(KeyCode.B))
            ExpandTopRight();
        else if (Input.GetKeyDown(KeyCode.Y))
            ExpandTopLeft();
        else if (Input.GetKeyDown(KeyCode.Q))
            SetDefaultSize();
    }
    
    /// <summary>
    /// Resizes each slice to default size
    /// </summary>
    /// <param name="callback">Callback which is called after the expansion</param>
    private void SetDefaultSize(System.Action callback = null)
    {
        Slice.expandedSlice = null;
        slices.ToList().ForEach(slice => slice.Expand(
                ExpansionDataDefaults.defaultSize,
                ExpansionDataDefaults.defaultSize,
                ExpansionDataDefaults.defaultSize,
                ExpansionDataDefaults.defaultSize,
                callback
            ));
    }

    /// <summary>
    /// Expands bottom left slice and fits the others
    /// </summary>
    private void ExpandBottomLeft()
    {
        // Fixes the overlapping bug
        if (Slice.expandedSlice == slices[2])
        {
            SetDefaultSize(ExpandBottomLeft);
            return;
        }
        Slice.expandedSlice = slices[0];
        slices.ToList().ForEach(slice => slice.Expand(
                ExpansionDataDefaults.expanded, ExpansionDataDefaults.shrinked,
                ExpansionDataDefaults.expandedHeight, ExpansionDataDefaults.expandedWidth
           ));
    }

    /// <summary>
    /// Expands bottom right slice and fits the others
    /// </summary>
    private void ExpandBottomRight()
    {
        Slice.expandedSlice = slices[1];
        slices.ToList().ForEach(slice => slice.Expand(
                ExpansionDataDefaults.shrinked, ExpansionDataDefaults.expanded,
                ExpansionDataDefaults.expandedWidth, ExpansionDataDefaults.expandedHeight
            ));
    }

    /// <summary>
    /// Expands top right slice and fits the others
    /// </summary>
    private void ExpandTopRight()
    {
        // Fixes the overlapping bug
        if (Slice.expandedSlice == slices[0])
        {
            SetDefaultSize(ExpandTopRight);
            return;
        }
        Slice.expandedSlice = slices[2];
        slices.ToList().ForEach(slice => slice.Expand(
               ExpansionDataDefaults.expandedWidth, ExpansionDataDefaults.shrinked,
               ExpansionDataDefaults.expanded, ExpansionDataDefaults.expandedHeight
           ));
    }

    /// <summary>
    /// Expands top left slice and fits the others
    /// </summary>
    private void ExpandTopLeft()
    {
        Slice.expandedSlice = slices[3];
        slices.ToList().ForEach(slice => slice.Expand(
               ExpansionDataDefaults.expandedWidth, ExpansionDataDefaults.expandedHeight,
               ExpansionDataDefaults.shrinked, ExpansionDataDefaults.expanded
           ));
    }

}
