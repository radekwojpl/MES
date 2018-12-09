using MES_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.BasicStruct
{
    public class Element
    {
        private List<double> _NodesIDList = new List<double>();

        public List<double> NodesIDList
        {
            get { return _NodesIDList; }
            set { _NodesIDList = value; }
        }

        private double _K;

        public double K
        {
            get { return _K; }
            set { _K = value; }
        }


        public static List<Element> BuildElements(double nh, double nb, double conductivity)
        {
            var result = new List<Element>();
            int leftPoint = 1;
         

            for (double i = 1; i < (nh * nb) - nh; i++)
            {
                double rightPoint = leftPoint + nh;
                var tmp = new List<double>();
                tmp.Add(leftPoint);
                tmp.Add(rightPoint);
                tmp.Add(rightPoint + 1);
                tmp.Add(leftPoint + 1);

                if (i % 4 != 0)
                {
                    var element = new Element();
                    element.K = conductivity;
                    element.NodesIDList = tmp;
                    leftPoint++;

                    result.Add(element);
                }
                else
                {
                    leftPoint++;
                }
                

            }
            return result;
        }
    }


}
