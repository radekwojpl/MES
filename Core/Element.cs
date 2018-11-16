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
        private List<int> _NodesIDList = new List<int>();

        public List<int> NodesIDList
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


        public static List<Element> BuildElements(int nh, int nl, int k)
        {
            var result = new List<Element>();
            int leftPoint = 1;
         

            for (int i = 1; i < (nh * nl) - nh; i++)
            {
                int rightPoint = leftPoint + nh;
                var tmp = new List<int>();
                tmp.Add(leftPoint);
                tmp.Add(rightPoint);
                tmp.Add(rightPoint + 1);
                tmp.Add(leftPoint + 1);

                if (i % nh != 0)
                {
                    var element = new Element();
                    element.K = k;
                    element.NodesIDList = tmp;
                    leftPoint++;

                    result.Add(element);
                }

            }
            return result;
        }
    }


}
