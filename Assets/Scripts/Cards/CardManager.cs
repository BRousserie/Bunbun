using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class CardManager : MonoBehaviour
{
    public List<Card> AllCards { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        AllCards = Resources.LoadAll("ScriptableObjects/Cards", typeof(Card)).Cast<Card>().ToList();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public Card GetRandomCard()
    {
        throw new NotImplementedException();
    }
}
