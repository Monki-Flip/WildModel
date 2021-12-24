using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GameObject;


public class Graph0 : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 ZeroCoordinates;
    private static RectTransform StartPoint;
    [SerializeField] private Canvas backgroundPanel;
    [SerializeField] private LotkaVolterraModel lotkaVolterra;
    public String TypeOfCreatures;

    private IterationsManager CurrentIteration;
    private int PreviousIteration;

    //// делегат события, что график достиг нужного количества зверей обеих популяций
    //public delegate void ActionToWin(bool gotIt);
    //private bool PreysOnLimit = false;
    //private bool PredatorsOnLimit = false;
    //public event ActionToWin GraphGotResultAmounts;

    // макс. количество хищников и жертв
    //private float PredatorsLimit;
    //private float PredatorsMaxCount;
    //private float PreysLimit;
    //private float PreysMaxCount;

    private float LineRendererStartWidth = 0.07f;
    private float LineRendererEndWidth = 0.07f;

    private bool IfGraphHaveStartPoint = false;


    private void Start()
    {
        lotkaVolterra = FindObjectOfType<LotkaVolterraModel>();
        CurrentIteration = FindObjectOfType<IterationsManager>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.alignment = LineAlignment.View;

        StartPoint = Find("O").GetComponent<RectTransform>();

        //PredatorsLimit = FindGameObjectWithTag("PredatorsLimit").GetComponent<RectTransform>().position.y * (float)(1 / 100);
        //PreysLimit = FindGameObjectWithTag("PreysLimit").GetComponent<RectTransform>().position.y * (float)(1 / 100);

        PreviousIteration = CurrentIteration.Day;
        //ZeroCoordinates = new Vector3(startX, startY);
        ZeroCoordinates = new Vector3(5.809998f, 5.68f, 0);
        //zeroCoordinates = GameObject.FindGameObjectWithTag("ZeroCoordinates").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (backgroundPanel.enabled)
        {
            lotkaVolterra.FindPredicts();
            if (TypeOfCreatures.ToLower() == "preys")
            {
                lineRenderer.startColor = Color.blue;
                lineRenderer.endColor = Color.blue;
                lineRenderer.startWidth = LineRendererStartWidth;
                lineRenderer.endWidth = LineRendererEndWidth;
                //Draw(lotkaVolterra.PreysPredict, lotkaVolterra.Preys);
                Draw(CreateNewRandomDoubleArray(2500), lotkaVolterra.Preys);

                //if (IfGraphGetsTheResult())
                //{
                //    PreysOnLimit = true;
                //    //посылай сигнал о событии
                //}
            }

            if (TypeOfCreatures.ToLower() == "predators")
            {
                lineRenderer.startWidth = LineRendererStartWidth;
                lineRenderer.endWidth = LineRendererEndWidth;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                //Draw(lotkaVolterra.PredatorsPredict, lotkaVolterra.Predators);
                Draw(CreateNewRandomDoubleArray(2500), lotkaVolterra.Predators);
                //if (IfGraphGetsTheResult())
                //{
                //    PredatorsOnLimit = true;
                //    //посылай сигнал о событии
                //}
            }
        }
        //if (!backgroundPanel.enabled)
        //{
        //    Clear();
        //}
    }

    // НЕ СДЕЛАН. должен давать true, когда график достиг максимума(!) на пределе
    //private bool IfGraphGetsTheResult()
    //{
    //    var yStep = transform.parent.GetComponent<RectTransform>().rect.height * 1 / 100;
    //    // переводим координаты лимита в высоту Y
    //
    //    return false;
    //}

    private double[] CreateNewRandomDoubleArray(int v)
    {
        System.Random random = new System.Random();
        double[] array = new double[v];
        for (int i = 0; i < v; i++)
        {
            array[i] = (double)(int)random.Next(1, 100);
        }
        return array;
    }

    /*
     * 1 итерация: первые 100 точек из predictions
     * 2 итерация: [1 итерация] + первые 100 точек из predictions (где уже новые значения)
     */

    private void Draw(double[] predictions, double startPos)
    {
        int index = 1;

        Vector3 startPoint = new Vector3(0, (float)startPos); ;
        List<Vector3> allAmounts = new List<Vector3>();
        if (!IfGraphHaveStartPoint)
            startPoint = new Vector3(0, (float)startPos);
        else if (CurrentIteration.Day != PreviousIteration && startPoint.x + 100 <= gameObject.transform.parent.GetComponent<RectTransform>().rect.width)
        // и прозошло событие {сменилась итерация})
        {
            //StartPoint.rect.Set(StartPoint.rect.x + 100f,
            //                    StartPoint.position.y,
            //                    StartPoint.rect.width,
            //                    StartPoint.rect.height);
            startPoint.x += 100;
            PreviousIteration = CurrentIteration.Day;
        }

        Vector3[] Tops = ConvertDoubleToVector3(predictions, index);


        if (allAmounts.Count < 2500)
            for (int i = 0; i < Tops.Length; i++)
            {
                allAmounts.Add(Tops[i]);
            }

        lineRenderer.gameObject.transform.localPosition = ZeroCoordinates; /*startPoint;*/
        lineRenderer.positionCount = allAmounts.Count + 1;
        lineRenderer.SetPosition(0, startPoint);

        for (int i = index; i <= allAmounts.Count; i++)
        {
            lineRenderer.SetPosition(i, allAmounts[i - 1]);
        }

        index += 100;
    }

    private Vector3[] ConvertDoubleToVector3(double[] array, int index)
    {
        // всего длина X = 50
        var xStep = gameObject.transform.parent.GetComponent<RectTransform>().rect.width * 1 / 2500;
        var yStep = gameObject.transform.parent.GetComponent<RectTransform>().rect.height * 1 / 100;
        Vector3[] result = new Vector3[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = new Vector3((float)(xStep * index), (float)array[i] * yStep);
            index += 1;

            //if (yStep == curAnimalsMaxCount)
            //{
            //    //return;
            //}
        }
        return result;
    }

    private void Clear()
    {
        lineRenderer.positionCount = 0;
    }
}