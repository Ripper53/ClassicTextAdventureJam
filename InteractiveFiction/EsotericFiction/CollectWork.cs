using System.Collections.Generic;

namespace EsotericFiction {
    public abstract class CollectWork : IWork {
        public IItem Item { get; }
        public IEnumerable<string> Action {
            get {
                yield return "COLLECT";
                yield return "PICK UP";
                yield return "PICKUP";
                yield return "PICK-UP";
                yield return "GRAB";
                yield return "STORE";
                yield return "CARRY";
            }
        }
        public abstract IEnumerable<string> Upon { get; }

        public CollectWork(IItem item) {
            Item = item;
        }

        public virtual void Execute(IWork.Token token) {
            if (token.GameManager.PlayerEntity.Inventory.ContainsItem(Item)) {
                Act.WriteLine($"{Item.Name} is already in your inventory.");
                return;
            }
            token.GameManager.ActiveScene.RemoveItem(Item);
            token.GameManager.PlayerEntity.Inventory.AddItem(Item);
            Act.WriteLine($"Added {Item.Name.ToLower()} to inventory.");
            token.GameManager.PlayerEntity.Inventory.GenerateDisplay();
        }

    }
}
