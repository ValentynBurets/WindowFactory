using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowFactory.Model
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Paid { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateComplite { get; set; }
        public DateTime ApproximateDateComplite { get; set; }
        public bool IsSelected { get; set; }
    }
}
