using System;


namespace num_math_lab2_task5_6
{
    class Program
    {
        static void Main(string[] args)
        {
            double mean, disp, delta, sum;
            int N;
            Console.Write("Размер выборки: ");
            N = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Delta= ");
            delta = Convert.ToDouble(Console.ReadLine());

            double[] arrayX = new double[N];
            double[] arr;
            sum = 0;
            for (int i = 0; i < N; i++)
            {
                Random rand = new Random();
                arrayX[i] = Convert.ToDouble(rand.Next(1, 10));
                sum += arrayX[i];
            }
            mean = DirectMean(sum, N);
            arr = Samples(arrayX, N, mean, delta);
            disp = delta * delta;
            Console.WriteLine("Точное среднее: " + mean);
            Console.WriteLine("Примерное среднее: " + DirectMean(Summ(arr), N));
            Console.WriteLine("Дисперсия: " + disp);
            Console.WriteLine();
            Console.WriteLine("****************************************************");
            Console.WriteLine("Первая оценка дисперсии: " + DirectFirstDisp(arr));
            Console.WriteLine("Ошибка первой оценки дисперсии: " + Error(disp, DirectFirstDisp(arr)));
            Console.WriteLine();
            Console.WriteLine("***************************************************************");
            Console.WriteLine("Вторая оценка дисперсии: " + DirectSecondDisp(arr));
            Console.WriteLine("Ошибка второй оценки дисперсии: " + Error(disp, DirectSecondDisp(arr)));
            Console.WriteLine();
            if (Error(disp, DirectFirstDisp(arr)) > Error(disp, DirectSecondDisp(arr)))
            {
                Console.WriteLine("###############Вторая оценка более точна########################");
            }
            else Console.WriteLine("################Первая оценка более точна#################");
            Console.WriteLine();
            Console.WriteLine("***************************************************************");
            Console.WriteLine("Вторая оценка дисперсии для однопроходного суммирования: " + OnlineSecondDisp(arr));
            Console.WriteLine("Ошибка второй оценки для однопроходного суммирования: " + Error(disp, OnlineSecondDisp(arr)));
            Console.WriteLine();
            Console.WriteLine("***************************************************************");
            Console.WriteLine("Первая оценка дисперсии для однопроходного суммирования: " + OnlineFirstDisp(arr));
            Console.WriteLine("Ошибка первая оценки для однопроходного суммирования: " + Error(disp, OnlineFirstDisp(arr)));
            Console.WriteLine();
            if (Error(disp, OnlineFirstDisp(arr)) > Error(disp, OnlineSecondDisp(arr)))
            {
                Console.WriteLine("################Вторая оценка более точна###############");
            }
            else Console.WriteLine("################Первая оценка более точна#################");
        }
        public static double[] Samples(double[] sample, int N, double mean, double delta)
        {
            for (int i = 0; i < (N) / 2; i++)
            {
                sample[i] = mean + delta;
            }
            for (int i = (N) / 2; i < N; i++)
            {
                sample[i] = mean - delta;
            }
            return sample;
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
        public static double SummDouble(double[] x)
        {
            double sum = 0;
            foreach (double item in x)
            {
                sum += item * item;
            }
            return sum;
        }
        public static double SummOne(double[] x)
        {
            double sum = 0;
            foreach (double item in x)
            {
                sum += Math.Pow((item - DirectMean(Summ(x), x.Length)), 2);
            }
            return sum;
        }
        public static double DirectMean(double sum, int N)
        {
            return sum / N;
        }

        //Первая оценка дисперсии для последовательного суммирования
        public static double DirectFirstDisp(double[] arr)
        {
            return DirectMean(SummOne(arr), arr.Length);
        }
        //Первая оценка дисперсии для однопроходного суммирования
        public static double OnlineFirstDisp(double[] arr)
        {
            double en = arr[0];
            double en2 = (arr[0] + arr[1]) / 2;
            double disp = Math.Pow((arr[0] - en), 2) + arr.Length * Math.Pow((en - en2), 2);

            for (int i = 1; i < arr.Length - 1; i++)
            {
                en = en2;
                en2 = (en2 * (i + 1) + arr[i + 1]) / (i + 2);
                disp = Math.Pow((arr[i] - en), 2) + Math.Pow((en - en2), 2);
            }
            return disp;
        }
        //Вторая оценка дисперсии для последовательного суммирования
        public static double DirectSecondDisp(double[] arr)
        {
            return DirectMean(SummDouble(arr), arr.Length) - Math.Pow((DirectMean(Summ(arr), arr.Length)), 2);
        }
        //Вторая оценка диспрсии для однопроходного суммирования
        public static double OnlineSecondDisp(double[] arr)
        {
            double m = arr[0];
            double m2 = Math.Pow(arr[0], 2);
            for (int i = 1; i < arr.Length; i++)
            {
                m = (m * (i - 1) + arr[i]) / i;
                m2 = (m2 * (i - 1) + Math.Pow(arr[i], 2)) / i;
            }
            return m2 - Math.Pow(m, 2);
        }
        public static double Error(double x, double y)
        {
            return Math.Abs(x - y) / Math.Abs(y);
        }
    }
}
