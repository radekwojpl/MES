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
        private List<float> _NodesIDList = new List<float>();

        public List<float> NodesIDList
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


        public static List<Element> BuildElements(float nh, float nb, float conductivity)
        {
            var result = new List<Element>();
            int leftPoint = 1;
         

            for (float i = 1; i < (nh * nb) - nh; i++)
            {
                float rightPoint = leftPoint + nh;
                var tmp = new List<float>();
                tmp.Add(leftPoint);
                tmp.Add(rightPoint);
                tmp.Add(rightPoint + 1);
                tmp.Add(leftPoint + 1);

                if (i % nh != 0)
                {
                    var element = new Element();
                    element.K = conductivity;
                    element.NodesIDList = tmp;
                    leftPoint++;

                    result.Add(element);
                }

            }
            return result;
        }
    }


}
