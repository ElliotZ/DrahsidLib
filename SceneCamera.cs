using FFXIVClientStructs.FFXIV.Client.Graphics.Scene;

namespace DrahsidLib;

/// <summary>
/// FFXIVClientStructs.FFXIV.Client.Graphics.Scene.CameraManager with a less ambiguos name
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x120)]
public unsafe partial struct SceneCameraManager {
    [FieldOffset(0x50)] public int CameraIndex;
    [FixedSizeArray<Pointer<Camera>>(14)]
    [FieldOffset(0x58)] public fixed byte CameraArray[14 * 8]; //14 * Camera*

    public Camera* CurrentCamera {
        get {
            fixed (byte* ptr = CameraArray)
                return ((Camera**)ptr)[CameraIndex];
        }
    }
    public static SceneCameraManager* Instance() => (SceneCameraManager*)CameraManager.Instance();
}

