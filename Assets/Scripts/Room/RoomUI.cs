using Map;
using UnityEngine;
using UnityEngine.UI;

namespace Room
{
    public class RoomUI : MonoBehaviour
    {
        public Button Compass;
        private bool showMap = true;
    
        void Start()
        {
            Compass.onClick.AddListener(ShowMap);
        }
    
        void ShowMap()
        {
            MapView.Instance.SetVisible(showMap);
            showMap = !showMap;
        }
    }
}
