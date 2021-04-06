using UnityEngine;

namespace Cards
{
    public class LootCardView : CardView
    {
        protected override void MouseUp()
        {
            if (target == null)
                ReturnToPosition();
            else
                lootCard();
        }

        protected override void Drag()
        {
            Vector3 mousePos = Input.mousePosition;
            transform.localPosition = new Vector3(
                mousePos.x - originalMousePosition.x, 
                mousePos.y - originalMousePosition.y,
                transform.localPosition.z);
        }

        private void lootCard()
        {
            target.Deck.AddCard(Card);
            DissolveOut();
        }
    }
}
