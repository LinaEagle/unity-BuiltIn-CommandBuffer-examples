using UnityEngine;
using UnityEngine.Rendering;

public static class CommandBufferUtils
{
    public static CommandBuffer CreateNew(string commandBufferName)
    {
        var cBuffer = new CommandBuffer();
        cBuffer.name = commandBufferName;
        cBuffer.Clear();
        return cBuffer;
    }
    
    public static void Destroy(
        CommandBuffer commandBuffer, 
        string commandBufferName, 
        CameraEvent cameraEvent,
        Camera camera)
    {
        if (commandBuffer != null)
        {
            camera.RemoveCommandBuffer(cameraEvent, commandBuffer);
            commandBuffer.Clear();
            commandBuffer.Dispose();
            commandBuffer = null;
        }

        // Make sure we don't have any duplicates of our command buffer.
        var commandBuffers = camera.GetCommandBuffers(cameraEvent);
        foreach (var cBuffer in commandBuffers)
        {
            if (cBuffer.name.Equals(commandBufferName))
            {
                camera.RemoveCommandBuffer(cameraEvent, cBuffer);
                cBuffer.Clear();
                cBuffer.Dispose();
            }
        }
    }
}