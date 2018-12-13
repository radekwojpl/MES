using MES_App.BasicStruct;
using MES_App.Controller;
using MES_App.Core;
using MES_App.DataLoaders.File;
using MES_App.Interfaces;
using MES_App.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App
{

    class Program
    {

        static void Main(string[] args)
        {
            StartUpData startUPData = new StartUpData(100, 500, 50, 1200, 300, 0.1, 0.1, 4, 4, 700, 25, 780);
            GridController GridEngine = new GridController(startUPData);


            //for (double i = 0; i < startUPData.SimulationTime; i+= startUPData.SimulationStepTime)
            //{

            //}

            //var tmp = 1 / (double)Math.Sqrt(3);
            //UniversalPoint [] universalPoint = new UniversalPoint[2];
            //universalPoint[0] = new UniversalPoint(-tmp, -1);



            //universalPoint[1] = new UniversalPoint(tmp, -1);



            //BorderContitionMatrixHProvider vs = new BorderContitionMatrixHProvider(universalPoint, 5, 25);
            var tmp = 1 / (double)Math.Sqrt(3);
           
            UniversalPoint universalPoint = new UniversalPoint(-1, -tmp);
            VectorPProvider vs = new VectorPProvider(universalPoint, 0.01666666, 1200.0, 300.0);


            Console.WriteLine("Test");

            Console.ReadKey();


        }
        #region  BuildJacobian cos tam

        public static void BuildJacobiaMatrix(out double[,] result, List<Node> points, IUniversalElement universalElement)
        {
            result = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                result[0, i] = universalElement.dN_dKSI[i, 0] * points[0].X + universalElement.dN_dKSI[i, 1] * points[1].X + universalElement.dN_dKSI[i, 2] * points[2].X + universalElement.dN_dKSI[i, 3] * points[3].X;
                result[1, i] = universalElement.dN_dKSI[i, 0] * points[0].Y + universalElement.dN_dKSI[i, 1] * points[1].Y + universalElement.dN_dKSI[i, 2] * points[2].Y + universalElement.dN_dKSI[i, 3] * points[3].Y;
                result[2, i] = universalElement.dN_dETA[i, 0] * points[0].X + universalElement.dN_dETA[i, 1] * points[1].X + universalElement.dN_dETA[i, 2] * points[2].X + universalElement.dN_dETA[i, 3] * points[3].X;
                result[3, i] = universalElement.dN_dETA[i, 0] * points[0].Y + universalElement.dN_dETA[i, 1] * points[1].Y + universalElement.dN_dETA[i, 2] * points[2].Y + universalElement.dN_dETA[i, 3] * points[3].Y;

            }

        }

        public static void BuildDetFromJacobianMatrix(out double[] result, double[,] jacobianMatrix)
        {
            result = new double[4];
            for (int i = 0; i < 4; i++)
            {

                result[i] = jacobianMatrix[0, i] * jacobianMatrix[3, i] - jacobianMatrix[1, i] * jacobianMatrix[2, i];
            }
        }

        public static void ReversJacobian(double[,] jacobian, double[] detJacobian, out double[,] result, int row, int column)
        {
            result = new double[row, column];

            for (int i = 0; i < 4; i++)
            {
           
                for (int j = 0; j < 4; j++)
                {
                    result[j, i] = jacobian[3 - j, i] / detJacobian[i];
                }
            }


        }

        #endregion

        public static void PrintMatrix(double[,] cos)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(string.Format("{0} ", cos[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

        }

    }
}
