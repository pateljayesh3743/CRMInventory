using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMInventory.ViewModel
{
    public class SalesInvoiceCollectionViewModel
    {
        public SalesInvoiceCollectionViewModel()
        {
            sr = null;
            name = null;
            qty = null;
            grossnet = null;
            less = null;
            netweight = null;
            rate = null;
            per = null;
            amount = null;
        }
        public int? sr { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
        public string grossnet { get; set; }
        public string less { get; set; }
        public string netweight { get; set; }
        public string rate { get; set; }
        public string per { get; set; }
        public string amount { get; set; }
    }
}
