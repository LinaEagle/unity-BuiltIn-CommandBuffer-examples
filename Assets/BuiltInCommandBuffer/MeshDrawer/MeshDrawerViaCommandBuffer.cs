using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class MeshDrawerViaCommandBuffer : MonoBehaviour
{
    [SerializeField] private CameraEvent _cameraEvent = CameraEvent.BeforeForwardOpaque;
    [SerializeField] private string _commandBufferName = "MeshDrawer";
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Vector3 _worldPosition;
    [SerializeField] private Material _material;
    private Camera _camera;
    private CommandBuffer _cBuffer;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        if (_mesh == null)
        {
            _mesh = PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Sphere);
        }
    }
    
    private void OnPreRender()
    {
        CreateCommandBuffer();
    }
    
    private void CreateCommandBuffer()
    {
        CommandBufferUtils.Destroy(_cBuffer, _commandBufferName, _cameraEvent, _camera);
        _cBuffer = CommandBufferUtils.CreateNew(_commandBufferName);
        
        _cBuffer.DrawMesh(_mesh, Matrix4x4.Translate(_worldPosition), _material);

        _camera.AddCommandBuffer(_cameraEvent, _cBuffer);
    }
}