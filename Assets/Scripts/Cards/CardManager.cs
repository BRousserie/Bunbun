using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CardManager : MonoBehaviour
{
    public List<Card> AllCards { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        AllCards = Resources.LoadAll("ScriptableObjects/Cards/Players", typeof(Card)).Cast<Card>().ToList();
        DontDestroyOnLoad(this);
    }

    public Card GetRandomCard(CardType type = CardType.Default)
    {
        if (type == CardType.Default)
            return AllCards.Random();
        else
            return AllCards.Where(c => c.CardType == type).ToList().Random();
    }
    
    // TODO : LE JOUEUR CHOISIT QUEL TYPE DE CARTE IL LOOT
}
