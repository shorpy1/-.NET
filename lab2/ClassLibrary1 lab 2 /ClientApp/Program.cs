using System.Reflection;

namespace ClientApp;

class Program
{
    static void Main(string[] args)
    {
        // Динамічне завантаження бібліотеки TextLib.dll 
        string dllPath = Path.Combine(AppContext.BaseDirectory, "TextLib.dll");
        if (!File.Exists(dllPath))
        {
            Console.WriteLine("Помилка: TextLib.dll не знайдено. Зберіть спочатку проєкт бібліотеки.");
            Console.ReadKey();
            return;
        }

        Assembly asm = Assembly.LoadFrom(dllPath);

        // Клас у глобальному просторі імен 
        Type t = asm.GetType("TextProcessor");
        if (t == null)
        {
            Console.WriteLine("Помилка: тип TextProcessor не знайдено в бібліотеці.");
            Console.ReadKey();
            return;
        }

        object? obj = Activator.CreateInstance(t);
        if (obj == null)
        {
            Console.WriteLine("Помилка: не вдалося створити екземпляр TextProcessor.");
            Console.ReadKey();
            return;
        }
        Console.Write("Введіть речення: ");
        string? userInput = Console.ReadLine() ?? "";

        object[] parameters = new object[] { userInput };

        // Виклик методу CountSpaces
        MethodInfo? methodCount = t.GetMethod("CountSpaces");
        object? spaceCount = methodCount?.Invoke(obj, parameters);
        Console.WriteLine($"Кількість пробілів: {spaceCount}");

        // Виклик методу ConvertToUpper
        MethodInfo? methodConvert = t.GetMethod("ConvertToUpper");
        object? upperText = methodConvert?.Invoke(obj, parameters);
        Console.WriteLine($"Результат (верхній регістр): {upperText}");

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}
