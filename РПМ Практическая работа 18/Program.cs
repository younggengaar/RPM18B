using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace РПМ_Практическая_работа_18
{
    class Program
    {
        delegate void ВычислениеФакториала(int number);

        static void Main(string[] args)
        {
            int[] числа = { 5, 7, 10, 3, 6 };
            Thread[] threads = new Thread[числа.Length];

            // Создаем и запускаем потоки
            for (int i = 0; i < числа.Length; i++)
            {
                int число = числа[i]; // Копируем значение, чтобы избежать проблемы замыкания
                ВычислениеФакториала вычислениеФакториала = new ВычислениеФакториала(Факториал);
                threads[i] = new Thread(() => вычислениеФакториала(число));
                threads[i].Start();
            }

            // Ожидаем завершения всех потоков
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Все потоки завершены.");
            Console.ReadKey();
        }
        static void Факториал(int число)
        {
            long результат = 1;
            for (int i = 1; i <= число; i++)
            {
                результат *= i;
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: вычисление факториала {число}, шаг {i}, текущий результат {результат}");
                Thread.Sleep(100); 
            }
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: факториал числа {число} равен {результат}");
        }
    }
}
