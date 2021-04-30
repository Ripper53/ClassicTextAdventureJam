namespace EsotericFiction {
    public interface IEpisode {
        void Play(Token token);
        public class Token {
            public GameManager GameManager { get; }
            public Scene ActiveScene { get; }
            public Token(GameManager gameManager, Scene activeScene) {
                GameManager = gameManager;
                ActiveScene = activeScene;
            }
            public bool IsCanceled { get; private set; } = false;
            public bool Cancel() => IsCanceled = true;
        }
    }
}
