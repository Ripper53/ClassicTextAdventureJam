using System;
using System.Collections.Generic;
using EsotericFiction;

namespace ClassicTextAdventureJam.Items {
    public class RadioItem : IItem {
        public string Name => "Radio";
        public string Description => "A circular microphone attaches to the radio with a black wire. A red ring goes around the perimeter on its back, where the palm fits onto comfortably. The speakers behind a crosshatch of steel to protect its internal components.";
        private readonly UseWork useWork;
        private readonly ExamineWork examineWork;
        public IEnumerable<IWork> Work {
            get {
                yield return useWork;
                yield return examineWork;
            }
        }

        public RadioItem() {
            useWork = new UseWork();
            examineWork = new ExamineWork(this);
        }

        #region Class
        private const string upon = "RADIO";
        private class UseWork : EsotericFiction.UseWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return upon;
                }
            }

            public override void Execute(IWork.Token token) {
                Act.WriteLine(@"""Hello,"" you say. ""Is anyone there?"" No response. ""Hello,"" you say again. ""If anyone can hear me, broadcast your signal."" The radio sits in silence.");
            }
        }
        private class ExamineWork : EsotericFiction.ExamineWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return upon;
                }
            }

            public readonly RadioItem radioItem;
            public ExamineWork(RadioItem radioItem) {
                this.radioItem = radioItem;
            }

            public override void Execute(IWork.Token token) {
                Act.Display(radioItem);
            }
        }
        #endregion

    }
}
