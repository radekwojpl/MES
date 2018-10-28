using MES_App.BasicStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    class UniversalElement
    {

        private UniversalPoint[] _UniversalPoints = new UniversalPoint[4];

        public UniversalPoint[] UniversalPoints
        {
            get { return _UniversalPoints; }
            set { _UniversalPoints = value; }
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

        /// <summary>
        /// Przenies to do innej funkcji 
        /// </summary>
        //public void BuildJacobianMatrix(out float[,] result, UniversalPoint[] points)
        //{
        //    result = new float[4, 4];

        //    for (int i = 0; i < 4; i++)
        //    {
        //        result[i, 0] = dN_dKSI[i, 0] * 0 + dN_dKSI[i, 1] * 10 + dN_dKSI[i , 2] * 10 + dN_dKSI[i, 3] * 0;
        //        result[i, 1] = dN_dETA[i, 0] * 0 + dN_dETA[i, 1] * 10 + dN_dETA[i , 2] * 10 + dN_dETA[i, 3] * 0;
        //        result[i, 2] = dN_dKSI[i, 0] * 0 + dN_dKSI[i, 1] * 0 +  dN_dKSI[i , 2] * 8 +  dN_dKSI[i, 3] * 8;
        //        result[i, 3] = dN_dETA[i, 0] * 0 + dN_dETA[i, 1] * 0 +  dN_dETA[i , 2] * 8 +  dN_dETA[i, 3] * 8;

        //    }
        //}

        public void SetUp()
        {
            var tmp = 1 / (float)Math.Sqrt(3);
            _UniversalPoints[0] = new UniversalPoint(-tmp,  -tmp);
            _UniversalPoints[1] = new UniversalPoint( tmp, -tmp);
            _UniversalPoints[2] = new UniversalPoint( tmp,  tmp);
            _UniversalPoints[3] = new UniversalPoint(-tmp,  tmp);
        }

        public UniversalElement()
        {
            
            SetUp();

            CountFunctionShapeDerative_DN_Ksi(_UniversalPoints, out _dN_dKSI, 4, 4);
            CountFunctionShapeDerative_DN_ETA(_UniversalPoints, out _dN_dETA, 4, 4);

        }

        public void CountFunctionShapeDerative_DN_Ksi(UniversalPoint[] point, out float[,] result, int rows, int columns)
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

        public void CountFunctionShapeDerative_DN_ETA(UniversalPoint[] point, out float[,] result, int rows, int columns)
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
