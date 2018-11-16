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

        private float[,] _dN_dETA;

        public float[,] dN_dETA
        {
            get { return _dN_dETA; }
            set { _dN_dETA = value; }
        }

        private float[,] _dN_dKSI;

        public float[,] dN_dKSI
        {
            get { return _dN_dKSI; }
            set { _dN_dKSI = value; }
        }



        public void SetUp()
        {
            var tmp = 1 / (float)Math.Sqrt(3);
            _PointsOfIntegration[0] = new UniversalPoint(-tmp, -tmp);
            _PointsOfIntegration[1] = new UniversalPoint(tmp, -tmp);
            _PointsOfIntegration[2] = new UniversalPoint(tmp, tmp);
            _PointsOfIntegration[3] = new UniversalPoint(-tmp, tmp);
        }

        public UniversalElement()
        {

            SetUp();

            int rows = 4;
            int columns = 4;

            CountFunctionShapeDerative_DN_Ksi(_PointsOfIntegration, out _dN_dKSI, rows, columns);
            CountFunctionShapeDerative_DN_ETA(_PointsOfIntegration, out _dN_dETA, rows, columns);

        }

        private void CountFunctionShapeDerative_DN_Ksi(UniversalPoint[] point, out float[,] result, int rows, int columns)
        {
            float tmp;
            result = new float[rows, columns];

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

        private void CountFunctionShapeDerative_DN_ETA(UniversalPoint[] point, out float[,] result, int rows, int columns)
        {
            float tmp;
            result = new float[rows, columns];

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


    }
}
