using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Extension
{
    public static class OneDimentionTableExtension
    {
        public static void PrintTable(this double[] table, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(string.Format("{0:F3} ", table[i]));
            }
        }
    }
}
