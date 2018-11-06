using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    class UniversalPoint
    {
        private float _X;

        public float X
        {
            get { return _X; }
            set { _X = value; }
        }

        private float _Y;

        public float Y
        {
            get { return _Y; }
            set { _Y = value; }

        }

        public UniversalPoint(float x, float y)
        {
            _X = x;
            _Y = y;
        }
    }
}
