using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D[] _textures;
    [SerializeField] private FaceExtractor _faceExtractor;
    public void SetRectTexture(int index) => _faceExtractor.ApplyTextureToQuad(_textures[index]);
    public void ZoomToFace() => _faceExtractor.ApplyTextureToQuad(_faceExtractor.CropTheMiddleFace());
}
