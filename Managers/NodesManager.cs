using MES_App.BasicStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Managers
{
    class NodesManager
    {
       
         public static  List<Node> BuildNodes(int nh, int nl, double t)
        {
            List<Node> nodes = new List<Node>();
           
            for (int i = 0; i < nl; i++)
            {
                for (int j = 0; j < nh; j++)
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
