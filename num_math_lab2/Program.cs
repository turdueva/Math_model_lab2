using System;

namespace num_math_lab2
{
    class Program
    {
        //task 3 and task 4
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число k:");
            double k = Convert.ToDouble(Console.ReadLine());
            double[] arrayOfX;
            double[] arrayOfXAbs;
            arrayOfX = List_create(Convert.ToInt32(k));
            arrayOfXAbs = List_create1(Convert.ToInt32(k));
            Console.WriteLine("Точная сумма: " + Exact_solution(k));
            Console.WriteLine("Последовательное суммирование: " + Summ(arrayOfX));
            Console.WriteLine("Алгоритм Кэхэна: " + Algorithm_Kahans(arrayOfX));
            Console.WriteLine("Ошибка при последовательном суммировании: " + Error(Exact_solution(k), Summ(arrayOfX)));
            Console.WriteLine("Ошибка при алгоритме Кэхена: " + Error(Exact_solution(k), Algorithm_Kahans(arrayOfX)));
            Console.WriteLine("*****************************************************************");
            Array.Sort(arrayOfX); Array.Sort(arrayOfXAbs);
            Console.WriteLine("Упорядоченное суммирование: " + Summ(arrayOfX));
            Console.WriteLine("Упорядоченное суммирование по модулю: " + Summ(arrayOfXAbs));
            Console.WriteLine("Ошибка упорядоченном суммировании: " + Error(Exact_solution(k), Summ(arrayOfX)));
            Console.WriteLine("Ошибка упорядоченном суммировании по модулю: " + Error(Exact_solution(k), Summ(arrayOfXAbs)));
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("Упорядоченное суммирование алгоритмом Кэхена: " + Algorithm_Kahans(arrayOfX));
            Console.WriteLine("Упорядоченное суммирование по модулю алгоритмом Кэхэна: " + Algorithm_Kahans(arrayOfXAbs));
            Console.WriteLine("Ошибка упорядоченном суммировании алгоритмом Кэхена: " + Error(Exact_solution(k), Algorithm_Kahans(arrayOfX)));
            Console.WriteLine("Ошибка упорядоченном суммировании по модулю алгоритмом Кэхена: " + Error(Exact_solution(k), Algorithm_Kahans(arrayOfXAbs)));

        }
        public static double Exact_solution(double k)
        {
            return 1.0 / 2.0 * (Math.Sin(k) - (Math.Cos(k) / Math.Tan(1.0 / 2.0)) + 1.0 / Math.Tan(1.0 / 2.0));
        }


        public static double Summ(double[] x)
        {
            double sum = 0;
            foreach (double item in x)
            {
                sum += item;
            }
            return sum;
        }

        public static double[] List_create(int k)
        {
            double[] listOfX = new double[k + 1];
            for (int i = 1; i < k + 1; i++)
            {
                listOfX[i] = Math.Sin(i);
            }
            return listOfX;
        }
        public static double[] List_create1(int k)
        {
            double[] listOfX = new double[k + 1];
            for (int i = 1; i < k + 1; i++)
            {
                listOfX[i] = Math.Abs(Math.Sin(i));
            }
            return listOfX;
        }

        public static double Algorithm_Kahans(double[] x)
        {
            double y, t, sum, error_sum;
            sum = 0;
            error_sum = 0;
            foreach (var item in x)
            {
                y = item - error_sum;
                t = sum + y;
                error_sum = (t - sum) - y;
                sum = t;
            }
            return sum;
        }

        public static double Error(double x, double y)
        {
            return Math.Abs(x - y) / Math.Abs(y);
        }
    }
}
