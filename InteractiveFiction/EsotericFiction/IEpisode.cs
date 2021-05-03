using System.Collections.Generic;

namespace EsotericFiction {
    public interface IEpisode {
        void Play(Token token);
        public class Token {
            public GameManager GameManager { get; }
            public Scene ActiveScene { get; }
            public Token(GameManager gameManager, Scene activeScene, ICollection<IEpisode> toRemove) {
                GameManager = gameManager;
                ActiveScene = activeScene;
                this.toRemove = toRemove;
            }
            public bool IsCanceled { get; private set; } = false;
            public bool Cancel() => IsCanceled = true;
            private readonly ICollection<IEpisode> toRemove;
            public IEnumerable<IEpisode> ToRemove => toRemove;
            public void RemoveEpisode(IEpisode episode) {
                toRemove.Add(episode);
            }
        }
    }
}
