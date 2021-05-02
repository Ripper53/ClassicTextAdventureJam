using System.Text;

namespace EsotericFiction {
    public class Inventory : ItemHolder, ITitle, IDescription {
        public string Title => "Inventory";
        public string Description { get; private set; }

        public void GenerateDisplay() {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IItem item in Items) {
                stringBuilder.AppendLine(item.Name);
            }
            Description = stringBuilder.ToString();
        }

    }
}
