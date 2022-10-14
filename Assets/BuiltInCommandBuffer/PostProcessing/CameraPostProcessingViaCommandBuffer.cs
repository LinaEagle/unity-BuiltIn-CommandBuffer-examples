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
    }

    private void OnPreRender()
    {
        CreateCommandBuffer();
    }

    private void CreateCommandBuffer()
    {
        DestroyCommandBuffer();
        InitializeCommandBuffer();

        //// vertically mirrored, BuiltinRenderTextureType.CurrentActive
        //_cBuffer.GetTemporaryRT(_screenCopyID, -1, -1);
        ////_cBuffer.Blit(BuiltinRenderTextureType.CurrentActive, BuiltinRenderTextureType.CurrentActive);
        //_cBuffer.Blit(BuiltinRenderTextureType.CurrentActive, _screenCopyID);
        //_cBuffer.Blit(_screenCopyID, BuiltinRenderTextureType.CurrentActive, _material);
        //_cBuffer.ReleaseTemporaryRT(_screenCopyID);

        // 
        _cBuffer.GetTemporaryRT(_screenCopyID, -1, -1);
        _cBuffer.Blit(BuiltinRenderTextureType.CurrentActive, _screenCopyID);
        _cBuffer.Blit(_screenCopyID, BuiltinRenderTextureType.CurrentActive, _material);
        _cBuffer.ReleaseTemporaryRT(_screenCopyID);

        //// vertically mirrored, BuiltinRenderTextureType.CameraTarget
        //_cBuffer.GetTemporaryRT(_screenCopyID, -1, -1);
        //_cBuffer.Blit(BuiltinRenderTextureType.CameraTarget, _screenCopyID);
        //_cBuffer.Blit(_screenCopyID, BuiltinRenderTextureType.CameraTarget, _material);
        //_cBuffer.ReleaseTemporaryRT(_screenCopyID);

        //// output is white
        //_cBuffer.Blit(BuiltinRenderTextureType.CurrentActive, BuiltinRenderTextureType.CurrentActive, _material);
        //// or
        //_cBuffer.Blit(BuiltinRenderTextureType.CameraTarget, BuiltinRenderTextureType.CameraTarget, _material);

        _camera.AddCommandBuffer(_cameraEvent, _cBuffer);
    }

    private void InitializeCommandBuffer()
    {
        _cBuffer = new CommandBuffer();
        _cBuffer.name = _commandBufferName;
        _cBuffer.Clear();
    }

    private void DestroyCommandBuffer()
    {
        if (_cBuffer != null)
        {
            _camera.RemoveCommandBuffer(_cameraEvent, _cBuffer);
            _cBuffer.Clear();
            _cBuffer.Dispose();
            _cBuffer = null;
        }

        // Make sure we don't have any duplicates of our command buffer.
        var commandBuffers = _camera.GetCommandBuffers(_cameraEvent);
        foreach (var cBuffer in commandBuffers)
        {
            if (cBuffer.name.Equals(_commandBufferName))
            {
                _camera.RemoveCommandBuffer(_cameraEvent, cBuffer);
                cBuffer.Clear();
                cBuffer.Dispose();
            }
        }
    }
}