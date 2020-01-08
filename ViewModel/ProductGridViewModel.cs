using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMInventory.ViewModel
{
    public class ProductGridViewModel
    {
        public int id { get; set; }
        public string product { get; set; }
        public decimal? mrp { get; set; }
        public string unit { get; set; }
    }
}
