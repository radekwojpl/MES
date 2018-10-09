
using MES_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Interfaces
{
    interface IDataLoader
    {
        StartUpData LoadData();
    }
}
