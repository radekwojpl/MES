using MES_App.BasicStruct;
using MES_App.Controller;
using MES_App.Core;
using MES_App.DataLoaders.File;
using MES_App.Extension;
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
            

            MatrixHProvider matrixHProvider;
            MatrixCProvider matrixCProvider;

            IUniversalElement universalElement = new UniversalElement();
            var GlobalH = new double[16, 16];
            var GlobalC = new double[16, 16];
            var GlobalP = new double[16];

            var localid = new int[4];
            localid[0] = 1;
            localid[1] = 2;
            localid[2] = 3;
            localid[3] = 4;

            int z = 0;
            for (double i = 0; i < startUPData.SimulationTime; i += startUPData.SimulationStepTime)
            {
                GlobalH = new double[16, 16];
                GlobalC = new double[16, 16];
                GlobalP = new double[16];
                foreach (var item in grid.Elements)
                {
                    var nodes = grid.GetNodesByElement(grid.GetElementByID(z));
                    List<bool> hbcPlaces;

                    HBCMatrixPlaces(out hbcPlaces, nodes);


                    JacobianProvider jacobianProvider = new JacobianProvider(nodes,
                                                                                universalElement);

                     matrixHProvider = new MatrixHProvider(universalElement,
                                                                            jacobianProvider.ReverseJacobian,
                                                                                jacobianProvider.DejJacobian,
                                                                                    startUPData.Conductivity);

                    double[] Ids = new double[4];
                    Ids[0] = item.NodesIDList[0];
                    Ids[1] = item.NodesIDList[1];
                    Ids[2] = item.NodesIDList[2];
                    Ids[3] = item.NodesIDList[3];

                    Local2DMarixToGlobal(localid, Ids, matrixHProvider.MatrixH, GlobalH);


                    matrixCProvider = new MatrixCProvider(universalElement,
                                                                          startUPData.SpecificHeat,
                                                                            startUPData.Density,
                                                                                jacobianProvider.DejJacobian);

                    Local2DMarixToGlobal(localid, Ids, matrixCProvider.MatrixC, GlobalC);


                   
                    for (int j = 0; j < universalElement.SurfacePointsOfIntegration.Length; j += 2)
                    {
                        if (hbcPlaces[j / 2] == true)
                        {

                            var surfacePoint = new UniversalPoint[2];
                            surfacePoint[0] = universalElement.SurfacePointsOfIntegration[j];
                            surfacePoint[1] = universalElement.SurfacePointsOfIntegration[j + 1];
                            BorderContitionMatrixHProvider borderContitionMatrixHProvider = new BorderContitionMatrixHProvider(surfacePoint,
                                                                                                                               0.0333333,
                                                                                                                              startUPData.Alfa);


                            var tmp = new VectorPProvider(surfacePoint[0], 0.033333333333, 1200, 300);
                            Local1DMatrixToGlobal(localid, Ids, tmp.Result, GlobalP);


                            var tmp1 = new VectorPProvider(surfacePoint[1], 0.03333333333, 1200, 300);
                            Local1DMatrixToGlobal(localid, Ids, tmp1.Result, GlobalP);

                            Local2DMarixToGlobal(localid, Ids, borderContitionMatrixHProvider.Result, GlobalH);
                        }

                    }

                    



                    z++;
                }
                CountGlobalMatrixH(startUPData.SimulationStepTime, GlobalC, ref GlobalH);
                CountGlobalVectorP(50, GlobalC,grid,ref GlobalP);

                GauseProvider gauseProvider = new GauseProvider();
                var tempresult = gauseProvider.GaussCalculation(16, GlobalH, GlobalP);
                Console.WriteLine("GlobalH--------------------------");
                GlobalH.PrintTable(16, 16);
                Console.WriteLine("GlobalP--------------------------");
                GlobalP.PrintTable(16);
                Console.WriteLine();
                Console.WriteLine("Temperatury--------------------");
                tempresult.PrintTable(16);
                for (int p = 0; p < 16; p++)
                {
                    grid.Nodes[p].T = tempresult[p];
                }

                
               

                Console.WriteLine("--------------------------------------");


                z = 0;

            }

            Console.WriteLine("Test");

            Console.ReadKey();


        }
        #region  BuildJacobian cos tam
        public static void CountGlobalMatrixH(double dt, double[,] MatrixC, ref double[,] MatrixH)
        {
            var tmpMatrixC = MatrixC;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    MatrixH[i, j] += tmpMatrixC[i, j]/dt;
                }
            }
        }

        public static void CountGlobalVectorP(double dt, double[,] MatrixC, Grid grid, ref double[] VektorP )
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    MatrixC[i, j] /= dt;
                    MatrixC[j, i] *= grid.Nodes[i].T;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    VektorP[i] += MatrixC[i, j];
                }
            }
        }

        #endregion


        public static void Local2DMarixToGlobal(int[] LocalIds, double[] GlobalIds, double[,] localTab, double[,] gloabalTab)
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

        public static void Local1DMatrixToGlobal(int[] LocalIds, double[] GlobalIds, double[] loclaTab, double [] gloabalTab)
        {
            for (int i = 0; i < 4; i++)
            {
                var localx = LocalIds[i];
                int globalx = (int)GlobalIds[localx - 1];

                gloabalTab[globalx - 1] += loclaTab[i];
            }
        }

        public static void HBCMatrixPlaces(out List<bool> places, List<Node> nodes)
        {
            places = new List<bool>();
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (nodes[0].BC && nodes[1].BC)
                        {
                            places.Add(true);
                        }
                        else
                        {
                            places.Add(false);
                        }
                        break;
                    case 1:
                        if (nodes[1].BC && nodes[2].BC)
                        {
                            places.Add(true);
                        }
                        else
                        {
                            places.Add(false);
                        }
                        break;
                    case 2:
                        if (nodes[2].BC && nodes[3].BC)
                        {
                            places.Add(true);
                        }
                        else
                        {
                            places.Add(false);
                        }
                        break;
                    case 3:
                        if (nodes[3].BC && nodes[0].BC)
                        {
                            places.Add(true);
                        }
                        else
                        {
                            places.Add(false);
                        }
                        break;
                }
            }


        }
    }
}
