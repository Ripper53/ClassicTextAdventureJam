using System.Collections.Generic;

namespace EsotericFiction {
    public class DisplayExamineWork : ExamineWork {
        public IItem Item { get; }
        public override IEnumerable<string> Upon { get; }

        public DisplayExamineWork(IItem item, params string[] upon) {
            Item = item;
            Upon = upon;
        }

        public override void Execute(IWork.Token token) {
            Act.Display(Item);
        }

    }
}
