using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityProj {
    public class Graph : MonoBehaviour
    {
        //public int scale = 1;
        private LineRenderer lineRenderer;
        private Vector3 ZeroCoordinates;
        private static RectTransform StartPoint;
        [SerializeField] private Canvas backgroundPanel;
        [SerializeField] private LotkaVolterraModel lotkaVolterra;
        public String TypeOfCreatures;

        
        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.alignment = LineAlignment.View;

            StartPoint = GameObject.FindGameObjectWithTag("ZeroCoordinates").GetComponent<RectTransform>();
            float startX = StartPoint.position.x;
            float startY = StartPoint.position.y;
            //ZeroCoordinates = new Vector3(startX, startY);
            ZeroCoordinates = new Vector3(5.799377f, 5f, 0);
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
                    Draw(lotkaVolterra.PreysPredict, lotkaVolterra.Preys);
                    //Draw(CreateNewRandomDoubleArray(2500), lotkaVolterra.Preys);
                }

                if (TypeOfCreatures.ToLower() == "predators")
                {
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = Color.red;
                    Draw(lotkaVolterra.PredatorsPredict, lotkaVolterra.Predators);
                    //Draw(CreateNewRandomDoubleArray(2500), lotkaVolterra.Predators);
                }
            }
            if (!backgroundPanel.enabled)
            {
                Clear();
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

        private void Draw(double[] predictions, double startPos)
        {
            Vector3 startPoint = new Vector3(0, (float)startPos);
            Vector3[] Tops = ConvertDoubleToVector3(predictions);

            lineRenderer.gameObject.transform.localPosition = ZeroCoordinates; //zeroCoordinates.position;
            lineRenderer.positionCount = Tops.Length + 1;
            lineRenderer.SetPosition(0, startPoint);
            for (int i = 1; i <= Tops.Length; i++)
            {
                lineRenderer.SetPosition(i, Tops[i-1]);
            }
        }

        private Vector3[] ConvertDoubleToVector3(double[] array)
        {
            var xStep = transform.parent.GetComponent<RectTransform>().rect.width * 1/2500;
            var yStep = transform.parent.GetComponent<RectTransform>().rect.height * 1/100;
            Vector3[] result = new Vector3[array.Length];
            for(int i = 0; i < array.Length; i++)
            {
                result[i] = new Vector3((float)(xStep * i), (float)array[i] * yStep);
            }
            return result;
        }

        private void Clear()
        {
            lineRenderer.positionCount = 0;
        }
    }
}