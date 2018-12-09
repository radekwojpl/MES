using MES_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Providers
{
    class MatrixCProvider
    {
        public MatrixCProvider(IUniversalElement universalElement, double ro, double c, double[] detJ)
        {
            _UniversalElement = universalElement;
            _Ro = ro;
            _C = c;
            _DetJ = detJ;

            CountMatrixC();
        }

        private double[] _DetJ;

        public double[] DetJ
        {
            get { return _DetJ; }
            set { _DetJ = value; }
        }


        private double _Ro;

        public double Ro
        {
            get { return _Ro; }
            set { _Ro = value; }
        }

        private double _C;

        public double C
        {
            get { return _C; }
            set { _C = value; }
        }

        private IUniversalElement _UniversalElement;

        public IUniversalElement UniversalElement
        {
            get { return _UniversalElement; }
            set { _UniversalElement = value; }
        }

        private double[,] _MatrixC;

        public double[,] MatrixC
        {
            get { return _MatrixC; }
            set { _MatrixC = value; }
        }

        private void CountMatrixC()
        {
          
            List<double[,]> tmp = new List<double[,]>();

            for (int i = 0; i < 4; i++)
            {
                double[,] PointOfIntegration = new double[4, 4];
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        PointOfIntegration[j, k] = _UniversalElement.N[i, j] * _UniversalElement.N[i, k] * _Ro * _C * DetJ[i] ;
                    }
                }
                tmp.Add(PointOfIntegration);
            }

            _MatrixC = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _MatrixC[i, j] = tmp[0][i, j] + tmp[1][i, j] + tmp[2][i, j] + tmp[3][i, j];
                }
            }
            

        }



    }
}
