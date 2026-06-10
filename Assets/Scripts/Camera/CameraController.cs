using UnityEngine;

namespace Camera {
    public class CameraController {
        
        private static CameraController _theInstance;
        public static CameraController Instance { get {
            if (_theInstance == null)
                _theInstance = new CameraController();
            return _theInstance;
        } }

        public float size = 1;

        public void UpdateCamera() {
            GetCamera().orthographicSize = size * 10;
        }

        private UnityEngine.Camera GetCamera() { // mom get the camera!
            return Object.FindAnyObjectByType<UnityEngine.Camera>();
        }
    }
}