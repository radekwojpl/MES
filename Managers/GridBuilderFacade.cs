using MES_App.BasicStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Managers
{
    class GridBuilderFacade
    {
        private List<Node> _Nodes;
        private List<Element> _Elements;
        

        public Grid BuildGrid()
        {
            _Nodes = Node.BuildNodes(6, 4, 20);
            _Elements = Element.BuildElements(6, 4, 10);

            return new Grid(_Nodes, _Elements);
        }
    }
}
