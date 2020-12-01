using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Debug.Log("map");
            SceneManager.LoadScene("Map");
        }
    }
}
