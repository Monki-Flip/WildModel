using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardOutlineManager : MonoBehaviour
{
    public Card CurrentCard;
    public List<Card> Cards;
    public bool IsOutlineEnabled;

    public void ChangeCardsOutlineStates(Card newCard)
    {
        foreach (var card in Cards)
            if (card == newCard)
                card.EnableOutlineState();
            else
                card.DisableOutlineState();
        CurrentCard = newCard;
    }

    public void DisableAllCardsOutline()
    {
        CurrentCard = null;
        IsOutlineEnabled = false;
        foreach (var card in Cards)
            card.DisableOutlineState();
    }
}
