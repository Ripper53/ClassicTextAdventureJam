using System.Collections.Generic;

namespace EsotericFiction {
    public class MapExamineWork : ExamineWork {
        public Map Map { get; }
        public override IEnumerable<string> Upon {
            get {
                yield return Map.Name.ToUpper();
            }
        }

        public MapExamineWork(Map map) {
            Map = map;
        }

        public override void Execute(IWork.Token token) {
            Act.Display(Map);
        }

    }
}
