using System.Collections.Generic;

namespace EsotericFiction {
    public class Scene {
        public string Title { get; }
        public string Description { get; }

        public Scene(string title, string description) {
            Title = title;
            Description = description;
        }

        #region Items
        private readonly HashSet<IItem> items = new HashSet<IItem>();
        public IEnumerable<IItem> Items => items;

        public bool AddItem(IItem item) {
            return items.Add(item);
        }
        public bool RemoveItem(IItem item) {
            return items.Remove(item);
        }
        #endregion

        #region Episodes
        private readonly List<IEpisode> episodes = new List<IEpisode>();
        public IEnumerable<IEpisode> Episodes => episodes;

        public void AddEpisode(IEpisode episode) {
            episodes.Add(episode);
        }
        public bool RemoveEpisode(IEpisode episode) {
            return episodes.Remove(episode);
        }
        #endregion

    }
}
