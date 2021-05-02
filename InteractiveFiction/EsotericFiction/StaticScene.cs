namespace EsotericFiction {
    public class StaticScene : Scene {
        public override string Title { get; }
        public override string Description { get; }

        public StaticScene(string title, string description) {
            Title = title;
            Description = description;
        }

    }
}
