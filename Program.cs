using MES_App.BasicStruct;
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

            var tmp1 = Node.BuildNodes(6,4,20);
            var tmp2 = Element.BuildElements(6, 4, 10);

        }
    }
}
