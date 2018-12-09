using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    public class StartUpData
    {
        public double InitialTemperature;
        public double SimulationTime;
        public double SimulationStepTime;
        public double AmbitientTemperature;
        public double Alfa;
        public double H;
        public double B;
        public double N_H;
        public double N_B;
        public double SpecificHeat;
        public double Conductivity;
        public double Density;

        public StartUpData(double initialTemp,
                           double simulationTime,
                           double simulationStepTime,
                           double ambitientTemp,
                           double alfa,
                           double h,
                           double b,
                           double n_h,
                           double n_b,
                           double specificHeat,
                           double conductivity,
                           double density
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
