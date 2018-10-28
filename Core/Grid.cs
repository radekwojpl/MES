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

       public List<Node> GetNodesByElement(Element element)
        {
            var result = new List<Node>();
            foreach (var item in element.NodesIDList)
            {
                for (int i = 0; i <= item; i++)
                {
                    if (i == item-1)
                    {
                        result.Add(Nodes[i]);
                    }
                   
                }
            }
            return result;
        }

        public Element GetElementByID( int id)
        {
            return Elements[id];
        }


    }
}
