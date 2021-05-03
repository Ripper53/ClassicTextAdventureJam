using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassicTextAdventureJam {
    public static class AsyncConsole {

        public static IEnumerator<Task<string>> AsyncConsoleInput() {
            static IEnumerator<Task<string>> Loop() {
                while (true) yield return Task.Run(() => Console.ReadLine());
            }
            var e = Loop();
            e.MoveNext();
            return e;
        }

        public static Task<string> ReadLine(this IEnumerator<Task<string>> console) {
            if (console.Current.IsCompleted) console.MoveNext();
            return console.Current;
        }

    }
}
