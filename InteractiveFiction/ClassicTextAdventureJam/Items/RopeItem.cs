using System.Collections.Generic;
using EsotericFiction;

namespace ClassicTextAdventureJam.Items {
    public class RopeItem : IItem {
        public string Name => "Rope";
        public string Description => "A ten-meter long rope.";
        public bool CanUse { get; set; } = false;
        private readonly UseWork useWork;
        private readonly CollectWork collectWork;
        private readonly ExamineWork examineWork;
        public IEnumerable<IWork> Work {
            get {
                yield return useWork;
                yield return collectWork;
                yield return examineWork;
            }
        }
        #region Classes
        private class UseWork : EsotericFiction.UseWork {
            private readonly RopeItem ropeItem;
            private readonly Scene setScene;
            private readonly Scene scene;
            public UseWork(RopeItem ropeItem, Scene scene, Scene setScene) {
                this.ropeItem = ropeItem;
                this.scene = scene;
                this.setScene = setScene;
            }

            public override IEnumerable<string> Upon {
                get {
                    yield return "ROPE";
                }
            }

            public override void Execute(IWork.Token token) {
                if (ropeItem.CanUse && token.GameManager.ActiveScene == scene) {
                    Act.WriteLine("You throw the rope onto the glowing figure, and it pulls it in like a blackhole along with you.");
                    token.GameManager.SetActiveScene(setScene);
                } else {
                    Act.WriteLine("Nowhere to use the rope.");
                }
            }
        }
        private class CollectWork : EsotericFiction.CollectWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return "ROPE";
                }
            }
            public DynamicScene Scene { get; }
            public CollectWork(DynamicScene storageScene, IItem item) : base(item) {
                Scene = storageScene;
            }
            public override void Execute(IWork.Token token) {
                base.Execute(token);
                Scene.SetDescription("Empty room with a hook on its wall. The Lab is to the east.");
            }
        }
        private class ExamineWork : EsotericFiction.ExamineWork {
            public override IEnumerable<string> Upon {
                get {
                    yield return "ROPE";
                }
            }
            private readonly RopeItem ropeItem;
            public ExamineWork(RopeItem ropeItem) {
                this.ropeItem = ropeItem;
            }

            public override void Execute(IWork.Token token) {
                Act.Display(ropeItem);
            }
        }
        #endregion
        public RopeItem(DynamicScene storageScene, Scene scene, Scene setScene) {
            useWork = new UseWork(this, scene, setScene);
            collectWork = new CollectWork(storageScene, this);
            examineWork = new ExamineWork(this);
        }

    }
}
