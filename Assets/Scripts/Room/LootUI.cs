using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootUI : MonoBehaviour
{
    public GameObject CardsDisplayer;
    public int NumberOfCards = 5;
    public Rarity CardsRarity;
    public int Picks = 2;
    public List<LootCardView> Cards;

    [Header("PlayerChoiceButtons")] 
    public Button Random;
    public Button Attack;
    public Button Skill;
    public Button Power;
    
    // Start is called before the first frame update
    void Start()
    {
        Random.onClick.AddListener(() => ApplyChoice(CardTypes.Default));
        Attack.onClick.AddListener(() => ApplyChoice(CardTypes.Attack));
        Skill.onClick.AddListener(() => ApplyChoice(CardTypes.Skill));
        Power.onClick.AddListener(() => ApplyChoice(CardTypes.Power));
    }

    private void ApplyChoice(CardTypes type)
    {
        if (type == CardTypes.Default)
        {
            NumberOfCards = 5;
            Picks = 2;
        }
        else
        {
            NumberOfCards = 3;
            Picks = 1;
        }
        DrawCards(type);
    }

    private void DrawCards(CardTypes type)
    {
        for (int card = 0; card < NumberOfCards; card++)
        {
            Card loot = CardManager.Instance.GetRandomCard(type, CardsRarity);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
