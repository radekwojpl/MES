using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    public class StartUpData
    {
        public float InitialTemperature;
        public float SimulationTime;
        public float SimulationStepTime;
        public float AmbitientTemperature;
        public float Alfa;
        public float H;
        public float B;
        public float N_H;
        public float N_B;
        public float SpecificHeat;
        public float Conductivity;
        public float Density;

        public StartUpData(float initialTemp,
                           float simulationTime,
                           float simulationStepTime,
                           float ambitientTemp,
                           float alfa,
                           float h,
                           float b,
                           float n_h,
                           float n_b,
                           float specificHeat,
                           float conductivity,
                           float density
                           )
        {
            InitialTemperature = initialTemp;
            SimulationTime = simulationTime;
            SimulationStepTime = simulationStepTime;
            AmbitientTemperature = ambitientTemp;
            Alfa = alfa;
            H = h;
            B = b;
            N_H = n_h;
            N_B = n_b;
            SpecificHeat = specificHeat;
            Conductivity = conductivity;
            Density = density;
        }

    }
}
