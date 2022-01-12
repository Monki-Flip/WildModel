using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GameObject;

public class Graph : MonoBehaviour
{
    public LotkaVolterraModel Lotka;

    public GameObject ZeroDot;
    public GameObject X;
    public GameObject Y;

    public LineRenderer DeersLineRenderer;
    public LineRenderer WolvesLineRenderer;

    public float Scale;
    public GameObject XDivision;
    public int AmountXDivision;
    public GameObject YDivision;
    public int AmountYDivision;

    public List<GameObject> XDivs = new List<GameObject>();
    public List<GameObject> YDivs = new List<GameObject>();

    private void Awake()
    {
        for (var i = 1; i < AmountXDivision + 1; i++)
        {
            var division = Instantiate(XDivision);
            division.transform.parent = ZeroDot.transform;
            division.transform.localScale = new Vector3(1f, 1f, 1f);
            division.transform.localPosition = new Vector3(i * Scale, 0, 0);
            division.GetComponentInChildren<TMP_Text>().text = i.ToString();
            XDivs.Add(division);
        }

        for (var i = 1; i < AmountYDivision + 1; i++)
        {
            var division = Instantiate(YDivision);
            division.transform.parent = ZeroDot.transform;
            division.transform.localScale = new Vector3(1f, 1f, 1f);
            division.transform.localPosition = new Vector3(0, i * Scale, 0);
            division.GetComponentInChildren<TMP_Text>().text = i.ToString();
            YDivs.Add(division);
        }
    }

    public void UpdateAll()
    {
        DrawPreysGraph();
        DrawPredatorsGraph();
        UpdateYDivs();
    }

    public void UpdateYDivs()
    {
        var step = GetStep(GetMaxDiv(), GetMinDiv());
        var currentValue = GetMinDiv() + step;
        //Debug.Log(maxDiv + ",      " + minDiv + ",     " + step + ",        " + YDivs.Count);
        for (var i = 0; i < YDivs.Count; i++)
        {
            //Debug.Log("пишу");
            YDivs[i].GetComponentInChildren<TMP_Text>().text = currentValue.ToString();
            currentValue += step;
        }
    }

    public int GetStep(int max, int min)
    {
        return (max - min + 1) / 10;
    }

    public int GetMaxDiv()
    {
        Lotka.FindPredicts();
        var dots = Lotka.PreysPredict;
        var max = 0f;
        for (var i = 0; i < dots.Length - (dots.Length - AmountXDivision * 100); i++)
            max = Math.Max(max, (float) dots[i]);
        return ((int)max + 9) / 10 * 10;
    }

    public int GetMinDiv()
    {
        Lotka.FindPredicts();
        var dots = Lotka.PreysPredict;
        var min = 0f;
        for (var i = 0; i < dots.Length - (dots.Length - AmountXDivision * 100); i++)
            min = Math.Min(min, (float)dots[i]);
        return ((int)min + 9) / 10 * 10;
    }

    public void DrawPreysGraph()
    {
        Lotka.FindPredicts();
        var dots = Lotka.PreysPredict;
        var dotsVectors = new Vector3[dots.Length - (dots.Length - AmountXDivision * 100)];
        var x = 0d;
        var step = GetStep(GetMaxDiv(), GetMinDiv());
        //Debug.Log(dots.Length);
        for (var i = 0; i < dots.Length - (dots.Length - AmountXDivision * 100); i++)
        {
            dotsVectors[i] = new Vector3((float) x * Scale, (float)dots[i] * Scale / step, 0);
            x += 0.010004001600640256;
        }

        DrawGraph(dotsVectors, DeersLineRenderer);
    }

    public void DrawPredatorsGraph()
    {
        Lotka.FindPredicts();
        var dots = Lotka.PredatorsPredict;
        var dotsVectors = new Vector3[dots.Length - (dots.Length - AmountXDivision * 100)];
        var x = 0d;
        var step = GetStep(GetMaxDiv(), GetMinDiv());
        Debug.Log(step);
        for (var i = 0; i < dots.Length - (dots.Length - AmountXDivision * 100); i++)
        {
            dotsVectors[i] = new Vector3((float)x * Scale, (float)dots[i] * Scale / step, 0);
            x += 0.010004001600640256;
        }

        DrawGraph(dotsVectors, WolvesLineRenderer);
    }

    public void DrawGraph(Vector3[] tops, LineRenderer renderer)
    {
        renderer.positionCount = tops.Length;
        renderer.SetPositions(tops);
    }
}
