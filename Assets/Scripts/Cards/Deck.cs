using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Deck : ScriptableObject
{
    public List<Card> Cards;
    
    [HideInInspector] public List<Card> DrawPile { get; private set; }
    [HideInInspector] public List<Card> Hand { get; private set; }
    [HideInInspector] public List<Card> Discards { get; private set; }
    [HideInInspector] public List<Card> Exhausts { get; private set; }

    // Deck Building methods
    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        Cards.Remove(card);
    }
    
    // Combat methods
    public void Draw(int numberOfCards)
    {
        for (int cards = 0; cards < numberOfCards; cards++) DrawCard();
    }

    private void DrawCard()
    {
        if (DrawPile.Count == 0) ShuffleDiscards();
        DrawCard(DrawPile[Random.Range(0, DrawPile.Count)]);
    }

    private void DrawCard(Card card)
    {
        if (card != null && DrawPile.Contains(card))
        {
            Hand.Add(card);
            DrawPile.Remove(card);
        } 
        else if (card != null) Debug.LogWarning("Tried to draw null card");
        else Debug.LogWarning("Tried to draw " + card.Name + " but it's not been found in drawpile");
    }

    public void FlushHand()
    {
        Discards.AddRange(Hand);
        Hand.Clear();
    }

    public void Exhaust(Card card)
    {
        if (Hand.Contains(card))
        {
            Exhausts.Add(card);
            Hand.Clear();
        }
        else Debug.LogWarning("Tried to exhaust " + card.Name + " but it's not been found in hand");
    }

    public void ShuffleAll()
    {
        ShuffleDiscards();
        ShuffleHand();
    }

    public void ShuffleHand()
    {
        DrawPile.AddRange(Hand);
        DrawPile.Clear();
    }

    public void ShuffleDiscards()
    {
        DrawPile.AddRange(Discards);
        Discards.Clear();
    }
}
