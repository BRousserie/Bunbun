using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.UI;

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
