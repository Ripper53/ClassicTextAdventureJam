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
        }

        public GameManager(IEntity playerEntity) {
            PlayerEntity = playerEntity;
        }

        private readonly HashSet<IEpisode> toRemoveEpisodes = new HashSet<IEpisode>();
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
                        if (ActiveScene != previousScene) {
                            Act.Display(ActiveScene);
                            IEpisode.Token token = new IEpisode.Token(this, ActiveScene, toRemoveEpisodes);
                            foreach (IEpisode episode in ActiveScene.Episodes) {
                                episode.Play(token);
                                if (token.IsCanceled)
                                    return;
                            }
                            foreach (IEpisode episode in toRemoveEpisodes)
                                ActiveScene.RemoveEpisode(episode);
                            toRemoveEpisodes.Clear();
                        }
                        break;
                }
            } while (input != "QUIT");
        }

        private void DisplayError() {
            Act.WriteLine(ErrorMessage);
        }

    }
}
