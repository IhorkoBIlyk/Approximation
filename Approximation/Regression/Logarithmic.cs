﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Approximation.Regression
{
    class Logarithmic : Graph
    {

        public Logarithmic(List<double> x, List<double> y) : base(x, y)
        {
            b = getB();
            a = getA();
            r = getR();
            det = getDet();
            err = getRelativeError();
            name = rm.GetString("Logarithmic");
            funcText = "a + b * ln(x)";

            function = (z) => a + b * Math.Log(z);
        }

        public override double getB()
        {
            double part1 = x.Count * Funcs.sumLnX(x, y) - Funcs.sumLn(x) * Funcs.sum(y);
            double part2 = x.Count * Funcs.sumPowLn(x, 2) - Math.Pow(Funcs.sumLn(x),2);
            double b = part1 / part2;
            return b;
        }

        public override double getA()
        {
            double a = 1f / x.Count * Funcs.sum(y) - b / x.Count * Funcs.sumLn(x);
            return a;
        }


        //Коефіцієнт Кореляції
        public override double getR()
        {
            double r = Math.Sqrt(1f - (sumCor1(y) / sumCor2(y)));
            return r;
        }

        public double sumCor1(List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                //sum += Math.Pow((y[i] - yx(y[i])), 2);
                sum += Math.Pow((y[i] - a - b * Math.Log(x[i])), 2);
            }
            return sum;
        }

        public double sumCor2(List<double> y)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow((y[i] - y_(y)), 2);
            }
            return sum;
        }

        //Коефіцієнт детермінації
        public override double getDet()
        {
            return Math.Pow(r, 2);
        }

        //Середня помилка апроксимації
        public override double getRelativeError()
        {
            double part1 = 1f / x.Count * sumEr() * 100f;
            return part1;
        }

        //Сума
        private double sumEr()
        {
            double sum = 0;

            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Abs((y[i] - yx(x[i])) / y[i]);
            }
            return sum;
        }

        //y*
        private double yx(double X)
        {
            //double yx = a * Math.Pow(X,2) + b * X + c;
            double yx = a + b * Math.Log(X);
            return yx;
        }

        private double y_(List<double> x)
        {
            double y_ = Funcs.sum(x) / x.Count;
            return y_;
        }


    }
}
