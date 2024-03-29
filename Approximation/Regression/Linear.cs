﻿using Approximation.Regression;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Approximation
{
    public class Linear : Graph
    {

        public Linear(List<double> x, List<double> y) : base(x, y)
        {
            a = getA();
            b = getB();
            r = getR();
            det = getDet();
            err = getRelativeError();
            name = rm.GetString("Linear");
            funcText = "ax + b";

            function = (z) => a * z + b;
        }
        
        public override double getA()
        {
            double part1 = Funcs.sum(x) * Funcs.sum(y) - x.Count * Funcs.sum(x, y);
            double part2 = Math.Pow(Funcs.sum(x), 2) - x.Count * Funcs.sumPow(x,2);
            double a = part1 / part2;
            return a;
        }

        public override double getB()
        {             
            double part1 = Funcs.sum(x) * Funcs.sum(x, y) - Funcs.sumPow(x,2) * Funcs.sum(y);
            double part2 = Math.Pow(Funcs.sum(x), 2) - x.Count * Funcs.sumPow(x,2);
            double b = part1 / part2; ;
            return b;
        }

        //Коефіцієнт детермінації
        public override double getR()
        {
            double part1 = x.Count * Funcs.sum(x, y) - Funcs.sum(x) * Funcs.sum(y);
            double part2 = Math.Sqrt((x.Count * Funcs.sumPow(x,2) - Math.Pow(Funcs.sum(x), 2)) * 
                                     (x.Count * Funcs.sumPow(y,2) - Math.Pow(Funcs.sum(y), 2)));
            double r = part1 / part2; ;
            return r;
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

        public override string getFuncText()
        {
            return funcText;
        }

        //Сума
        private double sumEr()
        {
            double sum=0;

            for(int i = 0; i< x.Count;i++)
            {
                sum += Math.Abs((y[i] - yx(x[i])) / y[i]);
            }
            return sum;
        }

        //y*
        private double yx(double X)
        {
            double yx = a * X + b;            
            return yx;
        }
    }
}