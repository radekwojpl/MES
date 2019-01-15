using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Providers
{
    public class GauseProvider
    {

        public double[] GaussCalculation(int numberOfElementsInArray, double[,] globalMatrixH, double[] globalVectorP)
        {
            double m, s, e;
            e = Math.Pow(10, -12);
            double[] tabResult = new double[numberOfElementsInArray];

            double[,] tabAB = new double[numberOfElementsInArray, numberOfElementsInArray+1];
            for (int i = 0; i < numberOfElementsInArray; i++)
            {
                for (int j = 0; j < numberOfElementsInArray; j++)
                {
                    tabAB[j, i] = globalMatrixH[j,i];
                }
            }

            for (int i = 0; i < numberOfElementsInArray; i++)
            {
                tabAB[i,numberOfElementsInArray] = globalVectorP[i];
            }

            for (int i = 0; i < numberOfElementsInArray - 1; i++)
            {
                for (int j = i + 1; j < numberOfElementsInArray; j++)
                {
                    if (Math.Abs(tabAB[i,i]) < e)
                    {
                        Console.WriteLine("Can not divide by 0");
                        break;
                    }

                    m = -tabAB[j,i] / tabAB[i,i];
                    for (int k = 0; k < numberOfElementsInArray + 1; k++)
                    {
                        tabAB[j,k] += m * tabAB[i,k];
                    }
                }
            }

            for (int i = numberOfElementsInArray - 1; i >= 0; i--)
            {
                s = tabAB[i,numberOfElementsInArray];
                for (int j = numberOfElementsInArray - 1; j >= 0; j--)
                {
                    s -= tabAB[i,j] * tabResult[j];
                }
                if (Math.Abs(tabAB[i,i]) < e)
                {
                    Console.WriteLine("Can not divide by 0"); 
                    break;
                }
                tabResult[i] = s / tabAB[i,i];
            }
            return tabResult;
        }
    }
}
