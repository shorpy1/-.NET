using System;

namespace Lab4_Delegates
{
    // Делегат, який описує сигнатуру математичної функції (приймає double, повертає double)
    public delegate double MathFunction(double x);

    class Program
    {
        // Метод обчислення визначеного інтеграла методом правих прямокутників
        static double CalculateIntegral(MathFunction func, double a, double b, int n)
        {
            double h = (b - a) / n; // Крок інтегрування
            double sum = 0;

            for (int i = 1; i <= n; i++)
            {
                double x = a + i * h;
                sum += func(x);
            }

            return sum * h;
        }

        // Аналітичне обчислення інтеграла для функції f(x) = x^2 для перевірки похибки
        static double AnalyticalIntegralX2(double a, double b)
        {
            return (Math.Pow(b, 3) / 3.0) - (Math.Pow(a, 3) / 3.0);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double a = 1.0;
            double b = 2.0;
            int n = 1000;

            Console.WriteLine("Обчислення iнтеграла для f(x) = x^2 та пошук похибки:");

            // Використання лямбда-виразу для передачі функції f(x) = x^2
            double numericalResultX2 = CalculateIntegral(x => x * x, a, b, n);
            double analyticalResultX2 = AnalyticalIntegralX2(a, b);
            double errorX2 = Math.Abs(analyticalResultX2 - numericalResultX2);

            Console.WriteLine($"Чисельний метод: {numericalResultX2}");
            Console.WriteLine($"Аналiтичний метод: {analyticalResultX2}");
            Console.WriteLine($"Похибка: {errorX2}\n");

            Console.WriteLine("Обчислення iнтегралiв для функцiй з варiанта:");

            // 1. Функція f(x) = 1 / ³√x
            MathFunction func1 = x => 1.0 / Math.Pow(x, 1.0 / 3.0);
            Console.WriteLine($"Iнтеграл 1 (1/³√x): {CalculateIntegral(func1, a, b, n)}");

            // 2. Функція f(x) = sin(x) / √x² (√x² = |x|)
            MathFunction func2 = x => Math.Sin(x) / Math.Abs(x);
            Console.WriteLine($"Iнтеграл 2 (sin(x)/|x|): {CalculateIntegral(func2, a, b, n)}");

            // 3. Функція f(x) = x·cos(x)
            MathFunction func3 = x => x * Math.Cos(x);
            Console.WriteLine($"Iнтеграл 3 (x·cos(x)): {CalculateIntegral(func3, a, b, n)}\n");

            Console.WriteLine("Робота з подiями клавiатури:");

            KeyboardMonitor monitor = new KeyboardMonitor();

            // Підписка на подію за допомогою лямбда-виразу
            monitor.OnFirstLetterPressed += () =>
            {
                Console.WriteLine("\nСпрацювала подiя! Повне iм'я: Даніїл");
            };

            // Запуск моніторингу натискань (перша літера імені — «д»)
            monitor.StartListening('д');
        }
    }

    // Клас для генерації подій на основі введення з клавіатури
    class KeyboardMonitor
    {
        public delegate void KeyPressHandler();
        public event KeyPressHandler OnFirstLetterPressed;

        public void StartListening(char targetLetter)
        {
            Console.WriteLine($"Натискайте клавiшi. Для виводу iменi натиснiть '{targetLetter}'.");
            Console.WriteLine("Для виходу натиснiть 'Esc'.");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }

                // Перевірка збігу натиснутої клавіші з цільовою літерою (без урахування регістру)
                if (char.ToLower(keyInfo.KeyChar) == char.ToLower(targetLetter))
                {
                    OnFirstLetterPressed?.Invoke();
                }
            }
        }
    }
}
