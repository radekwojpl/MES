using MES_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Interfaces
{
    interface IMatrixH
    {
        double[,] dNx_dx { get; set; }
        double[,] dNx_dy { get; set; }
        double[,] MatrixH { get; set; }
        double K { get; set; }
        IUniversalElement  UniversalElement { get; set; }

    }
}
