using MES_App.BasicStruct;
using MES_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Providers
{
   public class JacobianProvider
    {
        private double[,] _Jacobian;

        public double[,] Jacobian
        {
            get { return _Jacobian; }
            set { _Jacobian = value; }
        }

        private double[] _DetJacobian;

        public double[] DejJacobian
        {
            get { return _DetJacobian; }
            set { _DetJacobian = value; }
        }

        private double[,] _ReverseJacobian;

        public double[,] ReverseJacobian
        {
            get { return _ReverseJacobian; }
            set { _ReverseJacobian = value; }
        }

        public JacobianProvider( List<Node> nodes, IUniversalElement universalElement )
        {
            BuildJacobiaMatrix(out _Jacobian, nodes, universalElement);
            BuildDetFromJacobianMatrix(out _DetJacobian, _Jacobian);
            ReversJacobian(_Jacobian, _DetJacobian, out _ReverseJacobian);

        }

        private void BuildJacobiaMatrix(out double[,] result, List<Node> points, IUniversalElement universalElement)
        {
            result = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                result[0, i] = universalElement.dN_dKSI[i, 0] * points[0].X + universalElement.dN_dKSI[i, 1] * points[1].X + universalElement.dN_dKSI[i, 2] * points[2].X + universalElement.dN_dKSI[i, 3] * points[3].X;
                result[1, i] = universalElement.dN_dKSI[i, 0] * points[0].Y + universalElement.dN_dKSI[i, 1] * points[1].Y + universalElement.dN_dKSI[i, 2] * points[2].Y + universalElement.dN_dKSI[i, 3] * points[3].Y;
                result[2, i] = universalElement.dN_dETA[i, 0] * points[0].X + universalElement.dN_dETA[i, 1] * points[1].X + universalElement.dN_dETA[i, 2] * points[2].X + universalElement.dN_dETA[i, 3] * points[3].X;
                result[3, i] = universalElement.dN_dETA[i, 0] * points[0].Y + universalElement.dN_dETA[i, 1] * points[1].Y + universalElement.dN_dETA[i, 2] * points[2].Y + universalElement.dN_dETA[i, 3] * points[3].Y;

            }

        }

        private void BuildDetFromJacobianMatrix(out double[] result, double[,] jacobianMatrix)
        {
            result = new double[4];
            for (int i = 0; i < 4; i++)
            {

                result[i] = jacobianMatrix[0, i] * jacobianMatrix[3, i] - jacobianMatrix[1, i] * jacobianMatrix[2, i];
            }
        }

        private void ReversJacobian(double[,] jacobian, double[] detJacobian, out double[,] result)
        {
            result = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    result[j, i] = jacobian[3 - j, i] / detJacobian[i];
                }
            }


        }



    }
}
