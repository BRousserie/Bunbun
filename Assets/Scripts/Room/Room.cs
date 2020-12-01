using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Room : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        RoomManager.Instance.CurrentRoom = this;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.M))
            RoomManager.Instance.ExitRoom();
        
    }
}
