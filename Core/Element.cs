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

            for (int i = 1; i < (nh * nl)- nh; i++)
            {
                var elemnt = new Element();
                elemnt.K = k;
                elemnt.NodesIDList.Add(i);
                elemnt.NodesIDList.Add(i + 1);
                elemnt.NodesIDList.Add(i + 6);
                elemnt.NodesIDList.Add(i + 7);

                result.Add(elemnt);
            }
            return result;
        }
    }


}
