namespace EsotericFiction {
    public class DynamicScene : Scene {
        private string title;
        public override string Title => title;
        public void SetTitle(string title) => this.title = title;
        private string description;
        public override string Description => description;
        public void SetDescription(string description) => this.description = description;

        public DynamicScene(string title, string description) {
            this.title = title;
            this.description = description;
        }

    }
}
