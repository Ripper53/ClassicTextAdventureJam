using System;

namespace EsotericFiction {
    public static class Act {
        public static ConsoleColor BackgroundColor {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }
        public static ConsoleColor ForegroundColor {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }
        public static ConsoleColor TitleBackgroundColor { get; set; } = ConsoleColor.White;
        public static ConsoleColor TitleForegroundColor { get; set; } = ConsoleColor.Black;

        public static void Write(string text) {
            Console.Write(text);
        }

        public static void WriteLine(string text) {
            Console.WriteLine(text + Environment.NewLine);
        }

        public static void WriteTitle(string text) {
            BackgroundColor = TitleBackgroundColor;
            ForegroundColor = TitleForegroundColor;
            Write(text);
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static string ReadLine() {
            string str = Console.ReadLine();
            Console.WriteLine();
            return str;
        }

        public static void Display(IDescription description) {
            WriteLine(description.Description);
        }
        public static void Display<T>(T item) where T : ITitle, IDescription {
            WriteTitle(item.Title);
            WriteLine(item.Description);
        }

    }
}
