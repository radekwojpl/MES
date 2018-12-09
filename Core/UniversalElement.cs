using MES_App.BasicStruct;
using MES_App.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    public class UniversalElement : IUniversalElement
    {

        private UniversalPoint[] _PointsOfIntegration = new UniversalPoint[4];

        public UniversalPoint[] PointsOfIntegration
        {
            get { return _PointsOfIntegration; }
            set { _PointsOfIntegration = value; }
        }

        private UniversalPoint[] _SurfacePointsOfIntegration = new UniversalPoint[8];

        public UniversalPoint[] SurfacePointsOfIntegration
        {
            get { return _SurfacePointsOfIntegration; }
            set { _SurfacePointsOfIntegration = value; }
        }


        private double[,] _dN_dETA;

        public double[,] dN_dETA
        {
            get { return _dN_dETA; }
            set { _dN_dETA = value; }
        }

        private double[,] _dN_dKSI;

        public double[,] dN_dKSI
        {
            get { return _dN_dKSI; }
            set { _dN_dKSI = value; }
        }

        private double[,] _N;

        public double[,] N
        {
            get { return _N; }
            set { _N = value; }
        }


        public void SetUp()
        {
            var tmp = 1 / (double)Math.Sqrt(3);
            _PointsOfIntegration[0] = new UniversalPoint(-tmp, -tmp);
            _PointsOfIntegration[1] = new UniversalPoint(tmp, -tmp);
            _PointsOfIntegration[2] = new UniversalPoint(tmp, tmp);
            _PointsOfIntegration[3] = new UniversalPoint(-tmp, tmp);

            _SurfacePointsOfIntegration[0] = new UniversalPoint(-tmp, -1);
            _SurfacePointsOfIntegration[1] = new UniversalPoint(tmp, -1);
            _SurfacePointsOfIntegration[2] = new UniversalPoint(1, -tmp);
            _SurfacePointsOfIntegration[3] = new UniversalPoint(1, tmp);

            _SurfacePointsOfIntegration[4] = new UniversalPoint(tmp, 1);
            _SurfacePointsOfIntegration[5] = new UniversalPoint(-tmp, 1);
            _SurfacePointsOfIntegration[6] = new UniversalPoint(-1, tmp);
            _SurfacePointsOfIntegration[7] = new UniversalPoint(-1, -tmp);



        }

        public UniversalElement()
        {

            SetUp();

            int rows = 4;
            int columns = 4;

            CountFunctionShapeDerative_DN_Ksi(_PointsOfIntegration, out _dN_dKSI, rows, columns);
            CountFunctionShapeDerative_DN_ETA(_PointsOfIntegration, out _dN_dETA, rows, columns);
            CountValueForPointOfIntegrationForFunctionShape(_PointsOfIntegration, out _N, rows, columns);
        }

        private void CountFunctionShapeDerative_DN_Ksi(UniversalPoint[] point, out double[,] result, int rows, int columns)
        {
            double tmp;
            result = new double[rows, columns];

            for (int i = 0; i < point.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            tmp = -0.25f * (1.0f - point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 1:
                            tmp = 0.25f * (1.0f - point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 2:
                            tmp = 0.25f * (1.0f + point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 3:
                            tmp = -0.25f * (1.0f + point[i].Y);
                            result[i, j] = tmp;
                            break;
                    }
                }
            }


        }

        private void CountFunctionShapeDerative_DN_ETA(UniversalPoint[] point, out double[,] result, int rows, int columns)
        {
            double tmp;
            result = new double[rows, columns];

            for (int i = 0; i < point.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            tmp = -0.25f * (1.0f - point[i].X);
                            result[i, j] = tmp;
                            break;
                        case 1:
                            tmp = -0.25f * (1.0f + point[i].X);
                            result[i, j] = tmp;
                            break;
                        case 2:
                            tmp = 0.25f * (1.0f + point[i].X);
                            result[i, j] = tmp;
                            break;
                        case 3:
                            tmp = 0.25f * (1.0f - point[i].X);
                            result[i, j] = tmp;
                            break;
                    }
                }
            }


        }

        private void CountValueForPointOfIntegrationForFunctionShape(UniversalPoint[] point, out double[,] result, int rows, int columns)
        {
            double tmp;
            result = new double[rows, columns];

            for (int i = 0; i < point.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            tmp = 0.25f * (1.0f - point[i].X) * (1.0f - point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 1:
                            tmp = 0.25f * (1.0f + point[i].X) * (1.0f - point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 2:
                            tmp = 0.25f * (1.0f + point[i].X) * (1.0f + point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 3:
                            tmp = 0.25f * (1.0f - point[i].X) * (1.0f + point[i].Y);
                            result[i, j] = tmp;
                            break;
                    }
                }
            }
        }


    }
}
