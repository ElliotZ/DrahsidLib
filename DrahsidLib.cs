using System;

namespace DrahsidLib; 

public static class DrahsidLib {
    internal const string CameraManagerSig = "4C 8D 35 ?? ?? ?? ?? 85 D2";

    static unsafe void Initialize() {
        Service.CameraManager = (GameCameraManager*)Service.SigScanner.GetStaticAddressFromSig(CameraManagerSig);
    }
}
