using System.Collections.Generic;
using EsotericFiction;

namespace ClassicTextAdventureJam.Items {
    public class CapsuleItem : IItem {
        public string Name => "Capsule";
        public string Description { get; set; }
        private readonly ExamineWork examineWork;
        public IEnumerable<IWork> Work {
            get {
                yield return examineWork;
            }
        }
        #region Classes
        private class ExamineWork : EsotericFiction.ExamineWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return "CAPSULE";
                    yield return "CAPSULES";
                    yield return "GLASS";
                }
            }
            private readonly CapsuleItem capsuleItem;
            public ExamineWork(CapsuleItem capsuleItem) {
                this.capsuleItem = capsuleItem;
            }

            public override void Execute(IWork.Token token) {
                Act.Display(capsuleItem);
            }
        }
        #endregion

        public CapsuleItem() {
            examineWork = new ExamineWork(this);
        }

    }
}
