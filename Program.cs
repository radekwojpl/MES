using MES_App.BasicStruct;
using MES_App.Core;
using MES_App.DataLoaders.File;
using MES_App.Interfaces;
using MES_App.Managers;
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

        delegate Element ElementByID(int id);
        delegate List<Node> NodesByElement(Element element);

        static void Main(string[] args)
        {
            IDataLoader dataLoader = new FileLoader();

            var tmp = dataLoader.LoadData();

            GridBuilderFacade gridBuilder = new GridBuilderFacade();

            var grid = gridBuilder.BuildGrid(tmp);

            ElementByID GetElementByID = grid.GetElementByID;
            NodesByElement GetNodesByElement = grid.GetNodesByElement;

            IUniversalElement universalElement = new UniversalElement();
            float[,] jacobianMatrix = new float [4,4];
            float[] detJacobianMatrix = new float[4];
            float[,] fullJacobian = new float[4, 4];



            var elemntPoints = GetNodesByElement(GetElementByID(0));
            BuildJacobiaMatrix(out jacobianMatrix, elemntPoints, universalElement);
            BuildDetFromJacobianMatrix(out detJacobianMatrix, jacobianMatrix);
            ReversJacobian(jacobianMatrix, detJacobianMatrix, out fullJacobian, 4, 4);

         

            PrintMatrix(universalElement.N);
            Console.WriteLine('\n');
            Console.WriteLine('\n');

            PrintMatrix(universalElement.dN_dKSI);
            Console.WriteLine('\n');

            Console.WriteLine('\n');

            PrintMatrix(universalElement.dN_dETA);

            Console.WriteLine('\n');

            PrintMatrix(jacobianMatrix);
            Console.WriteLine('\n');


            PrintMatrix(fullJacobian);

            Console.WriteLine('\n');

            IMatrixH _MatrixHProvier = new MatrixHProvider(universalElement, fullJacobian, detJacobianMatrix, 30.0f );

            PrintMatrix(_MatrixHProvier.dNx_dx);

            Console.WriteLine('\n');

            PrintMatrix(_MatrixHProvier.dNx_dy);

            Console.WriteLine('\n');

            PrintMatrix(_MatrixHProvier.MatrixH);
            Console.WriteLine('\n');

            MatrixCProvider matrixCProvider = new MatrixCProvider(universalElement, 7800, 700,detJacobianMatrix);
            Console.WriteLine('\n');

            PrintMatrix(matrixCProvider.MatrixC);

            Console.WriteLine('\n');

            Console.WriteLine('\n');
            UniversalPoint[] points = new UniversalPoint[2];
            points[0] = new UniversalPoint(-0.577350f, -1);
            points[1] = new UniversalPoint(0.577350f, 1);
            BorderContitionMatrixHProvider borderContitionMatrixHProvider = new BorderContitionMatrixHProvider(points, 5, 10);

            PrintMatrix(borderContitionMatrixHProvider.Result);

            Console.ReadKey();


        }
        #region  BuildJacobian cos tam

        public static void BuildJacobiaMatrix(out float[,] result, List<Node> points, IUniversalElement universalElement)
        {
            result = new float[4, 4];

            for (int i = 0; i < 4; i++)
            {
                result[0, i] = universalElement.dN_dKSI[i, 0] * points[0].X + universalElement.dN_dKSI[i, 1] * points[1].X + universalElement.dN_dKSI[i, 2] * points[2].X + universalElement.dN_dKSI[i, 3] * points[3].X;
                result[1, i] = universalElement.dN_dKSI[i, 0] * points[0].Y + universalElement.dN_dKSI[i, 1] * points[1].Y + universalElement.dN_dKSI[i, 2] * points[2].Y + universalElement.dN_dKSI[i, 3] * points[3].Y;
                result[2, i] = universalElement.dN_dETA[i, 0] * points[0].X + universalElement.dN_dETA[i, 1] * points[1].X + universalElement.dN_dETA[i, 2] * points[2].X + universalElement.dN_dETA[i, 3] * points[3].X;
                result[3, i] = universalElement.dN_dETA[i, 0] * points[0].Y + universalElement.dN_dETA[i, 1] * points[1].Y + universalElement.dN_dETA[i, 2] * points[2].Y + universalElement.dN_dETA[i, 3] * points[3].Y;

            }

        }

        public static void BuildDetFromJacobianMatrix(out float[] result, float[,] jacobianMatrix)
        {
            result = new float[4];
            for (int i = 0; i < 4; i++)
            {

                result[i] = jacobianMatrix[0, i] * jacobianMatrix[3, i] - jacobianMatrix[1, i] * jacobianMatrix[2, i];
            }
        }

        public static void ReversJacobian(float[,] jacobian, float[] detJacobian, out float[,] result, int row, int column)
        {
            result = new float[row, column];

            for (int i = 0; i < 4; i++)
            {
           
                for (int j = 0; j < 4; j++)
                {
                    result[j, i] = jacobian[3 - j, i] / detJacobian[i];
                }
            }


        }

        #endregion

        public static void PrintMatrix(float[,] cos)
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
