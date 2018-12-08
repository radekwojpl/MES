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
        public static List<Node> BuildNodes(float nh, float nb, float H, float B, float initailTemperature)
        {
            List<Node> nodes = new List<Node>();

            var cos = 0.1 / (4 - 1);

            float deltax = H / (nh - 1);
            float deltay = B / (nh - 1);
            

            for (float i = 0f; i < B; i += deltay)
            {
                for (float j = 0; j < H; j += deltax)
                {
                    if (i == 0 || j == 0)
                    {
                        Node tmp = new Node(i, j, initailTemperature);
                        tmp.BC = true;
                        nodes.Add(tmp);
                    }
                    else if (i == B || j == H)
                    {
                        Node tmp = new Node(i, j, initailTemperature);
                        tmp.BC = true;
                        nodes.Add(tmp);
                    }
                    else
                    {
                        Node tmp = new Node(i, j, initailTemperature);
                        nodes.Add(tmp);
                    }

                }
            }

            return nodes;
        }


    }
}
