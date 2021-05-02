using System.Collections.Generic;

namespace EsotericFiction {
    public abstract class ExamineWork : IWork {
        public IEnumerable<string> Action {
            get {
                yield return "EXAMINE";
                yield return "E";
            }
        }
        public abstract IEnumerable<string> Upon { get; }

        public abstract void Execute(IWork.Token token);

    }
}
