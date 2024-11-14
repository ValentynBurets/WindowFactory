using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowFactory.Model
{
    public class MaterialModel
    {
        public int Id { get; set; }
        public string MaterialType { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Country { get; set; }
        public string QuantityType { get; set; }

        public bool IsSelected { get; set; }
    }
}
