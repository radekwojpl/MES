using MES_App.BasicStruct;
using MES_App.Core;
using MES_App.DataLoaders.File;
using MES_App.Interfaces;
using MES_App.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App
{
    class Program
    {
        delegate Element ElementByID(int id);
        delegate List<Node> NodesByElement(Element element);


       static void Main(string[] args)
        {
            IDataLoader dataLoader = new FileLoader();

            var tmp = dataLoader.LoadData();

            GridBuilderFacade gridBuilder = new GridBuilderFacade();

            var grid = gridBuilder.BuildGrid( tmp);

            ElementByID GetElementByID = grid.GetElementByID;
            NodesByElement GetNodesByElement = grid.GetNodesByElement;

            IUniversalElement universalElement = new UniversalElement();
            float[,] jacobianMatrix;
            float[] detJacobianMatrix;

            var elemntPoints = GetNodesByElement(GetElementByID(0));
            BuildJacobiaMatrix(out jacobianMatrix, elemntPoints, universalElement);
            BuildDetFromJacobianMatrix(out detJacobianMatrix, jacobianMatrix);

            Console.ReadKey();
           
        }

        public static void BuildJacobiaMatrix(out float[,] result, List<Node> points, IUniversalElement universalElement)
        {
            result = new float[4, 4];

            for (int i = 0; i < 4; i++)
            {
                result[i, 0] = universalElement.dN_dKSI[i, 0] * points[0].X + universalElement.dN_dKSI[i, 1] * points[1].X + universalElement.dN_dKSI[i, 2] * points[2].X + universalElement.dN_dKSI[i, 3] * points[3].X;
                result[i, 1] = universalElement.dN_dKSI[i, 0] * points[0].Y + universalElement.dN_dKSI[i, 1] * points[1].Y + universalElement.dN_dKSI[i, 2] * points[2].Y + universalElement.dN_dKSI[i, 3] * points[3].Y;
                result[i, 2] = universalElement.dN_dETA[i, 0] * points[0].X + universalElement.dN_dETA[i, 1] * points[1].X + universalElement.dN_dETA[i, 2] * points[2].X + universalElement.dN_dETA[i, 3] * points[3].X;
                result[i, 3] = universalElement.dN_dETA[i, 0] * points[0].Y + universalElement.dN_dETA[i, 1] * points[1].Y + universalElement.dN_dETA[i, 2] * points[2].Y + universalElement.dN_dETA[i, 3] * points[3].Y;

            }
        }

        public static void BuildDetFromJacobianMatrix(out float[] result, float[,] jacobianMatrix)
        {
            result = new float[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = jacobianMatrix[i, 0] * jacobianMatrix[i, 3]-jacobianMatrix[i,1]*jacobianMatrix[i,2];
            }
        }

    }
}
