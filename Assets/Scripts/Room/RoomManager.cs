using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : Singleton<RoomManager>
{
    [HideInInspector] public RoomType CurrentRoomType;
    [HideInInspector] public Room CurrentRoom;
    [HideInInspector] public GameObject RoomUIPrefab;


    // Start is called before the first frame update
    void Start()
    {
        RoomUIPrefab = Resources.Load<GameObject>("Prefabs/RoomUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EnterRoom(RoomType newRoomType)
    {
        Debug.Log("EnterRoom " + newRoomType);
        CurrentRoomType = newRoomType;
        switch (CurrentRoomType)
        {
            case RoomType.MinorEnemy:
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                break;
            case RoomType.EliteEnemy:
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                break;
            case RoomType.RestSite:
                SceneManager.LoadScene("Restsite", LoadSceneMode.Additive);
                break;
            case RoomType.Treasure:
                SceneManager.LoadScene("Treasure", LoadSceneMode.Additive);
                break;
            case RoomType.Store:
                SceneManager.LoadScene("Store", LoadSceneMode.Additive);
                break;
            case RoomType.Boss:
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                break;
            case RoomType.Mystery:
                EnterRoom(MapGenerator.RandomRoomTypes.Random());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        SceneManager.MoveGameObjectToScene(Instantiate(RoomUIPrefab), SceneManager.GetSceneAt(1));
    }

    public void ExitRoom()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        MapPlayerTracker.Instance.Locked = false;
        MapView.Instance.SetVisible(true);
    }
}
