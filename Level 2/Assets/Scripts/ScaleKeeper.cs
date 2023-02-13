using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleKeeper : MonoBehaviour, IResizable
{
    private Vector3 _defaultSize;
    private float _decreaseScaleFactor = 0.7f;
    private void Start()
    {
        SetDefaultSize();
    }


    /// <summary>
    /// Updates the size (inverted to keep object's scale) based on Slice.expansionPercent. 
    /// </summary>
    public void UpdateSize(Vector3 size)
    {
        transform.localScale = Vector3.Lerp(
            _defaultSize,
            _decreaseScaleFactor * size.z * size.Invert(),
            Slice.expansionPercent
        );  
    }
    public void SetDefaultSize() => _defaultSize = transform.localScale;


}
