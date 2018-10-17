using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.BasicStruct
{
    public class Grid
    {
        public Grid(List<Node> nodes, List<Element> elements)
        {
            Nodes = nodes;
            Elements = elements;
        }

        private List<Node> _Nodes;

        public List<Node> Nodes
        {
            get { return _Nodes; }
            set { _Nodes = value; }
        }

        private List<Element> _Elements;

        public List<Element> Elements
        {
            get { return _Elements; }
            set { _Elements = value; }
        }

        


    }
}
