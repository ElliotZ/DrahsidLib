using System;
using System.Runtime.InteropServices;

namespace DrahsidLib;

public enum CameraControlMode {
    FirstPerson = 0,
    ThirdPerson = 1
}

public enum CameraControlType {
    FirstPerson = 0,
    Legacy = 1,
    Standard = 2
}

public enum MovementMode {
    Standard = 0,
    Legacy = 1,
    Count
}

// Jacked from cammy

[StructLayout(LayoutKind.Explicit)]
public unsafe struct GameCameraManager {
    [FieldOffset(0x0)] public GameCamera* WorldCamera;
    [FieldOffset(0x8)] public GameCamera* IdleCamera;
    [FieldOffset(0x10)] public GameCamera* MenuCamera;
    [FieldOffset(0x18)] public GameCamera* SpectatorCamera;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe struct GameCamera {
    [FieldOffset(0x0)] public nint* vtbl;
    [FieldOffset(0x60)] public float X;
    [FieldOffset(0x64)] public float Z;
    [FieldOffset(0x68)] public float Y;
    [FieldOffset(0x90)] public float LookAtX; // Position that the camera is focused on (Actual position when zoom is 0)
    [FieldOffset(0x94)] public float LookAtZ;
    [FieldOffset(0x98)] public float LookAtY;
    [FieldOffset(0x114)] public float CurrentZoom; // 6
    [FieldOffset(0x118)] public float MinZoom; // 1.5
    [FieldOffset(0x11C)] public float MaxZoom; // 20
    [FieldOffset(0x120)] public float CurrentFoV; // 0.78
    [FieldOffset(0x124)] public float MinFoV; // 0.69
    [FieldOffset(0x128)] public float MaxFoV; // 0.78
    [FieldOffset(0x12C)] public float AddedFoV; // 0
    [FieldOffset(0x130)] public float CurrentHRotation; // -pi -> pi, default is pi
    [FieldOffset(0x134)] public float CurrentVRotation; // -0.349066
    [FieldOffset(0x138)] public float HRotationDelta;
    [FieldOffset(0x148)] public float MinVRotation; // -1.483530, should be -+pi/2 for straight down/up but camera breaks so use -+1.569
    [FieldOffset(0x14C)] public float MaxVRotation; // 0.785398 (pi/4)
    [FieldOffset(0x160)] public float Tilt;
    [FieldOffset(0x170)] public int Mode; // Camera mode? (0 = 1st person, 1 = 3rd person, 2+ = weird controller mode? cant look up/down)
    [FieldOffset(0x174)] public int ControlType; // 0 first person, 1 legacy, 2 standard, 4 talking to npc in first person (with option enabled), 5 talking to npc (with option enabled), 3/6 ???
    [FieldOffset(0x17C)] public float InterpolatedZoom;
    [FieldOffset(0x190)] public float Transition; // Seems to be related to the 1st <-> 3rd camera transition
    [FieldOffset(0x1B0)] public float ViewX;
    [FieldOffset(0x1B4)] public float ViewZ;
    [FieldOffset(0x1B8)] public float ViewY;
    [FieldOffset(0x1E4)] public byte IsFlipped; // 1 while holding the keybind
    [FieldOffset(0x21C)] public float InterpolatedY;
    [FieldOffset(0x224)] public float LookAtHeightOffset; // No idea what to call this (0x230 is the interpolated value)
    [FieldOffset(0x228)] public byte ResetLookatHeightOffset; // No idea what to call this
    [FieldOffset(0x230)] public float InterpolatedLookAtHeightOffset;
    [FieldOffset(0x2B0)] public byte LockPosition;
    [FieldOffset(0x2C4)] public float LookAtY2;
}

