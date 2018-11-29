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

        private float[,] _dNx_dx = new float[4,4];
        public float[,] dNx_dx { get => _dNx_dx; set => _dNx_dx=value; }

        private float[,] _dNx_dy = new float[4,4];
        public float[,] dNx_dy { get => _dNx_dy;  set => _dNx_dy = value; }

        private float _K;
        public float K { get => _K; set => _K = value; }

        private IUniversalElement _UniversalElement;
        public IUniversalElement UniversalElement { get => _UniversalElement; set => _UniversalElement = value; }

        private float[] _detJacobian;

        public float[] DetJacobian
        {
            get { return _detJacobian; }
            private set { _detJacobian = value; }
        }

        private float[,] _jacobian;

        public float[,] Javobian
        {
            get { return _jacobian; }
           private set { _jacobian = value; }
        }

        private float[,] _MatrixH = new float [4,4];

        public float[,] MatrixH
        {
            get { return _MatrixH; }
            set { _MatrixH = value; }
        }


        public MatrixHProvider(IUniversalElement universalElement, float[,] jacobian, float[] detJacobian, float k)
        {
            _jacobian = jacobian;
            _detJacobian = detJacobian;
            _UniversalElement = universalElement;
            _K = k;

            _dNx_dx = BuildNx_X(_UniversalElement.dN_dKSI, universalElement.dN_dETA, jacobian);
            _dNx_dy = BuildNx_Y(_UniversalElement.dN_dKSI, universalElement.dN_dETA, jacobian);
            _MatrixH = BuildMatrixH(_dNx_dx, _dNx_dy, _detJacobian, _K);
        }

        private float[,] BuildNx_X(float[,] dn_dksi, float[,] dn_deta, float[,] jacobian)
        {
            float[,] result = new float[4, 4];
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

        private float[,] BuildNx_Y(float[,] dn_dksi, float[,] dn_deta, float[,] jacobian)
        {
            float[,] result = new float[4, 4];
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

        private float[,] BuildMatrixH(float[,] dNx_X, float[,] dNx_Y, float[] detJ, float K)
        {

            List<float[,]> dN_dx_dN_dx_T = new List<float[,]>();

            for (int z = 0; z < 4; z++)
            {
                float[,] Pcx = new float[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Pcx[i, j] = dNx_X[z, i] * dNx_X[z, j] * detJ[z];
                    }
                }

                dN_dx_dN_dx_T.Add(Pcx);

            }


            List<float[,]> dN_dy_dN_dy_T = new List<float[,]>();

            for (int z = 0; z < 4; z++)
            {
                float[,] Pcy = new float[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Pcy[i, j] = dNx_Y[z, i] * dNx_Y[z, j] * detJ[z];
                    }
                }

                dN_dy_dN_dy_T.Add(Pcy);
            }

            List<float[,]> tmp = new List<float[,]>();

            for (int z = 0; z < 4; z++)
            {
                float[,] Pcp = new float[4, 4];
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

            float[,] result = new float[4, 4];

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
