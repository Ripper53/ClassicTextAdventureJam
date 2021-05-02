using System.Collections.Generic;

namespace EsotericFiction {
    public abstract class Scene : ItemHolder, ITitle, IDescription {
        public abstract string Title { get; }
        public abstract string Description { get; }

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
