using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LotkaVolterraModel : MonoBehaviour
{
        public double Preys;
        public double Predators;


        public double[] PreysPredict;
        public double[] PredatorsPredict;

        public double Alpha;
        public double Beta;
        public double Gamma;
        public double Sigma;
        private double StepSize;
        private int StepCount;
        private int Time;


    public TMP_Text PreysCounter;
    public TMP_Text PredatorsCounter;

    public LotkaVolterraModel()
    {
        StepSize = 0.01;
        StepCount = 2500;
        Time = 25;
        PreysPredict = new double[StepCount];
        PredatorsPredict = new double[StepCount];
    }


        public void FindPredicts()
        {
            var x = GetDots(new double[] { Preys, Predators }, StepCount, StepSize);
            PreysPredict = x[0];
            PredatorsPredict = x[1];
        }

        public double[] GetFuncValues(double preys, double pred)
        {
            var xDot = new double[] { Alpha * preys - Beta * preys * pred, Sigma * preys * pred - Gamma * pred };
            return xDot;
        }

        public double[][] GetDots(double[] x0, int t, double step)
        {
            var x = new double[2][];
            x[0] = new double[t];
            x[1] = new double[t];
            x[0][0] = x0[0];
            x[1][0] = x0[1];
            for (var k = 0; k < t - 1; k++)
            {
                var newDots = RG4(x[0][k], x[1][k], step);

                x[0][k + 1] = newDots[0];
                x[1][k + 1] = newDots[1];
            }

            return x;
        }

        public double[] RG4(double firstX, double secondX, double step)
        {
            var k1 = GetFuncValues(firstX, secondX).Select(num => Math.Round(num * step, 15));
            var k2 = GetFuncValues(firstX + k1.First() / 2, secondX + k1.Last() / 2).Select(num => Math.Round(num * step, 15));
            var k3 = GetFuncValues(firstX + k2.First() / 2, secondX + k2.Last() / 2).Select(num => Math.Round(num * step, 15));
            var k4 = GetFuncValues(firstX + k3.First(), secondX + k3.Last()).Select(num => Math.Round(num * step, 15));

            var dx = new double[] { Math.Round((k1.First() + 2 * k2.First() + 2 * k3.First() + k4.First()) / 6, 15), Math.Round((k1.Last() + 2 * k2.Last() + 2 * k3.Last() + k4.Last()) / 6, 15) };

            return new double[] { Math.Round(firstX + dx[0], 15), Math.Round(secondX + dx[1], 15) };
        }


    public void UpdateAnimalsCounters()
    {
        PreysCounter.text = Preys.ToString();
        PredatorsCounter.text = Predators.ToString();

    }
}