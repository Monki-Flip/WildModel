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
    public LotkaVolterraModel LotkaModel;
    public Animals Animals;

    public TMP_Text DayText;
    public TMP_Text DeersCount;
    public TMP_Text WolvesCount;

    public Image Bar;

    public bool IsPlaying;

    public GameObject NPCPanel;
    public GameObject Defeat;

    private void Start()
    {
        Day = 1;
    }
    public void Play()
    {
        if (!IsPlaying)
        {
            LotkaModel.FindPredicts();
            LotkaModel.Preys = LotkaModel.PreysPredict[99];
            LotkaModel.Predators = LotkaModel.PredatorsPredict[99];
            StartCoroutine(BarFilling());
            //Animals.MakeRandomMoves();
        }
    }

    IEnumerator BarFilling() // �������� �� ����� 5�, � �������� 7.5
    {
        IsPlaying = true;
        var initTime = 5f;
        while (Bar.fillAmount < 1f)
        {
            Bar.fillAmount += 0.003f;
            yield return new WaitForSeconds(0.01f);
        }
        Bar.fillAmount = 0f;
        IsPlaying = false;
        Day++;
        DayText.text = string.Format("{0} ����", Day);
        DeersCount.text = Math.Round(LotkaModel.Preys, 1).ToString();
        WolvesCount.text = Math.Round(LotkaModel.Predators, 1).ToString();
        if (Math.Round(LotkaModel.Preys, 1) < 0.1 || Math.Round(LotkaModel.Predators, 1) < 0.1)
        {
            NPCPanel.SetActive(true);
            Defeat.SetActive(true);
        }
    }
}
