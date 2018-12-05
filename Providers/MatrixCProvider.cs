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
        public MatrixCProvider(IUniversalElement universalElement, float ro, float c, float[] detJ)
        {
            _UniversalElement = universalElement;
            _Ro = ro;
            _C = c;
            _DetJ = detJ;

            CountMatrixC();
        }

        private float[] _DetJ;

        public float[] DetJ
        {
            get { return _DetJ; }
            set { _DetJ = value; }
        }


        private float _Ro;

        public float Ro
        {
            get { return _Ro; }
            set { _Ro = value; }
        }

        private float _C;

        public float C
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

        private float[,] _MatrixC;

        public float[,] MatrixC
        {
            get { return _MatrixC; }
            set { _MatrixC = value; }
        }

        private void CountMatrixC()
        {
          
            List<float[,]> tmp = new List<float[,]>();

            for (int i = 0; i < 4; i++)
            {
                float[,] PointOfIntegration = new float[4, 4];
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        PointOfIntegration[j, k] = _UniversalElement.N[i, j] * _UniversalElement.N[i, k] * _Ro * _C * DetJ[i] ;
                    }
                }
                tmp.Add(PointOfIntegration);
            }

            _MatrixC = new float[4, 4];

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
