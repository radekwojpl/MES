using MES_App.BasicStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_App.Core
{
    class UniversalElement
    {
        private Element[] _Element;

        public Element[] Element
        {
            get { return _Element; }
            set { _Element = value; }
        }

        private Node[] _Nodes;

        public Node[] Nodes
        {
            get { return _Nodes; }
            set { _Nodes = value; }
        }

        private UniversalPoint[] _UniversalPoints = new UniversalPoint[4];

        public UniversalPoint[] UniversalPoints
        {
            get { return _UniversalPoints; }
            set { _UniversalPoints = value; }
        }

        private ArrayList _UniversalElementGridPoints;

        public ArrayList UniversalElementGridPoints
        {
            get { return _UniversalElementGridPoints; }
            set { _UniversalElementGridPoints = value; }
        }

        private float[,] _dN_dETA;

        public float[,] dN_dETA
        {
            get { return _dN_dETA; }
            set { _dN_dETA = value; }
        }

        private float[,] _dN_dKSI;

        public float[,] dN_dKSI
        {
            get { return _dN_dKSI; }
            set { _dN_dKSI = value; }
        }

        private float[,] _JacobianMatrix;

        public float[,] JacobianMatrix
        {
            get { return _JacobianMatrix; }
            set { _JacobianMatrix = value; }
        }

        public void BuildJacobianMatrix(out float[,] result, UniversalPoint[] points)
        {
            result = new float[4, 4];

            float tmp = new float();
            tmp = 0.0f;

            for (int i = 0; i < 4; i++)
            {
                result[i, 0] = dN_dKSI[i, 0] * 0 + dN_dKSI[i, 1] * 10 + dN_dKSI[i , 2] * 10 + dN_dKSI[i, 3] * 0;
                result[i, 1] = dN_dETA[i, 0] * 0 + dN_dETA[i, 1] * 10 + dN_dETA[i , 2] * 10 + dN_dETA[i, 3] * 0;
                result[i, 2] = dN_dKSI[i, 0] * 0 + dN_dKSI[i, 1] * 0 +  dN_dKSI[i , 2] * 8 +  dN_dKSI[i, 3] * 8;
                result[i, 3] = dN_dETA[i, 0] * 0 + dN_dETA[i, 1] * 0 +  dN_dETA[i , 2] * 8 +  dN_dETA[i, 3] * 8;

            }

            

        }



        public UniversalElement(Element[] element, Node[] nodes)
        {
            _Element = element;
            _Nodes = nodes;

       
            _UniversalPoints[0] = (new UniversalPoint(-1 / (float)Math.Sqrt(3), -1 / (float)Math.Sqrt(3)));
            _UniversalPoints[1] = (new UniversalPoint(1 / (float)Math.Sqrt(3), -1 / (float)Math.Sqrt(3)));
            _UniversalPoints[2] = (new UniversalPoint(1 / (float)Math.Sqrt(3), 1 / (float)Math.Sqrt(3)));
            _UniversalPoints[3] = (new UniversalPoint(-1 / (float)Math.Sqrt(3), 1 / (float)Math.Sqrt(3)));


            CountFunctionShapeDerative_DN_Ksi(_UniversalPoints, out _dN_dKSI, 4, 4);
            CountFunctionShapeDerative_DN_ETA(_UniversalPoints, out _dN_dETA, 4, 4);
            UniversalPoint[] points = new UniversalPoint[4];
            points[0] = new UniversalPoint(0, 0);
            points[1] = new UniversalPoint(0.025f, 0);
            points[2] = new UniversalPoint(0.025f, 0.025f);
            points[3] = new UniversalPoint(0, 0.025f);

            BuildJacobianMatrix(out _JacobianMatrix, points);

            Console.WriteLine(_dN_dETA.GetLength(0));

        }

        public void CountFunctionShapeDerative_DN_Ksi(UniversalPoint[] point, out float[,] result, int rows, int columns)
        {
            float tmp;
            result = new float[rows, columns];

            for (int i = 0; i < point.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            tmp = -0.25f * (1.0f - point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 1:
                            tmp = 0.25f * (1.0f - point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 2:
                            tmp = 0.25f * (1.0f + point[i].Y);
                            result[i, j] = tmp;
                            break;
                        case 3:
                            tmp = -0.25f * (1.0f + point[i].Y);
                            result[i, j] = tmp;
                            break;
                    }
                }
            }


        }

        public void CountFunctionShapeDerative_DN_ETA(UniversalPoint[] point, out float[,] result, int rows, int columns)
        {
            float tmp;
            result = new float[rows, columns];

            for (int i = 0; i < point.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            tmp = -0.25f * (1.0f - point[i].X);
                            result[i, j] = tmp;
                            break;
                        case 1:
                            tmp = -0.25f * (1.0f + point[i].X);
                            result[i, j] = tmp;
                            break;
                        case 2:
                            tmp = 0.25f * (1.0f + point[i].X);
                            result[i, j] = tmp;
                            break;
                        case 3:
                            tmp = 0.25f * (1.0f - point[i].X);
                            result[i, j] = tmp;
                            break;
                    }
                }
            }


        }

        #region UniversalPoint Class
        public class UniversalPoint
        {
            private float _X;

            public float X
            {
                get { return _X; }
                set { _X = value; }
            }

            private float _Y;

            public float Y
            {
                get { return _Y; }
                set { _Y = value; }


            }

            public UniversalPoint(float x, float y)
            {
                _X = x;
                _Y = y;
            }

        }
        #endregion
    }
}
