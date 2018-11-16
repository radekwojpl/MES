using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.BasicStruct
{
    public class Node
    {
        public Node()
        {

        }

        public Node(float x, float y)
        {
            _X = x;
            _Y = y;
        }
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

        private float _T;

        public float T
        {
            get { return _T; }
            set { _T = value; }
        }

        //TODO zmienić to na dane z pliku 
        public static List<Node> BuildNodes(int nh, int nl, int L, int H, float t)
        {
            List<Node> nodes = new List<Node>();

            int deltax = H / (nh - 1);
            int deltay = L / (nl - 1);


            for (int i = 0; i <= L; i += deltay)
            {
                for (int j = 0; j <= H; j += deltax)
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
