using UnityEngine;

namespace Utils
{
    public static class Statics
    {
        private static bool _gotCamera = false;
        private static Camera mainCamera;
        public static Camera MainCamera
        {
            get
            {
                if (!_gotCamera) // less expensive than mainCamera == null 
                {
                    mainCamera = Camera.main;
                    _gotCamera = true;
                }
                return mainCamera;
            }
        }
        public static Vector3 MouseInWorldCoords()
        {
            return MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
