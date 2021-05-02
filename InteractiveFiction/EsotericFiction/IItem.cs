using System.Collections.Generic;

namespace EsotericFiction {
    public interface IItem : IDescription {
        string Name { get; }
        IEnumerable<IWork> Work { get; }
    }
}
