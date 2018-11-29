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

        public Node(float x, float y, float t)
        {
            _X = x;
            _Y = y;
            _T = t;
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

        private bool _BC = false;

        public bool BC
        {
            get { return _BC; }
            set { _BC = value; }
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
                    if (i == 0 || j == 0)
                    {
                        Node tmp = new Node(i, j, t);
                        tmp.BC = true;
                        nodes.Add(tmp);
                    }
                    else if (i == L  || j == H)
                    {
                        Node tmp = new Node(i, j, t);
                        tmp.BC = true;
                        nodes.Add(tmp);
                    }
                    else
                    {
                        Node tmp = new Node(i, j, t);
                        nodes.Add(tmp);
                    }
                  
                }
            }

            return nodes;
        }

       
    }
}
