using System.Collections.Generic;

namespace EsotericFiction {
    public class ItemHolder {
        #region Items
        private readonly HashSet<IItem> items = new HashSet<IItem>();
        public IEnumerable<IItem> Items => items;

        public bool AddItem(IItem item) {
            if (items.Add(item)) {
                foreach (IWork work in item.Work)
                    AddWork(work);
                return true;
            }
            return false;
        }
        public bool RemoveItem(IItem item) {
            if (items.Remove(item)) {
                foreach (IWork work in item.Work)
                    RemoveWork(work);
                return true;
            }
            return false;
        }
        #endregion
        #region Work
        private readonly Dictionary<string, Dictionary<string, IWork>> work = new Dictionary<string, Dictionary<string, IWork>>();

        public void AddWork(IWork work) {
            foreach (string action in work.Action) {
                if (this.work.ContainsKey(action)) {
                    foreach (string upon in work.Upon)
                        this.work[action].Add(upon, work);
                } else {
                    Dictionary<string, IWork> dict = new Dictionary<string, IWork>();
                    this.work.Add(action, dict);
                    foreach (string upon in work.Upon)
                        dict.Add(upon, work);
                }
            }
        }
        public bool RemoveWork(IWork work) {
            foreach (string action in work.Action) {
                if (this.work.ContainsKey(action)) {
                    if (this.work[action].Count > 1) {
                        foreach (string upon in work.Upon) {
                            this.work[action].Remove(upon);
                        }
                        if (this.work[action].Count == 0)
                            this.work.Remove(action);
                    } else {
                        this.work.Remove(action);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool Work(GameManager gameManager, string text) {
            if (ParseWorkText(text, out string action, out string upon)) {
                work[action][upon].Execute(new IWork.Token(gameManager, this));
                return true;
            }
            return false;
        }
        private bool ParseWorkText(string text, out string action, out string upon) {
            action = null;
            upon = null;
            string[] actions = text.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            if (actions.Length < 2)
                return false;
            foreach (string k in actions) {
                if (action == null) {
                    if (work.ContainsKey(k))
                        action = k;
                } else if (upon == null) {
                    if (work[action].ContainsKey(k)) {
                        upon = k;
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
