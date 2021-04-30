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

        public static void Write(string text) {
            Console.Write(text);
        }

        public static void WriteLine(string text) {
            Console.WriteLine(text + Environment.NewLine);
        }

        public static string ReadLine() {
            string str = Console.ReadLine();
            Console.WriteLine();
            return str;
        }

    }
}
