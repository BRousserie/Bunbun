using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    
    public RoomType CurrentRoomType;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EnterNode(RoomType newRoomType)
    {
        Debug.Log("EnterNode " + newRoomType);
        CurrentRoomType = newRoomType;
        switch (CurrentRoomType)
        {
            case RoomType.MinorEnemy:
                SceneManager.LoadScene("Combat");
                break;
            case RoomType.EliteEnemy:
                SceneManager.LoadScene("Combat");
                break;
            case RoomType.RestSite:
                SceneManager.LoadScene("Restsite");
                break;
            case RoomType.Treasure:
                SceneManager.LoadScene("Treasure");
                break;
            case RoomType.Store:
                SceneManager.LoadScene("Store");
                break;
            case RoomType.Boss:
                SceneManager.LoadScene("Combat");
                break;
            case RoomType.Mystery:
                EnterNode(MapGenerator.RandomRoomTypes.Random());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
