using System.Collections.Generic;

namespace EsotericFiction {
    public interface IWork {
        /// <summary>
        /// The action key, ex. USE, EQUIP, EAT, ect...
        /// </summary>
        IEnumerable<string> Action { get; }
        /// <summary>
        /// The upon key (something to work on), ex. APPLE, ROPE, SWORD, ect...
        /// </summary>
        IEnumerable<string> Upon { get; }
        void Execute(Token token);
        public class Token {
            public GameManager GameManager { get; }
            public ItemHolder ItemHolder { get; }
            public Token(GameManager gameManager, ItemHolder itemHolder) {
                GameManager = gameManager;
                ItemHolder = itemHolder;
            }
        }
    }
}
