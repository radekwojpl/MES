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
        double[,] dN_dETA { get; set; }
        double[,] dN_dKSI { get; set; }
        double[,] N { get; set; }
        UniversalPoint[] SurfacePointsOfIntegration { get; set; }

    }
}
