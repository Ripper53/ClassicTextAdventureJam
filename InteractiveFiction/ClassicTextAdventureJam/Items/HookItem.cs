using System.Collections.Generic;
using EsotericFiction;

namespace ClassicTextAdventureJam.Items {
    public class HookItem : IItem {
        public string Name => "Hook";
        public string Description => "The hook bends around softly to carry any item upon it.";
        private readonly DisplayExamineWork displayExamineWork;
        public IEnumerable<IWork> Work {
            get {
                yield return displayExamineWork;
            }
        }

        public HookItem() {
            displayExamineWork = new DisplayExamineWork(this, "HOOK");
        }

    }
}
