using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Extension
{
   public static class DwoDimentionTableExtension
    {
        public static void PrintTable(this double[,] table, int row, int col )
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(string.Format("{0:F3} ", table[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

    }
}
