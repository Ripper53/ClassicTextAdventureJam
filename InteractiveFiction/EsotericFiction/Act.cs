using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        #region ASYNC
        public class AsyncConsole {
            private readonly IEnumerator<Task<string>> console;
            public bool IsReading { get; private set; } = false;

            public AsyncConsole() {
                IEnumerator<Task<string>> Loop() {
                    while (true) yield return Task.Run(() => {
                        IsReading = true;
                        string r = Act.ReadLine();
                        IsReading = false;
                        return r;
                    });
                }
                console = Loop();
            }

            public Task<string> ReadLine() {
                if (console.Current == null || console.Current.IsCompleted) console.MoveNext();
                return console.Current;
            }

        }
        #endregion

    }
}
