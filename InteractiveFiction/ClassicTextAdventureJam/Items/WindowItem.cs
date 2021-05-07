using System.Collections.Generic;
using EsotericFiction;

namespace ClassicTextAdventureJam.Items {
    public class WindowItem : IItem {
        public string Name => "Window";
        public string Description { get; set; }
        private readonly ExamineWork examineWork;
        public IEnumerable<IWork> Work {
            get {
                yield return examineWork;
            }
        }
        #region Class
        private class ExamineWork : EsotericFiction.ExamineWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return "WINDOW";
                    yield return "WINDOWS";
                    yield return "SPACE";
                }
            }
            private readonly WindowItem windowItem;
            public ExamineWork(WindowItem windowItem) {
                this.windowItem = windowItem;
            }

            public override void Execute(IWork.Token token) {
                Act.Display(windowItem);
            }
        }
        #endregion

        public WindowItem() {
            examineWork = new ExamineWork(this);
        }

    }
}
