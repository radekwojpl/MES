using MES_App.BasicStruct;
using MES_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Providers
{
    class BorderContitionMatrixHProvider
    {
        public BorderContitionMatrixHProvider(UniversalPoint[] points, double detJ, double alfa)
        {
            CountN(points);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        _Result[j,z] += N[i, j] * N[i,z]*alfa * detJ;
                    }
                    
                }
            }

        }

        private double[,] N = new double[2, 4];

        private double[,] _Result = new double[4, 4];

        public double[,] Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        private void CountN(UniversalPoint[] points)
        {
            for (int i = 0; i < 2; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    N[i, j] = 1 / 4 * (1 - points[i].X) * (1 - points[i].Y);
                    N[i, j] = 1 / 4 * (1 + points[i].X) * (1 - points[i].Y);
                    N[i, j] = 1 / 4 * (1 + points[i].X) * (1 + points[i].Y);
                    N[i, j] = 1 / 4 * (1 - points[i].X) * (1 + points[i].Y);

                }

            }
        }


    }
}
