﻿using MES_App.Core;
using MES_App.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.DataLoaders.File
{
    class FileLoader : IDataLoader
    {
        public StartUpData LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\MesApp\MES\TextFile1.txt"))
                {
                  
                    string line = sr.ReadLine();
                    string[] tmp = line.Split(',');

                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
