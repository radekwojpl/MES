using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
   public class UniversalPoint
    {
        private double _X;

        public double X
        {
            get { return _X; }
            set { _X = value; }
        }

        private double _Y;

        public double Y
        {
            get { return _Y; }
            set { _Y = value; }

        }

        public UniversalPoint(double x, double y)
        {
            _X = x;
            _Y = y;
        }
    }
}
