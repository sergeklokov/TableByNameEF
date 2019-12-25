using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableByNameEF
{
    public class Animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public Nullable<int> Age { get; set; }

        // Adapter pattern
        public Animal(object o)
        {
            // we may check for existance
            if (o.GetType().GetProperty("ID") != null)
                ID = (int)o.GetType().GetProperty("ID").GetValue(o, null);

            // let's play dangerous without existance check
            Name = (string)o.GetType().GetProperty("Name").GetValue(o, null);
            Color = (string)o.GetType().GetProperty("Color").GetValue(o, null);
            Age = (Nullable<int>)o.GetType().GetProperty("Age").GetValue(o, null);
        }
    }
}
