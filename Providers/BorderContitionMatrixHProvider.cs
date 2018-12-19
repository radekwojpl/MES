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

        private double[] N1 = new double[4];
        private double[] N2 = new double[4];

        private double[,] _Result = new double[4, 4];

        public double[,] Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        public BorderContitionMatrixHProvider(UniversalPoint[] points, double detJ, double alfa)
        {
            CountN(points);


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _Result[i, j] += N1[i] * N1[j] * alfa * detJ;
                    _Result[i, j] += N2[i] * N2[j] * alfa * detJ;

                }

            }


        }


        private void CountN(UniversalPoint[] points)
        {



            N1[0] = 0.25 * (1.0 - points[0].X) * (1.0 - points[0].Y);
            N1[1] = 0.25 * (1.0 + points[0].X) * (1.0 - points[0].Y);
            N1[2] = 0.25 * (1.0 + points[0].X) * (1.0 + points[0].Y);
            N1[3] = 0.25 * (1.0 - points[0].X) * (1.0 + points[0].Y);

            N2[0] = 0.25 * (1.0 - points[1].X) * (1.0 - points[1].Y);
            N2[1] = 0.25 * (1.0 + points[1].X) * (1.0 - points[1].Y);
            N2[2] = 0.25 * (1.0 + points[1].X) * (1.0 + points[1].Y);
            N2[3] = 0.25 * (1.0 - points[1].X) * (1.0 + points[1].Y);


        }
    }


}

