using System;
using System.Collections.Generic;
using System.Text;

namespace ChessPolymorphism
{
    class ChessFigure
    {
        public string Name { get; set; }

        public ChessFigure(string name)
        {
            Name = name;
        }

        // Конструктор копіювання
        public ChessFigure(ChessFigure other)
        {
            Name = other.Name;
        }

        // Віртуальний метод. Якщо не перевизначити, виведе це повідомлення.
        public virtual void CheckMove(string currentPos, string targetPos)
        {
            Console.WriteLine($"{Name}: Я не знаю, як мені ходити з {currentPos} на {targetPos}.");
        }

        // Допоміжний метод перекладу координат (a1 -> 0,0)
        protected bool ParsePosition(string pos, out int x, out int y)
        {
            x = -1; y = -1;
            if (pos.Length != 2) return false;

            char col = pos[0];
            char row = pos[1];

            if (col < 'a' || col > 'h' || row < '1' || row > '8')
                return false;

            x = col - 'a';
            y = row - '1';
            return true;
        }

        // Перевизначаємо Equals, щоб порівнювати фігури за суттю, а не за посиланням
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ChessFigure))
                return false;

            ChessFigure other = (ChessFigure)obj;
            // Фігури рівні, якщо у них одне ім'я і один тип класу
            return this.Name == other.Name && this.GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ GetType().GetHashCode();
        }
    }

    class Rook : ChessFigure
    {
        public Rook() : base("Тура") { }

        public Rook(Rook other) : base(other) { }

        public override void CheckMove(string currentPos, string targetPos)
        {
            int x1, y1, x2, y2;
            if (!ParsePosition(currentPos, out x1, out y1) || !ParsePosition(targetPos, out x2, out y2))
            {
                Console.WriteLine($"Помилка координат!");
                return;
            }

            // Тура ходить прямо
            bool isPossible = ((x1 == x2) || (y1 == y2)) && !(x1 == x2 && y1 == y2);
            PrintResult(currentPos, targetPos, isPossible);
        }

        private void PrintResult(string p1, string p2, bool result)
        {
            string msg = result ? "Хід можливий" : "Хід НЕможливий";
            Console.WriteLine($"{Name} ({p1} -> {p2}): {msg}");
        }
    }

    class Bishop : ChessFigure
    {
        public Bishop() : base("Слон") { }

        public Bishop(Bishop other) : base(other) { }

        public override void CheckMove(string currentPos, string targetPos)
        {
            int x1, y1, x2, y2;
            if (!ParsePosition(currentPos, out x1, out y1) || !ParsePosition(targetPos, out x2, out y2))
            {
                Console.WriteLine($"Помилка координат!");
                return;
            }

            // Слон ходить по діагоналі
            bool isPossible = (Math.Abs(x1 - x2) == Math.Abs(y1 - y2)) && !(x1 == x2 && y1 == y2);
            Console.WriteLine($"{Name} ({currentPos} -> {targetPos}): {(isPossible ? "Хід можливий" : "Хід НЕможливий")}");
        }
    }

    class Queen : ChessFigure
    {
        public Queen() : base("Ферзь") { }

        public Queen(Queen other) : base(other) { }

        public override void CheckMove(string currentPos, string targetPos)
        {
            int x1, y1, x2, y2;
            if (!ParsePosition(currentPos, out x1, out y1) || !ParsePosition(targetPos, out x2, out y2))
            {
                Console.WriteLine($"Помилка координат!");
                return;
            }

            // Ферзь = Тура + Слон
            bool isRookMove = ((x1 == x2) || (y1 == y2));
            bool isBishopMove = (Math.Abs(x1 - x2) == Math.Abs(y1 - y2));

            bool isPossible = (isRookMove || isBishopMove) && !(x1 == x2 && y1 == y2);
            Console.WriteLine($"{Name} ({currentPos} -> {targetPos}): {(isPossible ? "Хід можливий" : "Хід НЕможливий")}");
        }
    }

    class Knight : ChessFigure
    {
        public Knight() : base("Кінь") { }

        public Knight(Knight other) : base(other) { }

        public override void CheckMove(string currentPos, string targetPos)
        {
            int x1, y1, x2, y2;
            if (!ParsePosition(currentPos, out x1, out y1) || !ParsePosition(targetPos, out x2, out y2))
            {
                Console.WriteLine($"Помилка координат!");
                return;
            }

            // Хід буквою Г
            int dx = Math.Abs(x1 - x2);
            int dy = Math.Abs(y1 - y2);

            bool isPossible = (dx == 1 && dy == 2) || (dx == 2 && dy == 1);
            Console.WriteLine($"{Name} ({currentPos} -> {targetPos}): {(isPossible ? "Хід можливий" : "Хід НЕможливий")}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<ChessFigure> figures = new List<ChessFigure>
            {
                new Rook(),
                new Bishop(),
                new Knight(),
                new Queen(),
                new ChessFigure("Невідома фігура")
            };

            Console.WriteLine("Демонстрація поліморфізму");
            
            string start = "a1";
            string end = "a5";
            Console.WriteLine($"\nСпроба ходу {start} -> {end}:");
            foreach (var fig in figures)
            {
                fig.CheckMove(start, end);
            }

            start = "b1";
            end = "c3";
            Console.WriteLine($"\nСпроба ходу {start} -> {end}:");
            foreach (var fig in figures)
            {
                fig.CheckMove(start, end);
            }

            Console.WriteLine("\nРобота з конструкторами копіювання:");
            Rook rook1 = new Rook();
            Rook rook2 = new Rook(rook1);
            Console.WriteLine($"Оригінал: {rook1.Name}");
            Console.WriteLine($"Копія: {rook2.Name}");

            Knight knight1 = new Knight();
            Knight knight2 = new Knight(knight1);
            Console.WriteLine($"Оригінал: {knight1.Name}");
            Console.WriteLine($"Копія: {knight2.Name}");

            Console.WriteLine("\nПеревірка методу Equals:");
            Rook r1 = new Rook();
            Rook r2 = new Rook();
            // Має бути true, бо ми перевизначили Equals
            Console.WriteLine($"Чи рівні дві нові тури? {r1.Equals(r2)}");
            
            Bishop b1 = new Bishop();
            Console.WriteLine($"Чи рівні тура і слон? {r1.Equals(b1)}");

            Console.WriteLine("\nРізниця між посиланням і значенням:");
            Rook link1 = new Rook();
            Rook link2 = link1; // копіюємо посилання
            Rook val1 = new Rook(); // новий об'єкт
            
            Console.WriteLine($"ReferenceEquals (один об'єкт): {ReferenceEquals(link1, link2)}");
            Console.WriteLine($"ReferenceEquals (різні об'єкти): {ReferenceEquals(link1, val1)}");
            Console.WriteLine($"Equals (порівняння значень): {link1.Equals(val1)}");

            Console.WriteLine("\nРаннє та пізнє зв'язування:");
            
            // Раннє зв'язування
            Console.WriteLine("1. Раннє (Static):");
            Rook specificRook = new Rook();
            specificRook.CheckMove("a1", "a8");
            
            // Пізнє зв'язування
            Console.WriteLine("2. Пізнє (Dynamic):");
            ChessFigure polyFigure = new Rook(); // Тип змінної базовий, а об'єкт - спадкоємець
            polyFigure.CheckMove("a1", "a8");

            Console.WriteLine("\nДодатковий тест масивом:");
            ChessFigure[] arr = { new Rook(), new Bishop(), new Knight(), new Queen() };
            foreach (ChessFigure f in arr)
            {
                f.CheckMove("d4", "d5");
            }

            Console.ReadKey();
        }
    }
    
}