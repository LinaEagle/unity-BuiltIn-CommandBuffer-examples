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

    private readonly int _screenCopyID = Shader.PropertyToID("_MainTex");

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
        CommandBufferUtils.Destroy(_cBuffer, _commandBufferName, _cameraEvent, _camera);
        _cBuffer = CommandBufferUtils.CreateNew(_commandBufferName);

        _cBuffer.GetTemporaryRT(_screenCopyID, -1, -1);
        _cBuffer.Blit(BuiltinRenderTextureType.CameraTarget, _screenCopyID, _material);
        _cBuffer.Blit(_screenCopyID, BuiltinRenderTextureType.CameraTarget);
        _cBuffer.ReleaseTemporaryRT(_screenCopyID);

        _camera.AddCommandBuffer(_cameraEvent, _cBuffer);
    }
}