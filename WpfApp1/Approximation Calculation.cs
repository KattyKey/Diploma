using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Approximation_Calculation
    {
        public void Calculation(int DEGREE,int NOPTS, float[] X, float[] Y, float[] COEF)
        {
           
            //float[] COEF = new float[DEGREE + 1];C:\Users\Администратор\source\repos\WpfApp1\BLL\Approximation Calculation.cs

           // float XValue = new float();
           // float YValue = new float();           

           // XValue = (float)Convert.ToDouble(Console.ReadLine());
           int  DEGP1 = DEGREE + 1;

            NORMEQ(DEGREE, DEGP1, NOPTS, X, Y, COEF);

            // WRITE(*,3)(I,COEF(I),I=1,DEGP1)
            // 3 FORMAT(1X,' ORDER    COEFFICIENT'/(1X,I4,2X,F15.7))

            //Console.WriteLine(string.Format("{0}", "ORDER    COEFFICIENT"));
            //for (int i = 0; i < DEGREE + 1; i++)
            //{
            //    Console.WriteLine(string.Format("{0}, {1}", i, COEF[i]));
            //}


            for (int I = 0; I < NOPTS; I++)
            {
                Y[I] = (float)0.0;
                for (int J = 0; J < DEGREE + 1; J++)
                {
                    Y[I] = Y[I] + COEF[J] * (float)Math.Pow(X[I], (J));
                }
            }
           
            for (int I = 0; I < NOPTS; I++)
            {
                if (Y[I] < 0)
                {
                    Y[I] = (float)0.0;
                }
            }

            //   WRITE(*,15)(X(I),I=1,NOPTS)
            //Console.WriteLine("X");
            //for (int i = 0; i < NOPTS; i++)
            //{
            //    Console.WriteLine(X[i]);
            //}

            //Console.WriteLine(string.Format("{0}, {1}", "YLMS", DEGREE));
            //for (int i = 0; i < NOPTS; i++)
            //{
            //    Console.WriteLine(Y[i]);
            //}
           // Console.WriteLine(YValue);
        }
        static void NORMEQ(int DEGREE, int DEGP1, int NOPTS, float[] X, float[] Y, float[] COEF)
        {
            int DEGT2;

            float[] POWX = new float[200];
            float[] RHS = new float[DEGREE + 1];
            float[,] SUM1 = new float[DEGREE + 1, DEGREE + 1];
            DEGT2 = DEGREE * 2;
            for (int I = 0; I < DEGT2; I++)
            {
                POWX[I] = (float)0.0;
                for (int J = 0; J < NOPTS; J++)
                {
                    POWX[I] = POWX[I] + (float)Math.Pow(X[J], I + 1);
                }
            }

            for (int i = 0; i < DEGREE + 1; i++)
            {
                for (int j = 0; j < DEGREE + 1; j++)
                {
                    int k = (i + 1) + (j + 1) - 2;
                    Console.WriteLine(k);
                    if (k <= 0)
                    {
                        SUM1[i, j] = NOPTS;
                    }
                    else
                    {
                        SUM1[i, j] = POWX[k - 1];
                    }
                }
            }
            RHS[0] = (float)0.0;

            for (int j = 0; j < NOPTS; j++)
            {
                RHS[0] = RHS[0] + Y[j];
            }

            for (int i = 1; i < DEGREE + 1; i++)
            {
                RHS[i] = (float)0.0;
                for (int j = 0; j < NOPTS; j++)
                {
                    RHS[i] = RHS[i] + Y[j] * (float)Math.Pow(X[j], i);
                }
            }

            GAUSS(DEGREE, DEGP1, RHS, COEF, SUM1);
        }

        static  void GAUSS(int DEGREE, int DEGP1, float[] RHS, float[] COEF, float[,] SUM1)
        {
            float FACTOR = (float)0.0;
            for (int K = 0; K < DEGREE; K++)
            {
                int KPLUS1 = K + 1;
                int L = K;
                for (int I = KPLUS1; I < DEGREE + 1; I++)
                {
                    if (!(Math.Abs(SUM1[I, K]) <= Math.Abs(SUM1[L, K])))
                    {
                        L = I;
                    }
                }

                if (!(L <= K))
                {
                    float DUMP;
                    for (int j = K; j < DEGREE + 1; j++)
                    {
                        DUMP = SUM1[K, j];
                        SUM1[K, j] = SUM1[L, j];
                        SUM1[L, j] = DUMP;
                    }
                    DUMP = RHS[K];
                    RHS[K] = RHS[L];
                    RHS[L] = DUMP;
                }

                for (int I = KPLUS1; I < DEGREE + 1; I++)
                {
                    FACTOR = SUM1[I, K] / SUM1[K, K];
                    SUM1[I, K] = (float)0.0;
                    for (int J = KPLUS1; J < DEGREE + 1; J++)
                    {
                        SUM1[I, J] = SUM1[I, J] - FACTOR * SUM1[K, J];
                    }
                    RHS[I] = RHS[I] - FACTOR * RHS[K];
                }

            }
            COEF[DEGREE] = RHS[DEGREE] / SUM1[DEGREE, DEGREE];
            int Iter = DEGREE;

            while (Iter > 0)
            {
                int IPLUS1 = Iter + 1;
                double TOTAL = 0.0;

                for (int J = IPLUS1 - 1; J < DEGREE + 1; J++)
                {
                    TOTAL = TOTAL + SUM1[Iter - 1, J] * COEF[J];
                }
                COEF[Iter - 1] = (float)(RHS[Iter - 1] - TOTAL) / SUM1[Iter - 1, Iter - 1];

                Iter--;
            }
        }
        public static float CalculateHRV(float X, float[] COEF)
        {
            float Y=0;
            for(int i = 0; i < COEF.Length;i++)
            {
                Y += COEF[i] * (float)Math.Pow(X, i);
            }
            return Y;
        }
    }
}

