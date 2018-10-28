using MES_App.BasicStruct;
using MES_App.Core;
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

        private StartUpData _StartData;

        public StartUpData StartUpData
        {
            get { return _StartData; }
            set { _StartData = value; }
        }


        public Grid BuildGrid( StartUpData startUpData)
        {
            _StartData = startUpData;
            _Nodes = Node.BuildNodes(StartUpData.nh, startUpData.nl,startUpData.L,startUpData.H, 20);
            _Elements = Element.BuildElements(startUpData.nh, startUpData.nl, 10);

            return new Grid(_Nodes, _Elements);
        }
    }
}
