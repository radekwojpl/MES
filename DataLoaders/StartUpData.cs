using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    public class StartUpData
    {
        public int H;
        public int L;
        public int nh;
        public int nl;
        public double k;
        public double t0;

        public StartUpData(string[] tmp)
        {
            H = int.Parse(tmp[0]);
            L = int.Parse(tmp[1]);
            nh = int.Parse(tmp[2]);
            nl = int.Parse(tmp[3]);
            k = double.Parse(tmp[4]);
            t0 = double.Parse(tmp[5]);
        }

    }
}
