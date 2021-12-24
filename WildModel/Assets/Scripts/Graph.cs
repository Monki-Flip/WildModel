using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GameObject;

public class Graph : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 ZeroCoordinates;
    private static RectTransform StartPoint;
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

        StartPoint = FindGameObjectWithTag("ZeroCoordinates").GetComponent<RectTransform>();
        ZeroCoordinates = StartPoint.rect.position;
    }

    // Update is called once per frame
    void Update()
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
                Draw(lotkaVolterra.PreysPredict, lotkaVolterra.Preys);
            }
            
            if (TypeOfCreatures.ToLower() == "predators")
            {
                lineRenderer.startWidth = LineRendererStartWidth;
                lineRenderer.endWidth = LineRendererEndWidth;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                Draw(lotkaVolterra.PredatorsPredict, lotkaVolterra.Predators);
            }
        }
    }

    private void Draw(double[] predictions, double startY)
    {
        Vector3[] Tops = ConvertDoubleToVector3(predictions);
        Vector3 startY_vector = new Vector3(0f, (float)startY);

        lineRenderer.gameObject.transform.localPosition = ZeroCoordinates;
        lineRenderer.positionCount = Tops.Length + 1;
        lineRenderer.SetPosition(0, startY_vector);

        for (int i = 1; i <= Tops.Length; i++)
        {
            lineRenderer.SetPosition(i, Tops[i - 1]);
        }
    }

    private Vector3[] ConvertDoubleToVector3(double[] predictions)
    {
        var xStep = transform.parent.GetComponent<RectTransform>().rect.width * 1 / 2500;
        var yStep = transform.parent.GetComponent<RectTransform>().rect.height * 1 / 100;

        Vector3[] result = new Vector3[predictions.Length];
        for (int i = 0; i < predictions.Length; i++)
        {
            result[i] = new Vector3((float)(xStep * i), (float)(predictions[i] * yStep));

        }
        return result;
    }
}
