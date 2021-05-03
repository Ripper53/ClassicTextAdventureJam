using EsotericFiction;

namespace ClassicTextAdventureJam.Scenes {
    public class ConcourseScene : Scene {
        public override string Title => "Concourse";
        private string description;
        public override string Description => description;
        public void SetDescription(string description) => this.description = description;
    }
}
