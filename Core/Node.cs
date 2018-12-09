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

        public Node(double x, double y, double t)
        {
            _X = x;
            _Y = y;
            _T = t;
        }
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

        private bool _BC = false;

        public bool BC
        {
            get { return _BC; }
            set { _BC = value; }
        }


        //TODO zmienić to na dane z pliku 
        public static List<Node> BuildNodes(double nh, double nb, double H, double B, double initailTemperature)
        {
            List<Node> nodes = new List<Node>();

            var cos = 0.1 / (4 - 1);

            double deltax = H / (nh - 1);
            double deltay = B / (nh - 1);
            

            for (double i = 0f; i <= B; i += deltay)
            {
                for (double j = 0; j <= H; j += deltax)
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
