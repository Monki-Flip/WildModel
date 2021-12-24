using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GameObject;

public class Graph : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 ZeroCoordinates;
    private static RectTransform ZeroPoint;
    [SerializeField] private Canvas backgroundPanel;
    [SerializeField] private LotkaVolterraModel lotkaVolterra;
    public String TypeOfCreatures;

    // толщина линии
    private float LineRendererStartWidth = 0.07f;
    private float LineRendererEndWidth = 0.07f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.alignment = LineAlignment.View;

        ZeroPoint = FindGameObjectWithTag("ZeroCoordinates").GetComponent<RectTransform>();
        ZeroCoordinates = ZeroPoint.anchoredPosition3D; //new Vector3(5f, 5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (backgroundPanel.enabled)
        {
            StartCoroutine(DrawLine());
        }

        if (!backgroundPanel.enabled)
        {
            Clear();
        }
    }

    IEnumerator DrawLine()
    {
        lotkaVolterra.FindPredicts();
        if (TypeOfCreatures.ToLower() == "preys")
        {
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
            lineRenderer.startWidth = LineRendererStartWidth;
            lineRenderer.endWidth = LineRendererEndWidth;
            Draw(lotkaVolterra.PreysPredict, lotkaVolterra.Preys);
            //Draw(CreateNewRandomDoubleArray(2500), lotkaVolterra.Preys);
            yield return new WaitForSecondsRealtime(.1f);
        }

        if (TypeOfCreatures.ToLower() == "predators")
        {
            lineRenderer.startWidth = LineRendererStartWidth;
            lineRenderer.endWidth = LineRendererEndWidth;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            Draw(lotkaVolterra.PredatorsPredict, lotkaVolterra.Predators);
            //Draw(CreateNewRandomDoubleArray(2500), lotkaVolterra.Predators);
            yield return new WaitForSecondsRealtime(.1f);
        }
    }

        //private double[] CreateNewRandomDoubleArray(int v)
        //{
        //    System.Random random = new System.Random();
        //    double[] array = new double[v];
        //    for (int i = 0; i < v; i++)
        //    {
        //        array[i] = (double)(int)random.Next(1, 100);
        //    }
        //    return array;
        //}

        private void Draw(double[] predictions, double startY)
    {
        Vector3[] Tops = ConvertDoubleToVector3(predictions);
        Vector3 startY_vector = new Vector3(0f, (float)startY);

        gameObject.transform.GetComponent<RectTransform>().anchoredPosition3D = ZeroCoordinates;
        lineRenderer.positionCount = Tops.Length + 1;
        lineRenderer.SetPosition(0, startY_vector);

        for (int i = 1; i <= Tops.Length; i++)
        {
            lineRenderer.SetPosition(i, Tops[i - 1]);
        }
    }

    private Vector3[] ConvertDoubleToVector3(double[] predictions)
    {
        var xStep = (transform.parent.GetComponent<RectTransform>().rect.width - 10f) * 1 / predictions.Length;
        var yStep = (transform.parent.GetComponent<RectTransform>().rect.height - 10f) * 1 / 100;

        Vector3[] result = new Vector3[predictions.Length];
        for (int i = 0; i < predictions.Length; i++)
        {
            result[i] = new Vector3((float)(xStep * i), (float)(predictions[i] * yStep));

        }
        return result;
    }

    private void Clear()
    {
        lineRenderer.positionCount = 0;
    }
}
