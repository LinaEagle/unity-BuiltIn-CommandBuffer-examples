# Unity BuiltIn CommandBuffer Examples

Examples contain:
- Basic Command Buffer flow implementation: construction / deconstruction
- Mesh Drawer
- PostProcessing

All scripts can be used independedntly. You can also notice, that all command buffer related scripts contain construction / deconstruction logic, this flow is recommended to achieve proper rendering without errors or dublication.

This is quite important to understand a general [Built-In Render Pipeline Flow](https://imgbb.com/BLSGdPQ), considering your rendering path.

Unity docs about
- [CommandBuffer](https://docs.unity3d.com/ScriptReference/Rendering.CommandBuffer.html)
- [Camera Events](https://docs.unity3d.com/ScriptReference/Rendering.CameraEvent.html)

### Other examples
Also you can check these materials 
- General info and raymarching sample - [Hacking into Unity’s rendering pipelines](https://www.youtube.com/watch?v=xrmbtBHJXt8)
- Article [Extending Unity 5 rendering pipeline: Command Buffers](https://blog.unity.com/technology/extending-unity-5-rendering-pipeline-command-buffers) (yes, it's that old)
- Refraction blur sample - [Unity-CommandBufferRefraction](https://github.com/Doppelkeks/Unity-CommandBufferRefraction)