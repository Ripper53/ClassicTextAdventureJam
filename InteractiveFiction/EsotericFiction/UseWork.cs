using System.Collections.Generic;

namespace EsotericFiction {
    public abstract class UseWork : IWork {
        public virtual IEnumerable<string> Action {
            get {
                yield return "USE";
                yield return "U";
            }
        }
        public abstract IEnumerable<string> Upon { get; }

        public abstract void Execute(IWork.Token token);

    }
}
