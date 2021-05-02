using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsotericFiction {
    public class Entity : IEntity {
        public Inventory Inventory { get; }

        public Entity() {
            Inventory = new Inventory();
        }

    }
}
