using System;

namespace MicrowaveTools.Helper
{
    public static class Helper
    {
        public static double Coth(double x)
        {
            double result;

            double temp;
            temp = Math.Exp(x);
            result = (temp + 1 / temp) / (temp - 1 / temp);

            return result;
        }

        public static double Sech(double x)
        {
            double result;

            double temp;
            temp = Math.Exp(x);
            result = 2 / (temp + 1 / temp);

            return result;
        }
    }
}
