using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    public class StartUpData
    {
        private double H;
        private double L;
        private double nh;
        private double nl;
        private double k;
        private double t0;

        public StartUpData(string[] tmp)
        {
            H = double.Parse(tmp[0]);
            L = double.Parse(tmp[1]);
            nh = double.Parse(tmp[2]);
            nl = double.Parse(tmp[3]);
            k = double.Parse(tmp[4]);
            t0 = double.Parse(tmp[5]);
        }

    }
}
