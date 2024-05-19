namespace ConsoleApp44
{
    using MathNet.Numerics.Integration;
    using System;

    public class Matrix
    {
        public int[,] Data { get; }
        public int Rows => Data.GetLength(0);
        public int Cols => Data.GetLength(1);

        public Matrix(int rows, int cols)
        {
            Data = new int[rows, cols];
        }
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            var result = new Matrix(a.Rows, b.Cols);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Cols; j++)
                {
                    for (int k = 0; k < a.Cols; k++)
                    {
                        result.Data[i, j] += a.Data[i, k] * b.Data[k, j];
                    }
                }
            }
            return result;
        }

        public static async Task<Matrix> MultiplyAsync(Matrix a, Matrix b)
        {
            return await Task.Run(() => Multiply(a, b));
        }

    }




    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {



                Random random = new Random();
                // Инициализация матриц A и B
                Matrix a = new Matrix(random.Next(5, 16), random.Next(5, 16));
                Matrix b = new Matrix(random.Next(5, 16), random.Next(5, 16));


                a = AddMatrix(a, random);

                b = AddMatrix(b, random);
                Console.WriteLine("Matrix a вывод после заполения");
                foreach (var m in a.Data)
                {
                    Console.Write($"{m}");
                }
                Console.WriteLine();
                Console.WriteLine("Matrix b вывод после заполения");
                foreach (var m in b.Data)
                {
                    Console.Write($"{m}");
                }
                // Заполнение матриц данными для примера
                // Предположим, матрицы уже заполнены соответствующими значениями...

                // Асинхронное умножение
                Matrix result = await Matrix.MultiplyAsync(a, b);
                Console.WriteLine();

                Console.WriteLine("Вывод матрицы");
                // Вывод результата
                for (int i = 0; i < result.Rows; i++)
                {
                    for (int j = 0; j < result.Cols; j++)
                    {
                        Console.Write(result.Data[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.ReadLine();
            }catch (Exception ex)
            {
                Console.WriteLine("Матрица вышла за приделы"+ex.Message);
            }
        }
         static  Matrix AddMatrix(Matrix a, Random random)
         {
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols - 1; j++)
                {
                    a.Data[i, j] = random.Next(5, 16);
                }
            }

            return a;
        }

    }


}
