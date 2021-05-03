using EsotericFiction;
using System.Collections.Generic;
using ClassicTextAdventureJam.Scenes;

namespace ClassicTextAdventureJam.Items {
    public class LecternItem : IItem {
        public string Name => "Lectern";
        public string Description => "A lectern with buttons stands at the center.";
        private readonly UseWork useWork;
        private readonly ExamineWork examineWork;
        public IEnumerable<IWork> Work {
            get {
                yield return useWork;
                yield return examineWork;
            }
        }
        #region Class
        private class UseWork : EsotericFiction.UseWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return "LECTERN";
                    yield return "BUTTON";
                    yield return "BUTTONS";
                }
            }
            public override IEnumerable<string> Action {
                get {
                    foreach (string action in base.Action)
                        yield return action;
                    yield return "PRESS";
                }
            }
            private readonly LabScene labScene;
            private readonly ConcourseScene concourseScene;
            private readonly CapsuleItem capsuleItem;
            private readonly WindowItem windowItem;
            public UseWork(LabScene labScene, ConcourseScene concourseScene, CapsuleItem capsuleItem, WindowItem windowItem) {
                this.labScene = labScene;
                this.concourseScene = concourseScene;
                this.capsuleItem = capsuleItem;
                this.windowItem = windowItem;
            }

            private readonly string[] text = new string[] {
                "The capsules fill with a pure white liquid.",
                "The buttons do not seem to do anything."
            };
            private int index = 0;
            public override void Execute(IWork.Token token) {
                Act.WriteLine(text[index]);
                if (index < text.Length - 1) {
                    labScene.CapsulesDescription = "A pure white liquid floats in the four capsules.";
                    capsuleItem.Description = "A pure white liquid floats in the four capsules.";
                    concourseScene.SetDescription("The tables and walls are illuminated by a figure floating at the center of the room. The massive window glows with pure white, an identical energy the floating figure emits throughout the Concourse.");
                    windowItem.Description = "The stars from here are invisible to the naked eye because of the sharp light the figure emits within the Concourse.";
                    index += 1;
                }
            }
        }
        private class ExamineWork : EsotericFiction.ExamineWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return "LECTERN";
                    yield return "BUTTON";
                    yield return "BUTTONS";
                }
            }
            private readonly LecternItem lecternItem;
            public ExamineWork(LecternItem lecternItem) {
                this.lecternItem = lecternItem;
            }

            public override void Execute(IWork.Token token) {
                Act.Display(lecternItem);
            }
        }
        #endregion

        public LecternItem(LabScene labScene, ConcourseScene concourseScene, CapsuleItem capsuleItem, WindowItem windowItem) {
            useWork = new UseWork(labScene, concourseScene, capsuleItem, windowItem);
            examineWork = new ExamineWork(this);
        }

    }
}
