using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsotericFiction {
    public class GameManager {
        public IEntity PlayerEntity { get; }
        public string ErrorMessage { get; set; }
        public Scene ActiveScene { get; private set; }
        public void SetActiveScene(Scene scene) {
            ActiveScene = scene;
            IEpisode.Token token = new IEpisode.Token(this, ActiveScene);
            foreach (IEpisode episode in ActiveScene.Episodes) {
                episode.Play(token);
                if (token.IsCanceled)
                    return;
            }
        }

        public GameManager(IEntity playerEntity) {
            PlayerEntity = playerEntity;
        }

        public void Execute() {
            string input;
            Scene previousScene;
            Act.Display(ActiveScene);
            do {
                previousScene = ActiveScene;
                input = Act.ReadLine().ToUpper();
                switch (input) {
                    case "HELP":
                    case "H":
                        Act.WriteTitle("Help Menu");
                        Act.WriteLine(@"Commands:
INVENTORY/I
GO
EXAMINE/E");
                        break;
                    case "INVENTORY":
                    case "I":
                        Act.Display(PlayerEntity.Inventory);
                        break;
                    default:
                        if (!PlayerEntity.Inventory.Work(this, input) && !ActiveScene.Work(this, input))
                            DisplayError();
                        if (ActiveScene != previousScene)
                            Act.Display(ActiveScene);
                        break;
                }
            } while (input != "QUIT");
        }

        private void Display() {
            Act.BackgroundColor = ConsoleColor.White;
            Act.ForegroundColor = ConsoleColor.Black;
            Act.Write(ActiveScene.Title);

            Act.BackgroundColor = ConsoleColor.Black;
            Act.ForegroundColor = ConsoleColor.White;
            Act.WriteLine(Environment.NewLine + Environment.NewLine + ActiveScene.Description);
        }

        private void DisplayError() {
            Act.WriteLine(ErrorMessage);
        }

    }
}
