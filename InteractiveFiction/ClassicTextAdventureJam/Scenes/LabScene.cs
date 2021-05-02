using EsotericFiction;

namespace ClassicTextAdventureJam.Scenes {
    public class LabScene : Scene {
        public override string Title { get; }
        public string CapsulesDescription { get; set; }
        private readonly string staticDescription;
        public override string Description {
            get => $"{CapsulesDescription} {staticDescription}";
        }

        public LabScene(string title, string capsulesDescription, string staticDescription) {
            Title = title;
            CapsulesDescription = capsulesDescription;
            this.staticDescription = staticDescription;
        }

    }
}
