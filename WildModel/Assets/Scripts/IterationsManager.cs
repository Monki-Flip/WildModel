using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IterationsManager : MonoBehaviour
{
    public int Day;
    public int ToPlayCount;
    public LotkaVolterraModel LotkaModel;
    public Animals Animals;

    public TMP_Text DayText;
    public TMP_Text DeersCount;
    public TMP_Text WolvesCount;

    public Image Bar;

    private void Start()
    {
        Day = 1;
        ToPlayCount = 1;
    }
    public void Play()
    {
        Day += ToPlayCount;
        LotkaModel.FindPredicts();
        LotkaModel.Preys = LotkaModel.PreysPredict[99];
        LotkaModel.Predators = LotkaModel.PredatorsPredict[99];
        StartCoroutine(BarFilling());
        DayText.text = string.Format("{0} день", Day);
        DeersCount.text = Math.Round(LotkaModel.Preys, 1).ToString();
        WolvesCount.text = Math.Round(LotkaModel.Predators, 1).ToString();
        //Animals.MakeRandomMoves();
    }

    IEnumerator BarFilling() // работает не ровно 5с, а примерно 7.5
    {
        var initTime = 5f;
        while (Bar.fillAmount < 1f)
        {
            Bar.fillAmount += 0.003f;
            yield return new WaitForSeconds(0.01f);
        }
        Bar.fillAmount = 0f;
        yield return null;
    }
}
