using MES_App.BasicStruct;
using MES_App.Core;
using MES_App.DataLoaders.File;
using MES_App.Interfaces;
using MES_App.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataLoader dataLoader = new FileLoader();

            var tmp = dataLoader.LoadData();

            GridBuilderFacade gridBuilder = new GridBuilderFacade();

           var grid= gridBuilder.BuildGrid();

            var test = new UniversalElement(grid.Elements.ToArray(), grid.Nodes.ToArray());

            Console.ReadKey();
        }
    }
}
