using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsotericFiction {
    public class GameManager {
        public Scene ActiveScene { get; private set; }
        public void SetActiveScene(Scene scene) {
            ActiveScene = scene;
            Display();
            IEpisode.Token token = new IEpisode.Token(this, ActiveScene);
            foreach (IEpisode episode in ActiveScene.Episodes) {
                episode.Play(token);
                if (token.IsCanceled)
                    return;
            }
        }

        public void Execute() {
            string input;
            do {
                input = Act.ReadLine().ToUpper();
            } while (input != "QUIT");
        }

        private void Display() {
            Act.BackgroundColor = ConsoleColor.White;
            Act.ForegroundColor = ConsoleColor.Black;
            Act.WriteLine(ActiveScene.Title);

            Act.BackgroundColor = ConsoleColor.Black;
            Act.ForegroundColor = ConsoleColor.White;
            Act.WriteLine(ActiveScene.Description);
        }

    }
}
