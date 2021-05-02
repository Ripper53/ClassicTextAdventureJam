using System.Collections.Generic;

namespace EsotericFiction {
    public abstract class UseWork : IWork {
        public IEnumerable<string> Action {
            get {
                yield return "USE";
            }
        }
        public abstract IEnumerable<string> Upon { get; }

        public abstract void Execute(IWork.Token token);

    }
}
