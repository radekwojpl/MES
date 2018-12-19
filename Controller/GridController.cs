﻿using MES_App.BasicStruct;
using MES_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Controller
{
    class GridController
    {
        private List<Node> _Nodes;
        private List<Element> _Elements;
        private StartUpData _StartUpData;

        private Grid _Grid;

        public Grid Grid
        {
            get { return _Grid; }
            set { _Grid = value; }
        }


        public GridController(StartUpData startUpData)
        {
            _Nodes = Node.BuildNodes(startUpData.N_H,
                                        startUpData.N_B,
                                        startUpData.H,
                                        startUpData.B,
                                        startUpData.InitialTemperature);
            _Elements = Element.BuildElements(startUpData.N_H,
                                                    startUpData.N_B,
                                                    startUpData.Conductivity);

            _Grid = new Grid(_Nodes, _Elements);
        }

    }
}
