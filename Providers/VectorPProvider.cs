using MES_App.BasicStruct;
using MES_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Providers
{
    class VectorPProvider
    {
        public VectorPProvider(UniversalPoint point, double detJ, double t0, double alfa)
        {
            Result[0] = 0.25 * (1 - point.X) * (1 - point.Y) * t0 * detJ * alfa;
            Result[1] = 0.25 * (1 + point.X) * (1 - point.Y) * t0 * detJ * alfa;
            Result[2] = 0.25 * (1 + point.X) * (1 + point.Y) * t0 * detJ * alfa;
            Result[3] = 0.25 * (1 - point.X) * (1 + point.Y) * t0 * detJ * alfa;

        }

        private double[] _Result = new double[4];

        public double[] Result
        {
            get { return _Result; }
            set { _Result = value; }
        }


    }
}
