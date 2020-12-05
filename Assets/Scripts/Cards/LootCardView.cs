

public class LootCardView : CardView
{
    protected override void MouseUp()
    {
        if (target == null)
            ReturnToPosition();
        else
            lootCard();
    }

    private void lootCard()
    {
        target.Deck.AddCard(Card);
        DissolveOut();
    }
}
