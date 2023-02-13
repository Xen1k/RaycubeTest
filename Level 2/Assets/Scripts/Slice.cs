using System.Collections;
using UnityEngine;

public class Slice : MonoBehaviour, IResizable
{
    [System.Serializable] enum Position { BottomLeft, BottomRight, TopLeft, TopRight }
    [SerializeField] private Position _relativePosition;

    /// <summary>
    /// Lists of the children objects which scale should be keeped on parent's transform
    /// </summary>
    [SerializeField] private ScaleKeeper[] _keepScaleObjects;

    #region EXPANSION
    /// <summary>
    /// Slice that is currently expanded
    /// </summary>
    public static Slice expandedSlice;
    private readonly static float _expansionSpeedStart = 2.5f;
    private readonly static float _expansionSpeedEnd = 0.03f;
    private static float _currentExpansionSpeed = _expansionSpeedStart;
    /// <summary>
    /// Static expansion percent which is the same for all the slices
    /// </summary>
    public static float expansionPercent = 0;
    private Coroutine _expansionCoroutine;
    #endregion

    #region SIZES
    /// <summary>
    /// Size before a new expansion coroutine starts
    /// </summary>
    private Vector3 _defaultSize;
    /// <summary>
    /// Size of the slice on the start of the program
    /// </summary>
    public static float startSize;
    public readonly static float expandedSize = 1.5f;
    public readonly static float shrinkedSize = 0.5f;
    #endregion

    private void Start()
    {
        startSize = transform.localScale.x;
        SetDefaultSize();
    }

    /// <summary>
    /// Calls the expand coroutine based on given parameters for each slice
    /// </summary>
    public void Expand(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topRight, Vector3 topLeft, System.Action callback = null)
    {
        StopPreviousExpansion();
        _expansionCoroutine = _relativePosition switch
        {
            Position.BottomLeft => StartCoroutine(Expand(bottomLeft, callback)),
            Position.BottomRight => StartCoroutine(Expand(bottomRight)),
            Position.TopRight => StartCoroutine(Expand(topRight)),
            Position.TopLeft => StartCoroutine(Expand(topLeft))
        };

    }
    
    /// <summary>
    /// Stops last called expansion and updates necessary values 
    /// </summary>
    private void StopPreviousExpansion()
    {
        if (_expansionCoroutine != null)
            StopCoroutine(_expansionCoroutine);
        expansionPercent = 0;
        _currentExpansionSpeed = _expansionSpeedStart;
        SetDefaultSize();
        foreach (var keepScaleObject in _keepScaleObjects)
            keepScaleObject.SetDefaultSize();
    }

    /// <summary>
    /// Gradually changes slice value to the given size
    /// </summary>
    public IEnumerator Expand(Vector3 expandedSize, System.Action callback = null)
    {
        while (expansionPercent < 1)
        {
            expansionPercent += Time.deltaTime * _currentExpansionSpeed;
            yield return null;
            UpdateSize(expandedSize);
            foreach (var keepScaleObject in _keepScaleObjects)
                keepScaleObject.UpdateSize(expandedSize);
            _currentExpansionSpeed = Mathf.Lerp(_expansionSpeedStart, _expansionSpeedEnd, expansionPercent);
        }
        callback?.Invoke();
    }

    public void SetDefaultSize() => _defaultSize = transform.localScale;

    /// <summary>
    /// Updates slice's localscale based on the current expansionPercent and given target size
    /// </summary>
    public void UpdateSize(Vector3 size)
    {
        transform.localScale = Vector3.Lerp(_defaultSize, size, expansionPercent);
    }
}
