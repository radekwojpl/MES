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
            StartUpData startUPData = new StartUpData(100, 500, 50, 1200, 300, 0.1, 0.1, 4, 4, 700, 25, 7800);
            GridController GridEngine = new GridController(startUPData);
            Grid grid = GridEngine.Grid;
            IUniversalElement universalElement = new UniversalElement();
            var GlobalH = new double[16, 16];

            var localid = new int[4];
            localid[0] = 1;
            localid[1] = 2;
            localid[2] = 3;
            localid[3] = 4;

            int z = 0;
            for (double i = 0; i < startUPData.SimulationTime; i += startUPData.SimulationStepTime)
            {
                foreach (var item in grid.Elements)
                {
                    var nodes = grid.GetNodesByElement(grid.GetElementByID(z));

                    JacobianProvider jacobianProvider = new JacobianProvider(nodes,
                                                                                universalElement);

                    MatrixHProvider matrixHProvider = new MatrixHProvider(universalElement,
                                                                            jacobianProvider.ReverseJacobian,
                                                                                jacobianProvider.DejJacobian,
                                                                                    startUPData.Conductivity);
                    double[] Ids = new double[4];
                    Ids[0] = item.NodesIDList[0];
                    Ids[1] = item.NodesIDList[1];
                    Ids[2] = item.NodesIDList[2];
                    Ids[3] = item.NodesIDList[3];

                    LoclaToGlobal(localid, Ids, matrixHProvider.MatrixH,  GlobalH);


                   



                    MatrixCProvider matrixCProvider = new MatrixCProvider(universalElement,
                                                                          startUPData.SpecificHeat,
                                                                            startUPData.Density,
                                                                                jacobianProvider.DejJacobian);

                    for (int j = 0; j < universalElement.SurfacePointsOfIntegration.Length; j += 2)
                    {
                        var surfacePoint = new UniversalPoint[2];
                        surfacePoint[0] = universalElement.SurfacePointsOfIntegration[j];
                        surfacePoint[1] = universalElement.SurfacePointsOfIntegration[j + 1];
                        BorderContitionMatrixHProvider borderContitionMatrixHProvider = new BorderContitionMatrixHProvider(surfacePoint,
                                                                                                                           0.0333333,
                                                                                                                          startUPData.Alfa);
                     
                        //Console.WriteLine(z+  "---------------------" );
                    }


                    z++;
                }
                PrintMatrix(GlobalH, 16, 16);
                z = 0;

            }




            
           

            

 

            Console.WriteLine("Test");

            Console.ReadKey();


        }
        #region  BuildJacobian cos tam


        #endregion

        public static void PrintMatrix(double[,] cos , int row , int col)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(string.Format("{0} ", cos[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

        }

        public static void LoclaToGlobal(int[] LocalIds, double[] GlobalIds, double[,] localTab,  double[,] gloabalTab)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var localx = LocalIds[j];
                    int globalx = (int)GlobalIds[localx - 1];

                    var localy = LocalIds[i];
                    int globaly = (int)GlobalIds[localy - 1];

                    gloabalTab[globalx - 1, globaly - 1] += localTab[i, j];
                }
            }

        }

    }
}
