using MES_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Providers
{
    class MatrixHProvider : IMatrixH
    {

        private double[,] _dNx_dx = new double[4,4];
        public double[,] dNx_dx { get => _dNx_dx; set => _dNx_dx=value; }

        private double[,] _dNx_dy = new double[4,4];
        public double[,] dNx_dy { get => _dNx_dy;  set => _dNx_dy = value; }

        private double _K;
        public double K { get => _K; set => _K = value; }

        private IUniversalElement _UniversalElement;
        public IUniversalElement UniversalElement { get => _UniversalElement; set => _UniversalElement = value; }

        private double[] _detJacobian;

        public double[] DetJacobian
        {
            get { return _detJacobian; }
            private set { _detJacobian = value; }
        }

        private double[,] _jacobian;

        public double[,] Javobian
        {
            get { return _jacobian; }
           private set { _jacobian = value; }
        }

        private double[,] _MatrixH = new double [4,4];

        public double[,] MatrixH
        {
            get { return _MatrixH; }
            set { _MatrixH = value; }
        }


        public MatrixHProvider(IUniversalElement universalElement, double[,] jacobian, double[] detJacobian, double k)
        {
            _jacobian = jacobian;
            _detJacobian = detJacobian;
            _UniversalElement = universalElement;
            _K = k;

            _dNx_dx = BuildNx_X(_UniversalElement.dN_dKSI, universalElement.dN_dETA, jacobian);
            _dNx_dy = BuildNx_Y(_UniversalElement.dN_dKSI, universalElement.dN_dETA, jacobian);
            _MatrixH = BuildMatrixH(_dNx_dx, _dNx_dy, _detJacobian, _K);
        }

        private double[,] BuildNx_X(double[,] dn_dksi, double[,] dn_deta, double[,] jacobian)
        {
            double[,] result = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var J1_1_1 = jacobian[0, i];
                    var dN1_dKsi = dn_dksi[i, j];
                    var J1_1_2 = jacobian[1, i];
                    var dN1_dEta = dn_deta[i, j];
                    result[i, j] = J1_1_1 * dN1_dKsi + J1_1_2 * dN1_dEta;
                }
            }

            return result;
        }

        private double[,] BuildNx_Y(double[,] dn_dksi, double[,] dn_deta, double[,] jacobian)
        {
            double[,] result = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var J1_2_1 = jacobian[2, i];
                    var dN1_dKsi = dn_dksi[i, j];
                    var J1_2_2 = jacobian[3, i];
                    var dN1_dEta = dn_deta[i, j];
                    result[i, j] = J1_2_1 * dN1_dKsi + J1_2_2 * dN1_dEta;
                }
            }

            return result;
        }

        private double[,] BuildMatrixH(double[,] dNx_X, double[,] dNx_Y, double[] detJ, double K)
        {

            List<double[,]> dN_dx_dN_dx_T = new List<double[,]>();

            for (int z = 0; z < 4; z++)
            {
                double[,] Pcx = new double[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Pcx[i, j] = dNx_X[z, i] * dNx_X[z, j] * detJ[z];
                    }
                }

                dN_dx_dN_dx_T.Add(Pcx);

            }


            List<double[,]> dN_dy_dN_dy_T = new List<double[,]>();

            for (int z = 0; z < 4; z++)
            {
                double[,] Pcy = new double[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Pcy[i, j] = dNx_Y[z, i] * dNx_Y[z, j] * detJ[z];
                    }
                }

                dN_dy_dN_dy_T.Add(Pcy);
            }

            List<double[,]> tmp = new List<double[,]>();

            for (int z = 0; z < 4; z++)
            {
                double[,] Pcp = new double[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var tmpp = dN_dx_dN_dx_T[z][j, i];
                        var tmppp = dN_dy_dN_dy_T[z][j, i];
                        Pcp[i, j] = K * (tmpp + tmppp);
                    }
                }

                tmp.Add(Pcp);

            }

            double[,] result = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = tmp[0][i, j] + tmp[1][i, j] + tmp[2][i, j] + tmp[3][i, j];
                }
            }
            return result;
        }
    }
}
