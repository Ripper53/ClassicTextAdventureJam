using System.Collections.Generic;

namespace EsotericFiction {
    public class Map : IItem {
        public string Name { get; set; }
        public string Description { get; set; }
        public Grid Grid { get; }
        private readonly MapUseWork mapUseWork;
        public IEnumerable<IWork> Work {
            get {
                yield return mapUseWork;
            }
        }

        public Map(Grid grid) {
            Grid = grid;
            mapUseWork = new MapUseWork(this);
        }

    }
}
