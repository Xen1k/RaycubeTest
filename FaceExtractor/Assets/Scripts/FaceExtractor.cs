using UnityEngine;
using System.Collections.Generic;
using DlibFaceLandmarkDetector;

[RequireComponent(typeof(Renderer))]
public class FaceExtractor : MonoBehaviour
{
    [SerializeField] private Texture2D _texture2D;
    private static readonly string _dlibShapePredictorFileName = "DlibFaceLandmarkDetector/sp_human_face_68.dat";

    private void Start()
    {
        ApplyTextureToQuad(_texture2D);
    }
    
    /// <summary>
    /// Scans the texutre on the plane, crops the most middle face and returns the texture with it
    /// </summary>
    public Texture2D CropTheMiddleFace()
    {
        List<Rect> foundFaces = FindFaces();
        if (foundFaces.Count == 0)
            Debug.LogWarning("No faces found");
        return CropTexture(_texture2D, GetFaceInTheMiddle(foundFaces));
    }

    
    /// <summary>
    /// Crops the given texture using the given rect
    /// </summary>
    private Texture2D CropTexture(Texture2D sourceTexture, Rect cropRect)
    {
        int margin = 100;
        cropRect.width += margin / 2;
        cropRect.height += margin / 2;
        cropRect.xMin -= margin / 2;
        cropRect.yMin -= margin / 2;

        var newPixels = sourceTexture.GetPixels(
                            (int)cropRect.xMin,
                            (int)cropRect.yMin,
                            (int)cropRect.width,
                            (int)cropRect.height
                        );

        var newTexture = new Texture2D((int)cropRect.width, (int)cropRect.height);
        newTexture.SetPixels(newPixels);
        newTexture.Apply();

        return newTexture;
    }

    /// <summary>
    /// Returns distance from the center of the image to the given point
    /// </summary>
    float GetImageCenterToPointDistance(Vector2 point)
            => Vector2.Distance(point, new Vector2(_texture2D.width / 2, _texture2D.height / 2));

    /// <summary>
    /// Returns the closest to the middle face (faces are represented by rects) from the given array
    /// </summary>
    private Rect GetFaceInTheMiddle(List<Rect> faceRects)
    {
        float minCenterToPointDistance = float.MaxValue;
        Rect faceInTheMiddle = faceRects[0];

        foreach (Rect faceRect in faceRects)
        {
            float imageCenterToPointDistance = GetImageCenterToPointDistance(faceRect.center);
            if (imageCenterToPointDistance >= minCenterToPointDistance)
                continue;
            minCenterToPointDistance = imageCenterToPointDistance;
            faceInTheMiddle = faceRect;
        }
        return faceInTheMiddle;
    }

    /// <summary>
    /// Applies texture to quad and scales it properly
    /// </summary>
    public void ApplyTextureToQuad(Texture2D texture2D)
    {
        _texture2D = texture2D;
        GetComponent<Renderer>().material.mainTexture = texture2D;
        transform.localScale = new Vector3(texture2D.width, texture2D.height, 1);

        float quadWidth = gameObject.transform.localScale.x;
        float quadHeight = gameObject.transform.localScale.y;
        float widthScale = Screen.width / quadWidth;
        float heightScale = Screen.height / quadHeight;

        if (widthScale < heightScale)
            Camera.main.orthographicSize = quadWidth * Screen.height / Screen.width / 2;
        else
            Camera.main.orthographicSize = quadHeight / 2;
    }

    /// <summary>
    /// Finds all the faces on the texture assigned to the quad
    /// </summary>
    private List<Rect> FindFaces()
    {
        FaceLandmarkDetector faceLandmarkDetector = new FaceLandmarkDetector(_dlibShapePredictorFileName);
        faceLandmarkDetector.SetImage(_texture2D);

        return faceLandmarkDetector.Detect();
    }

}
