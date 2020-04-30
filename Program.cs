using System;

namespace lab3
{
    class Program
    {
        static double[,,] C;
        static double[,] A, B;
        static int m_counter = 0;
        // tweak to change max rand value
        // (1 - maxRandValue)
        static int maxRandValue = 10;
        //static int sizem = 2;

        //static void SupaAlgo(int i, int j, int k) {
        //    if (i < sizem && j < sizem && k < sizem) {
        //        Console.WriteLine(String.Format("i: {0}, j: {1}, k: {2}", i, j, k));
        //        if (A[i, j] != 0 && B[i, j] != 0) {
        //            C[i, j, sizem] = A[i, k] * B[k, j];
        //            m_counter += 1;
        //        }
        //        SupaAlgo(i, j, k + 1);
        //        SupaAlgo(i + 1, j, k);
        //        SupaAlgo(i, j + 1, k);
        //    }
        //}

        static void Main(string[] args)
        {
            //A = new double[2, 2] { { 2, 0 }, { 0, 6 } };
            //B = new double[2, 2] { { 0, 3 }, { 27, 67 } };

            //C = new double[sizem, sizem, sizem + 1];

            //for (int i = 0; i < sizem; i++)
            //{
            //    for (int j = 0; j < sizem; j++)
            //    {
            //        C[i, j, 0] = 0;
            //    }
            //}

            //SupaAlgo(0, 0, 0);

            //Console.WriteLine("Operations: " + m_counter);
            ////Console.WriteLine(C.GetLength(2) - 1);
            //for (int i = 0; i < sizem; i++)
            //{
            //    for (int j = 0; j < sizem; j++)
            //    {
            //        for (int k = 0; k < sizem + 1; k++)
            //        {
            //            Console.WriteLine(C[i, j, k] + " ");
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();
            //}


            Console.WriteLine("Enter matrix size");
            int size = Convert.ToInt32(Console.ReadLine());

            C = new double[size, size, size + 1];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    C[i, j, 0] = 0;
                }
            }

            A = new double[size, size];
            genA(ref A, size);
            printMatrix(A, "Matrix A");

            B = new double[size, size];
            genB(ref B, size);
            printMatrix(B, "Matrix B");

            matrixMull();
            matrixMulTwo();
            localRecursive(0, 0, 0);

            string title = "Local recursive algorithm\nNumber of operations: " + m_counter.ToString();
            printMatrix3D(C, title);
        }

        static void genA(ref double[,] matrix, int size)
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j <= i && j + i <= size - 1)
                    {
                        matrix[i, j] = rand.Next(1, maxRandValue);
                    }
                    else if (j >= i && j + i >= size - 1)
                    {
                        matrix[i, j] = rand.Next(1, maxRandValue);
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
        }

        static void genB(ref double[,] matrix, int size)
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j <= i && j + i >= size - 1)
                    {
                        matrix[i, j] = rand.Next(1, maxRandValue);
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
        }

        // standard multiplication
        static void matrixMull()
        {
            int counter = 0;
            int size = A.GetLength(0);
            double[,] Z = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        Z[i, j] += A[i, k] * B[k, j];
                        counter += 2;
                    }
                }
            }
            string title = "Standard multiplication\nNumber of operations: " + counter.ToString();
            printMatrix(Z, title);
        }
    
        // single variable assign
        static void matrixMulTwo()
        {
            int counter = 0;
            int size = A.GetLength(0);
            double[,,] Z = new double[size, size, size + 1];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Z[i, j, 0] = 0;
                    for (int k = 0; k < size; k++)
                    {
                        Z[i, j, k + 1] = Z[i, j, k] + A[i, k] * B[k, j];
                        counter += 2;
                    }
                }
            }
            string title = "Single variable assigment\nNumber of operations: " + counter.ToString();
            printMatrix3D(Z, title);
        }

        //static void matrixMulThree(int i, int j, int k)
        //{
        //    int size = A.GetLength(0);
        //    // Console.WriteLine(size);
        //    if (i < size && j < size && k < size) {
        //        Console.WriteLine(String.Format("i: {0}, j: {1}, k: {2}", i, j, k));
        //        if (A[i, k] != 0 && B[k, j] != 0) {
        //            C[i, j, k + 1] = C[i, j, k] + A[i, k] * B[k, j];
        //            m_counter += 2;
        //        }
        //        // 
        //        matrixMulThree(i, j, k + 1);
        //        matrixMulThree(i + 1, j, k);
        //        matrixMulThree(i, j + 1, k);
        //    }
        //}

        // local-recursive algorithm
        static void localRecursive(int i, int j, int k)
        {
            int size = A.GetLength(0);

            if (i < size && j < size && k < size)
            {
                //Console.WriteLine(String.Format("i: {0}, j: {1}, k: {2}", i, j, k));
                if (A[i, k] != 0 && B[k, j] != 0)
                {
                    C[i, j, size] += A[i, k] * B[k, j];
                    m_counter += 2;
                    //Console.WriteLine(A[i, k] + " " + B[k, j]);
                }

                localRecursive(i, j, k + 1);

                if (k == size - 1)
                {
                    // reset k and j because it makes new branch
                    // to run less empty runs which count the same values
                    k = 0;
                    localRecursive(i, j + 1, k);

                    if (j == size - 1)
                    {
                        j = 0;
                        localRecursive(i + 1, j, k);
                    }
                }
            }
        }

        static void printMatrix(double[,] matrix, string title)
        {
            Console.WriteLine(title + '\n');
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0} ", matrix[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        static void printMatrix3D(double[,,] matrix, string title)
        {
            Console.WriteLine(title + '\n');
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0} ", matrix[i, j, matrix.GetLength(2) - 1]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
