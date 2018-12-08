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
        public VectorPProvider(UniversalPoint point, float detJ, float t0, float alfa, float ds)
        {
            _Result[0] = 1 / 4 * (1 - point.X) * (1 - point.Y) * t0 * detJ * alfa * ds / 2;
            _Result[1] = 1 / 4 * (1 + point.X) * (1 - point.Y) * t0 * detJ * alfa * ds / 2;
            _Result[2] = 1 / 4 * (1 + point.X) * (1 + point.Y) * t0 * detJ * alfa * ds / 2;
            _Result[3] = 1 / 4 * (1 - point.X) * (1 + point.Y) * t0 * detJ * alfa * ds / 2;
            
        }

        private float[] _Result = new float[4];

        public float[] Result
        {
            get { return _Result; }
            set { _Result = value; }
        }


    }
}
