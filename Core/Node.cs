using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.BasicStruct
{
    public class Node
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

        private double _T;

        public double T
        {
            get { return _T; }
            set { _T = value; }
        }

        public static List<Node> BuildNodes(int nh, int nl, double t)
        {
            List<Node> nodes = new List<Node>();

            for (int i = 0; i <= 30; i+=10)
            {
                for (int j = 0; j <= 40; j+=8)
                {
                    Node tmp = new Node();
                    tmp.X = i;
                    tmp.Y = j;
                    tmp.T = t;
                    nodes.Add(tmp);
                }
            }
            return nodes;
        }
    }
}
