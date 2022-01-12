using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;


public class ModelTextManager : MonoBehaviour
{
    public LotkaVolterraModel CurrentModel;
    public LotkaVolterraModel NewModel;
    public TextMeshProUGUI CoeffChangesTitle;
    public TMP_Text CoeffChanges;
    public TMP_Text XEquation;
    public TMP_Text YEquation;
    public Score Score;
    public CardOutlineManager CardOutlineManager;
    public TMP_Text BuyButton;
    public GameObject ErrorMessage;
    public TMP_Text ErrorText;
    public GameObject LeftBlockInfo;
    public GameObject BuyPointsImg;

    public Card LastBoughtCard;
    public List<Card> PurchaseHistory;

    public Func<double, double, string> colorTagBegin = (x1, x2) => x1 > x2 ? "<color=#A1C218>"
                                                        : x1 < x2 ? "<color=#FD644F>" : "";
    public Func<double, double, string> colorTagEnd = (x1, x2) => x1 != x2 ? "</color>" : "";



    public void UpdateTextsOnStoreOpen()
    {
        CoeffChanges.text = "";
        CoeffChangesTitle.enabled = false;
        XEquation.text = GetXEquationText(CurrentModel);
        YEquation.text = GetYEquationText(CurrentModel);
        BuyPointsImg.SetActive(false);
        BuyButton.text = "";
    }

    private string GetXEquationText(LotkaVolterraModel model)
    {
        var text = new StringBuilder();

        var newD = model.GetFuncValues(model.Preys, model.Predators)[0];
        var currD = CurrentModel.GetFuncValues(CurrentModel.Preys, CurrentModel.Predators)[0];

        text.Append(colorTagBegin(newD, currD) + Math.Round(newD, 1).ToString() + colorTagEnd(newD, currD) + " = (" 
                    + colorTagBegin(model.Alpha, CurrentModel.Alpha) + model.Alpha.ToString() + colorTagEnd(model.Alpha, CurrentModel.Alpha) + " - "
                    + colorTagBegin(model.Beta, CurrentModel.Beta) + model.Beta.ToString() + colorTagEnd(model.Beta, CurrentModel.Beta) + " · " 
                    + Math.Round(model.Predators, 1).ToString() + ")" + Math.Round(model.Preys, 1).ToString());

        return text.ToString();
    }

    private string GetYEquationText(LotkaVolterraModel model)
    {
        var text = new StringBuilder();

        var newD = model.GetFuncValues(model.Preys, model.Predators)[1];
        var currD = CurrentModel.GetFuncValues(CurrentModel.Preys, CurrentModel.Predators)[1];

        text.Append(colorTagBegin(newD, currD) + Math.Round(newD, 1).ToString() + colorTagEnd(newD, currD) + " = (-"
                    + colorTagBegin(model.Gamma, CurrentModel.Gamma) + model.Gamma.ToString() + colorTagEnd(model.Gamma, CurrentModel.Gamma) + " + "
                    + colorTagBegin(model.Sigma, CurrentModel.Sigma) + model.Sigma.ToString() + colorTagEnd(model.Sigma, CurrentModel.Sigma) + " · "
                    + Math.Round(model.Preys, 1).ToString() + ")" + Math.Round(model.Predators, 1).ToString());

        return text.ToString();
    }

    public void UpdateCoeffChangesText(double alphaDiff, double betaDiff, double gammaDiff, double sigmaDiff)
    {
        CoeffChangesTitle.enabled = true;
        var coeffs = new Dictionary<String, double> { ["α"] = alphaDiff, ["β"] = betaDiff, ["γ"] = gammaDiff, ["δ"] = sigmaDiff };
        var text = new StringBuilder();
        foreach(var a in coeffs)
        {
            if (a.Value == 0) 
                continue;
            text.Append((a.Value > 0 ? "<color=#A1C218>" : "<color=#FD644F>") + a.Key + " " + (a.Value > 0 ? "+" : "") + a.Value.ToString() + " " + "</color>");
        }
        CoeffChanges.text = text.ToString();
    }

    public void UpdateNewSystem(double alphaDiff, double betaDiff, double gammaDiff, double sigmaDiff)
    {
        NewModel.Predators = CurrentModel.Predators;
        NewModel.Preys = CurrentModel.Preys;
        NewModel.Alpha = CurrentModel.Alpha + alphaDiff;
        NewModel.Beta = CurrentModel.Beta + betaDiff;
        NewModel.Gamma = CurrentModel.Gamma + gammaDiff;
        NewModel.Sigma = CurrentModel.Sigma + sigmaDiff;
        XEquation.text = GetXEquationText(NewModel);
        YEquation.text = GetYEquationText(NewModel);
    }

    public void Buy()
    {
        if (CardOutlineManager.CurrentCard != null)
        {
            if (CardOutlineManager.CurrentCard.Price > Score.Value)
                StartCoroutine(ShowErrorMessage("points"));
            else if (NewModel.Alpha < 0 || NewModel.Beta < 0 || NewModel.Sigma < 0 || NewModel.Gamma < 0)
                StartCoroutine(ShowErrorMessage("coeff"));
            else
            {
                UpdateCurrentSystem();
                UpdateTextsOnStoreOpen();
                Score.Add(-CardOutlineManager.CurrentCard.Price);
                LastBoughtCard = CardOutlineManager.CurrentCard;
                PurchaseHistory.Add(CardOutlineManager.CurrentCard);
                CardOutlineManager.DisableAllCardsOutline();
            }
        }
    }

    public void UpdateCurrentSystem()
    {
        CurrentModel.Alpha = NewModel.Alpha;
        CurrentModel.Beta = NewModel.Beta;
        CurrentModel.Gamma = NewModel.Gamma;
        CurrentModel.Sigma = NewModel.Sigma;
    }

    public void UpdateBuyButtonText(int price)
    {
        BuyButton.text = (-price).ToString();
        BuyPointsImg.SetActive(true);
    }

    IEnumerator ShowErrorMessage(string reason)
    {
        switch(reason)
        {
            case "coeff":
                ErrorText.text = "Один из коэффициентов стал ниже нуля, поэтому ты не можешь купить эту карту, друг";
                break;
            case "points":
                ErrorText.text = "Тебе не хватает очков, друг";
                break;
        }
        ErrorMessage.SetActive(true);
        LeftBlockInfo.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        ErrorMessage.SetActive(false);
        LeftBlockInfo.SetActive(true);
    }
}
