using MES_App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Interfaces
{
    public interface IUniversalElement
    {
       
        UniversalPoint[] PointsOfIntegration { get; set; }
        float[,] dN_dETA { get; set; }
        float[,] dN_dKSI { get; set; }

    }
}
