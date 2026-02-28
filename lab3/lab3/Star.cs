using System.Drawing;

namespace Lab3
{
    public class Star
    {
        // Статичний лічильник екземплярів
        public static int Count { get; private set; } = 0;

        // Розміри та розташування зірки
        public int A { get; set; } // зовнішній радіус
        public int B { get; set; } // внутрішній радіус
        public int X { get; set; } // центр X
        public int Y { get; set; } // центр Y
        public Color StarColor { get; set; }

        // Конструктор без параметрів – a=0, b=0
        public Star()
        {
            A = 0;
            B = 0;
            X = 0;
            Y = 0;
            StarColor = Color.Black;
            Count++;
        }

        // Конструктор з параметрами a і b (за умовою)
        public Star(int a, int b)
            : this(a, b, 0, 0, Color.Black)
        {
        }

        // Додатковий конструктор з усіма параметрами (зручно для малювання)
        public Star(int a, int b, int x, int y, Color color)
        {
            A = a;
            B = b;
            X = x;
            Y = y;
            StarColor = color;
            Count++;
        }

        // Малювання чотирикутної зірки
        public void Draw(Graphics g)
        {
            if (A == 0 && B == 0) return;

            Point[] points = new Point[8];

            points[0] = new Point(X,     Y - A); // верхній промінь
            points[1] = new Point(X + B, Y - B);
            points[2] = new Point(X + A, Y);     // правий промінь
            points[3] = new Point(X + B, Y + B);
            points[4] = new Point(X,     Y + A); // нижній промінь
            points[5] = new Point(X - B, Y + B);
            points[6] = new Point(X - A, Y);     // лівий промінь
            points[7] = new Point(X - B, Y - B);

            using (Pen pen = new Pen(StarColor, 2))
            {
                g.DrawPolygon(pen, points);
            }
        }
    }
}

