using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public TMP_Text PriceText;
    public ModelTextManager TextManager;
    public CardOutlineManager CardOutlineManager;
    public Outline Outline;

    public int Price;
    public double AlphaDiff;
    public double BetaDiff;
    public double GammaDiff;
    public double SigmaDiff;


    private void Start()
    {
        PriceText.text = Price.ToString();
    }


    public void Select()
    {
        TextManager.UpdateCoeffChangesText(AlphaDiff, BetaDiff, GammaDiff, SigmaDiff);
        TextManager.UpdateNewSystem(AlphaDiff, BetaDiff, GammaDiff, SigmaDiff);
        TextManager.UpdateBuyButtonText(Price);
    }

    public void UpdateOutlineManager()
    {
        CardOutlineManager.ChangeCardsOutlineStates(this);
    }

    public void EnableOutlineState()
    {
        CardOutlineManager.CurrentCard = this;
        Outline.enabled = true;
    }

    public void DisableOutlineState()
    {
        Outline.enabled = false;
    }
}