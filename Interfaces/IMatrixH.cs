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
        float[,] dNx_dx { get; set; }
        float[,] dNx_dy { get; set; }
        float[,] MatrixH { get; set; }
        float K { get; set; }
        IUniversalElement  UniversalElement { get; set; }

    }
}
