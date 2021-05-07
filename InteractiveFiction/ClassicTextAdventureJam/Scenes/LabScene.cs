using EsotericFiction;
using ClassicTextAdventureJam.Items;

namespace ClassicTextAdventureJam.Scenes {
    public class LabScene : Scene {
        public override string Title { get; }
        public string CapsulesDescription { get; set; }
        private readonly string staticDescription;
        public override string Description {
            get => $"{CapsulesDescription} {staticDescription}";
        }
        public RopeItem RopeItem { get; }

        public LabScene(string title, string capsulesDescription, string staticDescription, RopeItem ropeItem) {
            Title = title;
            CapsulesDescription = capsulesDescription;
            this.staticDescription = staticDescription;
            RopeItem = ropeItem;
        }

    }
}
