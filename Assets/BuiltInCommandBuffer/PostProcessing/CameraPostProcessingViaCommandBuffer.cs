using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class CameraPostProcessingViaCommandBuffer : MonoBehaviour
{
    [SerializeField] private CameraEvent _cameraEvent = CameraEvent.AfterImageEffects;
    [SerializeField] private string _commandBufferName = "PostProcessing";
    [SerializeField] private Material _material;
    private Camera _camera;
    private CommandBuffer _cBuffer;

    private int _screenCopyID = Shader.PropertyToID("_MainTex");

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.forceIntoRenderTexture = true;
    }

    private void OnPreRender()
    {
        CreateCommandBuffer();
    }

    private void CreateCommandBuffer()
    {
        CommandBufferUtils.Destroy(_cBuffer, _commandBufferName, _cameraEvent, _camera);
        _cBuffer = CommandBufferUtils.CreateNew(_commandBufferName);

        // Attempt1();
        // Attempt2();
        Attempt3();

        _camera.AddCommandBuffer(_cameraEvent, _cBuffer);
    }

    // flips image vertically
    private void Attempt1()
    {
        // -1 means current screen size
        _cBuffer.GetTemporaryRT(_screenCopyID, -1, -1);
        _cBuffer.Blit(BuiltinRenderTextureType.CurrentActive, _screenCopyID);
        _cBuffer.Blit(_screenCopyID, BuiltinRenderTextureType.CurrentActive, _material);
        _cBuffer.ReleaseTemporaryRT(_screenCopyID);
    }
    
    // renders white screen
    private void Attempt2()
    {
        _cBuffer.Blit(BuiltinRenderTextureType.CurrentActive, BuiltinRenderTextureType.CurrentActive, _material);
    }
    
    private void Attempt3()
    {
        _cBuffer.GetTemporaryRT(_screenCopyID, -1, -1);
        //_cBuffer.SetRenderTarget(_screenCopyID);
        _cBuffer.Blit(BuiltinRenderTextureType.CameraTarget, _screenCopyID, _material);
        _cBuffer.Blit(_screenCopyID, BuiltinRenderTextureType.CameraTarget);
        _cBuffer.ReleaseTemporaryRT(_screenCopyID);
    }
}