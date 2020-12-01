using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CardManager : Singleton<CardManager>
{
    public List<Card> AllPlayerCards { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        AllPlayerCards = Resources.LoadAll("ScriptableObjects/Cards/Players", typeof(Card)).Cast<Card>().ToList();
    }

    public Card GetRandomCard(CardType type = CardType.Default, Rarity rarity = Rarity.Default)
    {
        List<Card> desirableCards = AllPlayerCards;
        
        if (type != CardType.Default)
            desirableCards = desirableCards.Where(c => c.CardType == type).ToList();
        if (rarity != Rarity.Default)
            desirableCards = desirableCards.Where(c => c.Rarity == rarity).ToList();

        return desirableCards.Random();
    }
    
    // TODO : LE JOUEUR CHOISIT QUEL TYPE DE CARTE IL LOOT
}
