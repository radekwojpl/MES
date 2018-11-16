using MES_App.BasicStruct;
using MES_App.Core;
using MES_App.DataLoaders.File;
using MES_App.Interfaces;
using MES_App.Managers;
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
            float[,] jacobianMatrix;
            float[] detJacobianMatrix;
            float[,] fullJacobian;



            var elemntPoints = GetNodesByElement(GetElementByID(0));
            BuildJacobiaMatrix(out jacobianMatrix, elemntPoints, universalElement);
            BuildDetFromJacobianMatrix(out detJacobianMatrix, jacobianMatrix);
            BuildFullJacobian(jacobianMatrix, detJacobianMatrix, out fullJacobian, 4, 4);



            PrintMatrix(universalElement.dN_dKSI);
            Console.WriteLine('\n');

            Console.WriteLine('\n');

            PrintMatrix(universalElement.dN_dETA);

            Console.WriteLine('\n');

            PrintMatrix(jacobianMatrix);
            Console.WriteLine('\n');


            PrintMatrix(fullJacobian);

            Console.WriteLine('\n');


            PrintMatrix(BuildNx_X(universalElement.dN_dKSI, universalElement.dN_dETA, fullJacobian));

            Console.WriteLine('\n');

            PrintMatrix(BuildNx_Y(universalElement.dN_dKSI, universalElement.dN_dETA, fullJacobian));

            Console.WriteLine('\n');

            var tmp3 = BuildNx_X(universalElement.dN_dKSI, universalElement.dN_dETA, fullJacobian);
            var tmp2 = BuildNx_Y(universalElement.dN_dKSI, universalElement.dN_dETA, fullJacobian);

            BuildMatrixH(tmp3, tmp2, detJacobianMatrix, 30);
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

            //float[] tmp = new float[4];
            //float[] tmp1 = new float[4];
            //float[] tmp2 = new float[4];

            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        tmp[i] += universalElement.dN_dKSI[i, j] * points[j].X;
            //        tmp1[i] += universalElement.dN_dKSI[i, j] * points[j].Y;
            //    }
            //}


        }

        public static void BuildDetFromJacobianMatrix(out float[] result, float[,] jacobianMatrix)
        {
            result = new float[4];
            for (int i = 0; i < 4; i++)
            {

                result[i] = jacobianMatrix[0, i] * jacobianMatrix[3, i] - jacobianMatrix[1, i] * jacobianMatrix[2, i];
            }
        }

        public static void BuildFullJacobian(float[,] jacobian, float[] detJacobian, out float[,] result, int row, int column)
        {
            result = new float[row, column];

            for (int i = 0; i < 4; i++)
            {
                result[0, i] = jacobian[3, i] / detJacobian[0];
                result[1, i] = jacobian[2, i] / detJacobian[1];
                result[2, i] = jacobian[1, i] / detJacobian[2];
                result[3, i] = jacobian[0, i] / detJacobian[3];
            }


        }

        #endregion

        #region ConnectTwoCoordinateSystem

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

        public static float[,] BuildNx_X(float[,] dn_dksi, float[,] dn_deta, float[,] jacobian)
        {
            float[,] result = new float[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var J1_1_1 = jacobian[0, i];
                    var dN1_dKsi = dn_dksi[i, j];
                    var J1_1_2 = jacobian[1, i];
                    var dN1_dEta = dn_deta[i, j];
                    result[i, j] = J1_1_1 * dN1_dKsi + J1_1_2 * dN1_dEta;
                }
            }

            return result;
        }

        public static float[,] BuildNx_Y(float[,] dn_dksi, float[,] dn_deta, float[,] jacobian)
        {
            float[,] result = new float[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var J1_2_1 = jacobian[2, i];
                    var dN1_dKsi = dn_dksi[i, j];
                    var J1_2_2 = jacobian[3, i];
                    var dN1_dEta = dn_deta[i, j];
                    result[i, j] = J1_2_1 * dN1_dKsi + J1_2_2 * dN1_dEta;
                }
            }

            return result;
        }


        public static void BuildMatrixH(float[,] dNx_X, float[,] dNx_Y, float[] detJ, float K)
        {
            
            List<float[,]> dN_dx_dN_dx_T = new List<float[,]>();

            for (int z = 0; z < 4; z++)
            {
                float[,] Pcx = new float[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Pcx[i, j] = dNx_X[z, i] * dNx_X[z, j] * detJ[z];
                    }
                }

                dN_dx_dN_dx_T.Add(Pcx);
                Console.WriteLine('\n');
                PrintMatrix(Pcx);
                Console.WriteLine('\n');
            }


            List<float[,]> dN_dy_dN_dy_T = new List<float[,]>();
           
            for (int z = 0; z < 4; z++)
            {
                float[,] Pcy = new float[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Pcy[i, j] = dNx_Y[z, i] * dNx_Y[z, j] * detJ[z];
                    }
                }

                dN_dy_dN_dy_T.Add(Pcy);
            }

            List<float[,]> tmp = new List<float[,]>();
         
            for (int z = 0; z < 4; z++)
            {
                float[,] Pcp = new float[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var tmpp = dN_dx_dN_dx_T[z][j, i];
                        var tmppp = dN_dy_dN_dy_T[z][j, i];
                        Pcp[i, j] = K * (tmpp + tmppp);
                    }
                }

                tmp.Add(Pcp);
          
            }

            float[,] result = new float[4,4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = tmp[0][i, j] + tmp[1][i, j] + tmp[2][i, j] + tmp[3][i, j];
                }
            }
        }



        #endregion
    }
}
