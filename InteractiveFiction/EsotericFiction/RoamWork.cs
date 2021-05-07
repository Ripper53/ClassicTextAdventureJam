using System.Collections.Generic;

namespace EsotericFiction {
    /// <summary>
    /// Change active scene.
    /// </summary>
    public class RoamWork : IWork {
        public IEnumerable<string> Action {
            get {
                yield return "GO";
                yield return "TO";
                yield return "TRAVEL";
                yield return "TRAVERSE";
                yield return "WALK";
                yield return "RUN";
                yield return "JOG";
                yield return "ROAM";
            }
        }
        public IEnumerable<string> Upon { get; }
        public Scene Scene { get; }

        public RoamWork(IEnumerable<string> upon, Scene scene) {
            Upon = upon;
            Scene = scene;
        }

        public void Execute(IWork.Token token) {
            token.GameManager.SetActiveScene(Scene);
        }

        public const string North = "NORTH", East = "EAST", South = "SOUTH", West = "WEST";

    }
}
